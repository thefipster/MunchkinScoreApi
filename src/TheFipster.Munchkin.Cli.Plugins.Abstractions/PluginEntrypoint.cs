using System.Collections.Generic;

namespace TheFipster.Munchkin.Cli.Plugins.Abstractions
{
    public interface IPluginEntrypoint<TVerb>
    {
        int Execute(IEnumerable<string> args);
    }
}
