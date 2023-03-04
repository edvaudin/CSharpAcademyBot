using CSharpAcademyBot.Contexts;
using CSharpAcademyBot.Factories;
using CSharpAcademyBot.Repositories;
using CSharpAcademyBot.Services;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;

namespace CSharpAcademyBot;

internal class Bot
{
    public DiscordClient? Client { get; private set; }
    public CommandsNextExtension? Commands { get; private set; }

    public async Task RunAsync()
    {
        DiscordConfiguration config = GenerateDiscordConfig();

        Client = new DiscordClient(config);
        Client.Ready += OnClientReady;

        ServiceProvider services = GenerateServices();
        CommandsNextConfiguration commandsConfig = GenerateCommandsConfig(services);

        RegisterCommands(commandsConfig);
        await Client.ConnectAsync();

        Client.MessageCreated += async (s, e) =>
        {
            if (e.Message.Content.ToLower() == "ping")
            {
                await e.Message.RespondAsync("pong");
            }
        };

        // Keep the bot online when switched on.
        await Task.Delay(-1);
    }

    private static DiscordConfiguration GenerateDiscordConfig()
    {
        return new DiscordConfiguration()
        {
            Token = GetConfiguration()["DiscordConfiguration:Token"],
            TokenType = TokenType.Bot,
            AutoReconnect = true,
            MinimumLogLevel = LogLevel.Debug,
            Intents = DiscordIntents.AllUnprivileged
                    | DiscordIntents.MessageContents
        };
    }

    private static IConfigurationRoot GetConfiguration()
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddUserSecrets<Bot>()
                .Build();
        return configuration;
    }

    private Task OnClientReady(DiscordClient client, ReadyEventArgs e)
    {
        return Task.CompletedTask;
    }

    private static ServiceProvider GenerateServices()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddDbContextFactory<AcademyContext>();
        serviceCollection.AddScoped<IAcademyService, AcademyService>();
        serviceCollection.AddScoped<IAcademyRepository, AcademyRepository>();
        var services = serviceCollection.BuildServiceProvider();
        return services;
    }

    private static CommandsNextConfiguration GenerateCommandsConfig(ServiceProvider services)
    {
        return new CommandsNextConfiguration
        {
            StringPrefixes = new string[] { GetConfiguration()["DiscordConfiguration:Prefix"] },
            EnableDms = false,
            EnableMentionPrefix = true,
            Services = services
        };
    }

    private void RegisterCommands(CommandsNextConfiguration commandsConfig)
    {
        Commands = Client?.UseCommandsNext(commandsConfig);
    }
}
