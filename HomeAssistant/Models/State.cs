using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace HomeAssistant
{
	public class State
	{
		[JsonConverter(typeof(AttributeArrayConverter))]
		public IList<Attribute> attributes { get; set; }

		[JsonProperty("entity_id")]
		public string EntityId { get; set; }
		public DateTime last_changed { get; set; }
		public DateTime last_updated { get; set; }
		public object state { get; set; }
	}
}