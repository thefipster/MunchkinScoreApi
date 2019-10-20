using CommandLine;
using System.Collections.Generic;
using System.Linq;
using TheFipster.Munchkin.Cli.Plugins.CardStash;
using TheFipster.Munchkin.Cli.Plugins.Health;
using TheFipster.Munchkin.Cli.Client.Verbs;

namespace TheFipster.Munchkin.Cli.Client
{
    class Program
    {
        static int Main(string[] args) => Parser.Default
            .ParseArguments<
                StashVerb,
                HealthVerb
            >(takeMainArgumentFrom(args))
            .MapResult(
                (StashVerb _) => new StashCommand().Execute(withPluginArgumentsFrom(args)),
                (HealthVerb _) => new HealthCommand().Execute(withPluginArgumentsFrom(args)),
                noMatch => 1);

        private static IEnumerable<string> takeMainArgumentFrom(string[] args) => args.Take(1);
        private static IEnumerable<string> withPluginArgumentsFrom(string[] args) => args.Skip(1);
    }
}
