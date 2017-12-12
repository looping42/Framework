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
        public void CreateDirectory(string repertory)
        {
            //Si le répértoire n'existe pas
            if (!Directory.Exists(Path.Combine(Const.CurrentApplication + repertory)))
            {
                Directory.CreateDirectory(Path.Combine(Const.CurrentApplication + repertory));
            }
        }
    }
}