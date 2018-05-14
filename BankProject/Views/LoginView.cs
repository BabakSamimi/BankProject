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
using BankProject.Views;
using BankProject.UserStuff;
using Common;

namespace BankProject
{
    class LoginView : View
    {

        private readonly TextBox userNameField;
        private readonly TextBox passwordField;

        private readonly Button logInButton;
        private readonly Button resetPasswordButton;
        private readonly Button registerButton;

        private readonly Label title;

        public LoginView(ref User user, ref Client client) : base(ref user, ref client)
        { 
            
            clientData.Connect("127.0.0.1", 6060); // when connecting from LoginView, we also create the unique ID here

            userNameField = new TextBox
            {
                Anchor = AnchorStyles.None,
                Size = new Size(450, 30),
                Text = "E-mail",
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


            controlz.Add(userNameField);
            controlz.Add(passwordField);
            controlz.Add(logInButton);
            controlz.Add(resetPasswordButton);
            controlz.Add(registerButton);

            userNameField.Location = new Point(150, 250);
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

            // These next four eventhandlers will adjust depending on the current input, just a gimmick
            userNameField.Click += (x, y) =>
            {
                if (userNameField.Text == "E-mail")
                    userNameField.Text = "";

            };

            userNameField.LostFocus += (x, y) => 
            {
                if (userNameField.Text == "")
                    userNameField.Text = "E-mail";
            };

            passwordField.Click += (x, y) =>
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

            registerButton.Click += (x, y) =>
            {
                Hide();
                new RegisterView(ref userContext, ref clientData).Show();
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
