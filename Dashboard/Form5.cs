using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Windows.Forms;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using Ookii.Dialogs.WinForms;

namespace Dashboard
{
    [Obfuscation(ApplyToMembers = true, Exclude = false, Feature = "all", StripAfterObfuscation = true)]
    public partial class Form5 : Form
    {

        #region Variables
        public const string
            DownloadURL = "https://whis99.com/amongus/AmongUsFree.dll",
            PaidURL = "https://whis99.com/amongus/AmongUsPaid.dll",
            ReleasesAPI = "https://whis99.com/amongus/freeupdate.txt",
            PaidAPI = "https://whis99.com/amongus/paidupdate.txt",
            MelonURL = "https://whis99.com/amongus/melon.zip";

        private string
            _path,
            _pluginFile,
             _pluginFile2;

        private string path
        {
            get
            {
                if (string.IsNullOrEmpty(_path))
                {

                    _path = WhisLauncher.FileUtilities.FindGameLocation3();
                }

                return _path;
            }
        }

        private string pluginFile
        {
            get
            {

                if (string.IsNullOrEmpty(_pluginFile))
                {
                    string folder = Path.Combine(path, "Mods");

                    try
                    {
                        _pluginFile = Directory.
                            GetFiles(folder).
                            Where(f => AssemblyName.GetAssemblyName(f).Equals("AmongUsFree")).
                            First();
                    }
                    catch (Exception)
                    {
                        Directory.CreateDirectory(folder);
                        _pluginFile = Path.Combine(folder, "AmongUsFree.dll");
                    }

                }

                return _pluginFile;
            }
        }




        private string pluginFile2
        {
            get
            {

                if (string.IsNullOrEmpty(_pluginFile2))
                {
                    string folder = Path.Combine(path, "Mods");

                    try
                    {
                        _pluginFile = Directory.
                            GetFiles(folder).
                            Where(f => AssemblyName.GetAssemblyName(f).Equals("AmongUsPaid")).
                            First();
                    }
                    catch (Exception)
                    {
                        Directory.CreateDirectory(folder);
                        _pluginFile2 = Path.Combine(folder, "AmongUsPaid.dll");
                    }

                }

                return _pluginFile2;
            }
        }

        #endregion
        ///değişecek olan yerler burda bitiyor

        //methodlar


        static string
    tempFolder = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString()),
    zipExtract = Path.Combine(tempFolder, "Extracted"),
    zipFile = Path.Combine(tempFolder + "melon.zip");
        private void melonindir2()
        {
            // File variables

            // Create TempFolders
            Directory.CreateDirectory(tempFolder);
            Directory.CreateDirectory(zipExtract);

            // Download BepInEx
            melonindir(MelonURL, zipFile);


        }


        private void hileindir2()
        {
            // File variables

            // Create TempFolders
            Directory.CreateDirectory(tempFolder);
            Directory.CreateDirectory(zipExtract);

            // Download BepInEx
            hileindir(DownloadURL, zipFile);
        }




        private void paidindir2()
        {
            // File variables

            // Create TempFolders
            Directory.CreateDirectory(tempFolder);
            Directory.CreateDirectory(zipExtract);

            // Download BepInEx
            paidindir(PaidURL, zipFile);
        }





        private bool downloadComplete = false;

        private void melonindir(string url, string file)
        {
            WebClient client = new WebClient();
            textBox1.Text = "MelonLoader Downloading" + Environment.NewLine + textBox1.Text;
            client.DownloadProgressChanged += client_DownloadProgressChanged;
            client.DownloadFileCompleted += client_DownloadFileCompleted;
            client.DownloadFileAsync(new Uri(url), file);

            while (!downloadComplete)
            {
                Application.DoEvents();
            }

            downloadComplete = false;
        }


        private void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            // Extract BepInEx
            ZipFile.ExtractToDirectory(zipFile, zipExtract);

            // Copy BepInEx to Game Folder
            WhisLauncher.FileUtilities.CopyDir(zipExtract, path);

            if (paiduser == true)
            {
                paidindir2();
                MessageBox.Show("trueugs");
            }
            else
            {
                hileindir2();
                MessageBox.Show("qqqq");
            }
        }

        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            circularProgressBar1.Value = e.ProgressPercentage;
            circularProgressBar1.Text = e.ProgressPercentage + "%";
        }





        private bool UpdateAvailable()
        {
            try
            {

                string plugin = pluginFile;
                if (!File.Exists(plugin))
                    return false;

                using var client = new WebClient();
                // Some random user agent because with others it responds with 403
                client.Headers.Add("User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/93.0");
                string json = client.DownloadString(ReleasesAPI);

                JArray jArr = JArray.Parse(json);

                string stringVersion = jArr[0].ToObject<JObject>().GetValue("tag_name").ToObject<string>();

                // Compare GitHub and Local Version
                Version git = new(stringVersion);

                Version current = AssemblyName.GetAssemblyName(plugin).Version;

                int result = current.CompareTo(git);
                label7.Text = stringVersion;
                if (File.Exists(plugin))
                {
                    MessageBox.Show(current.ToString());
                }
                else
                {
                    textBox1.Text = "The Mod file not found!" + Environment.NewLine + textBox1.Text;
                }

                return result < 0;
            }
            catch (Exception)
            {
                return false;
            }
        }




        private bool PaidUpdateAvailable()
        {
            try
            {

                string plugin2 = pluginFile2;
                if (!File.Exists(plugin2))
                    return false;

                using var client = new WebClient();
                // Some random user agent because with others it responds with 403
                client.Headers.Add("User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/93.0");
                string json = client.DownloadString(PaidAPI);

                JArray jArr = JArray.Parse(json);

                string stringVersion = jArr[0].ToObject<JObject>().GetValue("tag_name").ToObject<string>();

                // Compare GitHub and Local Version
                Version git = new(stringVersion);

                Version current = AssemblyName.GetAssemblyName(plugin2).Version;

                int result = current.CompareTo(git);
                label16.Text = stringVersion;
                if (File.Exists(plugin2))
                {
                    MessageBox.Show(current.ToString());
                }
                else
                {
                    textBox1.Text = "The Mod file not found!" + Environment.NewLine + textBox1.Text;
                }

                return result < 0;
            }
            catch (Exception)
            {
                return false;
            }
        }



        private void paidindir(string url, string file)
        {
            WebClient client = new WebClient();
            client.DownloadFileCompleted += client2_DownloadFileCompleted;
            textBox1.Text = "Mod Downloading" + Environment.NewLine + textBox1.Text;
            client.DownloadFileAsync(new Uri(url), pluginFile2);

            while (!downloadComplete2)
            {
                Application.DoEvents();
            }

            downloadComplete2 = false;
        }







        private bool downloadComplete2 = false;
        private void hileindir(string url, string file)
        {
            WebClient client = new WebClient();
            client.DownloadFileCompleted += client2_DownloadFileCompleted;
            textBox1.Text = "Mod Downloading" + Environment.NewLine + textBox1.Text;
            client.DownloadFileAsync(new Uri(url), pluginFile);

            while (!downloadComplete2)
            {
                Application.DoEvents();
            }

            downloadComplete2 = false;
        }

        private void client2_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            textBox1.Text = "Game Started please wait until loaded" + Environment.NewLine + textBox1.Text;
            System.Diagnostics.Process.Start("steam://rungameid/945360");
        }






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





        //method bitiş




        public static string AuthorizationChecker = "https://whis99.com/x/amongus3.txt";

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Interval = 10000;
            using var client = new WebClient();
            string json = client.DownloadString(ReleasesAPI);
            string json2 = client.DownloadString(PaidAPI);
            JArray jArr = JArray.Parse(json);
            JArray jArr2 = JArray.Parse(json2);
            JArray jArr3 = JArray.Parse(json2);
            JArray jArr4 = JArray.Parse(json);
            string stringVersion = jArr[0].ToObject<JObject>().GetValue("tag_name").ToObject<string>();
            string stringVersion2 = jArr2[0].ToObject<JObject>().GetValue("tag_name").ToObject<string>();
            string stringVersion3 = jArr3[0].ToObject<JObject>().GetValue("tag_status").ToObject<string>();
            string stringVersion4 = jArr4[0].ToObject<JObject>().GetValue("tag_status").ToObject<string>();
            Version git = new(stringVersion);
            Version git2 = new(stringVersion2);
            string git3 = Convert.ToString(stringVersion3);
            string git4 = Convert.ToString(stringVersion4);
            label7.Text = stringVersion;
            label16.Text = stringVersion2;
            label5.Text = stringVersion3;
            label14.Text = stringVersion4;

            string plugin = pluginFile;

            if (File.Exists(plugin))
            {
                Version current = AssemblyName.GetAssemblyName(plugin).Version;
                int result = current.CompareTo(git);
                label3.Text = current.ToString();
            }
            else
            {
                label3.Text = "Not found";
            }


            string plugin2 = pluginFile2;
            if (File.Exists(plugin2))
            {
                Version current2 = AssemblyName.GetAssemblyName(plugin2).Version;

                int result2 = current2.CompareTo(git);
                label6.Text = current2.ToString();
            }
            else
            {
                label6.Text = "Not found";
            }





            if (label14.Text == "Updating")
            {
                label14.ForeColor = System.Drawing.Color.Red;
            }

            if (label5.Text == "Updating")
            {
                label5.ForeColor = System.Drawing.Color.Red;
            }




            if (label14.Text == "Maintenance")
            {
                label14.ForeColor = System.Drawing.Color.HotPink;
            }

            if (label5.Text == "Maintenance")
            {
                label5.ForeColor = System.Drawing.Color.HotPink;
            }




            if (label14.Text == "Working")
            {
                label14.ForeColor = System.Drawing.Color.FromArgb(50, 226, 178);
            }

            if (label5.Text == "Working")
            {
                label5.ForeColor = System.Drawing.Color.FromArgb(50, 226, 178);
            }
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            string plugin = pluginFile;
            label19.Text = path;
        }

        private void pictureBox2_Click_2(object sender, EventArgs e)
        {
            textBox1.ForeColor = System.Drawing.Color.Red;
            textBox1.Clear();
            foreach (System.Diagnostics.Process p in System.Diagnostics.Process.GetProcesses())
            {

                if (p.ProcessName == "Among Us")
                {
                    textBox1.Text = "Closing game";
                    p.Kill();
                    p.WaitForExit();
                }
            }
            if (Directory.Exists(_path + "/MelonLoader"))
            {
                textBox1.Text = "Deleted the MelonLoader folder" + Environment.NewLine + textBox1.Text;
                Directory.Delete(_path + "/MelonLoader", true);
            }
            if (Directory.Exists(_path + "/Mods"))
            {
                textBox1.Text = "Deleted the Mods folder" + Environment.NewLine + textBox1.Text;
                Directory.Delete(_path + "/Mods", true);
            }
            if (Directory.Exists(_path + "/Logs"))
            {
                textBox1.Text = "Deleted the Logs folder" + Environment.NewLine + textBox1.Text;
                Directory.Delete(_path + "/Logs", true);
            }
            if (Directory.Exists(_path + "/Plugins"))
            {
                textBox1.Text = "Deleted the Plugins folder" + Environment.NewLine + textBox1.Text;
                Directory.Delete(_path + "/Plugins", true);
            }
            if (Directory.Exists(_path + "/UserData"))
            {
                textBox1.Text = "Deleted the UserData folder" + Environment.NewLine + textBox1.Text;
                Directory.Delete(_path + "/UserData", true);
            }
            if (File.Exists(_path + "/bypass-log.txt"))
            {
                textBox1.Text = "Deleted the bypass-log.txt file" + Environment.NewLine + textBox1.Text;
                File.Delete(_path + "/bypass-log.txt");
            }
            if (File.Exists(_path + "/nickname.txt"))
            {
                textBox1.Text = "Deleted the nickname.txt file" + Environment.NewLine + textBox1.Text;
                File.Delete(_path + "/nickname.txt");
            }
            if (File.Exists(_path + "/version.dll"))
            {
                textBox1.Text = "Deleted the version.dll file" + Environment.NewLine + textBox1.Text;
                File.Delete(_path + "/version.dll");
            }
            if (File.Exists(_path + "/WhiskyHacks.dll"))
            {
                textBox1.Text = "Deleted the WhiskyHacks.dll file" + Environment.NewLine + textBox1.Text;
                File.Delete(_path + "/WhiskyHacks.dll");
            }
            if (File.Exists(_path + "/PhasBypass.dll"))
            {
                textBox1.Text = "Deleted the PhasBypass.dll file" + Environment.NewLine + textBox1.Text;
                File.Delete(_path + "/PhasBypass.dll");
            }
            textBox1.Text = "The uninstall process is successful!!!" + Environment.NewLine + textBox1.Text;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_path))
            {
                timer1.Start();
                timer2.Stop();
            }
        }

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.Cursor = Cursors.Hand;
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.Cursor = Cursors.Hand;
        }

        private void pictureBox5_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.Cursor = Cursors.Hand;
        }

        private void pictureBox4_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.Cursor = Cursors.Hand;
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.Cursor = Cursors.Hand;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Cursor = Cursors.Default;
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Cursor = Cursors.Default;
        }

        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Cursor = Cursors.Default;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Cursor = Cursors.Default;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Cursor = Cursors.Default;
        }

        private void pictureBox3_Click_2(object sender, EventArgs e)
        {
            try
            {


                if (Directory.Exists(_path + "/MelonLoader"))
                {
                    Directory.Delete(_path + "/MelonLoader", true);
                }
                if (Directory.Exists(_path + "/Mods"))
                {
                    Directory.Delete(_path + "/Mods", true);
                }
                if (Directory.Exists(_path + "/Logs"))
                {
                    Directory.Delete(_path + "/Logs", true);
                }
                if (Directory.Exists(_path + "/Plugins"))
                {
                    Directory.Delete(_path + "/Plugins", true);
                }
                if (Directory.Exists(_path + "/UserData"))
                {
                    Directory.Delete(_path + "/UserData", true);
                }
                if (File.Exists(_path + "/bypass-log.txt"))
                {
                    File.Delete(_path + "/bypass-log.txt");
                }
                if (File.Exists(_path + "/nickname.txt"))
                {
                    File.Delete(_path + "/nickname.txt");
                }
                if (File.Exists(_path + "/version.dll"))
                {
                    File.Delete(_path + "/version.dll");
                }
                if (File.Exists(_path + "/WhiskyHacks.dll"))
                {
                    File.Delete(_path + "/WhiskyHacks.dll");
                }
                if (File.Exists(_path + "/PhasBypass.dll"))
                {
                    File.Delete(_path + "/PhasBypass.dll");
                }

                textBox1.ForeColor = System.Drawing.Color.Yellow;
                textBox1.Clear();
                Process.Start(_path);
                Directory.CreateDirectory(_path + "/Mods");
                textBox1.Text = "The mod detection started" + Environment.NewLine + textBox1.Text;
                if (melonyuklumu() == false)
                {
                    textBox1.Text = "The mode could not be detected" + Environment.NewLine + textBox1.Text;
                    melonindir2();
                }
                else
                {
                    textBox1.Text = "The MelonLoader Check SuccesFly!" + Environment.NewLine + textBox1.Text;
                }

                var updateAvailable = UpdateAvailable();

                if (updateAvailable || !IsCheatInstalled())
                {
                    hileindir2();
                    System.Diagnostics.Process.Start("steam://rungameid/945360");
                    textBox1.Text = "Installing is successful. Wait for game loading!" + Environment.NewLine + textBox1.Text;
                    UpdateAvailable();
                    MessageBox.Show(null, "Cheat " + (updateAvailable ? "Updated" : "Installed") + " sucessfully!",
                        "Installed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch { }
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox1.ForeColor = System.Drawing.Color.Red;
            foreach (System.Diagnostics.Process p in System.Diagnostics.Process.GetProcesses())
            {

                if (p.ProcessName == "Among Us")
                {
                    textBox1.Text = "Closing game";
                    p.Kill();
                    p.WaitForExit();
                }
            }
            if (Directory.Exists(_path + "/MelonLoader"))
            {
                textBox1.Text = "Deleted the MelonLoader folder" + Environment.NewLine + textBox1.Text;
                Directory.Delete(_path + "/MelonLoader", true);
            }
            if (Directory.Exists(_path + "/Mods"))
            {
                textBox1.Text = "Deleted the Mods folder" + Environment.NewLine + textBox1.Text;
                Directory.Delete(_path + "/Mods", true);
            }
            if (Directory.Exists(_path + "/Logs"))
            {
                textBox1.Text = "Deleted the Logs folder" + Environment.NewLine + textBox1.Text;
                Directory.Delete(_path + "/Logs", true);
            }
            if (Directory.Exists(_path + "/Plugins"))
            {
                textBox1.Text = "Deleted the Plugins folder" + Environment.NewLine + textBox1.Text;
                Directory.Delete(_path + "/Plugins", true);
            }
            if (Directory.Exists(_path + "/UserData"))
            {
                textBox1.Text = "Deleted the UserData folder" + Environment.NewLine + textBox1.Text;
                Directory.Delete(_path + "/UserData", true);
            }
            if (File.Exists(_path + "/bypass-log.txt"))
            {
                textBox1.Text = "Deleted the bypass-log.txt file" + Environment.NewLine + textBox1.Text;
                File.Delete(_path + "/bypass-log.txt");
            }
            if (File.Exists(_path + "/nickname.txt"))
            {
                textBox1.Text = "Deleted the nickname.txt file" + Environment.NewLine + textBox1.Text;
                File.Delete(_path + "/nickname.txt");
            }
            if (File.Exists(_path + "/version.dll"))
            {
                textBox1.Text = "Deleted the version.dll file" + Environment.NewLine + textBox1.Text;
                File.Delete(_path + "/version.dll");
            }
            if (File.Exists(_path + "/WhiskyHacks.dll"))
            {
                textBox1.Text = "Deleted the WhiskyHacks.dll file" + Environment.NewLine + textBox1.Text;
                File.Delete(_path + "/WhiskyHacks.dll");
            }
            if (File.Exists(_path + "/PhasBypass.dll"))
            {
                textBox1.Text = "Deleted the PhasBypass.dll file" + Environment.NewLine + textBox1.Text;
                File.Delete(_path + "/PhasBypass.dll");
            }
            textBox1.Text = "The uninstall process is SUCCESSFUL!!!" + Environment.NewLine + textBox1.Text;
        }

        private void pictureBox5_Click_1(object sender, EventArgs e)
        {

            VistaFolderBrowserDialog dialog = new()
            {
                Description = "Select Phasmophobia Game folder",
                ShowNewFolderButton = false
            };

            // Show dialog and return Path
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                _path = dialog.SelectedPath;
                label19.Text = _path;
            }
            else
            {
                _path = dialog.SelectedPath = "NOT SELECTED PLEASE RESTART LAUNCHER";
                label19.Text = "NOT SELECTED PLEASE SELECT GAME FOLDER";
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

            if (Directory.Exists(_path + "/MelonLoader"))
            {
                Directory.Delete(_path + "/MelonLoader", true);
            }
            if (Directory.Exists(_path + "/Mods"))
            {
                Directory.Delete(_path + "/Mods", true);
            }
            if (Directory.Exists(_path + "/Logs"))
            {
                Directory.Delete(_path + "/Logs", true);
            }
            if (Directory.Exists(_path + "/Plugins"))
            {
                Directory.Delete(_path + "/Plugins", true);
            }
            if (Directory.Exists(_path + "/UserData"))
            {
                Directory.Delete(_path + "/UserData", true);
            }
            if (File.Exists(_path + "/bypass-log.txt"))
            {
                File.Delete(_path + "/bypass-log.txt");
            }
            if (File.Exists(_path + "/nickname.txt"))
            {
                File.Delete(_path + "/nickname.txt");
            }
            if (File.Exists(_path + "/version.dll"))
            {
                File.Delete(_path + "/version.dll");
            }
            if (File.Exists(_path + "/WhiskyHacks.dll"))
            {
                File.Delete(_path + "/WhiskyHacks.dll");
            }
            if (File.Exists(_path + "/PhasBypass.dll"))
            {
                File.Delete(_path + "/PhasBypass.dll");
            }
            textBox1.ForeColor = System.Drawing.Color.Yellow;
            textBox1.Clear();
            Process.Start(_path);
            System.String address = "";
            System.Net.WebRequest requestip = System.Net.WebRequest.Create("http://checkip.dyndns.org/");
            using (System.Net.WebResponse response = requestip.GetResponse())
            using (System.IO.StreamReader streamCC = new System.IO.StreamReader(response.GetResponseStream()))
            {
                address = streamCC.ReadToEnd();
                int first = address.IndexOf("Address: ") + 9;
                int last = address.LastIndexOf("</body>");
                address = address.Substring(first, last - first);
            }
            System.Net.WebClient webxD = new System.Net.WebClient();
            webxD.Headers.Add("User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/93.0");

            System.IO.Stream streamxD = webxD.OpenRead(AuthorizationChecker);
            System.IO.StreamReader readerxD = new System.IO.StreamReader(streamxD);

            string hwid = readerxD.ReadToEnd();
            string myhwid = getunityhwid();

            if (hwid.Contains(myhwid) == true)
            {
                paiduser = true;
                Directory.CreateDirectory(_path + "/Mods");
                textBox1.Text = "The mod detection started" + Environment.NewLine + textBox1.Text;
                if (melonyuklumu() == false)
                {
                    textBox1.Text = "The mode could not be detected" + Environment.NewLine + textBox1.Text;
                    melonindir2();
                }
                else
                {
                    textBox1.Text = "The MelonLoader Check SuccesFly!" + Environment.NewLine + textBox1.Text;
                }

                var updateAvailable = PaidUpdateAvailable();

                if (updateAvailable || !IsCheatInstalled())
                {
                    paidindir2();
                    System.Diagnostics.Process.Start("steam://rungameid/945360");
                    textBox1.Text = "Installing is successful. Wait for game loading!" + Environment.NewLine + textBox1.Text;
                    PaidUpdateAvailable();
                    // MessageBox.Show(null, "Cheat " + (updateAvailable ? "Updated" : "Installed") + " sucessfully!",
                    //    "Installed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("You are not paid member so you can't download paid version!");
            }
        }


        public Form5()
        {
            InitializeComponent();
            circularProgressBar1.Value = 0;
            circularProgressBar1.Text = "0%";
        }


        public static bool paiduser = false;


        private bool IsCheatInstalled()
        {
            return File.Exists(pluginFile);
        }



        private bool melonyuklumu()
        {
            return File.Exists(Path.Combine(path, "MelonLoader", "MelonLoader.dll"));
        }


    }
}
