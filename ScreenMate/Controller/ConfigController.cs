using Newtonsoft.Json;
using ScreenMate.Controller.Components;
using ScreenMate.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace ScreenMate.Controller
{
	public class ConfigController
	{
		private readonly string configFilePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\ScreenMate\\configurations.json";
		private readonly ComponentConfigurator componentConfigurator = ComponentConfigurator.GetComponentConfigurator();
		public Configurations Configurations { get; set; }

		private static ConfigController configController;
		public static ConfigController GetConfigController()
		{
			configController ??= new ConfigController();
			return configController;
		}

		private ConfigController()
		{
			Configurations = LoadConfigurations();
		}

		private Configurations LoadConfigurations()
		{
			try
			{
				return JsonConvert.DeserializeObject<Configurations>(File.ReadAllText(configFilePath));
			}
			catch
			{
				return new Configurations() {
					EnabledComponents = new List<string>() { "MouseFollow" },
					ImagePath = "../../../Resources/RPGSoldier32x32.png",
					ProcessorThreshold = 70,
					RamThreshold = 70,
					IdleThresholdInSeconds = 10
				};
			}
		}

		public void SaveConfigurations()
		{
			string json = JsonConvert.SerializeObject(Configurations).ToString();
			//implicit check
			Directory.CreateDirectory($"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\ScreenMate");

			File.WriteAllText(configFilePath, json);

			componentConfigurator.ReconfigureComponents(Configurations);
		}
	}
}
