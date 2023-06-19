namespace Copyfy.View
{
    partial class DirControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.iconPictureBox = new System.Windows.Forms.PictureBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.lastWriteLabel = new System.Windows.Forms.Label();
            this.extensionLabel = new System.Windows.Forms.Label();
            this.sizeLabel = new System.Windows.Forms.Label();
            this.checkBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // iconPictureBox
            // 
            this.iconPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.iconPictureBox.Location = new System.Drawing.Point(7, 2);
            this.iconPictureBox.Name = "iconPictureBox";
            this.iconPictureBox.Size = new System.Drawing.Size(21, 22);
            this.iconPictureBox.TabIndex = 0;
            this.iconPictureBox.TabStop = false;
            // 
            // nameLabel
            // 
            this.nameLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.nameLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(169)))), ((int)(((byte)(109)))));
            this.nameLabel.Location = new System.Drawing.Point(38, 0);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.nameLabel.Size = new System.Drawing.Size(250, 26);
            this.nameLabel.TabIndex = 1;
            this.nameLabel.Text = "label1";
            // 
            // lastWriteLabel
            // 
            this.lastWriteLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lastWriteLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(169)))), ((int)(((byte)(109)))));
            this.lastWriteLabel.Location = new System.Drawing.Point(332, 0);
            this.lastWriteLabel.Name = "lastWriteLabel";
            this.lastWriteLabel.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.lastWriteLabel.Size = new System.Drawing.Size(200, 26);
            this.lastWriteLabel.TabIndex = 1;
            this.lastWriteLabel.Text = "label1";
            // 
            // extensionLabel
            // 
            this.extensionLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.extensionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(169)))), ((int)(((byte)(109)))));
            this.extensionLabel.Location = new System.Drawing.Point(593, 0);
            this.extensionLabel.Name = "extensionLabel";
            this.extensionLabel.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.extensionLabel.Size = new System.Drawing.Size(100, 26);
            this.extensionLabel.TabIndex = 1;
            this.extensionLabel.Text = "label1";
            // 
            // sizeLabel
            // 
            this.sizeLabel.AutoSize = true;
            this.sizeLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.sizeLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(169)))), ((int)(((byte)(109)))));
            this.sizeLabel.Location = new System.Drawing.Point(462, 5);
            this.sizeLabel.Name = "sizeLabel";
            this.sizeLabel.Size = new System.Drawing.Size(0, 15);
            this.sizeLabel.TabIndex = 1;
            // 
            // checkBox
            // 
            this.checkBox.AutoSize = true;
            this.checkBox.Location = new System.Drawing.Point(745, 6);
            this.checkBox.Name = "checkBox";
            this.checkBox.Size = new System.Drawing.Size(15, 14);
            this.checkBox.TabIndex = 2;
            this.checkBox.UseVisualStyleBackColor = true;
            // 
            // DirControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(32)))), ((int)(((byte)(30)))));
            this.Controls.Add(this.checkBox);
            this.Controls.Add(this.sizeLabel);
            this.Controls.Add(this.extensionLabel);
            this.Controls.Add(this.lastWriteLabel);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.iconPictureBox);
            this.Name = "DirControl";
            this.Size = new System.Drawing.Size(770, 26);
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox iconPictureBox;
        private Label nameLabel;
        private Label lastWriteLabel;
        private Label extensionLabel;
        private Label sizeLabel;
        private CheckBox checkBox;
    }
}
