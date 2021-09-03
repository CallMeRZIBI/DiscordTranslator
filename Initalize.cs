using Discord.Commands;
using Discord.WebSocket;
using DiscordTranslator.Google;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordTranslator
{
    public class Initalize
    {
        private readonly CommandService _commands;
        private readonly DiscordSocketClient _client;

        // Ask if there are existing CommandService and DiscordSocketClient instance.
        // If there are, we retrieve them and add them to the DI container;
        // If not, we create our own.
        public Initalize(CommandService commands = null, DiscordSocketClient client = null)
        {
            _commands = commands ?? new CommandService();
            _client = client ?? new DiscordSocketClient();
        }

        public IServiceProvider BuildServiceProvider() =>
            new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                // You can pass in an instance of the desired type
                .AddSingleton<ICommandHandler, CommandHandler>()
                .AddSingleton<IGoogleTranslate, GoogleTranslate>()
                .AddScoped<IGoogleAuthLoader, GoogleAuthLoader>()
                .AddSingleton<ISetUpGoogleAuth, SetupGoogleAuth>()
                .BuildServiceProvider();
    }
}
