using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAcademyBot
{
    public class Bot
    {
        const long TEST_SERVER_ID = 955584592163799040;
        public DiscordClient Client { get; private set; }
        public CommandsNextExtension Commands { get; private set; }
        public InteractivityExtension Interactivity { get; private set; }

        public static ConfigJson Config { get; private set; }
        public async Task RunAsync()
        {

            Config = await GetConfig();
            DiscordConfiguration config = GenerateConfig();
            Client = new DiscordClient(config);
            Client.Ready += OnClientReady;
            CommandsNextConfiguration commandsConfig = GenerateCommandsConfig();
            RegisterCommands(commandsConfig);

            await Client.ConnectAsync();

            Client.MessageReactionAdded += OnReactionAdded;
            Client.MessageReactionRemoved += OnReactionRemoved;

            // Keep the bot online when switched on
            await Task.Delay(-1);
        }


        private static DiscordConfiguration GenerateConfig()
        {
            return new DiscordConfiguration()
            {
                Token = Config.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                MinimumLogLevel = LogLevel.Debug,
                Intents = DiscordIntents.GuildMessages
                        | DiscordIntents.GuildMembers
                        | DiscordIntents.GuildMessageReactions
                        | DiscordIntents.Guilds
            };
        }

        private static CommandsNextConfiguration GenerateCommandsConfig()
        {
            return new CommandsNextConfiguration
            {
                StringPrefixes = new string[] { Config.Prefix },
                EnableDms = false,
                EnableMentionPrefix = true
            };
        }

        private void RegisterCommands(CommandsNextConfiguration commandsConfig)
        {
            Commands = Client.UseCommandsNext(commandsConfig);
            Commands.RegisterCommands<CoreCommands>();
        }

        private Task OnClientReady(DiscordClient client, ReadyEventArgs e)
        {
            return Task.CompletedTask;
        }

        private async Task<ConfigJson> GetConfig()
        {
            var json = string.Empty;

            using (var fs = File.OpenRead("config.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<ConfigJson>(json);
        }

        private async Task AssignRole(DiscordMember member, DiscordRole discordRole)
        {
            try
            {
                await member.GrantRoleAsync(discordRole);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private async Task CheckForUserRankUp(long userId)
        {
            using (DAL dal = new DAL())
            {
                try
                {
                    List<ReputationRole> roles = dal.GetRoles();
                    List<ReputationRole> sortedRoles = roles.OrderBy(r => r.requirement).ToList();
                    int userRep = dal.GetUserReputation(userId);
                    ReputationRole highestAchievedRole = null;
                    foreach (ReputationRole role in sortedRoles)
                    {
                        if (userRep >= role.requirement)
                        {
                            highestAchievedRole = role;
                        }
                    }
                    if (highestAchievedRole != null)
                    {
                        var guild = await Client.GetGuildAsync(TEST_SERVER_ID);
                        var discordRole = guild.GetRole((ulong)highestAchievedRole.discordId);
                        var member = await guild.GetMemberAsync((ulong)userId);
                        await AssignRole(member, discordRole);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
        }

        private async Task OnReactionRemoved(DiscordClient sender, MessageReactionRemoveEventArgs e)
        {
            if (e.Emoji != DiscordEmoji.FromUnicode(Client, "\u2705")) { return; }
            var author = e.Message.Author;
            using (DAL dal = new DAL())
            {
                try
                {
                    if (!dal.UserExists((long)author.Id))
                    {
                        dal.AddUser(author.Username, (long)author.Id);
                        dal.AddUserRepRecord((long)author.Id);
                    }
                    dal.AddRepForUser((long)author.Id, -1);
                    await CheckForUserRankUp((long)author.Id);
                }
                catch (Exception ex)
                {
                    await e.Channel.SendMessageAsync(ex.Message);
                }
            }
            await e.Channel.SendMessageAsync($"Someone unapproved of {author.Mention}'s answer.");
            return;
        }

        private async Task OnReactionAdded(DiscordClient sender, MessageReactionAddEventArgs e)
        {
            if (e.Emoji != DiscordEmoji.FromUnicode(Client, "\u2705")) { return; }
            var author = e.Message.Author;
            using (DAL dal = new DAL())
            {
                try
                {
                    if (!dal.UserExists((long)author.Id))
                    {
                        dal.AddUser(author.Username, (long)author.Id);
                        dal.AddUserRepRecord((long)author.Id);
                    }
                    dal.AddRepForUser((long)author.Id, 1);
                    await CheckForUserRankUp((long)author.Id);
                }
                catch (Exception ex)
                {
                    await e.Channel.SendMessageAsync(ex.Message);
                }
            }
            await e.Channel.SendMessageAsync($"Someone approved of {author.Mention}'s answer.");
            return;
        }
    }
}
