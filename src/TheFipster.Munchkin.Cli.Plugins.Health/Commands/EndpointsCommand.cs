using System;
using TheFipster.Munchkin.Cli.Plugins.Health.Verbs;
using TheFipster.Munchkin.Cli.Plugins.Abstractions;

namespace TheFipster.Munchkin.Cli.Plugins.Health.Commands
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
