using Newtonsoft.Json;
using System.IO;
using TheFipster.Munchkin.Cli.Plugins.Abstractions;
using TheFipster.Munchkin.Cli.Plugins.CardStash.Models;

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
