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
using System.Diagnostics;
using Common;
using System.IO;
using System.Xml;

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
        private Label balanceLabel;

        private Panel dynamicPanel;
        private readonly Button createSalaryAccount;
        private readonly Button createSavingsAccount;
        private readonly ComboBox accountBox;
        private readonly Label interestRateLabel;
        private readonly Button deleteAccount;

        private readonly Button depositButton;
        private readonly Button withdrawButton;
        private readonly TextBox depositBox;
        private readonly TextBox withdrawBox;

        private readonly Button sendMoney;
        private readonly TextBox amountBox;
        private readonly TextBox toWhoBox;

        private readonly ListView historyView;
        


        public UserMenuView(ref User user, ref Client client) : base(ref user, ref client)
        {
            historyView = new ListView
            {
                Size = new Size(500, 500),
                Anchor = AnchorStyles.None,
                GridLines = true,
                Visible = false,
                View = System.Windows.Forms.View.Details,
                
            };

            menuBar = new TableLayoutPanel
            {
                Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom,
                Size = new Size(250, Height),
                BackColor = Color.FromArgb(50, 50, 50),
                Dock = DockStyle.Top | DockStyle.Left,
                RowCount = 6,
                ColumnCount = 1,

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
                Text = "Balance: NaN",
                Size = new Size(280, 22),
                Font = new Font(FontFamily.GenericSansSerif, 14.0F, FontStyle.Regular),
                ForeColor = Color.Black,
                BorderStyle = BorderStyle.None,
                Visible = true,
            };

            dynamicPanel = new Panel
            {
                Anchor = AnchorStyles.None,
                Size = new Size(Width - menuBar.Width, Height - topBar.Height),
                BackColor = Color.White,

            };

            accountBox = new ComboBox()
            {
                Anchor = AnchorStyles.None,
                Size = new Size(200, 50),
                Text = "Accounts",
                Font = new Font(FontFamily.GenericSansSerif, 8.0F, FontStyle.Regular),
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Enabled = true,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Visible = true,

            };

            greetLabel = new Label
            {

                Anchor = AnchorStyles.None,
                Text = "Greetings, " + user.LastName,
                Size = new Size(200, 26),
                Font = new Font(FontFamily.GenericSansSerif, 14.0F, FontStyle.Regular),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.None,
                Visible = true

            };

            interestRateLabel = new Label
            {

                Anchor = AnchorStyles.None,
                Text = "Interest rate: ",
                Size = new Size(200, 26),
                Font = new Font(FontFamily.GenericSansSerif, 14.0F, FontStyle.Regular),
                ForeColor = Color.Black,
                BorderStyle = BorderStyle.None,
                Visible = true,

            };


            deleteAccount = new Button
            {
                Anchor = AnchorStyles.None,
                Size = new Size(150, 50),
                Text = "Delete selected account",
                Font = new Font(FontFamily.GenericSansSerif, 12.0F, FontStyle.Regular),
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Enabled = true,
                Visible = true,
            };

            createSalaryAccount = new Button
            {
                Anchor = AnchorStyles.None,
                Size = new Size(150, 50),
                Text = "Create a new salary account",
                Font = new Font(FontFamily.GenericSansSerif, 12.0F, FontStyle.Regular),
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Enabled = true,
                Visible = true,
            };

            createSavingsAccount = new Button
            {
                Anchor = AnchorStyles.None,
                Size = new Size(150, 50),
                Text = "Create a new savings account",
                Font = new Font(FontFamily.GenericSansSerif, 12.0F, FontStyle.Regular),
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Enabled = true,
                Visible = true,
            };

            depositBox = new TextBox
            {
                Anchor = AnchorStyles.None,
                Size = new Size(150, 30),
                Text = "",
                Font = new Font(FontFamily.GenericSansSerif, 12.0F, FontStyle.Regular),
                BorderStyle = BorderStyle.FixedSingle,
                Enabled = true,
                Visible = false,
            };

            withdrawBox = new TextBox
            {
                Anchor = AnchorStyles.None,
                Size = new Size(150, 30),
                Text = "",
                Font = new Font(FontFamily.GenericSansSerif, 12.0F, FontStyle.Regular),
                BorderStyle = BorderStyle.FixedSingle,
                Enabled = true,
                Visible = false,
            };

            withdrawButton = new Button
            {
                Anchor = AnchorStyles.None,
                Size = new Size(150, 50),
                Text = "Withdraw money",
                Font = new Font(FontFamily.GenericSansSerif, 12.0F, FontStyle.Regular),
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Enabled = true,
                Visible = false,
            };

            depositButton = new Button
            {
                Anchor = AnchorStyles.None,
                Size = new Size(150, 50),
                Text = "Deposit money",
                Font = new Font(FontFamily.GenericSansSerif, 12.0F, FontStyle.Regular),
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Enabled = true,
                Visible = false,
            };

            sendMoney = new Button
            {
                Anchor = AnchorStyles.None,
                Size = new Size(150, 50),
                Text = "Send money (with selected account)",
                Font = new Font(FontFamily.GenericSansSerif, 12.0F, FontStyle.Regular),
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Enabled = true,
                Visible = false,
            };

            toWhoBox = new TextBox
            {
                Anchor = AnchorStyles.None,
                Size = new Size(230, 30),
                Text = "Send to who? (SSN ID)",
                Font = new Font(FontFamily.GenericSansSerif, 12.0F, FontStyle.Regular),
                BorderStyle = BorderStyle.FixedSingle,
                Enabled = true,
                Visible = false,
            };

            amountBox = new TextBox
            {
                Anchor = AnchorStyles.None,
                Size = new Size(230, 30),
                Text = "How many dollars?",
                Font = new Font(FontFamily.GenericSansSerif, 12.0F, FontStyle.Regular),
                BorderStyle = BorderStyle.FixedSingle,
                Enabled = true,
                Visible = false,
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

            menuBar.Controls.Add(showAccounts);
            menuBar.Controls.Add(history);
            menuBar.Controls.Add(performTransaction);
            menuBar.Controls.Add(withdraw_deposit);
            menuBar.Controls.Add(viewProfile);
            menuBar.Controls.Add(logOut);
            menuBar.Controls.Add(greetLabel);

            controlz.Add(topBar);

            controlz.Add(dynamicPanel);
            dynamicPanel.Controls.Add(accountBox);
            dynamicPanel.Controls.Add(balanceLabel);
            dynamicPanel.Controls.Add(deleteAccount);
            dynamicPanel.Controls.Add(interestRateLabel);
            dynamicPanel.Controls.Add(depositButton);
            dynamicPanel.Controls.Add(depositBox);
            dynamicPanel.Controls.Add(withdrawBox);
            dynamicPanel.Controls.Add(withdrawButton);
            dynamicPanel.Controls.Add(createSalaryAccount);
            dynamicPanel.Controls.Add(createSavingsAccount);
            dynamicPanel.Controls.Add(sendMoney);
            dynamicPanel.Controls.Add(amountBox);
            dynamicPanel.Controls.Add(toWhoBox);
            dynamicPanel.Controls.Add(historyView);

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

            dynamicPanel.Location = new Point(menuBar.Width, topBar.Height);
            accountBox.Location = new Point(40, 5);
            balanceLabel.Location = new Point(40, 65);
            interestRateLabel.Location = new Point(40, 145);
            deleteAccount.Location = new Point(40, 195);
            createSalaryAccount.Location = new Point(40, 250);
            createSavingsAccount.Location = new Point(190, 250);
            withdrawButton.Location = new Point(40, 195);
            depositButton.Location = new Point(40, 250);
            withdrawBox.Location = new Point(60 + withdrawBox.Width, 195);
            depositBox.Location = new Point(60 + depositBox.Width, 250);
            sendMoney.Location = new Point(40, 180);
            amountBox.Location = new Point(40, 260);
            toWhoBox.Location = new Point(40, 340);
            historyView.Location = new Point(10, 10);

            AddEventHandlers();
            UpdateList();

            historyView.Columns.Add("From", 80);
            historyView.Columns.Add("To", 80);
            historyView.Columns.Add("Amount", 80);
            historyView.Columns.Add("Date", 100);

        }

        private void HideControls()
        {
            foreach (Control c in dynamicPanel.Controls)
            {
                c.Visible = false;
            }
        }

        private void UpdateList()
        {
            foreach (Account acc in userContext.Accounts) // Add all the accounts into the ComboBox
            {
                if (!accountBox.Items.Contains(acc))
                {
                    accountBox.Items.Add(acc);
                }

            }

        }


        private void UpdateTransactionLog()
        {
            for (int i = 0; i < historyView.Items.Count; ++i)
                historyView.Items.RemoveAt(i);

            foreach(Transaction t in userContext.TransactionHistory)
            {

                string[] items = new string[4];
                items[0] = t.From;
                items[1] = t.To;
                items[2] = t.Amount;
                items[3] = t.Date.ToString();

                historyView.Items.Add(new ListViewItem(items));
            }
        }

        protected override void AddEventHandlers()
        {
            history.Click += (x, y) =>
            {
                HideControls();

                historyView.Visible = true;
                UpdateTransactionLog();
            };

            sendMoney.Click += (x, y) =>
            {

                IAccount iAccount = (Account)accountBox.SelectedItem;
                if (Double.TryParse(amountBox.Text, out double value))
                {
                    try
                    {
                        iAccount.Withdraw(value);
                        balanceLabel.Text = iAccount.Balance.ToString();
                        userContext.TransactionHistory.Add(new Transaction(userContext.FirstName + userContext.LastName, toWhoBox.Text, value.ToString(), DateTime.Now));
                        toWhoBox.Text = "";
                        amountBox.Text = "";
                        MessageBox.Show("Sent!");
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Please provide a number");
                }
            };

            toWhoBox.Click += (x, y) =>
            {
                if(toWhoBox.Text == "Send to who? (SSN ID)")
                {
                    toWhoBox.Text = "";
                }
                else if (toWhoBox.Text == "")
                {
                    toWhoBox.Text = "Send to who? (SSN ID)";
                }
            };

            amountBox.Click += (x, y) =>
            {
                if (toWhoBox.Text == "How many dollars?")
                {
                    toWhoBox.Text = "";
                }
                else if (toWhoBox.Text == "")
                {
                    toWhoBox.Text = "How many dollars?";
                }
            };

            performTransaction.Click += (x, y) =>
            {
                HideControls();

                balanceLabel.Visible = true;
                accountBox.Visible = true;
                amountBox.Visible = true;
                sendMoney.Visible = true;
                toWhoBox.Visible = true;
            };

            // Content for Show account
            showAccounts.Click += (x, y) =>
            {
                HideControls();

                balanceLabel.Visible = true;
                accountBox.Visible = true;
                interestRateLabel.Visible = true;
                deleteAccount.Visible = true;
                createSavingsAccount.Visible = true;
                createSalaryAccount.Visible = true;

            };

            createSalaryAccount.Click += (x, y) =>
            {
                userContext.Accounts.Add(new SalaryAccount(0));
                UpdateList();
            };

            createSavingsAccount.Click += (x, y) =>
            {
                userContext.Accounts.Add(new SavingAccount(0));
                UpdateList();
            };

            withdraw_deposit.Click += (x, y) =>
            {
                HideControls();

                accountBox.Visible = true;
                balanceLabel.Visible = true;
                withdrawButton.Visible = true;
                withdrawBox.Visible = true;
                depositBox.Visible = true;
                depositButton.Visible = true;
            };

            depositButton.Click += (x, y) =>
            {
                IAccount iAccount = (Account)accountBox.SelectedItem;
                if (Double.TryParse(depositBox.Text, out double value))
                {
                    try
                    {
                        iAccount.Deposit(value);
                        balanceLabel.Text = iAccount.Balance.ToString();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Please provide a number");
                }
            };

            withdrawButton.Click += (x, y) =>
            {
                IAccount iAccount = (Account)accountBox.SelectedItem;
                if (Double.TryParse(withdrawBox.Text, out double value))
                {
                    try
                    {
                        iAccount.Withdraw(value);
                        balanceLabel.Text = iAccount.Balance.ToString();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                        return;
                    }

                }
                else
                {
                    MessageBox.Show("Please provide a number");
                }
            };

            deleteAccount.Click += (x, y) =>
            {
                userContext.Accounts.Remove((Account)accountBox.SelectedItem);
                accountBox.Items.Remove(accountBox.SelectedItem);
            };

            accountBox.SelectedIndexChanged += (x, y) =>
            {
                // If the selected item in the box is a type of SalaryAccount, then show the balance of that account
                if ((Account)accountBox.SelectedItem is SalaryAccount)
                {
                    IAccount iAccount = (Account)accountBox.SelectedItem;
                    balanceLabel.Text = iAccount.Balance.ToString();
                    interestRateLabel.Text = "Interest rate: " + SalaryAccount.InterestRate + "%";
                }
                else if ((Account)accountBox.SelectedItem is SavingAccount)
                {
                    IAccount iAccount = (Account)accountBox.SelectedItem;
                    balanceLabel.Text = iAccount.Balance.ToString();
                    interestRateLabel.Text = "Interest rate: " + SavingAccount.InterestRate + "%";
                }
            };

            // Log out
            logOut.Click += (x, y) =>
            {
                string xmlInfo = SaveNewData();
                byte[] xmlArray = new byte[xmlInfo.Length + 11];
                
                xmlArray[0] = 10;
                Array.Copy(Encoding.UTF8.GetBytes(userContext.SocialSecurityNumber), 0, xmlArray, 1, 10);
                Array.Copy(Encoding.UTF8.GetBytes(xmlInfo), 0, xmlArray, 11, xmlInfo.Length);

                clientData.SendData(xmlArray); // Send server the final data about the User before the user logs out

                Hide();
                userContext = null;
                new LoginView(ref userContext, ref clientData);

            };
        }

        // Save the data before logging out and send this to the server
        private string SaveNewData()
        {
            Task<string> task = Task.Run(() =>
            {
                using (StringWriter sw = new StringWriter())
                {
                    using (XmlWriter xmlWriter = XmlWriter.Create(sw))
                    {
                        xmlWriter.WriteStartDocument();
                        xmlWriter.WriteStartElement("User");
                        xmlWriter.WriteStartElement("Accounts");
                        xmlWriter.WriteStartElement("SalaryAccounts");

                        foreach (Account acc in userContext.Accounts) // Save each account
                        {
                            if (acc is SalaryAccount)
                            {
                                xmlWriter.WriteStartElement("SalaryAccount");
                                xmlWriter.WriteAttributeString("ID", acc.AccountID);
                                xmlWriter.WriteString(acc.Balance.ToString());
                                xmlWriter.WriteEndElement();
                            }
                        }
                        xmlWriter.WriteEndElement();

                        xmlWriter.WriteStartElement("SavingsAccounts");
                        foreach (Account acc in userContext.Accounts)
                        {
                            if (acc is SavingAccount)
                            {
                                xmlWriter.WriteStartElement("SavingsAccount");
                                xmlWriter.WriteAttributeString("ID", acc.AccountID);
                                xmlWriter.WriteString(acc.Balance.ToString());
                                xmlWriter.WriteEndElement();
                            }
                        }
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteEndElement();

                        // Save transaction logs
                        xmlWriter.WriteStartElement("Transactions");
                        foreach (Transaction t in userContext.TransactionHistory)
                        {
                            xmlWriter.WriteStartElement("Transaction");

                            xmlWriter.WriteAttributeString("Date", t.Date.ToString());
                            xmlWriter.WriteStartElement("From");
                            xmlWriter.WriteString(t.From);
                            xmlWriter.WriteEndElement();
                            xmlWriter.WriteStartElement("To");
                            xmlWriter.WriteString(t.To);
                            xmlWriter.WriteEndElement();
                            xmlWriter.WriteStartElement("Amount");
                            xmlWriter.WriteString(t.Amount);
                            xmlWriter.WriteEndElement();

                            xmlWriter.WriteEndElement();
                        }
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteEndElement();

                        xmlWriter.WriteEndDocument();
                        xmlWriter.Flush();
                        xmlWriter.Close();
                    }

                    Debug.WriteLine("UserMenu produced this:\n" + sw.ToString());
                    return sw.ToString();
                }
            });
            return task.Result;  
        }

    }
}
