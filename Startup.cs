using DiscordTranslator.Google;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordTranslator
{
    public class Startup
    {
        // Google
        private string _googleAuthPath = @"GoogleCloudKey_discord-translator.json";

        private readonly IServiceProvider _services;
        private readonly IGoogleAuthLoader _googleAuth;

        public Startup(IServiceProvider services, IGoogleAuthLoader googleAuth)
        {
            _services = services;
            _googleAuth = googleAuth;
        }

        public async Task StartAsync()
        {
            var commandHandler = _services.GetRequiredService<ICommandHandler>();
            await Task.Run(commandHandler.InitalizeAsync);

            var setupGoogleAuth = _services.GetRequiredService<ISetUpGoogleAuth>();
            setupGoogleAuth.AuthImplicit(_googleAuth.LoadAuth(_googleAuthPath).project_id);
        }
    }
}
