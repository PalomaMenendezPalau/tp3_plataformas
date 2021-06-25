
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace TP2
{
    class Program

    {

        [STAThread]
        static void Main(string[] args)
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            bool cerrar = false;

            do
            {
                Form1 mainApp = new Form1(new string[2]);
                Form2 login = new Form2();
                Application.Run(login);
                mainApp.setSessionValues(login.getLoginValues());
                string[] datosLogin = mainApp.getSessionValues();
                if (datosLogin[0] != null)
                {
                    try
                    {
                        Form3 mainScreen = new Form3(mainApp.getSessionValues());
                        Application.Run(mainScreen);
                        mainApp.setSessionValues(null);
                    }
                    catch
                    {
                        cerrar = true;
                    }
                }
                else {
                    cerrar = true;
                }
            } while (!cerrar);


        }
    }
}

    
