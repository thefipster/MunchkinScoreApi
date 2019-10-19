using CommandLine;
using System.Collections.Generic;
using TheFipster.Munchkin.Cli.Plugins.Abstractions;
using TheFipster.Munchkin.Console.StashPlugin.Commands;
using TheFipster.Munchkin.Console.StashPlugin.Verbs;

namespace TheFipster.Munchkin.Console.StashPlugin
{
    public class StashCommand : IPluginEntrypoint
    {
        public int Execute(IEnumerable<string> args) => Parser.Default
            .ParseArguments<
                DryrunVerb,
                SyncVerb
            >(args)
            .MapResult(
                (DryrunVerb opts) => new DryrunCommand().Execute(opts),
                (SyncVerb opts) => new SyncCommand().Execute(opts),
                noMatch => 1);
    }
}
