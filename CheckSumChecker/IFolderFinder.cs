using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckSumChecker
{
    interface IFolderFinder
    {
        string ChooseFolder();
        void CreateArchive(string sourcePath, string targetArchive);
        string GenerateHash(string sourcePath);
        void StoreHash(string destinationPath);
        void CheckHash(string sourcePath);
    }
}
