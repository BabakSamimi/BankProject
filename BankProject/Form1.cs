using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankProject
{
    public partial class Form1 : Form
    {

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        Button test = new Button
        {
            Text = "Button",
            Name = "Button1",
            Size = new Size(100, 40),
        };


        public Form1()
        {
            InitializeComponent();

            BackColor = Color.FromArgb(115, 172, 150);
            Size = new Size(550, 660);

            
            Controls.Add(test);
           
            
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
