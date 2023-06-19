using Copyfy.View;
using Copyfy.Model;
using System.Collections;
using System.Windows.Forms;
using System.Runtime;
using System.IO;
using System.Diagnostics;
using Microsoft.VisualBasic.Logging;

namespace Copyfy
{
    public partial class MainForm : Form
    {
        private bool sidebarExpand = false; //if true sidebar is closed otherwise opened
        private bool fileButtonExpand = true;
        private bool settingsButtonExpand = true;

        private System.Windows.Forms.Timer openHorizontal;

        private Dictionary<string, CustomButton> customButtons;

        private SourcePathWindow srcPathWindow;
        private OpenWindow openWindow;
        private LogFilesWindow logWindow;
        private InfoWindow infoWindow;
        private AutomationWindow automationWindow;

        public MainForm()
        {
            InitializeComponent();
            CreateButtons();
            InitUi();

            sidebarFlowLayoutPanel.Width = sidebarFlowLayoutPanel.MaximumSize.Width;
        }
        #region Methods
        private void CreateButtons()
        {
            customButtons = new Dictionary<string, CustomButton>();
            
            customButtons.Add("Menu", new CustomButton("Menu", Properties.Resources.icons8_menu_48, 6));
            customButtons.Add("Home", new CustomButton("Home", Properties.Resources.icons8_home_32__1_, 12, Home_Click));
            customButtons.Add("File", new CustomButton(new CustomButton("File", Properties.Resources.icons8_file_32, 12, File_Click), new CustomButton("Open", Properties.Resources.icons8_100__16, 35, Open_Click), new CustomButton("Log files", Properties.Resources.icons8_100__16, 35, LogFiles_Click)));
            customButtons.Add("Settings", new CustomButton(new CustomButton("Settings", Properties.Resources.icons8_settings_32, 12, Settings_Click), new CustomButton("Source Path", Properties.Resources.icons8_100__16, 35, SourcePath_Click), new CustomButton("Automation", Properties.Resources.icons8_automation_32, 27, Automation_Click)));
            customButtons.Add("Info", new CustomButton("Info", Properties.Resources.icons8_information_32, 12, Info_Click));
            customButtons.Add("Exit", new CustomButton("Exit", Properties.Resources.icons8_logout_32, 12, Exit_Click));
            customButtons.Add("Back", new CustomButton("Back", Properties.Resources.icons8_back_arrow_32__1_, 12, Back_Click));
            customButtons.Add("Select", new CustomButton("Select", Properties.Resources.icons8_checkmark_32, 12));
            customButtons.Add("DeleteForSourceWindow", new CustomButton("Delete", Properties.Resources.icons8_remove_32, 12));
            customButtons.Add("DeleteForAutomationWindow", new CustomButton("Delete", Properties.Resources.icons8_remove_32, 12));
            customButtons.Add("Copy", new CustomButton("Copy", Properties.Resources.icons8_copy_32, 12));
            customButtons.Add("BrowseForSourceWindow", new CustomButton("Browse..", Properties.Resources.icons8_browse_32, 12));
            customButtons.Add("BrowseForLogWindow", new CustomButton("Browse..", Properties.Resources.icons8_browse_32, 12));
            customButtons.Add("Add", new CustomButton("Add new", Properties.Resources.icons8_add_new_32, 12));
            customButtons.Add("Save", new CustomButton("Save", Properties.Resources.icons8_save_32, 12));
        }
        private void InitUi()
        {
            //gap between menu and other buttons
            Panel gap = new Panel();
            gap.Size = new Size(250, 50);

            //instantiate a Timer, which opens the sidebar
            openHorizontal = new System.Windows.Forms.Timer();
            openHorizontal.Tick += OpenHorizontal_Tick;
            openHorizontal.Interval = 10;

            customButtons["Menu"].button.FlatAppearance.MouseOverBackColor = Color.FromArgb(32,30,32);
            customButtons["Menu"].button.FlatAppearance.MouseDownBackColor = Color.FromArgb(32, 30, 32);
            //init custom buttons
            sidebarFlowLayoutPanel.Controls.Add(customButtons["Menu"]);
            sidebarFlowLayoutPanel.Controls.Add(gap);
            sidebarFlowLayoutPanel.Controls.Add(customButtons["Home"]);
            sidebarFlowLayoutPanel.Controls.Add(customButtons["File"]);
            sidebarFlowLayoutPanel.Controls.Add(customButtons["Settings"]);
            sidebarFlowLayoutPanel.Controls.Add(customButtons["Info"]);
            sidebarFlowLayoutPanel.Controls.Add(customButtons["Exit"]);
        }
        #endregion
        #region Events
        private void Menu_Click(object sender, EventArgs e)
        {
            openHorizontal.Start();
        }
        private void OpenHorizontal_Tick(object sender, EventArgs e)
        {
            //open & close the sidebar
            if (sidebarExpand)
            {
                sidebarFlowLayoutPanel.Width += 10;
                //meanwhile the sidbar is moving, need to change the location of control of the window to always be on center
                if (srcPathWindow != null)
                {
                    srcPathWindow.ReLocate(mainPanel);
                }
                if (openWindow != null)
                {
                    openWindow.ReLocate(mainPanel);
                }
                if (logWindow != null)
                {
                    logWindow.ReLocate(mainPanel);
                }
                if (infoWindow != null)
                {
                    infoWindow.ReLocate(mainPanel);
                }
                if (automationWindow != null)
                {
                    automationWindow.ReLocate(mainPanel);
                }

                if (sidebarFlowLayoutPanel.Width == sidebarFlowLayoutPanel.MaximumSize.Width)
                {
                    sidebarExpand = false;
                    ((System.Windows.Forms.Timer)sender).Stop();
                }
            }
            else
            {
                sidebarFlowLayoutPanel.Width -= 10;

                //meanwhile the sidbar is moving, need to change the location of control of the sourcePathWindow to always be on center
                if (srcPathWindow != null)
                {
                    srcPathWindow.ReLocate(mainPanel);
                }
                if (openWindow != null)
                {
                    openWindow.ReLocate(mainPanel);
                }
                if (logWindow != null)
                {
                    logWindow.ReLocate(mainPanel);
                }
                if (infoWindow != null)
                {
                    infoWindow.ReLocate(mainPanel);
                }
                if (automationWindow != null)
                {
                    automationWindow.ReLocate(mainPanel);
                }

                if (sidebarFlowLayoutPanel.Width == sidebarFlowLayoutPanel.MinimumSize.Width)
                {
                    sidebarExpand = true;
                    ((System.Windows.Forms.Timer)sender).Stop();
                }
            }
        }
        private void File_Click(object sender, EventArgs e)
        {
            //open & close file button
            Button button = (Button)sender;
            
            System.Windows.Forms.Timer openVertical = new System.Windows.Forms.Timer();
            openVertical.Tick += (object sender, EventArgs e) => 
            {
                if (fileButtonExpand)
                {
                    button.Parent.Parent.Height += 10;
                    if (button.Parent.Parent.Height == button.Parent.Parent.MaximumSize.Height)
                    {
                        fileButtonExpand = false;
                        ((System.Windows.Forms.Timer)sender).Stop();
                        
                    }
                }
                else
                {
                    button.Parent.Parent.Height -= 10;
                    if (button.Parent.Parent.Height == button.Parent.Parent.MinimumSize.Height)
                    {
                        fileButtonExpand = true;
                        ((System.Windows.Forms.Timer)sender).Stop();
                    }
                }

            };
            openVertical.Interval = 10;
            openVertical.Start();

        }
        private void Open_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear();
            mainPanel.Visible = true;
            
            //init controls of sourcePathWindow
            if (openWindow == null)
            {
                openWindow = new OpenWindow(mainPanel, sidebarExpand, customButtons["Back"], customButtons["Copy"]);
                openWindow.Show();
            }
            else
            {
                openWindow.sidebarExpand = sidebarExpand;
                openWindow.Show();
            }
        }
        private void Home_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear();
            mainPanel.Visible = false;
        }
        private void Settings_Click(object sender, EventArgs e)
        {
            //open & close settings button
            Button button = (Button)sender;
            
            System.Windows.Forms.Timer openVertical = new System.Windows.Forms.Timer();
            openVertical.Tick += (object sender, EventArgs e) =>
            {
                if (settingsButtonExpand)
                {
                    button.Parent.Parent.Height += 10;//button parent is a customButton, but we need the main customButton and it is parent of parent
                    if (button.Parent.Parent.Height == button.Parent.Parent.MaximumSize.Height)
                    {
                        settingsButtonExpand = false;
                        ((System.Windows.Forms.Timer)sender).Stop();
                    }
                }
                else
                {
                    button.Parent.Parent.Height -= 10;
                    if (button.Parent.Parent.Height == button.Parent.Parent.MinimumSize.Height)
                    {
                        settingsButtonExpand = true;
                        ((System.Windows.Forms.Timer)sender).Stop();
                    }
                }

            };
            openVertical.Interval = 10;

            openVertical.Start();
        }
        private void SourcePath_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear();
            mainPanel.Visible = true;
            
            //init controls of sourcePathWindow
            if (srcPathWindow == null)
            {
                srcPathWindow = new SourcePathWindow(mainPanel, sidebarExpand,  customButtons["Back"], customButtons["Select"], customButtons["DeleteForSourceWindow"], customButtons["BrowseForSourceWindow"]);
            }
            srcPathWindow.sidebarExpand = sidebarExpand;
            srcPathWindow.Show();
        }
        private void Exit_Click(object sender, EventArgs e)
        {
            Close();  
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //confirm exit
            if (MessageBox.Show("Are you sure you want to exit?", "Warning!", MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                e.Cancel = true;
            }
        }
        private void Info_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear();
            mainPanel.Visible = true;

            //init info window
            if (infoWindow == null)
            {
                infoWindow = new InfoWindow(mainPanel, sidebarExpand, customButtons["Back"]);
                infoWindow.Show();
            }
            else
            {
                infoWindow.sidebarExpand = sidebarExpand;
                infoWindow.Show();
            } 
        }
        private void Automation_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear();
            mainPanel.Visible = true;

            if (automationWindow == null)
            {
                automationWindow = new AutomationWindow(mainPanel, sidebarExpand, customButtons["Back"], customButtons["Add"], customButtons["DeleteForAutomationWindow"], customButtons["Save"]);
                automationWindow.Show();
            }
            else
            {
                automationWindow.sidebarExpand = sidebarExpand;
                automationWindow.Show();
            }
        }
        private void LogFiles_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear();
            mainPanel.Visible = true;

            if (logWindow == null)
            {
                logWindow = new LogFilesWindow(mainPanel, sidebarExpand, customButtons["Back"], customButtons["BrowseForLogWindow"]);
                logWindow.Show();
            }
            else
            {
                logWindow.sidebarExpand = sidebarExpand;
                logWindow.Show();
            }
        }
        private void Back_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear();
            mainPanel.Visible = false;
        }
        #endregion
    }
}