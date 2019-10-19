using System.IO;
using TheFipster.Munchkin.Cli.Plugins.Abstractions;
using TheFipster.Munchkin.Console.StashPlugin.Verbs;

namespace TheFipster.Munchkin.Console.StashPlugin.Commands
{
    public class DryrunCommand : IPluginCommand<DryrunVerb>
    {
        public int Execute(DryrunVerb options)
        {
            var content = File.ReadAllText(options.InputFile);
            System.Console.WriteLine("Found content:");
            System.Console.WriteLine(content);
            return 0;
        }
    }
}
