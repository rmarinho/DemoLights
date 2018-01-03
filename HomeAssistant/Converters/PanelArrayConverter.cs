using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HomeAssistant
{
	public class PanelsArrayConverter : JsonConverter
	{

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			serializer.Serialize(writer, value);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null)
				return null;

			JObject jObject = JObject.Load(reader);

			var list = new List<Panel>();
			foreach (var item in jObject)
			{
				var panel = ParsePanel(item);
				list.Add(panel);
			}
			return list;
		}

		public override bool CanConvert(Type objectType)
		{
			return true;
		}

		static Panel ParsePanel(KeyValuePair<string, JToken> item)
		{
			var p = item.Value.ToObject<Panel>();
			p.Name = item.Key;
			return p;
		}
	}
}
