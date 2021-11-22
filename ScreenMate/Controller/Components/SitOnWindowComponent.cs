using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ScreenMate.Controller.Components
{
    [Component("SitOnWindow")]
    public class SitOnWindowComponent : MovementComponentBase
    {
        [DllImport("user32.dll")]
        private static extern int GetWindowRect(IntPtr hwnd, out Rectangle rect);
        private Rectangle position;

        public override void RunComponent()
        {
            base.RunComponent();
        }

        public override void ResumeComponent()
        {
            var openProcesses = System.Diagnostics.Process.GetProcesses()
                    .Where(p => p.MainWindowTitle != "");
            List<Process> openWindowProcesses = new List<Process>();
            foreach (var p in openProcesses)
            {
                if (p.MainWindowTitle != "")
                    openWindowProcesses.Add(p);
            }

            var random = new Random();
            var selectedWindow = openWindowProcesses.ElementAt(random.Next(openWindowProcesses.Count()));
            Debug.WriteLine(selectedWindow.ProcessName);
            var windowHandler = selectedWindow.MainWindowHandle;

            GetWindowRect(windowHandler, out position);

            destination = position.Location;

            base.ResumeComponent();
        }
    }
}
