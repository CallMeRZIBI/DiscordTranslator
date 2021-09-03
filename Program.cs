using System;
using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordTranslator.Google;
using Microsoft.Extensions.DependencyInjection;

namespace DiscordTranslator
{
    class Program
    {
        static void Main(string[] args)
            => new Program().MainAsync(args).GetAwaiter().GetResult();

        // Discord
        private string _discordToken = "token.txt";
        private DiscordSocketClient _client;

        public async Task MainAsync(string[] args)
        {
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "your path goes here");

            _client = new DiscordSocketClient();

            _client.Log += Log;

            var discordToken = String.Empty;
            try
            {
                discordToken = File.ReadAllText(_discordToken);
            }
            catch(Exception e)
            {
                Console.WriteLine("Couldn't locate token.txt");
                Console.WriteLine(e);
            }

            await _client.LoginAsync(TokenType.Bot, discordToken);
            await _client.StartAsync();

            var commands = new CommandService();

            // Initaliazing services
            var initalize = new Initalize(commands: commands,client: _client);
            var services = initalize.BuildServiceProvider();

            // Starting up some services
            var startup = new Startup(services, services.GetRequiredService<IGoogleAuthLoader>());
            await startup.StartAsync();

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
