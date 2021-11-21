using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ScreenMate.Model
{
	public class Mate
	{
		public Point Position { get; set; } = new Point(500, 300);
		public bool IsRam { get; set; } = false;
		public bool IsProcessor { get; set; } = false;
		public bool IsIdle { get; set; } = false;

	}
}
