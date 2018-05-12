using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using BankProject.Views;
using BankProject.UserStuff;

namespace BankProject
{


    class View : Form
    {
        protected List<Control> controlz = new List<Control>();

        protected View()
        {
            Text = "XDDDDDDDDDD";
            InitializeComponent();
        }

        protected View(User user)
        {
            InitializeComponent();
        }

        ~View()
        {
            controlz.RemoveAll((x) => { return x is Control; }); // Redundant?
        }


        protected virtual void InitializeComponent()
        {
            SuspendLayout();

            ClientSize = new Size(850, 650);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = Color.FromArgb(255,255,255);

            // Prevent rezising of the window
            //FormBorderStyle = FormBorderStyle.FixedSingle;
            //MaximizeBox = false;
            //MinimizeBox = false;

            CenterToScreen();
            

            ResumeLayout(false);

        }

        protected virtual void AddEventHandlers() { }

    }

}
