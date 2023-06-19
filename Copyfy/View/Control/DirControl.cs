using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Copyfy.Properties;

namespace Copyfy.View
{
    // This class represents a custom UserControl for directory handling.
    public partial class DirControl : UserControl
    {
        private DirectoryInfo dirInfo;
        public string fullName { get; private set; }
        public string dirName
        {
            get 
            {
                return nameLabel.Text;
            }
        }
        public bool isChecked 
        { 
            get 
            { 
                return checkBox.Checked; 
            } 
            set 
            { 
                checkBox.Checked = value; 
            } 
        }
        public long size
        {
            get
            { 
                return DirSize(dirInfo); 
            } 
        }
        public DirControl(string path)
        {
            InitializeComponent();

            fullName = path;
            dirInfo = new DirectoryInfo(path);

            iconPictureBox.Image = Resources.icons8_file_folder_20;
            nameLabel.Text = dirInfo.Name;
            lastWriteLabel.Text = dirInfo.LastAccessTime.ToString();
            //extensionLabel.Text = (dirInfo.Extension == "") ? "File folder" : dirInfo.Extension;
            extensionLabel.Text = "File folder";

            iconPictureBox.Click += Click_Control;
            nameLabel.Click += Click_Control;
            lastWriteLabel.Click += Click_Control;
            extensionLabel.Click += Click_Control;
            this.Click += Click_Control;
        }
        private void Click_Control(object sender, EventArgs e)
        {
            checkBox.Checked = !checkBox.Checked;
        }
        private long DirSize(DirectoryInfo dirInfo)
        {
            long size = 0;
            // Add the size of each file in the directory.
            FileInfo[] fis = dirInfo.GetFiles();
            foreach (FileInfo fi in fis)
            {
                size += fi.Length;
            }
            // Recursively add the size of each subdirectory.
            DirectoryInfo[] dis = dirInfo.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                size += DirSize(di);
            }
            return size;
        }
    }
}
