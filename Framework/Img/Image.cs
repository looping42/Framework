using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;

namespace Framework.Img
{
    public static class Img
    {
        /// <summary>
        /// Redimensionne une image web ( en mémoire )
        /// </summary>
        /// <param name="OriginalFileLocation">chemin vers l'image</param>
        /// <param name="heigth">nouvelle taille</param>
        /// <param name="width">nouvelle largeur</param>
        /// <returns>image avce les nouvelle dimension</returns>
        public static WebImage resizeImageFromFile(String OriginalFileLocation, int heigth, int width)
        {
            if (File.Exists(Path.Combine(Const.CurrentApplication, OriginalFileLocation)))
            {
                return new WebImage(Path.Combine(Const.CurrentApplication, OriginalFileLocation)).Resize(width, heigth).Write();
            }
            else
            {
                throw new FileNotFoundException("Le Chemin vers le fichier n'existe pas ");
            }
        }
    }
}