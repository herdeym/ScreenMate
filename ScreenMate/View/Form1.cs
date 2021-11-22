using ScreenMate.Controller;
using ScreenMate.Controller.Components;
using ScreenMate.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenMate
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            this.BackColor = Color.LimeGreen;
            this.TransparencyKey = Color.LimeGreen;
            this.TopMost = true;
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;


            InitializeComponent();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.LimeGreen, e.ClipRectangle);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawCircle(e.Graphics);
        }

        void DrawCircle(Graphics g)
        {
            var mate = MateController.GetMateController().Mate;
            var diameter = 10;
            var color = mate.IsIdle ? Color.Blue : Color.Red;
            g.FillEllipse(new SolidBrush(color), mate.Position.X-diameter/2, mate.Position.Y-diameter/2, diameter, diameter);
        }
    }
}
