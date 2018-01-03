using System;
using Newtonsoft.Json;

namespace HomeAssistant
{
	public class Event
	{
		[JsonProperty("event")]
		public string eventname { get; set; }
		public int listener_count { get; set; }
	}
}
