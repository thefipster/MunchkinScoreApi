using CommandLine;
using System.Collections.Generic;
using TheFipster.Munchkin.Cli.Plugins.Abstractions;
using TheFipster.Munchkin.Cli.Plugins.CardStash.Commands;
using TheFipster.Munchkin.Cli.Plugins.CardStash.Verbs;

namespace TheFipster.Munchkin.Cli.Plugins.CardStash
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
