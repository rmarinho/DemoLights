using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HomeAssistant
{
	public class HomeAssistantClient
	{
		readonly string _baseUrl;
		public HomeAssistantClient(string url)
		{
			_baseUrl = url;
		}

		public async Task<string> GetAsync()
		{
			var client = GetClient();
			var s = await client.GetStringAsync("");
			return s;
		}

		public async Task<Config> GetConfigAsync()
		{
			var client = GetClient();
			var s = await client.GetStringAsync("config");

			return JsonConvert.DeserializeObject<Config>(s);
		}

		public async Task<DiscoveryInfo> GetDiscoveryInfoAsync()
		{
			var client = GetClient();
			var s = await client.GetStringAsync("discovery_info");

			return JsonConvert.DeserializeObject<DiscoveryInfo>(s);
		}

		public async Task<Bootstrap> GetBootstrapAsync()
		{
			var client = GetClient();
			var s = await client.GetStringAsync("bootstrap");

			return JsonConvert.DeserializeObject<Bootstrap>(s);
		}

		public async Task<IList<Service>> GetServicesAsync()
		{
			var client = GetClient();
			var s = await client.GetStringAsync("services");

			return JsonConvert.DeserializeObject<List<Service>>(s);
		}

		public async Task<IList<State>> GetStatesAsync()
		{
			var client = GetClient();
			var s = await client.GetStringAsync("states");

			return JsonConvert.DeserializeObject<List<State>>(s);
		}

		public async Task<bool> CallService(string domain, string service, object data)
		{
			System.Diagnostics.Debug.WriteLine(service);
			var client = GetClient();
			string json = JsonConvert.SerializeObject(data);
			var s = await client.PostAsync($"services/{domain}/{service}", new StringContent(json));

			return s.IsSuccessStatusCode;
		}

		HttpClient GetClient()
		{

			var _client = new HttpClient();
			_client.BaseAddress = new Uri(_baseUrl + "api/");
			_client.DefaultRequestHeaders.Add("x-ha-access", "");
			return _client;
		}
	}
}
