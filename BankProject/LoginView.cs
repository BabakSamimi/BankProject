using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankProject
{
    class LoginView : Form
    {
        RichTextBox userNameField;
        RichTextBox passwordField;

        Button logInButton;

        public RichTextBox UserNameField { get; set; }
        public RichTextBox PasswordField { get; set; }

        public LoginView()
        {
            
        }

        public void LogIn(Object sender, System.EventArgs e)
        {

        }

    }
}
