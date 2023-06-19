using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copyfy.View
{
    public abstract class Window
    {
        public CustomButton[] buttons { get; private set; }
        public Panel mainPanel { get; private set; }
        public bool sidebarExpand { get; set; }

        protected const int CUSTOMBUTTONWIDTH = 243;
        protected const int CUSTOMBUTTONHEIGHT = 70;
        protected const int SIDEBARDIFF = 176;

        public Window(Panel mainPanel, bool sidebarExpand, params CustomButton[] buttons)
        {
            //buttons 0. index always should be the button, which is "locked" to to bottom right corner
            this.mainPanel = mainPanel;
            this.buttons = buttons;
            this.sidebarExpand = sidebarExpand;
        }
        public abstract void ReLocate(Panel mainPanel);
    }
}
