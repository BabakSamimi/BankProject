using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace BankProject.Views
{
    class RegisterView : View
    {
        private readonly TextBox userNameField;
        private readonly TextBox passwordField;
        private readonly TextBox emailField;

        private readonly Button registerButton;

        private readonly Label userLabel;
        private readonly Label passwordLabel;
        private readonly Label emailLabel;
    

        public RegisterView()
        {
            userNameField = new TextBox
            {
                Anchor = AnchorStyles.None,
                Size = new Size(450, 30),
                Text = "",
                Font = new Font(FontFamily.GenericSansSerif, 12.0F, FontStyle.Regular),
                BorderStyle = BorderStyle.FixedSingle,
                Enabled = true,
                Multiline = false,
                TabIndex = 0,

            };

            passwordField = new TextBox
            {

                Anchor = AnchorStyles.None,
                Size = new Size(450, 30),
                Text = "",
                UseSystemPasswordChar = true,
                Font = new Font(FontFamily.GenericSansSerif, 12.0F, FontStyle.Regular),
                BorderStyle = BorderStyle.FixedSingle,
                Enabled = true,
                Multiline = false,
                TabIndex = 1,

            };

            emailField = new TextBox
            {
                Anchor = AnchorStyles.None,  
                Size = new Size(450, 30),
                Text = "",
                UseSystemPasswordChar = false,
                Font = new Font(FontFamily.GenericSansSerif, 12.0F, FontStyle.Regular),
                BorderStyle = BorderStyle.FixedSingle,
                Enabled = true,
                Multiline = false,
                TabIndex = 2,

            };

            passwordLabel = new Label
            {
                Anchor = AnchorStyles.None,
                Text = "Password:",
                Size = new Size(85, 50),
                Font = new Font(FontFamily.GenericSansSerif, 12.0F, FontStyle.Regular),
                BorderStyle = BorderStyle.None,
                Enabled = true,
                Visible = true

            };

            userLabel = new Label
            {
                Anchor = AnchorStyles.None,
                Text = "First and last name (e.g John Doe):",
                Size = new Size(270, 50),
                Font = new Font(FontFamily.GenericSansSerif, 12.0F, FontStyle.Regular),
                BorderStyle = BorderStyle.None,
                Enabled = true,
                Visible = true

            };

            emailLabel = new Label
            {
                Anchor = AnchorStyles.None,
                Text = "E-mail (optional):",
                Size = new Size(150, 50),
                Font = new Font(FontFamily.GenericSansSerif, 12.0F, FontStyle.Regular),
                BorderStyle = BorderStyle.None,
                Enabled = true,
                Visible = true

            };

            registerButton = new Button
            {
                Anchor = AnchorStyles.None,
                Size = new Size(100, 30),
                Text = "Register",
                Enabled = true,
                TabIndex = 3,
            };
;

            controlz.Add(userNameField);
            controlz.Add(passwordField);
            controlz.Add(emailField);
            controlz.Add(userLabel);
            controlz.Add(passwordLabel);
            controlz.Add(emailLabel);
            controlz.Add(registerButton);

            // 20 between label x and field x, 40 between label x and field y
            emailLabel.Location = new Point(146, 200);
            emailField.Location = new Point(150, 220);
            userLabel.Location = new Point(146, 260);
            userNameField.Location = new Point(150, 280);
            passwordLabel.Location = new Point(146, 320);
            passwordField.Location = new Point(150, 340);
            registerButton.Location = new Point(150, 380);


            Controls.AddRange(controlz.ToArray());
            AddEventHandlers();
        }

        protected override void AddEventHandlers()
        {
            registerButton.Click += (x, y) => 
            {
                if(userNameField.Text == string.Empty && passwordField.Text == string.Empty)
                {
                    MessageBox.Show("Please fill out the required fields!");
                }
                else
                {

                }
            };
        }

    }
}
