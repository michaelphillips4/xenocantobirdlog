using System.Net.Http.Json;
using XenoCanto.Models;

namespace XenoCanto
{
	public class XenoCantoHttpClient(HttpClient client, IConfiguration configuration)
	{


		public async Task<Data> GetAsync()
		{


			const string APIKeyName = "XenoCantoAPIKey";
			var query = "loc:\"surrey,england\"";//+grp:birds
			var apiKey = configuration[APIKeyName]; 

			if(string.IsNullOrEmpty(apiKey))
			{
				throw new Exception($"The API key {APIKeyName} is not set. It is avaliable from your Xenocanto account.");
			}

			var uri = $"recordings?query={query}&key={apiKey}";
			return await client.GetFromJsonAsync<Data>(uri) ?? new();
		}
	}



}

