using Copyfy.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.DirectoryServices;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static System.Net.WebRequestMethods;
using Copyfy.Model;

namespace Copyfy.View
{
    public partial class LoadingScreen : Form
    {
        private System.Windows.Forms.Timer animationTimer;
        private Stopwatch copyingTime;
        private CustomButton cancel;
        private static ProgressBar copyPB;
        private static ProgressBar checkPB;
        private static Label name;
        private List<PictureBox> pictureBoxes = new List<PictureBox>();

        private List<DirControl> dirsToCopy = new List<DirControl>();

        private string targetPath { get; set; }
        private readonly string softwareFolder= "M248";
        private const int GAP = 10;
        private bool automated { get; set; }

        private LogFile log = new LogFile();
        public static int copyStatus
        {
            get { return copyPB.Value; }
            set { copyPB.Value = value; }
        }
        public static int checkStatus
        {
            get { return checkPB.Value; }
            set { checkPB.Value = value; }
        }
        public static string text
        {
            get { return name.Text; }
            set { name.Text = value; }
        }

        public LoadingScreen(List<DirControl> folders, string targetPath, bool automated = false)
        {
            InitializeComponent();
            
            dirsToCopy = folders;
            this.targetPath = targetPath;
            this.automated = automated;

            InitUI();
            InitAnimation();

            Copy();
        }
        
        #region Animation
        private void InitAnimation()
        {
            int left = 0;

            Random rnd = new Random();
           
            while (left < animationPanel.Width)
            {
                PictureBox pictureBox = new PictureBox();
                int size = rnd.Next(20, 90);

                pictureBox.BackgroundImage = Properties.Resources.icons8_documents_94;
                pictureBox.BackgroundImageLayout = ImageLayout.Zoom;
                pictureBox.Size = new Size(size, size);
                pictureBox.Left = left;
                left += pictureBox.Width + GAP;
                
                pictureBoxes.Add(pictureBox);
            }

            animationPanel.Controls.AddRange(pictureBoxes.ToArray());

            animationTimer.Start();
        }
        private void animationTimer_Tick(object sender, EventArgs e)
        {
            Random rnd = new Random();
            foreach (var pic in pictureBoxes)
            {
                pic.Location = new Point(pic.Location.X, pic.Location.Y + rnd.Next(2,6));//add some pixel to Y location, so pic will fall
                if (pic.Location.Y > animationPanel.Height)//if pic reach the animation panel bottom
                {
                    pic.Location = new Point(pic.Location.X, 0 - (pic.Size.Height));//put it back to animation panel top side
                }
            }
        }
        #endregion
        #region Methods
        private void InitUI()
        {
            copyPB = new ProgressBar();
            copyPB.Location = new System.Drawing.Point(107, 121);
            copyPB.Size = new System.Drawing.Size(522, 28);
            copyPB.Left = (this.ClientSize.Width - copyPB.Width) / 2;
            this.Controls.Add(copyPB);

            checkPB = new ProgressBar();
            checkPB.Location = new System.Drawing.Point(107, 172);
            checkPB.Size = new System.Drawing.Size(522, 28);
            checkPB.Left = (this.ClientSize.Width - checkPB.Width) / 2;
            this.Controls.Add(checkPB);

            cancel = new CustomButton("Cancel", Properties.Resources.icons8_close_window_32, 12);
            cancel.Location = new Point(this.ClientSize.Width - cancel.Width, this.ClientSize.Height - cancel.Height);
            cancel.button.Click += Cancel_Click;
            this.Controls.Add(cancel);

            name = new Label();
            name.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            name.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            name.Location = new System.Drawing.Point(23, 229);
            name.Size = new System.Drawing.Size(400, 21);
            this.Controls.Add(name);

            animationPanel.Left = (this.ClientSize.Width - animationPanel.Width) / 2;

            animationTimer = new System.Windows.Forms.Timer();
            animationTimer.Interval = 10;
            animationTimer.Tick += animationTimer_Tick;

            copyingTime = new Stopwatch();
        }
        private void Copy()
        {
            try
            {
                if(!automated)// If not in automated mode, show the LoadingScreen form.
                    this.Show();

                log.Write("Copying started!");

                foreach (DirControl dir in dirsToCopy)// Loop through all directories to copy
                {
                    log.Write($"Started to Copy {dir.fullName} to {targetPath}!");

                    copyingTime.Start();

                    // Set the maximum value of the progress bars
                    checkPB.Maximum = (int)(dir.size / 1024);
                    copyPB.Maximum = (int)(dir.size / 1024);

                    Copy copy = new Copy(dir.fullName, targetPath, automated);// Instantiate the Copy object and start the copy operation

                    copyingTime.Stop();
                    log.Write($"Duration: {String.Format("{0:00}:{1:00}:{2:00}", copyingTime.Elapsed.Hours, copyingTime.Elapsed.Minutes, copyingTime.Elapsed.Seconds)}");

                    if (IsSoftware(dir.fullName))// Check if the directory is a software directory.
                    {
                        if (!automated)// If not in automated mode, prompt the user to extract the software.
                        {
                            if (MessageBox.Show($"Do you want to extract software {dir.Name}?", "Extraction!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                // If user chooses Yes, instantiate the ExtractionScreen form and show it.
                                ExtractionScreen exScreen = new ExtractionScreen(copy.copiedPath);
                                exScreen.Show();
                            }
                        }else
                        {
                            // If in automated mode, automatically start the ExtractionScreen form.
                            ExtractionScreen exScreen = new ExtractionScreen(copy.copiedPath, automated);
                        }
                    }
                }
                this.Dispose();
            }
            catch(Exception ex)
            {
                // If there is an exception during the copy operation
                // and if not in automated mode, show the exception message
                if (!automated)
                    MessageBox.Show(ex.Message, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                log.Write(ex.Message);
                this.Dispose();
            }
        }
        private bool IsSoftware(string path)// Method to check if the path contains software
        {
            return path.Contains(softwareFolder);//if source path contains folder name "M248", then it is a software
        }
        #endregion
        #region Events
        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void LoadingScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            //confirm exit
            if (MessageBox.Show("Are you sure you want to exit?", "Warning!", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                Copyfy.Model.Copy.cancelClicked = true;
            }
        }
        #endregion
    }
}
