using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Core;
using XenoCanto.Models;

namespace Services
{
	public class XenoCantoService(HttpClient httpClient, IConfiguration Configuration) : IXenoCantoService
	{
		private const string APIKeySecretName = "XenoCantoAPIKey";
		private readonly string VaultUrl = Configuration["VaultUrl"]?.ToString() ?? "";

		private static SecretClientOptions GetSecretClientOptions()
		{
			return new SecretClientOptions()
			{
				Retry =
		{
			Delay= TimeSpan.FromSeconds(2),
			MaxDelay = TimeSpan.FromSeconds(16),
			MaxRetries = 5,
			Mode = RetryMode.Exponential
		 }
			};
		}

		private string GetAPIKey()
		{
			var client = new SecretClient(new Uri(VaultUrl), new DefaultAzureCredential(), GetSecretClientOptions());
			KeyVaultSecret s = client.GetSecret(APIKeySecretName);
			return s.Value ?? "";
		}

		private string GetURI()
		{
			var query = "loc:\"surrey,england\"";//+grp:birds
			var apiKey = GetAPIKey();

			if (string.IsNullOrEmpty(apiKey))
			{
				throw new Exception($"The API key is not set. ");
			}

			return $"recordings?query={query}&key={apiKey}";
		}


		public async Task<Data> GetAsync()
		{
			return await httpClient.GetFromJsonAsync<Data>(GetURI()) ?? new();
		}
	}



}

