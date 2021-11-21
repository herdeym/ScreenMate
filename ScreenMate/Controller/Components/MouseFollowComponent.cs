using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ScreenMate.Controller.Components
{
	[Component("MouseFollow")]
	public class MouseFollowComponent : MovementComponentBase
	{
		public override void RunComponent()
		{
			destination= new Point(Cursor.Position.X, Cursor.Position.Y);
			base.RunComponent();
		}

	}
}
