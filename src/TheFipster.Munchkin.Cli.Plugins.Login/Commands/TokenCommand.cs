using System;
using TheFipster.Munchkin.Cli.Plugins.Abstractions;
using TheFipster.Munchkin.Cli.Plugins.Login.Options;

namespace TheFipster.Munchkin.Cli.Plugins.Login.Commands
{
    public class TokenCommand : IPluginCommand<TokenOptions>
    {
        public int Execute(TokenOptions options)
        {
            Console.WriteLine($"Logging in via token: '{options.Token}'");
            return 0;
        }
    }
}
