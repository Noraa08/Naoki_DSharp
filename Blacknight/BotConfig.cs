using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using Serilog;
using System.ComponentModel;

namespace Blacknight
{
    class BotConfig
    {
        public static async Task<BotConfig> LoadConfigAsync(string fp)
        {
            try
            {
                return JsonConvert.DeserializeObject<BotConfig>(await File.ReadAllTextAsync(fp));
            }
            catch (FileNotFoundException exception)
            {
                Log.Fatal(exception, $"Unable to locate file at path '{fp}', terminating client.");
                return null;
            }
            catch (Exception exception)
            {
                Log.Fatal(exception, "Unable to deserialize configuration file, terminating client.");
                return null;
            }
        }

        [JsonProperty("token")]
        [Description("The token that is used to log in the bot.")]
        public string Token { get; set; }

        [JsonProperty("prefix")]
        [Description("The prefix that the bot commands will use.")]
        public string Prefix { get; set; } = ">";
    }
}
