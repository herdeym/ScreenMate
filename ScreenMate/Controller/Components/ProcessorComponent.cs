using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace ScreenMate.Controller.Components
{
    [Component("Processor")]
    public class ProcessorComponent : ComponentBase
    {

        private int counter;
        PerformanceCounter CPUPerformance;
        public override void InitComponent()
        {
            CPUPerformance = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            counter = 0;
            base.InitComponent();
        }

        public override void RunComponent()
        {
            
            Thread.Sleep(500);
            var CPUPercentage= CPUPerformance.NextValue();
            if (CPUPercentage > configurations.ProcessorThreshold && counter < 25)
                counter++;
            else
                counter--;
            mate.IsProcessor = counter > 20;
            Debug.WriteLine(CPUPercentage);
        }

        public override void SuspendComponent()
        {
            mate.IsProcessor = false;
            base.SuspendComponent();
        }
    }
}
