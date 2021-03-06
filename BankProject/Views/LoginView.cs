﻿/*
 * Created by Babak Samimi. 2018.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using BankProject.Views;
using BankProject.UserStuff;
using System.Xml;
using System.Diagnostics;
using Common;

namespace BankProject
{
    class LoginView : View
    {

        private readonly TextBox ssnField;
        private readonly TextBox passwordField;

        private readonly Button logInButton;
        private readonly Button resetPasswordButton;
        private readonly Button registerButton;

        public LoginView(ref User user, ref Client client) : base(ref user, ref client)
        {
            if (!client.Running)
            {
                clientData.Connect("127.0.0.1", 6060); // when connecting from LoginView, we also create the unique session ID here
            }

            ssnField = new TextBox
            {
                Anchor = AnchorStyles.None,
                Size = new Size(450, 30),
                Text = "SSN (YYMMDDXXXX)",
                Font = new Font(FontFamily.GenericSansSerif, 12.0F, FontStyle.Regular),
                BorderStyle = BorderStyle.FixedSingle,
                Enabled = true,
                Multiline = false,
                TabIndex = 0,

            };

            passwordField = new TextBox
            {

                Anchor = AnchorStyles.None,  // This will make the control adjust to the position we set it to whenever the size of the Form changes
                Size = new Size(450, 30),
                Text = "Password",
                UseSystemPasswordChar = false,
                Font = new Font(FontFamily.GenericSansSerif, 12.0F, FontStyle.Regular),
                BorderStyle = BorderStyle.FixedSingle,
                Enabled = true,
                Multiline = false,
                TabIndex = 0,

            };

            logInButton = new Button
            {
                Anchor = AnchorStyles.None,
                Size = new Size(100, 30),
                Text = "Login",
                Enabled = true,
                TabIndex = 0,

            };

            resetPasswordButton = new Button
            {
                Anchor = AnchorStyles.None,
                Size = new Size(100, 30),
                Text = "Forgot Password",
                Enabled = true,
                TabIndex = 0,
            };

            registerButton = new Button
            {
                Anchor = AnchorStyles.None,
                Size = new Size(100, 30),
                Text = "Register",
                Enabled = true,
                TabIndex = 0,

            };

            controlz.Add(ssnField);
            controlz.Add(passwordField);
            controlz.Add(logInButton);
            controlz.Add(resetPasswordButton);
            controlz.Add(registerButton);

            ssnField.Location = new Point(150, 250);
            passwordField.Location = new Point(150, 300);
            logInButton.Location = new Point(150, 340);
            resetPasswordButton.Location = new Point(150, 380);
            registerButton.Location = new Point(150, 420);

            Controls.AddRange(controlz.ToArray());
            AddEventHandlers();
        }

        public void LogIn(Object sender, System.EventArgs e)
        {

        }

        private void OnFieldChange(Object sender, System.EventArgs e)
        {
            
        }
        
        protected override void AddEventHandlers()
        {

            // These next four eventhandlers will adjust the fields depending on the current input, just a gimmick
            ssnField.Click += (x, y) =>
            {
                if (ssnField.Text == "SSN (YYMMDDXXXX)")
                    ssnField.Text = "";

            };

            ssnField.LostFocus += (x, y) => 
            {
                if (ssnField.Text == "")
                    ssnField.Text = "SSN(YYMMDDXXXX)";
            };

            passwordField.GotFocus += (x, y) =>
            {
                if (passwordField.Text == "Password" && !passwordField.UseSystemPasswordChar)
                {
                    passwordField.UseSystemPasswordChar = true;
                    passwordField.Text = "";
                }
            };

            passwordField.LostFocus += (x, y) =>
            {
                if (passwordField.Text == "" && passwordField.UseSystemPasswordChar)
                {
                    passwordField.UseSystemPasswordChar = false;
                    passwordField.Text = "Password";
                }
            };

            // Takes you to the register page
            registerButton.Click += (x, y) =>
            {
                Hide();
                new RegisterView(ref userContext, ref clientData).Show();
            };

            logInButton.Click += (x, y) =>
            {
                byte[] data;

                if(ssnField.Text.Length == 10 && ssnField.Text.All(Char.IsDigit) && passwordField.Text.Length > 0)
                {
                    data = new byte[ssnField.Text.Length + passwordField.Text.Length + 1];
                    data[0] = 2; // Value 2 is used for Log in
                    // Copy the relevant parts of the array
                    Array.Copy(Encoding.UTF8.GetBytes(ssnField.Text), 0, data, 1, 10);
                    Array.Copy(Encoding.UTF8.GetBytes(passwordField.Text), 0, data, 11, passwordField.Text.Length);
                    clientData.SendData(data);

                    data = new byte[1024];
                    int messageLength = clientData.ReceiveData(ref data); // Response data

                    if (data[0] == 49) // First value being 49 means that the credentials sent to the server was correct
                    {
                        byte[] buffer = new byte[messageLength - 1];
                        Array.Copy(data, 1, buffer, 0, messageLength - 1);
                        userContext = User.CreateObjectFromXml(buffer); // Create a new User-object
                        Debug.WriteLine(userContext.FirstName + " " + userContext.LastName);

                        Hide();
                        new UserMenuView(ref userContext, ref clientData).Show();
                    }
                    else if(data[0] == 50)
                    {
                        MessageBox.Show("Wrong password.");
                    }
                    else if(data[0] == 51)
                    {
                        MessageBox.Show("That SSN doesn't exist in the database.");
                    }
                }
            };
        }
       


        protected override void InitializeComponent()
        {
            Load += new System.EventHandler(LoginView_Load);    
            base.InitializeComponent();

        }


        private void LoginView_Load(object sender, EventArgs e)
        {
            
        }
   
    }

}
