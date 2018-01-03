using System;
using System.Collections.Generic;

namespace HomeAssistant
{
	public class Config
	{
		public IList<string> components { get; set; }
		public double latitude { get; set; }
		public string location_name { get; set; }
		public double longitude { get; set; }
		public UnitSystem unit_system { get; set; }
		public string time_zone { get; set; }
		public string config_dir { get; set; }
		public string version { get; set; }
	}
}
