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
using System.ComponentModel.DataAnnotations;

namespace BankProject.Views
{
    class RegisterView : View
    {
        private readonly TextBox userNameField;
        private readonly TextBox passwordField;
        private readonly TextBox emailField;
        private readonly TextBox scNumberField; // Social Security Number (personnummer)

        private readonly Label userLabel;
        private readonly Label passwordLabel;
        private readonly Label emailLabel;
        private readonly Label scNumberLabel;

        private readonly Button registerButton;

        User user;

        public RegisterView(User u)
        {
            user = u;

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

            
            scNumberField = new TextBox
            {
                Anchor = AnchorStyles.None,
                Size = new Size(450, 30),
                Text = "YYMMDDXXXX",
                UseSystemPasswordChar = false,
                Font = new Font(FontFamily.GenericSansSerif, 12.0F, FontStyle.Regular),
                BorderStyle = BorderStyle.FixedSingle,
                Enabled = true,
                Multiline = false,
                TabIndex = 3,
            };

            passwordLabel = new Label
            {
                Anchor = AnchorStyles.None,
                Text = "Password:",
                Size = new Size(85, 50),
                Font = new Font(FontFamily.GenericSansSerif, 12.0F, FontStyle.Regular),
                BorderStyle = BorderStyle.None,
                Visible = true,

            };

            userLabel = new Label
            {
                Anchor = AnchorStyles.None,
                Text = "First and last name (e.g John Doe):",
                Size = new Size(270, 50),
                Font = new Font(FontFamily.GenericSansSerif, 12.0F, FontStyle.Regular),
                BorderStyle = BorderStyle.None,
                Visible = true,

            };

            emailLabel = new Label
            {
                Anchor = AnchorStyles.None,
                Text = "E-mail (optional):",
                Size = new Size(150, 50),
                Font = new Font(FontFamily.GenericSansSerif, 12.0F, FontStyle.Regular),
                BorderStyle = BorderStyle.None,
                Visible = true,

            };

            scNumberLabel = new Label
            {
                Anchor = AnchorStyles.None,
                Text = "Social Number:",
                Size = new Size(150, 50),
                Font = new Font(FontFamily.GenericSansSerif, 12.0F, FontStyle.Regular),
                BorderStyle = BorderStyle.None,
                Visible = true,
            };

            registerButton = new Button
            {
                Anchor = AnchorStyles.None,
                Size = new Size(100, 30),
                Text = "Register",
                Enabled = true,
                TabIndex = 3,
            };

            controlz.Add(userNameField);
            controlz.Add(passwordField);
            controlz.Add(emailField);
            controlz.Add(scNumberField);

            controlz.Add(userLabel);
            controlz.Add(passwordLabel);
            controlz.Add(emailLabel);
            controlz.Add(scNumberLabel);

            controlz.Add(registerButton);

            // 20 between label x and field x, 40 between label x and field y
            emailLabel.Location = new Point(146, 200);
            emailField.Location = new Point(150, 220);

            userLabel.Location = new Point(146, 260);
            userNameField.Location = new Point(150, 280);

            scNumberLabel.Location = new Point(146, 320);
            scNumberField.Location = new Point(150, 340);

            passwordLabel.Location = new Point(146, 380);
            passwordField.Location = new Point(150, 400);

            registerButton.Location = new Point(150, 440);

            Controls.AddRange(controlz.ToArray());
            AddEventHandlers();
        }
        
        
        protected override void AddEventHandlers()
        {
            
            registerButton.Click += (x, y) => 
            {
                if(userNameField.Text == string.Empty || passwordField.Text == string.Empty) // if the fields are not correctly filled out, warn the user
                {
                    MessageBox.Show("Please fill out the required fields!");
                }
                else // If the fields are correctly filled out
                {
                    if(emailField.Text == string.Empty) // Use the User-constructor without mail if the mail field is empty
                    {
                        try
                        {
                            user = new User(
                                userNameField.Text.Substring(0,
                                userNameField.Text.IndexOf(" ")),
                                userNameField.Text.Substring(userNameField.Text.IndexOf(" ") + 1), scNumberField.Text);
                        }

                        catch
                        {
                            MessageBox.Show("An error occured, please use the following format: 'First name, Last name'");
                            return;
                        }

                        MessageBox.Show(user.FirstName + " " + user.LastName); // Debug
                        
                    }
                    else // if the mail field was filled, use the mail constructor
                    {
                        try
                        {
                            /*user = new User(
                                userNameField.Text.Substring(0,
                                userNameField.Text.IndexOf(" ")),
                                userNameField.Text.Substring(userNameField.Text.IndexOf(" ") + 1), emailField.Text);*/
                        }

                        catch
                        {
                            MessageBox.Show("An error occured, please use the following format: 'First name, Last name'");
                            return;
                        }

                        MessageBox.Show(user.FirstName + " " + user.LastName + "\n" + emailField.Text); // Debug
                    }
                }

                    
            };

            scNumberField.Click += (x, y) =>
            {
                if(scNumberField.Text == "YYMMDDXXXX")
                {
                    scNumberField.Text = "";
                }
                

            };

            scNumberField.LostFocus += (x, y) =>
            {
                if(scNumberField.Text == "")
                {
                    scNumberField.Text = "YYMMDDXXXX";
                }
            };

        }

    }
}
