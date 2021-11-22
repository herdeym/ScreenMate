﻿using ScreenMate.Controller.Components;
using ScreenMate.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ScreenMate.Controller
{
	public class ComponentRepository
	{
		private Dictionary<string, IComponent> components = new Dictionary<string, IComponent>();


		private static ComponentRepository componentRepository;
		public static ComponentRepository GetComponentRepository()
		{
			componentRepository ??= new ComponentRepository();
			return componentRepository;
		}

		public void InsertOrUpdate(IComponent c)
		{
			var componentIdentifier = c.GetIdentifier();
			if (string.IsNullOrEmpty(componentIdentifier))
				throw new ArgumentException("Invalid Component, must have ComponentAttribute!");

			if(components.TryGetValue(componentIdentifier, out var component))
			{ 
				component.UpdateComponentConfiguration();
			}
            else
            {
				components[componentIdentifier] = c;
				c.InitComponent();
			}			
		}

		public void Remove(string componentIdentifier)
		{
			components[componentIdentifier].FinalizeComponent();
			components.Remove(componentIdentifier);
		}

		public List<string> GetActiveCoponents()
		{
			return components.Keys.ToList();
		}
		public List<IComponent> GetAllMovementComponent()
		{
			return components.Values.Where(c => c.GetType().IsSubclassOf(typeof(MovementComponentBase))).ToList();
		}

		public List<IComponent> GetComponents()
        {
			return components.Values.ToList();
        }
	}
}
