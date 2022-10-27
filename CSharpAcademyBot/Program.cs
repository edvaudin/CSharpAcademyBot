using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CSharpAcademyBot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Bot bot = new Bot();
            bot.RunAsync().GetAwaiter().GetResult();
        }
    }
}