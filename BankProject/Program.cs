/*
 * Created by Babak Samimi. 2018.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BankProject.Views;
using BankProject.UserStuff;
using Common;

namespace BankProject
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 



        [STAThread]
        static void Main()
        {
            User user = null;
            Client client = new Client();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginView(ref user, ref client));
        }
    }
}
