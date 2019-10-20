using System;
using System.Collections.Generic;
using System.Linq;
using TheFipster.Munchkin.CardStash.Domain;
using TheFipster.Munchkin.Cli.Domain;
using TheFipster.Munchkin.Cli.Plugins.Abstractions;
using TheFipster.Munchkin.Cli.Plugins.CardStash.Logic;
using TheFipster.Munchkin.Cli.Plugins.CardStash.Verbs;

namespace TheFipster.Munchkin.Cli.Plugins.CardStash.Commands
{
    public class DryrunCommand : IPluginCommand<DryrunVerb>
    {
        private readonly IJsonFileImporter<ImportFile> fileImporter;

        public DryrunCommand()
        {
            fileImporter = new FileImporter();
        }

        public int Execute(DryrunVerb options)
        {
            var import = fileImporter.ReadJsonFile(options.InputFile);

            Console.WriteLine("Found:");
            PrintResult(import.Monsters);
            PrintResult(import.Curses);
            PrintResult(import.Dungeons);

            return 0;
        }

        private static void PrintResult(IEnumerable<Card> cards)
        {
            if (cards != null)
            {
                Console.WriteLine($"{cards.Count()} {cards.First().GetType().Name}s");
                Console.WriteLine(string.Join(", ", cards.Select(x => x.Name)));
                Console.WriteLine();
            }
        }
    }
}
