using CommandLine;
using System.Collections.Generic;
using System.Linq;
using TheFipster.Munchkin.Cli.Plugins.Abstractions;
using TheFipster.Munchkin.Cli.Plugins.Login.Commands;
using TheFipster.Munchkin.Cli.Plugins.Login.Options;

namespace TheFipster.Munchkin.Cli.Plugins.Login
{
    public class LoginCommand : IPluginEntrypoint<LoginVerb>
    {
        public int Execute(IEnumerable<string> args)
        {
            if (args.Any())
                return executeMappedCommand(args);

            new BrowserCommand().Execute(null);
            return 0;
        }

        private static int executeMappedCommand(IEnumerable<string> args)
        {
            return Parser.Default
            .ParseArguments<
                TokenOptions
            >(args)
            .MapResult(
                (TokenOptions opts) => new TokenCommand().Execute(opts),
                noMatch => new BrowserCommand().Execute(null));
        }
    }
}
