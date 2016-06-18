using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Common;
using Microsoft.Experimental.IdentityModel.Clients.ActiveDirectory;

namespace ConsoleClient
{
    class Program
    {
        private const string RedirectUri = "urn:ietf:wg:oauth:2.0:oob";
        private const string ApiBase = "http://localhost:12892/";

        static void Main()
        {
            MainAsync().GetAwaiter().GetResult();
        }

        private static async Task MainAsync()
        {
            var scope = new[] { TenantConfig.ClientId };

            AuthenticationContext authContext = new AuthenticationContext($"https://login.microsoftonline.com/{TenantConfig.Tenant}");
            AuthenticationResult ar = await authContext.AcquireTokenAsync(scope, null, TenantConfig.ClientId, new Uri(RedirectUri), new PlatformParameters(PromptBehavior.Auto, null), TenantConfig.PolicyId);

            var client = new HttpClient { BaseAddress = new Uri(ApiBase) };
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(ar.TokenType, ar.Token);
            string result = await client.GetStringAsync("/api/test");
            Console.WriteLine(result);
        }
    }
}
