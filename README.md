# CSharpAcademyBot
 An open-source Discord Bot for the C# Academy community written using DSharpPlus and .NET 7. 

## Feature goals

- Assigning belt Discord roles based off the C# Academy website.
- Creating a reputation system within the Discord, awarding those that actively help others.
- Assigning Discord roles based off their reputation.

# How to contribute

After you have forked and cloned the repository, you will need to set up your own Discord Bot for testing purposes. The DSharpPlus documentation includes a [guide](https://dsharpplus.github.io/DSharpPlus/articles/basics/bot_account.html) to setting this up via the Discord Developer Portal.

Once you have your bot setup, you will need to insert your bot token into the `config.json` file:

```
{
  "token": "INSERT BOT TOKEN",
  "prefix": "?",
  "database": "INSERT LOCAL DB"
}
```

As of the 3rd March 2023, this project is brand new and does not currently have a database structure setup as I would like to discuss an inclusive system that would be most accessible to use. The goal will be to have a predominatly database agnostic repository, however there is more to discuss.

I will be setting up a project page where issues can be created and assigned so that people can try adding features. Once you have a feature, fix or improvement you would like to add, please create a pull request. 

For brainstorming ideas and practices, this can either be done on the C# Academy Discord server, or by creating an issue on GitHub.