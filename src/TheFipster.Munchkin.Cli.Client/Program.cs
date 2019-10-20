namespace TheFipster.Munchkin.Cli.Client
{
    class Program
    {
        static int Main(string[] args)
        {
            var container = new DependencyInjection().Container;
            var commander = new ArgumentCommander(container);
            return commander.ExecuteMappedCommand(args);
        }
    }
}
