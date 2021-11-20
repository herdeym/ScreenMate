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
        //https://social.msdn.microsoft.com/Forums/vstudio/en-US/0b34f53b-ba56-4939-b9a0-2ff25b365501/displaying-parts-of-an-image-animationally?forum=csharpgeneral
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
            this.Cursor = new Cursor(Cursor.Current.Handle);
            Point screenMatePos = new Point(Cursor.Position.X + 10, Cursor.Position.Y - 10);
            int radius = 30;
            g.FillEllipse(new SolidBrush(Color.Red), screenMatePos.X, screenMatePos.Y, radius, radius);
        }
    }
}
