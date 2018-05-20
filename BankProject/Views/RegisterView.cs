/*
 * Created by Babak Samimi. 2018.
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using BankProject.UserStuff;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Common;

namespace BankProject.Views
{
    class RegisterView : View
    {
        private readonly TextBox userNameField;
        private readonly TextBox passwordField;
        private readonly TextBox emailField;
        private readonly TextBox SSNumberField; // Social Security Number (personnummer)

        private readonly Label userLabel;
        private readonly Label passwordLabel;
        private readonly Label emailLabel;
        private readonly Label SSNLabel;
        

        private readonly Button registerButton;
        private readonly Button goBackButton;

        public RegisterView(ref User user, ref Client client) : base(ref user, ref client)
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
                TabIndex = 1,

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
                TabIndex = 3,

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
                TabIndex = 0,

            };

            
            SSNumberField = new TextBox
            {
                Anchor = AnchorStyles.None,
                Size = new Size(450, 30),
                Text = "YYMMDDXXXX",
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

            SSNLabel = new Label
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
                TabIndex = 4,
            };

            goBackButton = new Button
            {
                Anchor = AnchorStyles.None,
                Size = new Size(100, 30),
                Text = "Go back",
                Enabled = true,
                TabIndex = 5,
            };

            controlz.Add(userNameField);
            controlz.Add(passwordField);
            controlz.Add(emailField);
            controlz.Add(SSNumberField);

            controlz.Add(userLabel);
            controlz.Add(passwordLabel);
            controlz.Add(emailLabel);
            controlz.Add(SSNLabel);

            controlz.Add(registerButton);
            controlz.Add(goBackButton);

            // 20 between label x and field x, 40 between label x and field y
            emailLabel.Location = new Point(146, 200);
            emailField.Location = new Point(150, 220);

            userLabel.Location = new Point(146, 260);
            userNameField.Location = new Point(150, 280);

            SSNLabel.Location = new Point(146, 320);
            SSNumberField.Location = new Point(150, 340);

            passwordLabel.Location = new Point(146, 380);
            passwordField.Location = new Point(150, 400);

            registerButton.Location = new Point(150, 440);
            goBackButton.Location = new Point(150, 480);

            Controls.AddRange(controlz.ToArray());
            AddEventHandlers();
        }
        
        private bool SSNIsUnique(ref byte[] buffer)
        {
            buffer = new byte[SSNumberField.Text.Length + 1]; 
            byte[] temp = new byte[SSNumberField.Text.Length];
            temp = Encoding.UTF8.GetBytes(SSNumberField.Text);

            for(int i = 1; i < temp.Length; ++i)
            {
                buffer[i] = temp[i - 1];
            }

            buffer[0] = 255;

            clientData.SendData(buffer);

            buffer = new byte[1];

            clientData.ReceiveData(ref buffer);

            if (buffer[0] == 254)
            {
                return false;
            }

            return true;

        }

        protected override void AddEventHandlers()
        {
            User temp = null;
            byte[] buffer;

            // Go back to Login page
            goBackButton.Click += (x, y) =>
            {
                Hide();
                new LoginView(ref userContext, ref clientData).Show();
            };

            registerButton.Click += (x, y) => 
            {

                if (userNameField.Text == string.Empty || passwordField.Text == string.Empty || (SSNumberField.Text.Length < 10)) // if the fields are not correctly filled out, warn the user
                {
                    MessageBox.Show("Please fill out the required fields correctly!");
                }
                else // If the fields are correctly filled out
                {
                    buffer = null;

                    if (SSNIsUnique(ref buffer))
                    {
                        if (emailField.Text == string.Empty) // Use the User-constructor without mail if the mail field is empty
                        {
                            try
                            {
                                temp = new User(
                                    userNameField.Text.Substring(0, userNameField.Text.IndexOf(" ")), // Gets the first name 
                                    userNameField.Text.Substring(userNameField.Text.IndexOf(" ") + 1), // Gets the last name
                                    SSNumberField.Text, passwordField.Text); // Gets Social Security Number
                            }

                            catch
                            {
                                MessageBox.Show("An error occured, please use the following format: 'First name, Last name'");
                                return;
                            }

                            MessageBox.Show(temp.FirstName + " " + temp.LastName); // Debug
                            userContext = temp;

                        }
                        else // if the mail field was filled, use the mail constructor
                        {
                            try
                            {
                                temp = new User(
                                    userNameField.Text.Substring(0, userNameField.Text.IndexOf(" ")), // Gets the first name
                                    userNameField.Text.Substring(userNameField.Text.IndexOf(" ") + 1), // Gets the last name
                                    emailField.Text, // Gets email
                                    SSNumberField.Text, passwordField.Text); // Gets Social Security Number
                            }

                            catch
                            {
                                MessageBox.Show("An error occured, please use the following format: 'First name, Last name'");
                                return;
                            }

                            MessageBox.Show(temp.FirstName + " " + temp.LastName + "\n" + emailField.Text); // Debug
                            userContext = temp;
                        }

                        string xmlInfo = userContext.CreateXmlObject(); // Create an xml object and send it to server
                        byte[] stringBuffer = Encoding.UTF8.GetBytes(xmlInfo);
                        buffer = new byte[stringBuffer.Length + 1];
                        stringBuffer.CopyTo(buffer, 1);
                        buffer[0] = 1;

                        clientData.SendData(buffer);

                        Hide();
                        new LoginView(ref userContext, ref clientData).Show();
                    }
                    else
                    {
                        MessageBox.Show("That Social Security Number is already taken.");
                        return;
                    }
                }       
            };

            // Lambdas for placeholders
            SSNumberField.Click += (x, y) =>
            {
                if(SSNumberField.Text == "YYMMDDXXXX")
                {
                    SSNumberField.Text = "";
                }
                

            };

            SSNumberField.LostFocus += (x, y) =>
            {
                if(SSNumberField.Text == "")
                {
                    SSNumberField.Text = "YYMMDDXXXX";
                }
            };

        }

    }
}
