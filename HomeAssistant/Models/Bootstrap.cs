using System.Collections.Generic;
using Newtonsoft.Json;

namespace HomeAssistant
{
	public class Bootstrap
	{
		public Config config { get; set; }
		public IList<Event> events { get; set; }
		[JsonConverter(typeof(PanelsArrayConverter))]
		public IList<Panel> panels { get; set; }
		public IList<Service> services { get; set; }
		public IList<State> states { get; set; }
	}
}
