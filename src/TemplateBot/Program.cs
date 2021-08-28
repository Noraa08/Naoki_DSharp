using System.Threading.Tasks;

namespace TemplateBot
{
    internal static class Program
    {
        private static async Task<int> Main()
        {
            return await TemplateBot.RunBotAsync();
        }
    }
}
