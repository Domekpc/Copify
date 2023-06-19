using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Copyfy;

namespace Copyfy.View
{
    // This class represents a custom button user control.
    public partial class CustomButton : UserControl
    {
        public Button button { get { return button1;} }

        // This constructor is used to create a single button with an optional click event handler.
        public CustomButton(string text, Image icon, int leftPadding, EventHandler Click = null)
        {
            InitializeComponent();
            
            button1.Text = "    " + text;
            button1.Image = icon;

            if(Click != null) 
            {
                button1.Click += Click;
            }

            button1.Padding = new System.Windows.Forms.Padding(leftPadding, 0, 0, 0);
        }

        // This constructor is used to create a panel of buttons from the provided CustomButton instances.
        public CustomButton(params CustomButton[] buttons)
        {
            InitializeComponent();

            this.Controls.Clear();
            int YDIFF = 0;
            
            this.MaximumSize = new Size(243, buttons.Length * 71); // Set the maximum size of the UserControl based on the number of buttons.

            foreach (CustomButton button in buttons)// Add each button to the UserControl and position it correctly.
            {
                button.Left = 0;
                button.Top = YDIFF;
                YDIFF += 71;
                this.Controls.Add(button);
            }
        }
    }
}
