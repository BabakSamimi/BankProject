/*
 * Created by Babak Samimi. 2018.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using BankProject.UserStuff;
using Common;

namespace BankProject.Views
{
    class UserMenuView : View
    {

        private readonly TableLayoutPanel menuBar; // We're using a TableLayoutPanel so the "Forms engine" can readjust our controls inside the panel
        private readonly Label greetLabel;
        private readonly Button showAccounts;
        private readonly Button performTransaction;
        private readonly Button history;
        private readonly Button withdraw_deposit;
        private readonly Button viewProfile;
        private readonly Button logOut;


        private Panel topBar;
        private  Label balanceLabel;

        private Panel dynamicPanel;
        private readonly Button createAccount;


        public UserMenuView(ref User user, ref Client client) : base(ref user, ref client)
        {

            menuBar = new TableLayoutPanel
            {
                Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom,
                Size = new Size(250, Height),
                BackColor = Color.FromArgb(50, 50, 50),
                Dock = DockStyle.Top | DockStyle.Left,
                RowCount = 6,
                ColumnCount = 1,

            };

           
            greetLabel = new Label
            {

                Anchor = AnchorStyles.None,
                Text = "Greetings, " + user.FirstName + ".",
                Size = new Size(200, 26),
                Font = new Font(FontFamily.GenericSansSerif, 14.0F, FontStyle.Regular),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.None,
                Visible = true

            };

            showAccounts = new Button
            {
                Anchor = AnchorStyles.None,
                Size = new Size(220, 50),
                Text = "Show accounts",
                Font = new Font(FontFamily.GenericSansSerif, 14.0F, FontStyle.Regular),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Enabled = true,

            };

            history = new Button
            {
                Anchor = AnchorStyles.None,
                Size = new Size(220, 50),
                Text = "History",
                Font = new Font(FontFamily.GenericSansSerif, 14.0F, FontStyle.Regular),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Enabled = true,
            };

            performTransaction = new Button
            {
                Anchor = AnchorStyles.None,
                Size = new Size(220, 50),
                Text = "Perform transaction",
                Font = new Font(FontFamily.GenericSansSerif, 14.0F, FontStyle.Regular),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Enabled = true,

            };

            withdraw_deposit = new Button
            {
                Anchor = AnchorStyles.None,
                Size = new Size(220, 50),
                Text = "Withdraw/Deposit",
                Font = new Font(FontFamily.GenericSansSerif, 14.0F, FontStyle.Regular),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Enabled = true,

            };

            viewProfile = new Button
            {
                Anchor = AnchorStyles.None,
                Size = new Size(220, 50),
                Text = "View profile",
                Font = new Font(FontFamily.GenericSansSerif, 14.0F, FontStyle.Regular),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Enabled = true,

            };

            logOut = new Button
            {
                Anchor = AnchorStyles.None,
                Size = new Size(220, 50),
                Text = "Log out",
                Font = new Font(FontFamily.GenericSansSerif, 14.0F, FontStyle.Regular),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Enabled = true,
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
                Font = new Font(FontFamily.GenericSansSerif, 14.0F, FontStyle.Regular),
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
            // Since we have 7 controls in this panel we want 14.28571% margin between each control (1 divided by 7)
            menuBar.RowStyles.Add(new RowStyle(SizeType.Percent, 14.28571F));
            menuBar.RowStyles.Add(new RowStyle(SizeType.Percent, 14.28571F));
            menuBar.RowStyles.Add(new RowStyle(SizeType.Percent, 14.28571F));
            menuBar.RowStyles.Add(new RowStyle(SizeType.Percent, 14.28571F));
            menuBar.RowStyles.Add(new RowStyle(SizeType.Percent, 14.28571F));
            menuBar.RowStyles.Add(new RowStyle(SizeType.Percent, 14.28571F));
            menuBar.RowStyles.Add(new RowStyle(SizeType.Percent, 14.28571F));

            menuBar.Controls.Add(greetLabel);
            menuBar.Controls.Add(showAccounts);
            menuBar.Controls.Add(history);
            menuBar.Controls.Add(performTransaction);
            menuBar.Controls.Add(withdraw_deposit);
            menuBar.Controls.Add(viewProfile);
            menuBar.Controls.Add(logOut);

            controlz.Add(topBar);
            topBar.Controls.Add(balanceLabel);
            controlz.Add(dynamicPanel);

            Controls.AddRange(controlz.ToArray());

            // Menubar location and menuBar-control locations relative to menuBar
            menuBar.Location = new Point(0, 0);
            greetLabel.Location = new Point(0, 0);
            showAccounts.Location = new Point(0, 0);
            performTransaction.Location = new Point(0, 0);
            withdraw_deposit.Location = new Point(0, 0);
            viewProfile.Location = new Point(0, 0);
            logOut.Location = new Point(0, 0);

            // topBar location and topBar-control locations relative to topBar
            topBar.Location = new Point(250, 0);
            balanceLabel.Location = new Point(0, (topBar.Height - balanceLabel.Height) / 2);
            dynamicPanel.Location = new Point(menuBar.Width, topBar.Height);
        
        }

        protected override void AddEventHandlers()
        {
            // Content for Show account
            showAccounts.Click += (x, y) =>
            {

            };
        }
    }
}
