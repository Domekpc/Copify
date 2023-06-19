using Copyfy.View;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Copyfy.Model
{
    public class Copy
    {
       
        private bool automated { get; set; }
        private bool overwrite { get; set; } = true;
        private DirectoryInfo sourcePath { get; set; }
        private DirectoryInfo targetPath { get; set; }
        private long size { get; set; }
        private bool checking { get; set; } = false;
        public static bool cancelClicked { get; set; }
        public string copiedPath { get; private set; }

        public Copy(string sourcePath, string targetPath, bool automation = false)
        {
            this.sourcePath = new(sourcePath);
            this.targetPath = new(targetPath);
            this.automated = automation;

            size = DirSize(this.sourcePath);

            Start();
        }
        private void Start()
        {
            CheckAvaliableFreeSpace();

            Exists(sourcePath.Name);

            CreateMainDirectory();// createMainDirectory returns with the directory full path

            Copying(copiedPath);

            CheckCopy(copiedPath);
        }
        private Exception? CheckAvaliableFreeSpace()//check there is enough space on target drive or not
        {
            DriveInfo di = new DriveInfo(targetPath.FullName);

            if (size > di.AvailableFreeSpace)
                throw new Exception("There is not enough space on target path!");
            else
                return null;
        }
        private long DirSize(DirectoryInfo dirInfo)
        {
            long size = 0;

            //add file sizes
            FileInfo[] fis = dirInfo.GetFiles();
            foreach (FileInfo fi in fis)
            {
                size += fi.Length;

                if (checking)
                {
                    LoadingScreen.checkStatus += (int)(fi.Length / 1024);
                }
            }
            //add subdirectory sizes
            DirectoryInfo[] dis = dirInfo.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                size += DirSize(di);
            }

            return size;
        }
        private void Exists(string sourceDir)//check source folder exists on targetPath
        {
            foreach (DirectoryInfo Dir in targetPath.GetDirectories())//targetPath directory names
            {
                if (Dir.Name.Equals(sourceDir)) //check the sourceDir exist on targetPath
                {
                    if (!automated)
                    {
                        if (MessageBox.Show($"Directory: {sourceDir} alredy exists on targetPath!\nDo you want to overwrite?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                        {
                            overwrite = false;
                        }
                    }
                }
            }
        }
        private void CreateMainDirectory()
        {
            copiedPath = targetPath.FullName;

            if (targetPath.FullName.EndsWith('\\'))
            {
                copiedPath += sourcePath.Name;
            }
            else
            {
                copiedPath += "\\" + sourcePath.Name;
            }

            if (overwrite)
            {
                if (new DirectoryInfo(copiedPath).Exists)
                {
                    Directory.Move(copiedPath, copiedPath + "$");
                }

                copiedPath += "$";
            }
            else
            {
                copiedPath += " -Copy$";
                Directory.CreateDirectory(copiedPath);//create main directory
            }
        }
        private void Copying(string targetPathWithDirName)
        {
            // create all of the directories
            foreach (string dirPath in Directory.GetDirectories(sourcePath.FullName, "*", SearchOption.AllDirectories))//"*" mean that get all dirs. If it is "*t", get directories ends with t
            {
                DirectoryInfo dirInfo = new DirectoryInfo(dirPath);

                if (dirInfo.Exists)
                {
                    Directory.CreateDirectory(dirPath.Replace(sourcePath.FullName, targetPathWithDirName));
                }
                else
                {

                    throw new DirectoryNotFoundException($"Could not find '{dirInfo.Name}' !\nThe copying will be aborted!");
                }
            }
            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(sourcePath.FullName, "*.*", SearchOption.AllDirectories))
            {
                FileCopy(newPath, newPath.Replace(sourcePath.FullName, targetPathWithDirName));
            }
        }
        private void FileCopy(string sourcePath, string targetPath)
        {
            int bufferSize = 1024 * 1024;

            // Open the target file for writing with read/write shared access
            using (FileStream fsOut = new FileStream(targetPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
            {
                // Open the source file for reading
                using (FileStream fsIn = new FileStream(sourcePath, FileMode.Open, FileAccess.Read))
                {
                    FileInfo fi = new FileInfo(fsIn.Name);
                    fsOut.SetLength(fsIn.Length); // Set the length of the target file same as source file
                    int bytesRead = -1;
                    byte[] bytes = new byte[bufferSize];

                    // Read source file into the byte array until there's no more data to read
                    while ((bytesRead = fsIn.Read(bytes, 0, bufferSize)) > 0)
                    {
                        Application.DoEvents();

                        if (cancelClicked)// If a cancel operation is triggered, stop the file copying
                        {
                            return;
                        }

                        fsOut.Write(bytes, 0, bytesRead);// Write the bytes read from the source file to the target file

                        LoadingScreen.text = fi.Name;
                        LoadingScreen.copyStatus += (int)(bytesRead / 1024);// Update the copyStatus
                    }
                }
            }
        }
        private void CheckCopy(string copiedDirPath)//compare the source dirs size and copied dirs size
        {
            checking = true;

            if (size == DirSize(new DirectoryInfo(copiedDirPath)))// if they are equals the copy was successfull
            {
                RestoreDirName(sourcePath.Name, copiedDirPath);
            }
            else
            {
                throw new Exception("The copy was not successful!");
            }
        }
        private void RestoreDirName(string originalDir, string copiedDir)
        {
            string oldDirectoryName = copiedDir;//contains the $ sign
            string newDirectoryName = originalDir;//original dir name

            string path = targetPath.FullName;

            string oldPath = Path.Combine(path, oldDirectoryName);
            string newPath = Path.Combine(path, newDirectoryName);

            Directory.Move(oldPath, newPath);

            copiedPath = newPath;// if the copy was successful, then we remove $ sign from path
        }
    }
}
