namespace Copyfy
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.sidebarFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // sidebarFlowLayoutPanel
            // 
            this.sidebarFlowLayoutPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(30)))), ((int)(((byte)(32)))));
            this.sidebarFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidebarFlowLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.sidebarFlowLayoutPanel.MaximumSize = new System.Drawing.Size(250, 0);
            this.sidebarFlowLayoutPanel.MinimumSize = new System.Drawing.Size(74, 0);
            this.sidebarFlowLayoutPanel.Name = "sidebarFlowLayoutPanel";
            this.sidebarFlowLayoutPanel.Size = new System.Drawing.Size(74, 801);
            this.sidebarFlowLayoutPanel.TabIndex = 0;
            // 
            // mainPanel
            // 
            this.mainPanel.AutoSize = true;
            this.mainPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mainPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(195)))), ((int)(((byte)(165)))));
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(74, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(1170, 801);
            this.mainPanel.TabIndex = 1;
            this.mainPanel.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(195)))), ((int)(((byte)(165)))));
            this.BackgroundImage = global::Copyfy.Properties.Resources.kisspng_network_storage_systems_synology_inc_hard_drives_5ae7447f696d40_8305148215251057914318;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(1244, 801);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.sidebarFlowLayoutPanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1260, 840);
            this.MinimumSize = new System.Drawing.Size(1260, 840);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Copyfy";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FlowLayoutPanel sidebarFlowLayoutPanel;
        private Panel mainPanel;
    }
}