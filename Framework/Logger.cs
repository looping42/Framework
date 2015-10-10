using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Logger
{
    /****************************************************************
    Classe Logger :  Permet la logs des erreurs ,infos ,etc
    Fichier       :  Logger
    Auteur        :  ds
    Mise à jour   :  10/10/2015
    Fonction      :  log les erreurs
    ******************************************************************/

    public class Logger
    {
        public static void Error(string message, string module)
        {
            WriteEntry(message, "error", module);
        }

        public static void Error(Exception ex, string module)
        {
            WriteEntry(ex.Message, "error", module);
        }

        public static void Warning(string message, string module)
        {
            WriteEntry(message, "warning", module);
        }

        public static void Info(string message, string module)
        {
            WriteEntry(message, "info", module);
        }

        private static void WriteEntry(string message, string type, string module)
        {
            try
            {
                DirectoryMethod.DirectoryMethod.CreateDirectory(Properties.Settings.Default.Logs);
                using (StreamWriter writer = new StreamWriter(Path.Combine(Properties.Settings.Default.Logs, DateExtension.DateExtension.DayToday()), true))
                {
                    writer.WriteLine(string.Format("{0},{1},{2},{3}",
                                          DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                          type,
                                          module,
                                          message));
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }
    }
}