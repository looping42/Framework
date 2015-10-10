using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DateExtension
{
    /****************************************************************
    Fichier       :  DateExtension
    Auteur        :  ds
    Mise à jour   :  10/10/2015
    Fonction      :  appel des divers méthode comprenant des dates
    ******************************************************************/

    internal class DateExtension
    {
        public static string DayHourToday()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string DayToday()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }
    }