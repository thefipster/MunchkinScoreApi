using CommandLine;
using SimpleInjector;
using System.Collections.Generic;
using System.Linq;
using TheFipster.Munchkin.Cli.Plugins.Abstractions;
using TheFipster.Munchkin.Cli.Plugins.CardStash;
using TheFipster.Munchkin.Cli.Plugins.Health;
using TheFipster.Munchkin.Cli.Plugins.Login;

namespace TheFipster.Munchkin.Cli.Client
{
    public class ArgumentCommander
    {
        private Container depsInjector;
        private string[] arguments;

        private IEnumerable<string> mainArgument => arguments.Take(1);
        private IEnumerable<string> pluginArguments => arguments.Skip(1);

        public ArgumentCommander(string[] arguments, Container depsInjector)
        {
            this.depsInjector = depsInjector;
            this.arguments = arguments;
        }

        public int ExecuteMappedCommand()
        {
            return Parser.Default
            .ParseArguments<
                StashVerb,
                HealthVerb,
                LoginVerb
            >(mainArgument)
            .MapResult(
                (StashVerb verb) => depsInjector.GetInstance<IPluginEntrypoint<StashVerb>>().Execute(pluginArguments),
                (HealthVerb verb) => depsInjector.GetInstance<IPluginEntrypoint<HealthVerb>>().Execute(pluginArguments),
                (LoginVerb verb) => depsInjector.GetInstance<IPluginEntrypoint<LoginVerb>>().Execute(pluginArguments),
                noMatch => 1);
        }
    }
}
