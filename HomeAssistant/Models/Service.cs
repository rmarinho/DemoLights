using System.Collections.Generic;
using Newtonsoft.Json;

namespace HomeAssistant
{
	public class Service
	{
		public string domain { get; set; }

		[JsonConverter(typeof(ServiceInfoArrayConverter))]
		public IList<ServiceInfo> services { get; set; }
	}

}