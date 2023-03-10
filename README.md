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

# How to setup a MySql server using Docker for local development

Firstly, it is expected that you have Docker installed on your computer and have a Docker hub account. We will be using the official MySql image, you can read more about it [here](https://hub.docker.com/_/mysql).

Secondly, your MySqlConnection string should be filled in. Here is an example of the one that I use: `server=127.0.0.1;uid=root;pwd=mysecurepassword;database=csharpacademybot`

In Visual Studio open up the Developer PowerShell and copy in the following docker run command:

```
docker run --name mysql-csharpacademybot -e MYSQL_ROOT_PASSWORD=mysecurepassword -p 3306:3306 -d mysql:8
```

The docker run command is going to create a container for us containing the MySql server, to which we will be connecting to using the MySqlConnection string which is located in the 'secrets.json' file.

Options explained (for more options please see documentation [here](https://docs.docker.com/engine/reference/commandline/run/)):
1. --name is where we enter in the name we want for the container.
2. -e is where we enter in the environment variables such as the mysql root password. Make sure that the MYSQL_ROOT_PASSWORD is the same one as in the MySqlConnection string.
3. -p is where we enter in the port mappings. So the docker host port TCP 3306 (left side) is mapped to docker container port TCP 3306 (right side).
4. -d is for running the container in the background.
5. mysql:8 this is the image we will be using, and 8 is the version of the image.

After executing this command, you should see a new container called mysql-csharpacademybot, and it should have the status 'Running'. To confirm that the MySql server is indeed installed in the container and is running sucessfully, do the following:

1. Click on the mysql-csharpacademybot container we just created, and open the 'Terminal' tab.
2. Type in the following `mysql -u root -p` press enter, and then enter in the password we set for MYSQL_ROOT_PASSWORD and enter again.
3. Now we should see a MySQL monitor screen. If you type in the following SQL `SHOW DATABASES;`, we should see a table with 4 existing databases.

Now let's perform the database Migrations by running the following command in the Developer PowerShell within Visual Studio:

```
dotnet ef database update
```

We should see that the migrations were applied and the Done. message. To confirm this, we can use the terminal of the container to query for the databases again, and you should see a new database with the name that you picked, which is `csharpacademybot` in my case from the connection string.
