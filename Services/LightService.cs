using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HomeAssistant;
using System.Linq;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Dynamic;

namespace DemoLights
{
	public interface ILightService
	{
		Task<List<LightModel>> GetLightsAsync();
		Task UpdateLightState(LightModel lightVM);
	}

	public class MockLightService : ILightService
	{
		public Task<List<LightModel>> GetLightsAsync()
		{
			var models = new List<LightModel>();

			models.Add(new LightModel { IsOn = true, Color = Color.White.MultiplyAlpha(0.5), Brightness = 200, Id = "light1", Name = "Hue Light Lamp 1" });
			models.Add(new LightModel { IsOn = true, Color = Color.White.MultiplyAlpha(0.5), Brightness = 200, Id = "light2", Name = "Hue Light Lamp 2" });

			models.Add(new LightModel { IsOn = false, Id = "light3", Name = "Hue Light Strip 1" });

			models.Add(new LightModel { IsOn = false, Id = "light4", Name = "Hue Light Strip 2" });

			return Task.FromResult(models);
		}

		public Task UpdateLightState(LightModel lightVM)
		{
			//don't do nothing here.. 
			return Task.FromResult(1);
		}
	}

	public class LightService : ILightService
	{
		HomeAssistantClient clientService;
		Service lightService;
		const string currentService = "light";
		IList<State> states;
		bool isInitialized;
		public LightService()
		{
			clientService = new HomeAssistantClient("https://rmarinho.duckdns.org/");
			Init();
		}

		async Task Init()
		{
			if (isInitialized)
				return;
			try
			{
				var services = await clientService.GetServicesAsync();
				states = await clientService.GetStatesAsync();
				lightService = services.FirstOrDefault(c => c.domain.StartsWith(currentService, StringComparison.Ordinal));
				isInitialized = true;
			}
			catch (Exception ex)
			{

			}
		}

		public async Task<List<LightModel>> GetLightsAsync()
		{
			await Init();
			List<LightModel> lightsVM = new List<LightModel>();
			var ligths = states.Where(c => c.EntityId.StartsWith(currentService, StringComparison.Ordinal)).ToList();
			foreach (var item in ligths)
			{
				var vm = new LightModel { Id = item.EntityId, State = "Phillips Hue", IsOn = item.state.ToString() == "on" };
				vm.Name = item.attributes.FirstOrDefault((arg) => arg.Name == "friendly_name").Value.ToString();

				if (vm.IsOn)
				{
					vm.Brightness = int.Parse(item.attributes.FirstOrDefault((arg) => arg.Name == "brightness").Value.ToString());

					var s = JsonConvert.DeserializeObject<IList<int>>(item.attributes.FirstOrDefault((arg) => arg.Name == "rgb_color").Value.ToString());
					vm.Color = new Color(s[0] / 255, s[1] / 255, s[2] / 255, 0.5);
				}

				lightsVM.Add(vm);
			}

			return lightsVM;
		}

		LightModel _lastModel;
		public async Task UpdateLightState(LightModel lightVM)
		{
			await Init();
			_lastModel = lightVM;
			dynamic light = new ExpandoObject();
			light.entity_id = lightVM.Id;
			try
			{
				if (lightVM.IsOn)
				{
					//light.white_value = lightVM.WhiteValue;
					//light.transition = 10;
					light.brightness = _lastModel.Brightness;
					light.rgb_color = new[] { _lastModel.Color.R * 255, _lastModel.Color.G * 255, _lastModel.Color.B * 255 };
					await clientService.CallService(lightService.domain, "turn_on", light);

				}
				else
				{
					await clientService.CallService(lightService.domain, "turn_off", light);
				}
			}
			catch (Exception ex)
			{

			}
		}
	}
}
