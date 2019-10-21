namespace TheFipster.Munchkin.Cli.Plugins.Abstractions
{
    public interface IPluginCommand<TVerb>
    {
        int Execute(TVerb options);
    }
}
