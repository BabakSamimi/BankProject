using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace BankProject
{
    public class View : Form
    {
        protected List<Control> controlz = new List<Control>();

        protected View()
        {
            InitializeComponent();
        }

        ~View()
        {
            controlz.RemoveAll((x) => { return x is Control; });
        }


        protected virtual void InitializeComponent()
        {
            SuspendLayout();
            //FormBorderStyle = FormBorderStyle.None;
            ClientSize = new Size(750, 660);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            //BackColor = Color.FromArgb(115, 172, 150);
            BackColor = Color.FromArgb(255,255,255);
            CenterToScreen();

            ResumeLayout(false);

        }



    }
}
