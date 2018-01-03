using System.Collections.Generic;
using Newtonsoft.Json;

namespace HomeAssistant
{
	public class ServiceInfo
	{
		public string Name
		{
			get;
			set;
		}
		public string Description { get; set; }

		public IList<Field> Fields { get; set; }
	}




}