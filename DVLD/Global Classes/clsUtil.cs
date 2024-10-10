using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.classes
{
    public class clsUtil
    {
        public static string GenerateGuid()
        {
            Guid guid = Guid.NewGuid();
            return guid.ToString();
        }

        public static bool CreateFolderIfDoesNotExist(string FolderPath)
        {
            if (!Directory.Exists(FolderPath))
            {
                try
                {
                    Directory.CreateDirectory(FolderPath);
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error creating folder: " + ex.Message);
                    return false;
                }
            }
            return true;
        }

        public static string ReplaceFileNameWithGuid(string SourceFile)
        {
            FileInfo fileInfo = new FileInfo(SourceFile);
            return GenerateGuid() + fileInfo.Extension;
        }
        public static bool CopyImageToProjectFolderImages(ref string SourceFile)
        {
            string DestinationFolder = "C:\\DVLD people images\\";
            if (!CreateFolderIfDoesNotExist(DestinationFolder)) return false;
            string DestinationFile = DestinationFolder + ReplaceFileNameWithGuid(SourceFile);
            try
            {
                File.Copy(SourceFile, DestinationFile, true);
            }
            catch (Exception ioEx) {
                MessageBox.Show(("Error : " + ioEx.Message), "Error",MessageBoxButtons.OK , MessageBoxIcon.Warning);
                return false;
            }
            SourceFile = DestinationFile;
            return true;
        }
    }
}
