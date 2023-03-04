# CSharpAcademyBot
 An open-source Discord Bot for the C# Academy community written using DSharpPlus and .NET 7. 

## Feature goals

- Assigning belt Discord roles based off the C# Academy website.
- Creating a reputation system within the Discord, awarding those that actively help others.
- Assigning Discord roles based off their reputation.

# How to contribute

After you have forked and cloned the repository, you will need to set up your own Discord Bot for testing purposes. The DSharpPlus documentation includes a [guide](https://dsharpplus.github.io/DSharpPlus/articles/basics/bot_account.html) to setting this up via the Discord Developer Portal.

Once you have your bot setup, you will need to insert your bot token into the `secrets.json` file, which you can find by right clicking on the project and selecting "Manage User Secrets". This `secrets.json` file is stored locally on your machine outside of the solution directory and does not get involved with source control.
Please bear in mind that your `secrets.json` file will not be encrypted, but will be tied to your user account on your operating system.

```
{
  "DiscordConfiguration": {
    "Token": "YOUR BOT TOKEN HERE",
    "Prefix": "?"
  },
  "ConnectionStrings": {
    "MySqlConnection": "YOUR MYSQL CONNECTION STRING HERE"
  }
}
```

Currently there is no production solution for hosting or any central database, you will be expected to manage your own local database in order to trial new features. Currently, the bot relies on a MySQL server, however this can be expanded to support other types in due course.

A project page is currently in development where issues can be created and assigned so that people can try adding features. Once you have a feature, fix or improvement you would like to add, please create a pull request. 

For brainstorming ideas and practices, this can either be done on the C# Academy Discord server, or by creating an issue on GitHub.