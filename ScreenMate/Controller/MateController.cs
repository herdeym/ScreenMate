using ScreenMate.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ScreenMate.Controller
{
    public class MateController
    {
        public Mate Mate { get; }

        private static MateController mateController;
        public static MateController GetMateController()
        {
            mateController ??= new MateController();
            return mateController;
        }
        private MateController()
        {
            Mate = new Mate();
        }

        public bool LoadSprites()
        {
            int requiredWidth = Mate.SpriteWidth * Mate.NumOfSprites;
            int requiredHeight = Mate.SpriteHeight;

            try
            {
                Image image = Bitmap.FromFile(ConfigController.GetConfigController().Configurations.ImagePath);

                // Commented out because the sample file was 385, 32. If you want, you can change the check to less than.
                //if (image.Width != requiredWidth || image.Width != requiredHeight)
                //    throw new Exception(string.Format("File was not expected size ({0}, {1}).", requiredWidth, requiredHeight));

                for (int j = 0; j < Mate.NumOfRows; j++)
                {
                    List<Image> rowList = new List<Image>();
                    for (int i = 0; i < Mate.NumOfSprites; i++)
                    {
                        rowList.Add(new Bitmap(Mate.SpriteWidth, Mate.SpriteHeight));
                        using (Graphics g = Graphics.FromImage(rowList[i]))
                            g.DrawImage(image, new Rectangle(0, 0, 32, 32), new Rectangle(i * 32, 0, 32, 32), GraphicsUnit.Pixel);
                    }
                    Mate.Sprites.Add(rowList);
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

        public Image NextImage()
        {
            var row = Mate.Sprites[Mate.CurrentSpriteRow];
            var image = row[Mate.CurrentSpriteCol];

            ++Mate.CurrentSpriteCol;
            Mate.CurrentSpriteCol %= Mate.NumOfSprites;

            return image;
        }

    }
}
