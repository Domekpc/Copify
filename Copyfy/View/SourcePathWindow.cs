using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Copyfy.Model;
using Copyfy.View.Control;

namespace Copyfy.View
{
    public sealed class SourcePathWindow : Window
    {
        private Storage<ItemControl> storage;
        private FlowLayoutPanel pathFlowLayoutPanel;
       
        public SourcePathWindow(Panel mainPanel, bool sidebarExpand, params CustomButton[] buttons) : base(mainPanel, sidebarExpand, buttons)
        {
            storage = new Storage<ItemControl>();
            //the added paths are saved to file, therefore it is not needed to every time choose paths, when it is added before. Load them here
            storage.LoadFromFile("Paths.txt", (string name) => { return new ItemControl(name); });

            InitEvents();
        }
        #region Methods
        public void Show()
        {
            InitUi();
            buttons[0].Location = new Point(mainPanel.Width - CUSTOMBUTTONWIDTH, mainPanel.Height - CUSTOMBUTTONHEIGHT);
            mainPanel.Controls.Add(pathFlowLayoutPanel);
            mainPanel.Controls.AddRange(buttons);
        }
        private void InitUi()
        {
            pathFlowLayoutPanel = new FlowLayoutPanel();
            pathFlowLayoutPanel.Size = new Size(800, 400);
            pathFlowLayoutPanel.Left = (mainPanel.Width - pathFlowLayoutPanel.Width) / 2;
            pathFlowLayoutPanel.Top = ((mainPanel.Height - pathFlowLayoutPanel.Height) / 2) - 100;
            pathFlowLayoutPanel.BackColor = Color.FromArgb(221, 195, 165);
            pathFlowLayoutPanel.AutoScroll = true;

            CheckBoxEvents();//add event to every checkBox of pathControl 
            pathFlowLayoutPanel.Controls.AddRange(storage.ToArray());

            for (int i = 1; i < buttons.Length; i++)
            {
                buttons[i].Location = (sidebarExpand) ? new Point(mainPanel.Width - (((i + 1) * CUSTOMBUTTONWIDTH) + SIDEBARDIFF), mainPanel.Height - CUSTOMBUTTONHEIGHT) :
                                                        new Point(mainPanel.Width - ((i + 1) * CUSTOMBUTTONWIDTH), mainPanel.Height - CUSTOMBUTTONHEIGHT);
            }
        }
        private void CheckBoxEvents()
        {
            //hogy mi történik a lambda függvényben, az magyarázatra szorul. Tesztelgetések után sikerült működőképesre megírni
            foreach (ItemControl path in storage.items)
            {
                path.SelectionChanged += (ItemControl sender, bool isChecked) =>
                {
                    if (isChecked)
                    {
                        for (int i = 0; i < storage.Count; i++)
                        {
                            if (i != storage.IndexOf(sender))
                            {
                                storage[i].isChecked = false;
                                storage[i].BackColor = Color.FromArgb(32, 30, 32);
                            }
                        }
                    }
                };
            }
        } 
        private void InitEvents()
        {
            buttons[1].button.Click += Select_Click;
            buttons[2].button.Click += Delete_Click;
            buttons[3].button.Click += Browse_Click;
        }
        public override void ReLocate(Panel mainPanel)
        {
            pathFlowLayoutPanel.Left = (mainPanel.Width - pathFlowLayoutPanel.Width) / 2;
            pathFlowLayoutPanel.Top = ((mainPanel.Height - pathFlowLayoutPanel.Height) / 2) - 100;
            buttons[0].Location = new Point(mainPanel.Width - CUSTOMBUTTONWIDTH, mainPanel.Height - CUSTOMBUTTONHEIGHT);
        }
        private void ReInitContainer()
        {
            Write("Paths.txt");
            storage.Clear();
            pathFlowLayoutPanel.Controls.Clear();
            storage.LoadFromFile("Paths.txt", (string name) => { return new ItemControl(name); });//add items to the cleared pathControls list again
            CheckBoxEvents();//Init pathControls checkBox event again
            pathFlowLayoutPanel.Controls.AddRange(storage.ToArray());//finally add them to the Container
        }
        public void Write(string fileName)
        {
            using (StreamWriter sw = new StreamWriter(fileName))
            {
                foreach (ItemControl control in storage.items)
                {
                    sw.WriteLine(control.text);
                }
            }
        }
        public Dictionary<string, string> NetworkDrives()
        {
            Dictionary<string, string> output = new Dictionary<string, string>();

            string outputLine = "";
            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.FileName = "cmd";
            p.StartInfo.Arguments = "/c net use";
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.WaitForExit();

            using (StreamReader sw = p.StandardOutput)
            {
                while (!p.StandardOutput.EndOfStream)
                {
                    outputLine = sw.ReadLine();

                    if (outputLine.Contains(':'))
                    {
                        output.Add(outputLine.Substring(outputLine.IndexOf(':') - 1, 1), outputLine.Substring(outputLine.IndexOf(@"\\"))); //Drive letter, 
                    }

                }
            }
            foreach (string item in output.Keys)
            {
                if (output[item].Contains("Microsoft"))
                {
                    output[item] = output[item].Substring(0, output[item].IndexOf("Microsoft"));
                }
                if (output[item].Contains(""))
                {
                    for (int i = 0; i < output[item].Length; i++)
                    {
                        if (output[item][i].Equals(' '))
                        {
                            output[item] = output[item].Remove(i);
                        }
                    }
                }
            }

            p.Dispose();

            return output;
        }
        #endregion
        #region Events
        private void Select_Click(object sender, EventArgs e)
        {
            ItemControl control = storage.Find((ItemControl control) => { return control.isChecked; });
            try
            {
                control.isChecked = false;
                control.BackColor = Color.FromArgb(32, 30, 32);

                using (StreamWriter sw = new StreamWriter("SelectedPath.txt"))
                {
                    sw.WriteLine(control.text);//write selected path to a file,so next time it is not needed to select path
                }

                mainPanel.Controls.Clear();
                mainPanel.Visible = false;
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("There is no selected path!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void Delete_Click(object sender, EventArgs e)
        {
            ItemControl control = storage.Find((ItemControl control) => { return control.isChecked; });
            try
            {
                if (control == null)
                {
                    throw new NullReferenceException();//if control is null the Remove method can remove "null", therefore thow a new exception
                }
                storage.Remove(control);
                ReInitContainer();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("There is no selected path!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void Browse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            try
            {
                DialogResult selected = folderBrowserDialog.ShowDialog();
                
                if (selected == DialogResult.OK && !storage.Contains(new ItemControl(folderBrowserDialog.SelectedPath)))
                {
                    storage.Add(new ItemControl(folderBrowserDialog.SelectedPath));
                    ReInitContainer();//after choosed a new item(path), need to show it on the UI
                }
                else
                {
                    MessageBox.Show("The path is already added!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }   
            }
            catch (Exception)
            {
                MessageBox.Show("Read Error!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion
    }
}
