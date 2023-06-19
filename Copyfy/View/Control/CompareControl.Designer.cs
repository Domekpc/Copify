namespace Copyfy.View.Control
{
    partial class CompareControl
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
            this.sourcePathLabel = new System.Windows.Forms.Label();
            this.sourcePathTextBox = new System.Windows.Forms.TextBox();
            this.targetPathTextBox = new System.Windows.Forms.TextBox();
            this.targetPathLabel = new System.Windows.Forms.Label();
            this.browseSourePath = new System.Windows.Forms.Button();
            this.browseTargetPath = new System.Windows.Forms.Button();
            this.checkBox = new System.Windows.Forms.CheckBox();
            this.sourcePathError = new System.Windows.Forms.Label();
            this.targetPathError = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // sourcePathLabel
            // 
            this.sourcePathLabel.AutoSize = true;
            this.sourcePathLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.sourcePathLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(169)))), ((int)(((byte)(109)))));
            this.sourcePathLabel.Location = new System.Drawing.Point(6, 4);
            this.sourcePathLabel.Name = "sourcePathLabel";
            this.sourcePathLabel.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.sourcePathLabel.Size = new System.Drawing.Size(74, 20);
            this.sourcePathLabel.TabIndex = 2;
            this.sourcePathLabel.Text = "Source path";
            // 
            // sourcePathTextBox
            // 
            this.sourcePathTextBox.Location = new System.Drawing.Point(3, 31);
            this.sourcePathTextBox.Name = "sourcePathTextBox";
            this.sourcePathTextBox.Size = new System.Drawing.Size(374, 23);
            this.sourcePathTextBox.TabIndex = 3;
            this.sourcePathTextBox.TextChanged += new System.EventHandler(this.sourcePathTextBox_TextChanged);
            // 
            // targetPathTextBox
            // 
            this.targetPathTextBox.Location = new System.Drawing.Point(393, 31);
            this.targetPathTextBox.Name = "targetPathTextBox";
            this.targetPathTextBox.Size = new System.Drawing.Size(374, 23);
            this.targetPathTextBox.TabIndex = 3;
            this.targetPathTextBox.TextChanged += new System.EventHandler(this.targetPathTextBox_TextChanged);
            // 
            // targetPathLabel
            // 
            this.targetPathLabel.AutoSize = true;
            this.targetPathLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.targetPathLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(169)))), ((int)(((byte)(109)))));
            this.targetPathLabel.Location = new System.Drawing.Point(397, 5);
            this.targetPathLabel.Name = "targetPathLabel";
            this.targetPathLabel.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.targetPathLabel.Size = new System.Drawing.Size(71, 20);
            this.targetPathLabel.TabIndex = 2;
            this.targetPathLabel.Text = "Target path";
            // 
            // browseSourePath
            // 
            this.browseSourePath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(32)))), ((int)(((byte)(30)))));
            this.browseSourePath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.browseSourePath.ForeColor = System.Drawing.Color.White;
            this.browseSourePath.Location = new System.Drawing.Point(3, 59);
            this.browseSourePath.Name = "browseSourePath";
            this.browseSourePath.Size = new System.Drawing.Size(32, 29);
            this.browseSourePath.TabIndex = 4;
            this.browseSourePath.Text = "...";
            this.browseSourePath.UseVisualStyleBackColor = false;
            this.browseSourePath.Click += new System.EventHandler(this.browseSourePath_Click);
            // 
            // browseTargetPath
            // 
            this.browseTargetPath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(32)))), ((int)(((byte)(30)))));
            this.browseTargetPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.browseTargetPath.ForeColor = System.Drawing.Color.White;
            this.browseTargetPath.Location = new System.Drawing.Point(393, 59);
            this.browseTargetPath.Name = "browseTargetPath";
            this.browseTargetPath.Size = new System.Drawing.Size(32, 29);
            this.browseTargetPath.TabIndex = 4;
            this.browseTargetPath.Text = "...";
            this.browseTargetPath.UseVisualStyleBackColor = false;
            this.browseTargetPath.Click += new System.EventHandler(this.browseTargetPath_Click);
            // 
            // checkBox
            // 
            this.checkBox.AutoSize = true;
            this.checkBox.Location = new System.Drawing.Point(750, 7);
            this.checkBox.Name = "checkBox";
            this.checkBox.Size = new System.Drawing.Size(15, 14);
            this.checkBox.TabIndex = 5;
            this.checkBox.UseVisualStyleBackColor = true;
            // 
            // sourcePathError
            // 
            this.sourcePathError.AutoSize = true;
            this.sourcePathError.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.sourcePathError.ForeColor = System.Drawing.Color.Red;
            this.sourcePathError.Location = new System.Drawing.Point(41, 62);
            this.sourcePathError.Name = "sourcePathError";
            this.sourcePathError.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.sourcePathError.Size = new System.Drawing.Size(142, 20);
            this.sourcePathError.TabIndex = 2;
            this.sourcePathError.Text = "This path does not exist!";
            this.sourcePathError.Visible = false;
            // 
            // targetPathError
            // 
            this.targetPathError.AutoSize = true;
            this.targetPathError.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.targetPathError.ForeColor = System.Drawing.Color.Red;
            this.targetPathError.Location = new System.Drawing.Point(431, 62);
            this.targetPathError.Name = "targetPathError";
            this.targetPathError.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.targetPathError.Size = new System.Drawing.Size(142, 20);
            this.targetPathError.TabIndex = 2;
            this.targetPathError.Text = "This path does not exist!";
            this.targetPathError.Visible = false;
            // 
            // CompareControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(32)))), ((int)(((byte)(30)))));
            this.Controls.Add(this.checkBox);
            this.Controls.Add(this.browseTargetPath);
            this.Controls.Add(this.browseSourePath);
            this.Controls.Add(this.targetPathTextBox);
            this.Controls.Add(this.sourcePathTextBox);
            this.Controls.Add(this.targetPathLabel);
            this.Controls.Add(this.targetPathError);
            this.Controls.Add(this.sourcePathError);
            this.Controls.Add(this.sourcePathLabel);
            this.Name = "CompareControl";
            this.Size = new System.Drawing.Size(770, 91);
            this.Click += new System.EventHandler(this.CompareControl_Click);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label sourcePathLabel;
        private TextBox sourcePathTextBox;
        private TextBox targetPathTextBox;
        private Label targetPathLabel;
        private Button browseSourePath;
        private Button browseTargetPath;
        private CheckBox checkBox;
        private Label sourcePathError;
        private Label targetPathError;
    }
}
