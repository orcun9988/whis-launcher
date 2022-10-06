using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace Dashboard
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }


        private string ReleasesAPI = "https://whis99.com/amongus/Launcherupdate.txt";


        private string getunityhwid()
        {
            string ret = string.Empty;

            string concatStr = string.Empty;
            try
            {
                using ManagementObjectSearcher searcherBb = new ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard");
                foreach (var obj in searcherBb.Get())
                {
                    concatStr += (string)obj.Properties["SerialNumber"].Value ?? string.Empty;
                }

                using ManagementObjectSearcher searcherBios = new ManagementObjectSearcher("SELECT * FROM Win32_BIOS");
                foreach (var obj in searcherBios.Get())
                {
                    concatStr += (string)obj.Properties["SerialNumber"].Value ?? string.Empty;
                }

                using ManagementObjectSearcher searcherOs = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
                foreach (var obj in searcherOs.Get())
                {
                    concatStr += (string)obj.Properties["SerialNumber"].Value ?? string.Empty;
                }

                using var sha1 = SHA1.Create();
                ret = string.Join("", sha1.ComputeHash(Encoding.UTF8.GetBytes(concatStr)).Select(b => b.ToString("x2")));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return ret;
        }


        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            System.Windows.Forms.Clipboard.SetText(label4.Text);
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Clipboard.SetText(label4.Text);
            MessageBox.Show("your hwid adress: " + label4.Text + "\nCopied to Clipboard PRESS CTRL+V to paste");
        }

        private void pictureBox1_Click_2(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://discord.gg/DGzfQu3M6j");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://whis99.com");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                timer1.Stop();
                using var client = new WebClient();
                string json = client.DownloadString(ReleasesAPI);
                JArray jArr = JArray.Parse(json);
                JArray jArr2 = JArray.Parse(json);
                string stringVersion = jArr[0].ToObject<JObject>().GetValue("tag_name").ToObject<string>();
                string stringVersion2 = jArr2[0].ToObject<JObject>().GetValue("tag_news").ToObject<string>();
                Version git = new(stringVersion);
                label9.Text = stringVersion2;
                label2.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                label4.Text = getunityhwid();


                if (Convert.ToDecimal(stringVersion) > Convert.ToDecimal(label2.Text))
                {
                    MessageBox.Show("Current Launcher Version: " + Assembly.GetExecutingAssembly().GetName().Version.ToString()  + "\nServer Launcher Version: " + stringVersion + "\nThe Launcher Needs Update!");
                    System.Diagnostics.Process.Start("https://whis99.com/launcher/WhisLauncher.zip");
                    Application.Exit();
                }



            }
            catch { }
            
        }



        private void Form2_Load(object sender, EventArgs e)
        {
            timer1.Start();
            using var client = new WebClient();
            string json = client.DownloadString(ReleasesAPI);
            JArray jArr = JArray.Parse(json);
            JArray jArr2 = JArray.Parse(json);
            string stringVersion = jArr[0].ToObject<JObject>().GetValue("tag_name").ToObject<string>();
            string stringVersion2 = jArr2[0].ToObject<JObject>().GetValue("tag_news").ToObject<string>();
            Version git = new(stringVersion);
            label9.Text = stringVersion2;
            label2.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            label4.Text = getunityhwid();


        }

        string AuthorizationChecker = "https://whis99.com/x/phasmo3.txt";
        string AuthorizationChecker2 = "https://whis99.com/crabbb/guvenlik/crab.txt";
        string AuthorizationChecker3 = "https://whis99.com/x/amongus3.txt";

        private void timer2_Tick(object sender, EventArgs e)
        {

                System.Net.WebClient webxD = new System.Net.WebClient();
                webxD.Headers.Add("User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/93.0");
                System.IO.Stream streamxD = webxD.OpenRead(AuthorizationChecker);
                System.IO.StreamReader readerxD = new System.IO.StreamReader(streamxD);
                string hwid = readerxD.ReadToEnd();

                string myhwid = getunityhwid();

                if (hwid.Contains(myhwid) == true)
                {
                    label11.Text = "Yes";
                    label11.ForeColor = Color.Yellow;

                }


                timer2.Stop();
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.Cursor = Cursors.Hand;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Cursor = Cursors.Default;
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.Cursor = Cursors.Hand;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Cursor = Cursors.Default;
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            System.Net.WebClient webxD = new System.Net.WebClient();
            webxD.Headers.Add("User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/93.0");
            System.IO.Stream streamxD = webxD.OpenRead(AuthorizationChecker2);
            System.IO.StreamReader readerxD = new System.IO.StreamReader(streamxD);
            string hwid = readerxD.ReadToEnd();

            string myhwid = getunityhwid();



            if (hwid.Contains(myhwid) == true)
            {
                label17.Text = "Yes";
                label17.ForeColor = Color.Yellow;

            }

        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            System.Net.WebClient webxD = new System.Net.WebClient();
            webxD.Headers.Add("User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/93.0");
            System.IO.Stream streamxD = webxD.OpenRead(AuthorizationChecker3);
            System.IO.StreamReader readerxD = new System.IO.StreamReader(streamxD);
            string hwid = readerxD.ReadToEnd();

            string myhwid = getunityhwid();



            if (hwid.Contains(myhwid) == true)
            {
                label18.Text = "Yes";
                label18.ForeColor = Color.Yellow;

            }
        }
    }
}
