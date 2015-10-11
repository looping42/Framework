using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DirectoryMethod
{
    /****************************************************************
    Fichier       :  DirectoryMethod
    Auteur        :  ds
    Mise à jour   :  10/10/2015
    Fonction      :  Création de répértoire
    ******************************************************************/

    public class DirectoryMethod
    {
        private string _PathToLogs = AppDomain.CurrentDomain.BaseDirectory;

        public string PathToLogs
        {
            get { return _PathToLogs; }
        }

        public void CreateDirectory(string repertory)
        {
            string p1 = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string path2 = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            string temp3 = AppDomain.CurrentDomain.BaseDirectory;
            string temp2 = Directory.GetDirectoryRoot(repertory);
            string temp = Directory.GetCurrentDirectory();
            //Si le répértoire n'existe pas
            if (!Directory.Exists(Path.Combine(PathToLogs + repertory)))
            {
                Directory.CreateDirectory(Path.Combine(PathToLogs + repertory));
            }
        }
    }
}