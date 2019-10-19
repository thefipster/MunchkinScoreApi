using CommandLine;
using System.Collections.Generic;
using System.Linq;
using TheFipster.Munchkin.Cli.HealthPlugin;
using TheFipster.Munchkin.Console.StashPlugin;
using TheFipster.Munchkin.ConsoleClient.Verbs;

namespace TheFipster.Munchkin.ConsoleClient
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
