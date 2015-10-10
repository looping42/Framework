using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
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
        public static void Error(string message)
        {
            WriteEntry(message, "Error");
        }

        public static void Error(Exception ex)
        {
            WriteEntry(ex.Message, "Error");
        }

        public static void Warning(string message)
        {
            WriteEntry(message, "Warning");
        }

        public static void Info(string message)
        {
            WriteEntry(message, "Info");
        }

        private static void WriteEntry(string message, string type)
        {
            try
            {
                DirectoryMethod.DirectoryMethod.CreateDirectory(Properties.Settings.Default.Logs);
                using (TextWriterTraceListener writer = new TextWriterTraceListener(Path.Combine(Properties.Settings.Default.Logs, DateExtension.DateExtension.DayToday() + "-Logs.txt")))
                {
                    Trace.AutoFlush = true;
                    Trace.Listeners.Add(writer);
                    //Message du fichier de logs et debug Output
                    Trace.WriteLine(string.Format("{0} - {1}  :  {2}",
                                          DateExtension.DateExtension.DayHourToday(),
                                          type,
                                          message));
                    //Message console
                    ConsoleListener.ConsoleListener.WriteInConsole(string.Format("{0} - {1}  :  {2}",
                                          DateExtension.DateExtension.HourToday(),
                                          type,
                                          message));

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