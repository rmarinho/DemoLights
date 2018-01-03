using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HomeAssistant
{

	public class ServiceInfoArrayConverter : JsonConverter
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

			var list = new List<ServiceInfo>();
			foreach (var item in jObject)
			{
				var servInfo = ParseServiceInfo(item);
				list.Add(servInfo);
			}
			return list;
		}

		public override bool CanConvert(Type objectType)
		{
			System.Diagnostics.Debug.WriteLine(objectType.ToString());
			return true;
		}

		static ServiceInfo ParseServiceInfo(KeyValuePair<string, JToken> item)
		{
			var servInfo = new ServiceInfo { Name = item.Key };

			foreach (var serv in item.Value)
			{
				var prop = serv as JProperty;
				if (prop != null)
				{
					if (prop.Name.Equals(nameof(ServiceInfo.Description), StringComparison.OrdinalIgnoreCase))
					{
						servInfo.Description = prop.Value.ToString();
					}
					else

					if (prop.Name.Equals(nameof(ServiceInfo.Fields), StringComparison.OrdinalIgnoreCase))
					{
						servInfo.Fields = ParseFields(prop);
					}
					else
					{
						System.Diagnostics.Debug.WriteLine($"Need to parse this property: {prop.Name}");
					}

				}

			}

			return servInfo;
		}

		static List<Field> ParseFields(JProperty prop)
		{
			var fields = new List<Field>();
			foreach (var serviceField in prop.Value)
			{
				var tk = serviceField as JProperty;

				if (tk != null)
				{
					var field = new Field();
					field = tk.Value.ToObject<Field>();
					field.name = tk.Name;

					if (tk.Name.Equals(nameof(Field.values), StringComparison.OrdinalIgnoreCase))
					{
						//servInfo.Description = prop.Value.ToString();
					}
					else
					{
						System.Diagnostics.Debug.WriteLine($"Need to parse this property: {prop.Name}");
					}

					fields.Add(field);
				}
			}
			return fields;
		}
	}
}