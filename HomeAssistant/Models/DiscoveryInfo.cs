using System;
namespace HomeAssistant
{
	public class DiscoveryInfo
	{
		public string base_url { get; set; }
		public string location_name { get; set; }
		public bool requires_api_password { get; set; }
		public string version { get; set; }
	}
}
