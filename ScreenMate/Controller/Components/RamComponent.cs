using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.VisualBasic;

namespace ScreenMate.Controller.Components
{
    [Component("Ram")]
    public class RamComponent : ComponentBase
    {
        private int counter;
        public override void InitComponent()
        {
            counter = 0;
            base.InitComponent();
        }

        public override void RunComponent()
        {
            var availableRam = new PerformanceCounter("Memory", "Available MBytes", null).RawValue;
            //TODO: max memoria ???
            var totalRam = 24576.0f;
            //Debug.WriteLine(availableRam);
            //Debug.WriteLine(totalRam);
            //Debug.WriteLine(100 - (availableRam / totalRam * 100));
            //Debug.WriteLine(mate.IsRam);
            if (100 - (availableRam / totalRam * 100) > configurations.RamThreshold && counter < 25)
                counter++;
            else
                counter = Math.Max(0, --counter);
            mate.IsRam = counter > 20;
            Debug.WriteLine($"Ram: {mate.IsRam}");
            if (mate.IsRam)
                mate.CurrentSpriteRow = mate.IsProcessor ? 6 : 4;
            
        }

        public override void SuspendComponent()
        {
            base.SuspendComponent();
            counter = 0;
            mate.IsRam = false;
        }
    }
}
