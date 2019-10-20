using Newtonsoft.Json;
using System.IO;
using TheFipster.Munchkin.Cli.Domain;
using TheFipster.Munchkin.Cli.Plugins.Abstractions;

namespace TheFipster.Munchkin.Cli.Plugins.CardStash.Logic
{
    public class FileImporter : IJsonFileImporter<ImportFile>
    {
        public ImportFile ReadJsonFile(string filepath)
        {
            var jsonContent = File.ReadAllText(filepath);
            return JsonConvert.DeserializeObject<ImportFile>(jsonContent);
        }
    }
}
