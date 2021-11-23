using ScreenMate.Controller.Components;
using ScreenMate.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace ScreenMate.Controller
{
	public class ComponentConfigurator
	{
		private ComponentRepository componentRepository;
		private ManualResetEvent manualResetEvent;

		private static ComponentConfigurator componentConfigurator;
		public static ComponentConfigurator GetComponentConfigurator()
		{
			componentConfigurator ??= new ComponentConfigurator();
			return componentConfigurator;
		}
		public ComponentConfigurator()
		{
			componentRepository = new ComponentRepository();
		}

		public void Initialize(Configurations configuration)
		{
			manualResetEvent = new ManualResetEvent(true);
			ConfigureComponents(configuration);
			new Thread(UpdateMateMovementBehaviour).Start();
		}

		private void UpdateMateMovementBehaviour()
		{
			while (true)
			{
				manualResetEvent.WaitOne();
				var movementComponents = componentRepository.GetAllMovementComponent();
				foreach (var movementComponent in movementComponents)
				{
					movementComponent.SuspendComponent();
				}
				if(movementComponents.Count>0)
					movementComponents[new Random().Next(movementComponents.Count)].ResumeComponent();
				Thread.Sleep(10000);
			}
		}

		public List<string> GetAllComponentIdentifier()
		{
			return GetAllComponentTypes()
				.Select(c => c.GetCustomAttribute<ComponentAttribute>().Identifier)
				.ToList();
		}
		
		private IEnumerable<Type> GetAllComponentTypes()
		{
			var componentAssembly = typeof(IComponent).GetTypeInfo().Assembly;
			return componentAssembly.GetTypes()
				.Where(c => !string.IsNullOrEmpty(c.GetCustomAttribute<ComponentAttribute>()?.Identifier));
		}

		public void ReconfigureComponents(Configurations configuration)
		{
			var activeComponents = componentRepository.GetActiveCoponents();

			var componentsToRemove = activeComponents.Where(c => !configuration.EnabledComponents.Contains(c));
			foreach (var component in componentsToRemove)
			{
				componentRepository.Remove(component);
			}
			ConfigureComponents(configuration);
		}

		private void ConfigureComponents(Configurations configuration)
		{
			var components = GetAllComponentTypes()
				   .Where(c => configuration.EnabledComponents.Contains(c.GetCustomAttribute<ComponentAttribute>().Identifier))
				   .ToList();
			foreach (var component in components)
			{
				var newComponent = (IComponent)Activator.CreateInstance(component);
				componentRepository.InsertOrUpdate(newComponent);
			}
		}

		public void SuspendAllComponents()
        {
			manualResetEvent.Reset();
			foreach (var component in componentRepository.GetComponents())
            {
				component.SuspendComponent();
            }
        }

		public void ResumeAllComponents()
		{
			manualResetEvent.Set();
			foreach (var component in componentRepository.GetComponents())
			{
				component.ResumeComponent();
			}
		}
	}
}
