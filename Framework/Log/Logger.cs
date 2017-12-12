using Framework.DirectoryMethod;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Log
{
    /****************************************************************
    Classe Logger :  Permet la logs des erreurs ,infos ,etc
    Fichier       :  Logger
    Auteur        :  ds
    Mise à jour   :  10/10/2015
    Fonction      :  log les erreurs
    ******************************************************************/

    public static class Logger
    {
        public static void Error(string message)
        {
            WriteEntry("Error", message);
        }

        public static void Error(Exception ex)
        {
            WriteEntry("Error", ex.Message);
        }

        public static void Warning(string message)
        {
            WriteEntry("Warning", message);
        }

        public static void Info(string message)
        {
            WriteEntry("Info", message);
        }

        private static void WriteEntry(string type, string message)
        {
            try
            {
                Directory.CreateDirectory(Properties.Settings.Default.FMLogs);
                using (TextWriterTraceListener writer = new TextWriterTraceListener(Path.Combine(Const.CurrentApplication, Properties.Settings.Default.FMLogs, DateExtension.DateExtension.DayToday() + "- Logs.txt")))
                {
                    Trace.AutoFlush = true;
                    Trace.Listeners.Add(writer);
                    string messagetoWrite = string.Format("{0} - {1}        {2}", DateExtension.DateExtension.HourToday(), type, message);

                    //Message du fichier de logs et debug Output
                    Trace.WriteLine(messagetoWrite);

                    //Message console
                    ConsoleListener.ConsoleListener.WriteInConsole(messagetoWrite);

                    Trace.Listeners.Remove(writer);
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }
    }
}