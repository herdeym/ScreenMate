using Newtonsoft.Json;
using ScreenMate.Controller.Components;
using ScreenMate.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ScreenMate.Controller
{
	public class ConfigController
	{
		private readonly string configFilePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/ScreenMate/configurations.json";
		private readonly ComponentConfigurator componentConfigurator = ComponentConfigurator.GetComponentConfigurator();
		public Configurations Configurations { get; }

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
				return new Configurations();
			}
		}

		public void SaveConfigurations()
		{
			string json = JsonConvert.SerializeObject(Configurations).ToString();

			File.WriteAllText(configFilePath, json);

			componentConfigurator.ReconfigureComponents(Configurations);
		}
	}
}
