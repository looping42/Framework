using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.ConsoleListener
{
    /****************************************************************
    Fichier       :  ConsoleListener
    Auteur        :  ds
    Mise à jour   :  10/10/2015
    Fonction      :  log les erreurs Console
    ******************************************************************/

    internal class ConsoleListener
    {
        /// <summary>
        /// Ecrit dans la console le message
        /// </summary>
        /// <param name="message">message</param>
        public static void WriteInConsole(string message)
        {
            ConsoleTraceListener consoleTracer;
            consoleTracer = new ConsoleTraceListener(true);
            consoleTracer.WriteLine(message);
            consoleTracer.Close();
        }
    }
}