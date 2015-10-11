using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace Framework.Img
{
    public class Img
    {
        /// <summary>
        /// Resize image with a directory as source
        /// </summary>
        /// <param name="OriginalFileLocation">Image location</param>
        /// <param name="heigth">new height</param>
        /// <param name="width">new width</param>
        /// <returns>image with new dimentions</returns>
        public static WebImage resizeImageFromFile(String OriginalFileLocation, int heigth, int width)
        {
            return resizeImageFromFile(OriginalFileLocation, heigth, width, false, false);
        }

        /// <summary>
        /// Resize image with a directory as source
        /// </summary>
        /// <param name="OriginalFileLocation">Image location</param>
        /// <param name="heigth">new height</param>
        /// <param name="width">new width</param>
        /// <param name="keepAspectRatio">keep the aspect ratio</param>
        /// <returns>image with new dimentions</returns>
        public static WebImage resizeImageFromFile(String OriginalFileLocation, int heigth, int width, Boolean keepAspectRatio)
        {
            return resizeImageFromFile(OriginalFileLocation, heigth, width, keepAspectRatio, false);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="OriginalFileLocation"></param>
        /// <param name="heigth"></param>
        /// <param name="width"></param>
        /// <param name="keepAspectRatio"></param>
        /// <param name="getCenter"></param>
        /// <returns></returns>
        private static WebImage resizeImageFromFile(String OriginalFileLocation, int heigth, int width, Boolean keepAspectRatio, Boolean getCenter)
        {
            Log.Logger.Info("Classe");
            Log.Logger.Info(Directory.GetCurrentDirectory());
            if (File.Exists(Path.Combine(Directory.GetCurrentDirectory() + OriginalFileLocation)))
            {
                return new WebImage(OriginalFileLocation).Resize(width, heigth).Write();
            }
            else
            {
                throw new Exception("Le Chemin vers le fichier n'existe pas ");
            }
        }

        public static Bitmap Resize(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
    }
}