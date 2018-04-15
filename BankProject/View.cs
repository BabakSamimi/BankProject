using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace BankProject
{
    public abstract class View : Form
    {
        int height, width;
        Color background;


        protected View()
        {
            height = 550;
            width = 660;
            background = Color.FromArgb(115, 172, 150);
        }



    }
}
