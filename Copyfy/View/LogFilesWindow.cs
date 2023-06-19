using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copyfy.View
{
    class LogFilesWindow : Window
    {
        private FlowLayoutPanel logFlowLayoutPanel;
        

        public LogFilesWindow(Panel mainPanel, bool sidebarExpand, params CustomButton[] buttons) : base(mainPanel, sidebarExpand, buttons)
        {
            logFlowLayoutPanel = new FlowLayoutPanel();
            logFlowLayoutPanel.Size = new Size(800, 500);
            logFlowLayoutPanel.Left = (mainPanel.Width - logFlowLayoutPanel.Width) / 2;
            logFlowLayoutPanel.Top = ((mainPanel.Height - logFlowLayoutPanel.Height) / 2) - 100;
            logFlowLayoutPanel.BackColor = Color.FromArgb(221, 195, 165);
            logFlowLayoutPanel.AutoScroll = true;

            buttons[1].button.Click += Browse_Click;
        }
        #region Methods
        private void InitUI()
        {
            buttons[0].Location = new Point(mainPanel.Width - CUSTOMBUTTONWIDTH, mainPanel.Height - CUSTOMBUTTONHEIGHT);

            for (int i = 1; i < buttons.Length; i++)
            {
                buttons[i].Location = (sidebarExpand) ? new Point(mainPanel.Width - (((i + 1) * CUSTOMBUTTONWIDTH) + SIDEBARDIFF), mainPanel.Height - CUSTOMBUTTONHEIGHT) :
                                                        new Point(mainPanel.Width - ((i + 1) * CUSTOMBUTTONWIDTH), mainPanel.Height - CUSTOMBUTTONHEIGHT);
            }
        }
        public void Show()
        {
            InitUI();
            
            mainPanel.Controls.Add(logFlowLayoutPanel);
            mainPanel.Controls.AddRange(buttons);
        }
        public override void ReLocate(Panel mainPanel)
        {
            logFlowLayoutPanel.Left = (mainPanel.Width - logFlowLayoutPanel.Width) / 2;
            logFlowLayoutPanel.Top = ((mainPanel.Height - logFlowLayoutPanel.Height) / 2) - 100;
            buttons[0].Location = new Point(mainPanel.Width - CUSTOMBUTTONWIDTH, mainPanel.Height - CUSTOMBUTTONHEIGHT);
        }
        #endregion
        #region Events
        private void Browse_Click(object sender, EventArgs e)
        { 
            OpenFileDialog log = new OpenFileDialog();
            log.InitialDirectory = Environment.CurrentDirectory + "\\Logs";
            try
            {
                DialogResult selected = log.ShowDialog();

                if (selected == DialogResult.OK)
                {
                    using (StreamReader sr = new StreamReader(log.FileName))
                    {
                        logFlowLayoutPanel.Controls.Clear();
                        while (!sr.EndOfStream)
                        {
                            //logFile.Text = sr.ReadLine();
                            logFlowLayoutPanel.Controls.Add(new Label() 
                            {
                                Text = sr.ReadLine(),
                                AutoSize = false, 
                                Size = new Size(770,26),
                                Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point),
                                ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(30)))), ((int)(((byte)(32)))))
                        });
                        }
                    }  
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Can not read the file!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}
