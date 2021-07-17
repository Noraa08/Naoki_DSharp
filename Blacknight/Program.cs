using System;

namespace Blacknight
{
    class Program
    {
        static void Main(string[] args) => Blacknight.RunBotAsync().GetAwaiter().GetResult();
    }
}
