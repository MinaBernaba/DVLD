using DVLD.Login_Screen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the Application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            bool Relogin = true;
            while (Relogin)
            {
                Relogin = false;
                frmLogin frmLogin = new frmLogin();
                Application.Run(frmLogin);
                if (Convert.ToInt16(frmLogin.Tag) != 0)
                {
                    int UserID = (int)frmLogin.Tag;
                    frmMain frmMain = new frmMain(UserID);
                    Application.Run(frmMain);
                    if (Convert.ToByte(frmMain.Tag) == 1)
                    {
                        Relogin = true;
                    }
                }
                else return;
            }
        }
    }
}
