using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;

namespace ScreenMate.Controller.Components
{
	//will be abstract
	public class MovementComponentBase : ComponentBase
	{
		protected Point destination = new Point(0, 0);
		protected float stoppingDistance = 10.5f;

		public override void RunComponent()
		{
			var distance = GetDistanceFromMate(destination);
			Debug.WriteLine(distance);
			Debug.WriteLine(mate.Position);
			if (mate.IsProcessor || mate.IsIdle || mate.IsRam || distance < stoppingDistance)
				return;
			mate.Position = LerpFromMate(destination, 0.05f);
		}

		private int Lerp(int firtInt, int secondInt, float by)
		{
			return (int)(firtInt + (secondInt - firtInt) * by);
		}
		protected Point LerpFromMate(Point point, float by)
		{
			int retX = Lerp(mate.Position.X, point.X, by);
			int retY = Lerp(mate.Position.Y, point.Y, by);
			return new Point(retX, retY);
		}

		protected float GetDistanceFromMate(Point p)
		{
			return (float)Math.Sqrt((mate.Position.X - p.X) * (mate.Position.X - p.X) + (mate.Position.Y - p.Y) * (mate.Position.Y - p.Y));
		}
	}
}
