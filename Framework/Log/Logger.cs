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
            WriteEntry(message, "error");
        }

        public static void Error(Exception ex)
        {
            WriteEntry(ex.Message, "error");
        }

        public static void Warning(string message)
        {
            WriteEntry(message, "warning");
        }

        public static void Info(string message)
        {
            WriteEntry(message, "info");
        }

        private static void WriteEntry(string message, string type)
        {
            try
            {
                DirectoryMethod.DirectoryMethod.CreateDirectory(Properties.Settings.Default.Logs);
                using (TextWriterTraceListener writer = new TextWriterTraceListener(Path.Combine(Properties.Settings.Default.Logs, DateExtension.DateExtension.DayToday() + "-Logs.txt")))
                {
                    DefaultTraceListener temp = new DefaultTraceListener();

                    // Define a trace listener to direct trace output from this method
                    // to the console.
                    ConsoleTraceListener consoleTracer;
                    consoleTracer = new ConsoleTraceListener(true);

                    Trace.AutoFlush = true;
                    Trace.Listeners.Add(writer);
                    Trace.Indent();
                    Trace.WriteLine(string.Format("{0} : {1}  :  {2}",
                                          DateExtension.DateExtension.DayHourToday(),
                                          type,
                                          message));

                    consoleTracer.WriteLine(string.Format("{0},{1},{2}",
                                          DateExtension.DateExtension.DayHourToday(),
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