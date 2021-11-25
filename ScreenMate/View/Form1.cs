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
        private MateController mateController = MateController.GetMateController();
        protected Timer timer;
        public Form1()
        {
            this.BackColor = Color.LimeGreen;
            this.TransparencyKey = Color.LimeGreen;
            this.TopMost = true;
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            ShowInTaskbar = false;
            mateController.LoadSprites();

            InitializeComponent();
            timer = timer1;
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
            pictureBox1.SetBounds(mateController.Mate.Position.X, mateController.Mate.Position.Y, 32, 35);
            
            pictureBox1.Image = mateController.NextImage();
            base.OnPaint(e);
        }

        
    }
}
