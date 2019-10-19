using System.Collections.Generic;

namespace TheFipster.Munchkin.Cli.Plugins.Abstractions
{
    public interface IPluginEntrypoint
    {
        int Execute(IEnumerable<string> args);
    }
}
