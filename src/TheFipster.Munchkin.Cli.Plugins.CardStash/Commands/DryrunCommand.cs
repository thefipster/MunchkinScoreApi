using System.IO;
using TheFipster.Munchkin.Cli.Plugins.Abstractions;
using TheFipster.Munchkin.Cli.Plugins.CardStash.Verbs;

namespace TheFipster.Munchkin.Cli.Plugins.CardStash.Commands
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
