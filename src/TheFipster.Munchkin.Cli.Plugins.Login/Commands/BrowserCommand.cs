using System;
using TheFipster.Munchkin.Cli.Plugins.Abstractions;

namespace TheFipster.Munchkin.Cli.Plugins.Login.Commands
{
    public class BrowserCommand : IPluginCommand<object>
    {
        public int Execute(object options)
        {
            Console.WriteLine("Logging in via Browser...");
            return 0;
        }
    }
}
