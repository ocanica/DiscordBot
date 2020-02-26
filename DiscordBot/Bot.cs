using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot
{
    public class Bot
    {
        public DiscordClient Client { get; private set; }
        public CommandsNextExtension Commands { get; private set; }

        public async Task RunAsync()
        {
            var json = string.Empty;

            // Read stored token value from the config file
            using(var file = File.OpenRead("config.json"))
                using (var streamReader = new StreamReader(file, new UTF8Encoding(false)))
                    json = await streamReader.ReadToEndAsync().ConfigureAwait(false);
                    var configJson = JsonConvert.DeserializeObject<ConfigJson>(json);

            var config = new DiscordConfiguration()
            {
                // Token detail could have be inputted manually, however this creates another
                // level of abstract and protects against improper access
                Token = configJson.Token,
                // Boilerplate configuration
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                LogLevel = LogLevel.Debug,
                UseInternalLogHandler = true
            };

            var commandsConfig = new CommandsNextConfiguration()
            {
                // StringPrefix may have multiple prefixes, in this case I am using the one
                // declared in the config.json file
                StringPrefixes = new string[] { configJson.Prefix },
                EnableDms = false,
                EnableMentionPrefix = true
            };

            Client = new DiscordClient(config);
            Client.Ready += OnClientReady;
            Commands = Client.UseCommandsNext(commandsConfig);

            await Client.ConnectAsync();
            // Enforce indefinite delay to avoid closing the client -> ending the Program.cs class
            await Task.Delay(-1);
        }

        private Task OnClientReady(ReadyEventArgs e)
        {
            return Task.CompletedTask;
        }
    }
}
