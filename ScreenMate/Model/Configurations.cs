using System;
using System.Collections.Generic;
using System.Text;

namespace ScreenMate.Model
{
	public class Configurations
	{
		public List<string> EnabledComponents { get; set; }
		public int ProcessorThreshold;
		public int RamThreshold;
		public int IdleThresholdInSeconds;

	}
}
