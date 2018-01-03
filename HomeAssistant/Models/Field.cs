using System.Collections.Generic;
using Newtonsoft.Json;

namespace HomeAssistant
{

	public class Field
	{
		public string name
		{
			get;
			set;
		}
		public string description
		{
			get; set;
		}
		public string example { get; set; }

		public object values { get; set; }

		[JsonProperty("default")]
		public string Default { get; set; }

	}

}