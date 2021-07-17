using System;

namespace TemplateBot
{
    class Program
    {
        static void Main(string[] args) => TemplateBot.RunBotAsync().GetAwaiter().GetResult();
    }
}
