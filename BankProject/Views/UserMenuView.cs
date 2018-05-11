using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using BankProject.UserStuff;

namespace BankProject.Views
{
    class UserMenuView : View
    {
        User user;

        private Panel menuBar;

        private Panel topBar;
        private Label balanceLabel;

        private Panel dynamicPanel;

        public UserMenuView()
        {
            
            menuBar = new Panel
            {
                Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom,
                Size = new Size(250, Height),
                BackColor = Color.FromArgb(50, 50, 50),


            };

            topBar = new Panel
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Size = new Size(Width, 50),
                BackColor = Color.FromArgb(104, 104, 104),
                
            };

            balanceLabel = new Label
            {
                Text = "Balance: $100",
                Size = new Size(280,22),
                Font = new Font(FontFamily.GenericSansSerif, 15.0F, FontStyle.Regular),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.None,
            };

            dynamicPanel = new Panel
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                Size = new Size( Width - menuBar.Width, Height - topBar.Height),
                BackColor = Color.White,

            };

            controlz.Add(menuBar);
            controlz.Add(topBar);
            topBar.Controls.Add(balanceLabel);
            controlz.Add(dynamicPanel);

            Controls.AddRange(controlz.ToArray());

            menuBar.Location = new Point(0, 0);
            topBar.Location = new Point(250, 0);
            balanceLabel.Location = new Point(0, (topBar.Height - balanceLabel.Height) / 2);
            dynamicPanel.Location = new Point(menuBar.Width, topBar.Height);
        
        }
    }
}
