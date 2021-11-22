using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace ScreenMate.Controller.Components
{
    [Component("SitOnWindow")]
    public class SitOnWindowComponent : MovementComponentBase
    {
        [DllImport("user32.dll")]
        private static extern int GetWindowRect(IntPtr hwnd, out Rectangle rect);
        [DllImport("user32.dll")]
        private static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);

        private Rectangle position;

        public override void RunComponent()
        {
            base.RunComponent();
        }

        public override void ResumeComponent()
        {
            var openWindowProcesses = Process.GetProcesses()
                    .Where(p => p.MainWindowTitle != "" && p.ProcessName != "ScreenMate").ToList();
            
            Process top = null;
            int topz = int.MaxValue;
            foreach (Process p in openWindowProcesses)
            {
                IntPtr handle = p.MainWindowHandle;
                int z = 0;
                do
                {
                    z++;
                    handle = GetWindow(handle, 3);
                } while (handle != IntPtr.Zero);

                if (z < topz)
                {
                    top = p;
                    topz = z;
                }
            }

            if (top != null)
            {
                Debug.WriteLine(top.ProcessName);
                var windowHandler = top.MainWindowHandle;
                GetWindowRect(windowHandler, out position);
                if (position.Y > 60)
                {
                    var bounds = Screen.PrimaryScreen.Bounds;
                    destination = new Point(Math.Max(bounds.Width-10, Math.Min(10,position.X + new Random().Next(position.Width-position.X))), position.Location.Y-10);
                    base.ResumeComponent();
                }
            }
            
        }
    }
}
