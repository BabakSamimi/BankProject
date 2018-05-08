using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace BankProject
{
    class LoginView : View
    {

        private TextBox userNameField;
        private TextBox passwordField;
        private Button logInButton;
        private Button resetPassword;
        private Label title;

        public TextBox UserNameField { get { return userNameField; } }

        
        public LoginView()
        {
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

            resetPassword = new Button
            {
                Anchor = AnchorStyles.None,
                Size = new Size(100, 30),
                Text = "Forgot Password",
                Enabled = true,
                TabIndex = 0,
            };


            controlz.Add(userNameField);
            controlz.Add(passwordField);
            controlz.Add(logInButton);
            controlz.Add(resetPassword);

            userNameField.Location = new Point(150, 250);
            passwordField.Location = new Point(150, 300);
            logInButton.Location = new Point(150, 340);
            resetPassword.Location = new Point(150, 380);

            Controls.AddRange(controlz.ToArray());
            AddEventHandlers();
        }


        public void LogIn(Object sender, System.EventArgs e)
        {

        }

        private void OnFieldChange(Object sender, System.EventArgs e)
        {
            
        }
        
        private void AddEventHandlers()
        {
            userNameField.Click += (x, y) =>
            {
                if (userNameField.Text == "E-mail")
                    UserNameField.Text = "";

            };

            UserNameField.LostFocus += (x, y) => 
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
        }
       


        protected override void InitializeComponent()
        {
            Load += new System.EventHandler(LoginView_Load);    
            base.InitializeComponent();

        }


        private void LoginView_Load(object sender, EventArgs e)
        {
            
        }

        /* private void InitializeComponent()
         {
             this.SuspendLayout();
             // 
             // LoginView
             // 
             //this.ClientSize = new System.Drawing.Size(Width, Height);
             BackColor = Color.FromArgb(115, 172, 150); 
             this.Name = "LoginView";
             this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
             this.ClientSize = new System.Drawing.Size(750, 660);
             Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - Width) / 2,
     (Screen.PrimaryScreen.WorkingArea.Height - Height) / 2);

             this.Load += new System.EventHandler(this.LoginView_Load);
             this.ResumeLayout(false);

         }*/


        /*private void InitializeComponent()
        {
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(267, 180);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(100, 96);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // LoginView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(644, 711);
            this.Controls.Add(this.richTextBox1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "LoginView";
            this.ResumeLayout(false);

        }*/
    }

}
