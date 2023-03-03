namespace CSharpAcademyBot;

internal class Program
{
    static void Main()
    {
        Bot bot = new();
        bot.RunAsync().GetAwaiter().GetResult();
    }
}