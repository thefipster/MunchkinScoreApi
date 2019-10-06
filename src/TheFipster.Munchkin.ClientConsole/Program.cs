using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace TheFipster.Munchkin.ClientConsole
{
    class Program
    {
        private static HttpClient client;
        private static DiscoveryDocumentResponse disco;

        static async Task Main(string[] args)
        {
            await discoverEndpoints();
            var tokenResponse = await requestAuthToken();
            await callApi(tokenResponse);

            Console.ReadKey();
        }

        private static async Task discoverEndpoints()
        {
            client = new HttpClient();
            disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                throw new Exception(disco.Error, disco.Exception);
            }
        }

        private static async Task<TokenResponse> requestAuthToken()
        {
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(
                new ClientCredentialsTokenRequest
                {
                    Address = disco.TokenEndpoint,
                    ClientId = "console-client",
                    ClientSecret = "secret",
                    Scope = "sample-api"
                });

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                throw new Exception(tokenResponse.ErrorDescription);
            }

            Console.WriteLine(tokenResponse.Json);
            return tokenResponse;
        }

        private static async Task callApi(TokenResponse tokenResponse)
        {
            client.SetBearerToken(tokenResponse.AccessToken);

            var response = await client.GetAsync("https://localhost:5999/api/identity");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }
        }
    }
}
