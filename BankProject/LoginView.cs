using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace BankProject
{
    class LoginView : Form
    {

        
        private RichTextBox userNameField;
        private RichTextBox passwordField;

        private Button logInButton;
        private Button resetPassword;

        public RichTextBox UserNameField { get; set; }
        public RichTextBox PasswordField { get; set; }
        public Button LogInButton { get; set; }
        public Button ResetPassword { get; set; }

        public LoginView()
        {
            InitializeComponent();

            


        }

        public void LogIn(Object sender, System.EventArgs e)
        {

        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // LoginView
            // 
            this.ClientSize = new System.Drawing.Size(512, 401);
            BackColor = Color.FromArgb(115, 172, 150); 
            this.Name = "LoginView";
            this.Load += new System.EventHandler(this.LoginView_Load);
            this.ResumeLayout(false);

        }

        private void LoginView_Load(object sender, EventArgs e)
        {

        }
    }

}
