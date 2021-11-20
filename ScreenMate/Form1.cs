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
        private static readonly int SPRITE_WIDTH = 32;
        private static readonly int SPRITE_HEIGHT = 35;
        private static readonly int NUM_SPRITES = 9;

        private static readonly int TIMER_INTERVAL = 100; // ms

        private int currentSprite = 0;
        private Image[] sprites = new Image[NUM_SPRITES];

        //https://social.msdn.microsoft.com/Forums/vstudio/en-US/0b34f53b-ba56-4939-b9a0-2ff25b365501/displaying-parts-of-an-image-animationally?forum=csharpgeneral
        public Form1()
        {
            this.BackColor = Color.LimeGreen;
            this.TransparencyKey = Color.LimeGreen;
            this.TopMost = true;
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            InitializeComponent();
            pictureBox1.Width = SPRITE_WIDTH;
            pictureBox1.Height = SPRITE_HEIGHT;
            LoadSprites("../RPGSoldier32x32.png");
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
            Point screenMatePos = new Point(Cursor.Position.X + 10, Cursor.Position.Y - 10);
            pictureBox1.SetBounds(screenMatePos.X, screenMatePos.Y, 32, 35);

            NextImage();

            //DrawCircle(e.Graphics);
        }

        void DrawCircle(Graphics g)
        {
            this.Cursor = new Cursor(Cursor.Current.Handle);
            Point screenMatePos = new Point(Cursor.Position.X + 10, Cursor.Position.Y - 10);
            int radius = 30;
            g.FillEllipse(new SolidBrush(Color.Red), screenMatePos.X, screenMatePos.Y, radius, radius);
        }

        private bool LoadSprites(string fileName)
        {
            int requiredWidth = SPRITE_WIDTH * NUM_SPRITES;
            int requiredHeight = SPRITE_HEIGHT;

            try
            {
                Image image = Bitmap.FromFile(fileName);

                // Commented out because the sample file was 385, 32. If you want, you can change the check to less than.
                //if (image.Width != requiredWidth || image.Width != requiredHeight)
                //    throw new Exception(string.Format("File was not expected size ({0}, {1}).", requiredWidth, requiredHeight));

                currentSprite = 0;
                for (int i = 0; i < sprites.Length; i++)
                {
                    sprites[i] = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                    using (Graphics g = Graphics.FromImage(sprites[i]))
                        g.DrawImage(image, new Rectangle(0, 0, 32, 32), new Rectangle(i * 32, 0, 32, 32), GraphicsUnit.Pixel);
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading file." + Environment.NewLine + ex.Message, "Load Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
        }

        private void NextImage()
        {
            pictureBox1.Image = sprites[currentSprite];

            ++currentSprite;
            currentSprite %= NUM_SPRITES;
        }
    }
}
