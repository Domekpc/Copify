using Copyfy.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Copyfy.View.Control
{
    // This class represents a user control for comparing files in two directories.
    public partial class CompareControl : UserControl
    {
        private FolderBrowserDialog browse;
        public string sourcePath
        { 
            get
            {
                return sourcePathTextBox.Text;
            } 
        }
        public string targetPath
        {
            get
            {
                return targetPathTextBox.Text;
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
        public CompareControl(string sourcePath = "", string targetPath = "")
        {
            InitializeComponent();
            browse = new FolderBrowserDialog();

            sourcePathTextBox.Text = sourcePath;
            targetPathTextBox.Text = targetPath;

            if ( sourcePath != "")
            {
                sourcePathError.Visible = !new DirectoryInfo(sourcePath).Exists;
            }

            if (targetPath != "")
            {
                targetPathError.Visible = !new DirectoryInfo(targetPath).Exists;
            }
        }

        private void browseSourePath_Click(object sender, EventArgs e)
        {
            DialogResult selected = browse.ShowDialog();

            if (selected == DialogResult.OK)
            {
                sourcePathTextBox.Text = browse.SelectedPath;
                sourcePathError.Visible = false;
            }
            else
            {
                MessageBox.Show("There is no selected path!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void browseTargetPath_Click(object sender, EventArgs e)
        {
            DialogResult selected = browse.ShowDialog();

            if (selected == DialogResult.OK)
            {
                targetPathTextBox.Text = browse.SelectedPath;
                targetPathError.Visible = false;
            }
            else
            {
                MessageBox.Show("There is no selected path!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CompareControl_Click(object sender, EventArgs e)
        {
            if (checkBox.Checked)
            {
                checkBox.Checked = false;
            }
            else
            {
                checkBox.Checked = true;
            }
        }

        private void sourcePathTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                sourcePathError.Visible = !new DirectoryInfo(sourcePathTextBox.Text).Exists;
            }
            catch (Exception)
            {

            }
        }

        private void targetPathTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                targetPathError.Visible = !new DirectoryInfo(targetPathTextBox.Text).Exists;
            }
            catch (Exception)
            {

            }
        }
    }
}
