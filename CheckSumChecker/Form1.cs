using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;
using System.Configuration;

namespace CheckSumChecker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private FolderFinder folderFinder = new FolderFinder();

        private void button1_Click(object sender, EventArgs e)
        {
            //string path = folderFinder.ChooseFolder();
            string sourcePath = ConfigurationManager.AppSettings["FolderPath"];
            string destinationPath = ConfigurationManager.AppSettings["DestinationPath"];
            folderFinder.CreateArchive(sourcePath, "C:\\Users\\Nick\\Desktop\\test");
            MessageBox.Show("Archive Created!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //string path = folderFinder.ChooseFolder();
            string path = ConfigurationManager.AppSettings["FolderPath"];
            string hash = folderFinder.GenerateHash(path);
            MessageBox.Show(hash);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sourcePath = ConfigurationManager.AppSettings["FolderPath"];
            string destinationPath = ConfigurationManager.AppSettings["HashFile"];

            folderFinder.GenerateHash(sourcePath);
            folderFinder.StoreHash(destinationPath);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string sourcePath = ConfigurationManager.AppSettings["HashFile"];
            folderFinder.CheckHash(sourcePath);
        }
    }
}
