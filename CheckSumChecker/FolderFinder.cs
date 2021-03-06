using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;

namespace CheckSumChecker
{
    class FolderFinder : IFolderFinder
    {
        private string newlyGeneratedHash = "";
        private string lastHash = "";

        public string ChooseFolder()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();

            DialogResult result = dialog.ShowDialog();
            return dialog.SelectedPath;
        }
        public void CreateArchive(string sourcePath, string targetPath)
        {
            targetPath += (" " + DateTime.Now.Day.ToString() + "-"
                            + DateTime.Now.Month.ToString() + "-"
                            + DateTime.Now.Year.ToString() + "-"
                            + DateTime.Now.Hour.ToString() + "-"
                            + DateTime.Now.Minute.ToString() + "-"
                            + DateTime.Now.Second.ToString());
            ProcessStartInfo p = new ProcessStartInfo();
            p.FileName = "7za.exe";
            p.Arguments = "a -tzip \"" + targetPath + "\" \"" + sourcePath + "\"";
            p.WindowStyle = ProcessWindowStyle.Hidden;
            Process x = Process.Start(p);
            x.WaitForExit();
        }
        public string GenerateHash(string sourcePath)
        {
            ProcessStartInfo p = new ProcessStartInfo();
            p.FileName = "7za.exe";
            p.Arguments = "h -scrcsha256 " + sourcePath;
            p.WindowStyle = ProcessWindowStyle.Hidden;
            p.UseShellExecute = false;
            p.RedirectStandardOutput = true;
            Process x = Process.Start(p);
            string output = x.StandardOutput.ReadToEnd();
            x.WaitForExit();

            int i = output.IndexOf("SHA256 for data and names:");
            if (i >= 0)
            {
                output = output.Substring(i);
            }

            output = output.Substring(30);
            output = output.Remove(64, 22); //20

            newlyGeneratedHash = output;
            return output;
        }
        public void StoreHash(string destinationPath)
        {
            using(StreamWriter sw = File.CreateText(destinationPath))
            {
                sw.WriteLine(newlyGeneratedHash);
            }
            MessageBox.Show("Hash Stored!");
        }
        public void CheckHash(string sourcePath)
        {
            lastHash = "";
            //newlyGeneratedHash = GenerateHash();
            if (!File.Exists(sourcePath))
            {
                MessageBox.Show("File Doesn't Exist");
            }
            else
            {
                using(StreamReader sr = File.OpenText(sourcePath))
                {
                    while (!sr.EndOfStream)
                    {
                        lastHash += sr.ReadLine();
                    }
                }
            }

            if(newlyGeneratedHash == lastHash)
            {
                MessageBox.Show("Hashes are the same!");
            }
            else
            {
                StoreHash(sourcePath);
                MessageBox.Show("New Hash! Storing New Hash!");
            }
        }
    }
}
