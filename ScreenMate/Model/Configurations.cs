using System;
using System.Collections.Generic;
using System.Text;

namespace ScreenMate.Model
{
	public class Configurations
	{
		public List<string> EnabledComponents { get; set; } = new List<string>() { "Disappear", "Idle", "MouseFollow", "Processor", "Ram" };
		public int ProcessorThreshold=90;
		public int RamThreshold=45;
		public int IdleThresholdInSeconds=10;

	}
}
