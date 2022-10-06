using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Management;
using System.Security.Cryptography;
using Whis_Launcher;

namespace Dashboard
{
    public partial class Form1 : Form
    {

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

        private static extern IntPtr CreateRoundRectRgn
         (
               int nLeftRect,
               int nTopRect,
               int nRightRect,
               int nBottomRect,
               int nWidthEllipse,
               int nHeightEllipse

         );





        public Form1()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
            pnlNav.Height = btnDashbord.Height;
            pnlNav.Top = btnDashbord.Top;
            pnlNav.Left = btnDashbord.Left;
            //btnDashbord.BackColor = Color.FromArgb(46, 51, 73);
            lbltitle.Text = "DashBoard";
            this.panel3.Controls.Clear();
            Form2 form22 = new Form2() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            form22.FormBorderStyle = FormBorderStyle.None;
            this.panel3.Controls.Add(form22);
            form22.Show();
        }



        private void btnDashbord_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnDashbord.Height;
            pnlNav.Top = btnDashbord.Top;
            pnlNav.Left = btnDashbord.Left;
            btnDashbord.BackColor = Color.FromArgb(46, 51, 73);
            lbltitle.Text = "DashBoard";
            this.panel3.Controls.Clear();
            Form2 form22 = new Form2() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            form22.FormBorderStyle = FormBorderStyle.None;
            this.panel3.Controls.Add(form22);
            form22.Show();
        }

        private void btnAnalytics_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnAnalytics.Height;
            pnlNav.Top = btnAnalytics.Top;
            btnAnalytics.BackColor = Color.FromArgb(46, 51, 73);
            lbltitle.Text = "Phasmophobia Section";
            this.panel3.Controls.Clear();
            Form3 form33 = new Form3() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            form33.FormBorderStyle = FormBorderStyle.None;
            this.panel3.Controls.Add(form33);
            form33.Show();
        }

        private void btnCalender_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnCalender.Height;
            pnlNav.Top = btnCalender.Top;
            btnCalender.BackColor = Color.FromArgb(46, 51, 73);
            lbltitle.Text = "Crab Game Section";
            this.panel3.Controls.Clear();
            Form4 form44 = new Form4() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            form44.FormBorderStyle = FormBorderStyle.None;
            this.panel3.Controls.Add(form44);
            form44.Show();
        }

        private void btnContactUs_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnContactUs.Height;
            pnlNav.Top = btnContactUs.Top;
            btnContactUs.BackColor = Color.FromArgb(46, 51, 73);
            lbltitle.Text = "Among Us Section";
            this.panel3.Controls.Clear();
            Form5 form55 = new Form5() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            form55.FormBorderStyle = FormBorderStyle.None;
            this.panel3.Controls.Add(form55);
            form55.Show();
        }


        private void btnDashbord_Leave(object sender, EventArgs e)
        {
            btnDashbord.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void btnAnalytics_Leave(object sender, EventArgs e)
        {
            btnAnalytics.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void btnCalender_Leave(object sender, EventArgs e)
        {
            btnCalender.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void btnContactUs_Leave(object sender, EventArgs e)
        {
            btnContactUs.BackColor = Color.FromArgb(24, 30, 54);
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }





        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST)
                m.Result = (IntPtr)(HT_CAPTION);
        }

        private const int WM_NCHITTEST = 0x84;
        private const int HT_CLIENT = 0x1;
        private const int HT_CAPTION = 0x2;

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
