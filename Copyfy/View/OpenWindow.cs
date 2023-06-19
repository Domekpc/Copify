using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Copyfy.Model;

namespace Copyfy.View
{
    class OpenWindow : Window
    {
        private Storage<DirControl> storage = new Storage<DirControl>();
        private List<LoadingScreen> loadingScreens = new List<LoadingScreen>();
        private FlowLayoutPanel pathFlowLayoutPanel;
        private readonly string SELECTEDPATH = "SelectedPath.txt";
        public OpenWindow(Panel mainPanel, bool sidebarExpand, params CustomButton[] buttons) : base(mainPanel, sidebarExpand, buttons)
        {
            pathFlowLayoutPanel = new FlowLayoutPanel();
            pathFlowLayoutPanel.Size = new Size(800, 400);
            pathFlowLayoutPanel.Left = (mainPanel.Width - pathFlowLayoutPanel.Width) / 2;
            pathFlowLayoutPanel.Top = ((mainPanel.Height - pathFlowLayoutPanel.Height) / 2) - 100;
            pathFlowLayoutPanel.BackColor = Color.FromArgb(221, 195, 165);
            pathFlowLayoutPanel.AutoScroll = true;
            
            InitEvents();
        }

        #region Methods
        private void InitUi()
        {
            pathFlowLayoutPanel.Controls.AddRange(storage.ToArray());

            for (int i = 1; i < buttons.Length; i++)
            {
                buttons[i].Location = (sidebarExpand) ? new Point(mainPanel.Width - (((i + 1) * CUSTOMBUTTONWIDTH) + SIDEBARDIFF), mainPanel.Height - CUSTOMBUTTONHEIGHT) :
                                                        new Point(mainPanel.Width - ((i + 1) * CUSTOMBUTTONWIDTH), mainPanel.Height - CUSTOMBUTTONHEIGHT);
            }
        }
        private void ReInitContainer()
        {
            storage.Clear();
            pathFlowLayoutPanel.Controls.Clear();
            storage.LoadFromDirInfo(SELECTEDPATH, (string data) => { return new DirControl(data); });
            pathFlowLayoutPanel.Controls.AddRange(storage.ToArray());
        }
        public void Show()
        {
            InitUi();
            ReInitContainer();
            buttons[0].Location = new Point(mainPanel.Width - CUSTOMBUTTONWIDTH, mainPanel.Height - CUSTOMBUTTONHEIGHT);
            mainPanel.Controls.Add(pathFlowLayoutPanel);
            mainPanel.Controls.AddRange(buttons);
        }
        public override void ReLocate(Panel mainPanel)
        {
            pathFlowLayoutPanel.Left = (mainPanel.Width - pathFlowLayoutPanel.Width) / 2;
            pathFlowLayoutPanel.Top = ((mainPanel.Height - pathFlowLayoutPanel.Height) / 2) - 100;
            buttons[0].Location = new Point(mainPanel.Width - CUSTOMBUTTONWIDTH, mainPanel.Height - CUSTOMBUTTONHEIGHT);
        }
        private void InitEvents()
        {
            buttons[1].button.Click += Copy_Click;
        }
        private List<DirControl> GetCheckedFolders()
        {
            List<DirControl> checkedFolders = new List<DirControl>();

            foreach (DirControl item in storage.items)
            {
                if (item.isChecked == true)
                {
                    checkedFolders.Add(item);
                    item.isChecked = false;//after copy was clicked, uncheck the checked folders
                }
            }
            return checkedFolders;
        }
        #endregion
        #region Events
        private void Copy_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog chooseTargetPath = new FolderBrowserDialog();
            List<DirControl> checkedFolders = GetCheckedFolders();

            try
            {
                if (checkedFolders.Count == 0)
                {
                    throw new NullReferenceException();
                }
                if (chooseTargetPath.ShowDialog() == DialogResult.OK)
                {
                    string targetPath = chooseTargetPath.SelectedPath;
                    LoadingScreen ldScreen = new LoadingScreen(checkedFolders, targetPath);
                }
                else
                {
                    MessageBox.Show("No target path was selected!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("No folder was selected!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion
    }
}
