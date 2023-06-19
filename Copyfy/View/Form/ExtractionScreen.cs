using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Renci.SshNet;
using System.Net.Http.Headers;
using Copyfy.Model;
using System.Reflection.Metadata.Ecma335;
using System.CodeDom;
using System.Diagnostics;

namespace Copyfy.View
{
    public partial class ExtractionScreen : Form
    {
        private System.Windows.Forms.Timer animationTimer;
        private Stopwatch extarctionTime;
        private List<PictureBox> pictureBoxes = new List<PictureBox>();
        private CustomButton start;

        private LogFile log = new LogFile();

        private const int GAP = 10;
        private readonly string nasData = "Automation\\NasData.txt";
        private bool automated { get; set; }
        private string software { get; set; }

        public ExtractionScreen(string software, bool automated = false, string ipAddress = "", string userName = "", string password = "")
        {
            InitializeComponent();

            this.software = software;
            this.automated = automated;

            ipAddressTextBox.Text = ipAddress;
            userNameTextBox.Text = userName;
            passwordTextBox.Text = password;

            InitUI();
            InitAnimation();

            if (automated)
            {
                Start_Click(this, new EventArgs());
            }
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

                pictureBox.BackgroundImage = Properties.Resources.icons8_unpacking_94;
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
                pic.Location = new Point(pic.Location.X, pic.Location.Y + rnd.Next(2, 6));//add some pixel to Y location, so pic will fall
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
            animationPanel.Left = (this.ClientSize.Width - animationPanel.Width) / 2;

            animationTimer = new System.Windows.Forms.Timer();
            animationTimer.Interval = 10;
            animationTimer.Tick += animationTimer_Tick;

            extarctionTime = new Stopwatch();

            start = new CustomButton("Start", Properties.Resources.icons8_go_32, 12, Start_Click);
            start.Location = new Point(this.ClientSize.Width - start.Width, this.ClientSize.Height - start.Height);
            this.Controls.Add(start);
        }
        private void SSH(string ip, string username, string password)//connect to nas via ssh
        {
            log.Write("Trying to connect...");

            //foreach (string software in softwares)
           // {
                DirectoryInfo softwareDir = new DirectoryInfo(software);

                SshClient client = new SshClient(ip, username, password);

                FileInfo tgzFile = GetTgzFile(softwareDir.FullName);

                try
                {

                    if (tgzFile.Equals(null))// search for .tgz file, which we would extract to nas
                    {
                        throw new FileNotFoundException("Can not find .tgz file!");
                    }

                    client.Connect();

                    log.Write("Connected");

                    //var command = client.CreateCommand($"cd .. && tar xzf share/Aktudat/NTG6HU/M248/{softwareDir.Name}/target_update/{tgzFile.Name}");
                    var command = client.CreateCommand($"cd .. && cd share/Aktudat/NTG6HU/M248/{softwareDir.Name}/target_update/ && tar xzf {tgzFile.Name}");// this command start extracting .tgz file
                    command.Execute();



                    Stream ssh = command.ExtendedOutputStream;//it gives many result, so read out with a stream
                    string result = "";

                    List<string> results = new();

                    using (StreamReader sr = new StreamReader(ssh))
                    {
                        while (!sr.EndOfStream)
                        {
                            results.Add(sr.ReadLine()); //write the result to log file
                        }
                    }

                    if (results.Count > 0)
                    {
                        log.Write(results[results.Count - 2].Substring(results[results.Count - 2].Length - (results[results.Count - 2].Length - results[results.Count - 2].IndexOf(':')), (results[results.Count - 2].Length - results[results.Count - 2].IndexOf(':'))));
                    }
                    else
                    {
                        log.Write("Extraction was successful!");
                    }
                }
                catch (Exception ex)
                {
                    if (!automated)
                        MessageBox.Show(ex.Message, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    log.Write(ex.Message);
                }
                finally
                {
                    extarctionTime.Stop();
                    log.Write($"Duration: {String.Format("{0:00}:{1:00}:{2:00}", extarctionTime.Elapsed.Hours, extarctionTime.Elapsed.Minutes, extarctionTime.Elapsed.Seconds)}");

                    client.Disconnect();
                    log.Write("Disconnected");

                    start.button.Enabled = true;

                    //this.Dispose();
                }
            //}

        }
        private FileInfo? GetTgzFile(string software)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(software + "/target_update");

                foreach (var item in dir.GetFiles())
                {
                    if (item.Extension.Equals(".tgz"))
                    {
                        return item;
                    }
                }
            }
            catch (Exception ex)
            {
                if (!automated)
                    MessageBox.Show(ex.Message, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null;
        }
        private void FillNasAddress()
        {
            try
            {
                using (StreamReader sr = new StreamReader(nasData))
                {
                    string data = sr.ReadLine();
                    if (data.Any(char.IsLetter))
                    {
                        string[] words = data.Split(';');

                        ipAddressTextBox.Text = words[0];
                        userNameTextBox.Text = words[1];
                        passwordTextBox.Text = words[2];
                    }
                }
            }
            catch (Exception ex)
            {
                log.Write(ex.Message);
            }
        }
        #endregion
        #region Events
        private void Start_Click(object sender, EventArgs e)
        {
            if (automated)
            {
                FillNasAddress();
            }

            if (ipAddressTextBox.Text == "" || userNameTextBox.Text == "" || passwordTextBox.Text == "")
            {
                if (!automated)
                    MessageBox.Show("Fill out the empty textbox(es)!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    log.Write("Some nas address is missing!");
            }
            else
            {
                extarctionTime.Start();// start timer which count the elapsed time

                Task newTask = Task.Run(() => 
                {
                    SSH(ipAddressTextBox.Text, userNameTextBox.Text, passwordTextBox.Text);//start connection and extraction
                });

                start.button.Enabled = false;
            }
        }
        #endregion
    }
}
