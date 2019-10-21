using SimpleInjector;
using TheFipster.Munchkin.Cli.Plugins.Abstractions;
using TheFipster.Munchkin.Cli.Plugins.CardStash;
using TheFipster.Munchkin.Cli.Plugins.Health;
using TheFipster.Munchkin.Cli.Plugins.Login;

namespace TheFipster.Munchkin.Cli.Client
{
    public class DependencyInjection
    {
        public Container Container
        {
            get
            {
                var container = new Container();

                container.Register<IPluginEntrypoint<HealthVerb>, HealthCommand>();
                container.Register<IPluginEntrypoint<LoginVerb>, LoginCommand>();
                container.Register<IPluginEntrypoint<StashVerb>, StashCommand>();

                return container;
            }
        }
    }
}
