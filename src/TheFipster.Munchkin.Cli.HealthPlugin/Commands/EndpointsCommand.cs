using System;
using TheFipster.Munchkin.Cli.HealthPlugin.Verbs;
using TheFipster.Munchkin.Cli.Plugins.Abstractions;

namespace TheFipster.Munchkin.Cli.HealthPlugin.Commands
{
    public class EndpointsCommand : IPluginCommand<EndpointsVerb>
    {
        public int Execute(EndpointsVerb options)
        {
            Console.WriteLine("Checking endpoints...");
            Console.WriteLine("... not implemented yet");
            return 1;
        }
    }
}
