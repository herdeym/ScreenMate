using ScreenMate.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScreenMate.Controller
{
	public class MateController
	{
		public Mate Mate { get; }

		private static MateController mateController;
		public static MateController GetMateController()
		{
			mateController ??= new MateController();
			return mateController;
		}
		private MateController()
		{
			Mate= new Mate();
		}

	}
}
