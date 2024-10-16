using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Classes
{
    public class clsGlobal
    {
        public static bool RememberMe(string Username , string Password)
        {
            try
            {
                string CurrentDirectory = Directory.GetCurrentDirectory();
            string FilePath = CurrentDirectory + "\\Login data.txt";
            if (File.Exists(FilePath) && Username == "")
            {
                File.Delete(FilePath);
                return true;
            }
                string DataToSave = Username + "#//#" + Password;
                using (StreamWriter writer = new StreamWriter(FilePath))
                {
                    writer.WriteLine(DataToSave);
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error",
                    MessageBoxButtons.OK,MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool GetStoredCredential(ref string UserName , ref string Password)
        {
            try
            {
                string CurrentDirectory = Directory.GetCurrentDirectory();
                string FilePath = CurrentDirectory + "\\Login data.txt";
                if (File.Exists(FilePath))
                {
                    using (StreamReader reader = new StreamReader(FilePath))
                    {
                        string Line;
                        while ((Line = reader.ReadLine()) != null)
                        {
                            string[] Result = Line.Split(new string[] { "#//#" }, StringSplitOptions.RemoveEmptyEntries);
                            UserName = Result[0];
                            Password = Result[1];
                        }
                        return true;
                    }
                }
                else return false;
            }
            catch(Exception ex) {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
