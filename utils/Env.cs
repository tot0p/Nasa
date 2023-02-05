using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TLEMAITRE1_nasa.utils
{

    internal static class  Env
    {
        //load env file
        public static void Load()
        {
            //file exist
            
            if (!File.Exists(".env")){
                //show error
                MessageBox.Show("Missing '.env'", "Error: Missing '.env'",   MessageBoxButton.OK,MessageBoxImage.Error);
            }


            // read file

            string cont = File.ReadAllText(".env");

            string[] all = cont.Split("\n");


            //set in env
            foreach (string elem in all)
            {
                string[] temp = elem.Split("=");
                Environment.SetEnvironmentVariable(temp[0], temp[1]);
            }
        }
    }

    

}
