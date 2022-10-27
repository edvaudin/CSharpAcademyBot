using DSharpPlus;
using DSharpPlus.CommandsNext;
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

        private async Task OnReactionRemoved(DiscordClient sender, MessageReactionRemoveEventArgs e)
        {
            var author = e.Message.Author;
            await e.Channel.SendMessageAsync($"Someone revoked their approval of {author.Mention}'s answer.");
            return;
        }

        private async Task OnReactionAdded(DiscordClient sender, MessageReactionAddEventArgs e)
        {
            var author = e.Message.Author;
            await e.Channel.SendMessageAsync($"Someone approved of {author.Mention}'s answer.");
            return;
        }

        private static DiscordConfiguration GenerateConfig()
        {
            return new DiscordConfiguration()
            {
                Token = Config.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                MinimumLogLevel = LogLevel.Debug,
                Intents = DiscordIntents.AllUnprivileged
                | DiscordIntents.GuildMessages
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
    }
}
