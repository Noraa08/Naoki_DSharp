using System;
using Serilog;
using System.IO;
using System.Text.Json;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace TemplateBot
{
    public class BotConfig
    {
        public static async Task<BotConfig> LoadConfigAsync(string fp)
        {
            try
            {
                await using var fs = File.OpenRead(fp);
                return await JsonSerializer.DeserializeAsync<BotConfig>(fs);
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

        [JsonPropertyName("token"), JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        [Description("The token that is used to log in the bot.")]
        public string Token { get; set; }

        [JsonPropertyName("prefix"), JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        [Description("The prefix that the bot commands will use.")]
        public string Prefix { get; set; } = ">";
    }
}
