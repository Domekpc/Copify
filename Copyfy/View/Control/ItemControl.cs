using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Copyfy.View.Control
{
    // This class represents a custom UserControl for handling items with checkboxes.
    public partial class ItemControl : UserControl 
    {
        public delegate void SelectionChangedHandler(ItemControl sender, bool isChecked);
        // The SelectionChanged event is triggered when the checkbox's checked state changes.
        public event SelectionChangedHandler SelectionChanged;

        public bool isChecked { get { return checkBox1.Checked; } set { checkBox1.Checked = value; } }
        public string text { get { return label1.Text; } }

        public ItemControl(string name)
        {
            InitializeComponent();

            label1.Text = name;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox1.Checked = false;
                this.BackColor = Color.FromArgb(32, 30, 32);
            }
            else
            {
                checkBox1.Checked = true;
                this.BackColor = Color.FromArgb(38, 36, 38);   
            }
        }
        // This method handles the CheckedChanged event for the checkbox to invoke the SelectionChanged event.
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            SelectionChanged?.Invoke(this, checkBox1.Checked);
        }
    }
}
