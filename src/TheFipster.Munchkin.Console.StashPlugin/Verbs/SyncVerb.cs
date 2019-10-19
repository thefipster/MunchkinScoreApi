using CommandLine;

namespace TheFipster.Munchkin.Console.StashPlugin.Verbs
{
    [Verb("sync", HelpText = "Sync file contents the the stash API.")]
    public class SyncVerb
    {
        [Option('f', "file", Required = true, HelpText = "Input file to be processed.")]
        public string InputFile { get; set; }

        [Option('e', "endpoint", Required = true, HelpText = "Endpoint URL of the stash API.")]
        public string EndpointUrl { get; set; }
    }
}
