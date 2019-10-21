using CommandLine;

namespace TheFipster.Munchkin.Cli.Plugins.Login.Options
{
    public class TokenOptions
    {
        [Option('t', "token", Required = false, HelpText = "Login via security token.")]
        public string Token { get; set; }
    }
}
