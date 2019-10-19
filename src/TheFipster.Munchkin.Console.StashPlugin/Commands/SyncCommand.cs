using System.IO;
using TheFipster.Munchkin.Cli.Plugins.Abstractions;
using TheFipster.Munchkin.Console.StashPlugin.Verbs;

namespace TheFipster.Munchkin.Console.StashPlugin.Commands
{
    public class SyncCommand : IPluginCommand<SyncVerb>
    {
        public int Execute(SyncVerb options)
        {
            var content = File.ReadAllText(options.InputFile);

            System.Console.WriteLine("Syncing: ");
            System.Console.WriteLine(content);
            System.Console.WriteLine("against ");
            System.Console.WriteLine(options.EndpointUrl);
            return 0;
        }
    }
}
