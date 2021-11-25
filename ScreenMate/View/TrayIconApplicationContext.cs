using ScreenMate.Controller;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ScreenMate.View
{
    public class TrayIconApplicationContext : Form1
    {
        private NotifyIcon trayIcon;

        public TrayIconApplicationContext()
        {
            // Initialize Tray Icon
            trayIcon = new NotifyIcon()
            {
                Icon = new System.Drawing.Icon("../../../Resources/tray_icon.ico"),
                Text = "Configure ScreenMate settings by right-clicking.",
                ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(),
                Visible = true
            };
            trayIcon.ContextMenuStrip.Items.Add("Start", Image.FromFile("../../../Resources/tray_icon.ico"), OnStart);
            trayIcon.ContextMenuStrip.Items.Add("Stop", Image.FromFile("../../../Resources/tray_icon.ico"), OnStop);
            trayIcon.ContextMenuStrip.Items.Add("Configure", Image.FromFile("../../../Resources/tray_icon.ico"), OnConfigure);
            trayIcon.ContextMenuStrip.Items.Add("Exit",Image.FromFile("../../../Resources/tray_icon.ico") ,OnExit);
        }

        private void OnExit(object sender, EventArgs e)
        {
            // Hide tray icon, otherwise it will remain shown until user mouses over it
            trayIcon.Dispose();

            Environment.Exit(1);
        }

        private void OnStart(object sender, EventArgs e)
        {
            timer.Start();
            ComponentConfigurator.GetComponentConfigurator().ResumeAllComponents();
        }

        private void OnStop(object sender, EventArgs e)
        {
            timer.Stop();
            ComponentConfigurator.GetComponentConfigurator().SuspendAllComponents();
        }

        private void OnConfigure(object sender, EventArgs e)
        {
            var dialog = new Form2();
            dialog.Show();
        }

        private void OnRamComponent(object sender, EventArgs e)
        {

        }
    }
}
