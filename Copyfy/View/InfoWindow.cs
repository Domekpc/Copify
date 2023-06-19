using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Copyfy.View
{
    class InfoWindow : Window
    {
        private Label infoLabel;
        public InfoWindow(Panel mainPanel, bool sidebarExpand, params CustomButton[] buttons) : base(mainPanel, sidebarExpand, buttons)
        {
            infoLabel = new Label();

            infoLabel.Text = "The program \"Copyfy\" is an application tool that allows you to copy in Windows and extract files in Linux using SSH protocol. Here is some information on how to use Copyfy:\n\nTo copy a file, you will need to open \"SourcePath\" and navigate to the location of the file you want to copy. Once you selected the correct directory, click \"Select\". Then go to \"File>Open\" select directory(s) you want to copy, then click to \"copy\" and navigate to the target location.\n\nTo extract files, all you have to do is choose \"yes\", after the copying. It automatically sense, that it was a software.\n\nThe whole process can be automated. To turn it on, you will have to go \"Settings>Automation\" click to \"On/Off\" button, which allows the automation.\n\n\n\nCopyfy team";
            infoLabel.AutoSize = false;
            infoLabel.Size = new Size(770, 600);
            infoLabel.Location = new Point((mainPanel.Width - infoLabel.Width) / 2, (mainPanel.Height - infoLabel.Height) / 2);
            infoLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            infoLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(30)))), ((int)(((byte)(32)))));

            buttons[0].Location = new Point(mainPanel.Width - CUSTOMBUTTONWIDTH, mainPanel.Height - CUSTOMBUTTONHEIGHT);
        }
        #region Methods
        public override void ReLocate(Panel mainPanel)
        {
            infoLabel.Left = (mainPanel.Width - infoLabel.Width) / 2;
            infoLabel.Top = ((mainPanel.Height - infoLabel.Height) / 2);
            buttons[0].Location = new Point(mainPanel.Width - CUSTOMBUTTONWIDTH, mainPanel.Height - CUSTOMBUTTONHEIGHT);
        }
        public void Show()
        {
            mainPanel.Controls.Add(infoLabel);
            mainPanel.Controls.Add(buttons[0]);
        }
        #endregion
    }
}
