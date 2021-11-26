﻿using System;
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
            var CPUPercentage= CPUPerformance.NextValue();
            if (CPUPercentage > configurations.ProcessorThreshold && counter < 25)
                counter++;
            else
                counter = Math.Max(0, --counter);
            mate.IsProcessor = counter > 20;

            if (mate.IsProcessor)
                mate.CurrentSpriteRow = mate.IsRam ? 6 : 5;

            Debug.WriteLine($"Processor: {mate.IsProcessor}");
        }

        public override void SuspendComponent()
        {
            base.SuspendComponent();
            counter = 0;
            mate.IsProcessor = false;
        }
    }
}
