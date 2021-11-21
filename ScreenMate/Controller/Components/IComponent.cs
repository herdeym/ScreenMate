using System;
using System.Collections.Generic;
using System.Text;

namespace ScreenMate.Controller.Components
{
	public interface IComponent
	{
		void InitComponent();
		void FinalizeComponent();
		void SuspendComponent();
		void ResumeComponent();
		void UpdateComponentConfiguration();
		void RunComponent();
	}
}
