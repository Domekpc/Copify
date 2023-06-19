using Copyfy.Model;
using Copyfy.View.Control;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Copyfy.View
{
    class AutomationWindow : Window
    {
        private FlowLayoutPanel comparePathContainer;

        private TextBox ipAddressTextBox;
        private TextBox userNameTextBox;
        private TextBox passwordTextBox;
        private MaskedTextBox timeOfCheckTextBox;
        private Label errorLabel;
        private Label ipLabel;
        private Label userNameLabel;
        private Label passwordLabel;
        private Label checkTimeLabel;
        private CustomButton OnOff;

        private System.Windows.Forms.Timer checkDateTimer;
        private Task taskToCheckTime;

        private const int YLOCATION = 500;
        private readonly string comparePath = "Automation\\ComparePaths.cpy";
        private readonly string nasData = "Automation\\NasData.cpy";
        private int hour { get; set; } = 60;
        private int minute { get; set; } = 60;

        private Storage<CompareControl> compareControls;
        public AutomationWindow(Panel mainPanel, bool sidebarExpand, params CustomButton[] buttons) : base(mainPanel, sidebarExpand, buttons)
        {
            checkDateTimer = new System.Windows.Forms.Timer();
            checkDateTimer.Tick += CheckDateTimer_Tick;

            compareControls = new Storage<CompareControl>();

            comparePathContainer = new FlowLayoutPanel();
            comparePathContainer.Size = new Size(800, 400);
            comparePathContainer.Left = (mainPanel.Width - comparePathContainer.Width) / 2;
            comparePathContainer.Top = ((mainPanel.Height - comparePathContainer.Height) / 2) - 100;
            comparePathContainer.BackColor = Color.FromArgb(221, 195, 165);
            comparePathContainer.AutoScroll = true;

            ipAddressTextBox = new TextBox();
            ipAddressTextBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            ipAddressTextBox.Location = new System.Drawing.Point(comparePathContainer.Location.X + 100, YLOCATION + 20);
            ipAddressTextBox.Size = new System.Drawing.Size(188, 23);
            ipAddressTextBox.TabIndex = 6;

            userNameTextBox = new TextBox();
            userNameTextBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            userNameTextBox.Location = new System.Drawing.Point(comparePathContainer.Location.X + 100, YLOCATION + 53);
            userNameTextBox.Size = new System.Drawing.Size(188, 23);
            userNameTextBox.TabIndex = 6;

            passwordTextBox = new TextBox();
            passwordTextBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            passwordTextBox.Location = new System.Drawing.Point(comparePathContainer.Location.X + 100, YLOCATION + 86);
            passwordTextBox.Size = new System.Drawing.Size(188, 23);
            passwordTextBox.TabIndex = 6;

            timeOfCheckTextBox = new MaskedTextBox();
            timeOfCheckTextBox.TextChanged += TimeOfCheckTextBox_TextChanged;
            timeOfCheckTextBox.Mask = "00:00";
            timeOfCheckTextBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            timeOfCheckTextBox.Size = new System.Drawing.Size(100, 23);
            timeOfCheckTextBox.Location = new Point((comparePathContainer.Location.X + comparePathContainer.Width) - timeOfCheckTextBox.Width, 600);
            timeOfCheckTextBox.TabIndex = 6;

            ipLabel = new Label();
            ipLabel.AutoSize = false;
            ipLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            ipLabel.Location = new System.Drawing.Point(comparePathContainer.Location.X, YLOCATION + 20);
            ipLabel.Size = new System.Drawing.Size(87, 21);
            ipLabel.TabIndex = 7;
            ipLabel.Text = "IP address";

            userNameLabel = new Label();
            userNameLabel.AutoSize = false;
            userNameLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            userNameLabel.Location = new System.Drawing.Point(comparePathContainer.Location.X, YLOCATION + 53);
            userNameLabel.Size = new System.Drawing.Size(87, 21);
            userNameLabel.TabIndex = 7;
            userNameLabel.Text = "Username";

            passwordLabel = new Label();
            passwordLabel.AutoSize = false;
            passwordLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            passwordLabel.Location = new System.Drawing.Point(comparePathContainer.Location.X, YLOCATION + 86);
            passwordLabel.Size = new System.Drawing.Size(82, 21);
            passwordLabel.TabIndex = 7;
            passwordLabel.Text = "Password";

            OnOff = new CustomButton("ON/OFF", Properties.Resources.icons8_shutdown_32, 12);
            OnOff.Location = new Point((comparePathContainer.Location.X + comparePathContainer.Width) - OnOff.Width, 520);
            OnOff.button.BackColor = Color.Red;
            OnOff.button.FlatAppearance.MouseDownBackColor = Color.Red;
            OnOff.button.FlatAppearance.MouseOverBackColor = Color.Red;

            errorLabel = new Label();
            errorLabel.AutoSize = true;
            errorLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            errorLabel.ForeColor = Color.Red;
            errorLabel.Location = new Point(OnOff.Location.X, 640);
            errorLabel.TabIndex = 7;
            errorLabel.Text = "Incorrect type!";

            checkTimeLabel = new Label();
            checkTimeLabel.AutoSize = false;
            checkTimeLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            checkTimeLabel.Size = new System.Drawing.Size(143, 21);
            checkTimeLabel.Location = new Point(OnOff.Location.X, 600);
            checkTimeLabel.TabIndex = 7;
            checkTimeLabel.Text = "Checking Time";

            InitEvents();
        }
        private void InitUI()
        {
            buttons[0].Location = new Point(mainPanel.Width - CUSTOMBUTTONWIDTH, mainPanel.Height - CUSTOMBUTTONHEIGHT);

            for (int i = 1; i < buttons.Length; i++)
            {
                buttons[i].Location = (sidebarExpand) ? new Point(mainPanel.Width - (((i + 1) * CUSTOMBUTTONWIDTH) + SIDEBARDIFF), mainPanel.Height - CUSTOMBUTTONHEIGHT) :
                                                        new Point(mainPanel.Width - ((i + 1) * CUSTOMBUTTONWIDTH), mainPanel.Height - CUSTOMBUTTONHEIGHT);
            }
        }
        private void InitEvents()
        {
            buttons[1].button.Click += Add_Click;
            buttons[2].button.Click += Delete_Click;
            buttons[3].button.Click += Save_Click;
            OnOff.button.Click += OnOff_Click;
        }
        private void Load()
        {
            compareControls.LoadFromFileSeparately(comparePath, path => { return new CompareControl(path[0], path[1]); });

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
                        timeOfCheckTextBox.Text = words[3];

                        hour = int.Parse(timeOfCheckTextBox.Text.Substring(0, timeOfCheckTextBox.Text.IndexOf(":")));
                        minute = int.Parse(timeOfCheckTextBox.Text.Substring(timeOfCheckTextBox.Text.IndexOf(":") + 1));
                    }
                }
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public override void ReLocate(Panel mainPanel)
        {
            comparePathContainer.Left = (mainPanel.Width - comparePathContainer.Width) / 2;
            comparePathContainer.Top = ((mainPanel.Height - comparePathContainer.Height) / 2) - 100;

            buttons[0].Location = new Point(mainPanel.Width - CUSTOMBUTTONWIDTH, mainPanel.Height - CUSTOMBUTTONHEIGHT);

            ipAddressTextBox.Location = new System.Drawing.Point(comparePathContainer.Location.X + 100, YLOCATION + 20);
            userNameTextBox.Location = new System.Drawing.Point(comparePathContainer.Location.X + 100, YLOCATION + 53);
            passwordTextBox.Location = new System.Drawing.Point(comparePathContainer.Location.X + 100, YLOCATION + 86);

            ipLabel.Location = new System.Drawing.Point(comparePathContainer.Location.X, YLOCATION + 20);
            userNameLabel.Location = new System.Drawing.Point(comparePathContainer.Location.X, YLOCATION + 53);
            passwordLabel.Location = new System.Drawing.Point(comparePathContainer.Location.X, YLOCATION + 86);

            OnOff.Location = new Point((comparePathContainer.Location.X + comparePathContainer.Width) - OnOff.Width, 520);
        }
        public void Show()
        {
            InitUI();
            Load();
            ReInitContainer();
            mainPanel.Controls.Add(comparePathContainer);
            mainPanel.Controls.AddRange(buttons);
            mainPanel.Controls.Add(ipAddressTextBox);
            mainPanel.Controls.Add(userNameTextBox);
            mainPanel.Controls.Add(passwordTextBox);
            mainPanel.Controls.Add(ipLabel);
            mainPanel.Controls.Add(userNameLabel);
            mainPanel.Controls.Add(passwordLabel);
            mainPanel.Controls.Add(OnOff);
            mainPanel.Controls.Add(timeOfCheckTextBox);
            mainPanel.Controls.Add(checkTimeLabel);
        }
        private void ReInitContainer()
        {
            comparePathContainer.Controls.Clear();
            comparePathContainer.Controls.AddRange(compareControls.ToArray());
        }
        private void Add_Click(object? sender, EventArgs e)
        {
            compareControls.Add(new CompareControl());

            ReInitContainer();
        }
        private void Delete_Click(object? sender, EventArgs e)
        {
            CompareControl result = compareControls.Find((CompareControl control) => { return control.isChecked; });

            try
            {
                if (result == null)
                {
                    throw new NullReferenceException();//if control is null the Remove method can remove "null", therefore thow a new exception
                }

                foreach (CompareControl control in compareControls.items.ToList())
                {
                    if (control.isChecked)
                    {
                        compareControls.Remove(control);
                    }
                }

                ReInitContainer();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("There is no selected path!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void Save_Click(object? sender, EventArgs e)
        {
            using (StreamWriter sw = new StreamWriter(comparePath))
            {
                foreach (var control in compareControls.items)
                {
                    if (control.sourcePath != "" && control.targetPath != "")
                    {
                        sw.Write(control.sourcePath);
                        sw.Write(";");
                        sw.Write(control.targetPath);
                        sw.WriteLine();
                    }
                }
            }
            using (StreamWriter sw = new StreamWriter(nasData))
            {
                sw.Write(ipAddressTextBox.Text);
                sw.Write(";");
                sw.Write(userNameTextBox.Text);
                sw.Write(";");
                sw.Write(passwordTextBox.Text);
                sw.Write(";");
                sw.Write(timeOfCheckTextBox.Text);
            }

            MessageBox.Show("Saved!", "Info" ,MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void OnOff_Click(object? sender, EventArgs e)
        {
            if (OnOff.button.BackColor == Color.Red)
            {
                try
                {
                    if (hour == 60 || minute == 60)
                    {
                        throw new NullReferenceException("Can not turn on, because time is in incorrect fromat!");
                    }

                    OnOff.button.BackColor = Color.Green;
                    OnOff.button.FlatAppearance.MouseDownBackColor = Color.Green;
                    OnOff.button.FlatAppearance.MouseOverBackColor = Color.Green;

                    //Task taskToCheckTime = Task.Run(() =>
                    //{
                    //    int minuteDiff = (60 - DateTime.Now.Minute) * 1000;
                    //    checkDateTimer.Interval = minuteDiff;

                    //    if (hour == DateTime.Now.Hour)
                    //    {
                    //        checkDateTimer.Interval = 1000;
                    //    }

                    //    checkDateTimer.Start();
                    //});
                    int minuteDiff = (60 - DateTime.Now.Minute) * 1000;
                    checkDateTimer.Interval = minuteDiff;

                    if (hour == DateTime.Now.Hour)
                    {
                        checkDateTimer.Interval = 1000;
                    }

                    checkDateTimer.Start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                if (checkDateTimer != null)
                {
                    checkDateTimer.Stop();
                }

                OnOff.button.BackColor = Color.Red;
                OnOff.button.FlatAppearance.MouseDownBackColor = Color.Red;
                OnOff.button.FlatAppearance.MouseOverBackColor = Color.Red;
            }
        }
        private void TimeOfCheckTextBox_TextChanged(object? sender, EventArgs e)
        {
            try
            {
                if (OnOff != null && OnOff.button.BackColor == Color.Green)
                {
                    OnOff_Click(sender, e);
                }

                if (int.Parse(timeOfCheckTextBox.Text.Substring(0, timeOfCheckTextBox.Text.IndexOf(":"))) > 23 ||
                    int.Parse(timeOfCheckTextBox.Text.Substring(timeOfCheckTextBox.Text.IndexOf(":") + 1)) > 59)
                {
                    mainPanel.Controls.Add(errorLabel);
                    hour = 60;
                    minute = 60;
                }
                else
                {
                    mainPanel.Controls.Remove(errorLabel);

                    hour = int.Parse(timeOfCheckTextBox.Text.Substring(0, timeOfCheckTextBox.Text.IndexOf(":")));
                    minute = int.Parse(timeOfCheckTextBox.Text.Substring(timeOfCheckTextBox.Text.IndexOf(":") + 1));

                    using (StreamWriter sw = new StreamWriter(nasData))
                    {
                        sw.Write(ipAddressTextBox.Text);
                        sw.Write(";");
                        sw.Write(userNameTextBox.Text);
                        sw.Write(";");
                        sw.Write(passwordTextBox.Text);
                        sw.Write(";");
                        sw.Write(timeOfCheckTextBox.Text);
                    }
                }
            }
            catch (Exception)
            {

            }
        }
        private void CheckDateTimer_Tick(object? sender, EventArgs e)
        {
            //MessageBox.Show((DateTime.Now.Hour == hour).ToString());
            if (DateTime.Now.Hour == hour)
            {
                
                checkDateTimer.Interval = 360000;// check / minute what the time is
                MessageBox.Show("bent");
                MessageBox.Show(checkDateTimer.Interval.ToString());
                if (DateTime.Now.Minute >= minute)
                {
                    MessageBox.Show("nagyon bent");
                    checkDateTimer.Interval = 86400000; // once if we got the correct time, then it is enough when check the time a dayly basis
                    Automation auto = new Automation();
                }
            }
            else
            {
                checkDateTimer.Interval = 3600000;// check / hour what the time is
            }
        }
    }
}
