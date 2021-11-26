using ScreenMate.Controller;
using ScreenMate.Controller.Components;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ScreenMate.Extensions
{
	public static class ComponentExtensions
	{
		public static string GetIdentifier(this IComponent component)
		{
			return component.GetType().GetCustomAttribute<ComponentAttribute>()?.Identifier;
		}
	}
}
