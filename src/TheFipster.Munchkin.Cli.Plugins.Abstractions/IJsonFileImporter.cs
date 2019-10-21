namespace TheFipster.Munchkin.Cli.Plugins.Abstractions
{
    public interface IJsonFileImporter<TEntity>
    {
        TEntity ReadJsonFile(string filepath);
    }
}
