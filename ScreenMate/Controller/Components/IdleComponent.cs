using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;
namespace ScreenMate.Controller.Components
{
    [Component("Idle")]
    public class IdleComponent : ComponentBase
    {
        [DllImport("User32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);
        internal struct LASTINPUTINFO
        {
            public uint cbSize;

            public uint dwTime;
        }

        public override void InitComponent()
        {
            LASTINPUTINFO lastInPut = new LASTINPUTINFO();
            lastInPut.cbSize = (uint)Marshal.SizeOf(lastInPut);
            base.InitComponent();
        }
        public override void RunComponent()
        {
            Thread.Sleep(500);
            //Debug.WriteLine(IdleTime());
            mate.IsIdle = configurations.IdleThresholdInSeconds < IdleTime();
            Debug.WriteLine(mate.IsIdle);
        }
        private int IdleTime() //In seconds
        {
            LASTINPUTINFO lastinputinfo = new LASTINPUTINFO();
            lastinputinfo.cbSize = (uint)Marshal.SizeOf(lastinputinfo);
            GetLastInputInfo(ref lastinputinfo);
            return (int)(((Environment.TickCount & int.MaxValue) - (lastinputinfo.dwTime & int.MaxValue)) & int.MaxValue) / 1000;
        }

        public override void SuspendComponent()
        {
            mate.IsIdle = false;
            base.SuspendComponent();
        }
    }
}
