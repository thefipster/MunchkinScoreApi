using CommandLine;
using System.Collections.Generic;
using TheFipster.Munchkin.Cli.Plugins.Abstractions;
using TheFipster.Munchkin.Cli.Plugins.Health.Commands;
using TheFipster.Munchkin.Cli.Plugins.Health.Verbs;

namespace TheFipster.Munchkin.Cli.Plugins.Health
{
    public class HealthCommand : IPluginEntrypoint<HealthVerb>
    {
        public int Execute(IEnumerable<string> args) => Parser.Default
            .ParseArguments<
                CheckVerb,
                EndpointsVerb
            >(args)
            .MapResult(
                (CheckVerb opts) => new CheckCommand().Execute(opts),
                (EndpointsVerb opts) => new EndpointsCommand().Execute(opts),
                noMatch => 1);
    }
}
