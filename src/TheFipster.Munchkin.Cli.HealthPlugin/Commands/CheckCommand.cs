using System;
using TheFipster.Munchkin.Cli.HealthPlugin.Verbs;
using TheFipster.Munchkin.Cli.Plugins.Abstractions;

namespace TheFipster.Munchkin.Cli.HealthPlugin.Commands
{
    public class CheckCommand : IPluginCommand<CheckVerb>
    {
        public int Execute(CheckVerb options)
        {
            Console.WriteLine("Checking basic system...");
            Console.WriteLine("... maybe its working, maybe not.");
            return 1;
        }
    }
}
