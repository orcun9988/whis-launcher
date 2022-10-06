using Dashboard;
using Ookii.Dialogs.WinForms;
using System.IO;
using System.Windows.Forms;

namespace WhisLauncher
{
    public static class FileUtilities
    {

        private static string BrowseGame()
        {

            VistaFolderBrowserDialog dialog = new()
            {
                Description = "Select Phasmophobia Game folder",
                ShowNewFolderButton = false
            };

            // Show dialog and return Path
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.SelectedPath;
            }
            else
            {
                return dialog.SelectedPath = "NOT SELECTED PLEASE RESTART LAUNCHER";
            }

        }



        public static string FindGameLocation()
        {
            // Try to get the Game Folder through steam
            string location = SteamUtils.GetAppLocation(1782210, "Phasmophobia");

            // Fallback to browse the folder
            if (string.IsNullOrEmpty(location) || !File.Exists(Path.Combine(location, "Phasmophobia.exe")))
            {
                location = BrowseGame();
            }

            return location;
        }








        public static string FindGameLocation2()
        {
            // Try to get the Game Folder through steam
            string location = SteamUtils.GetAppLocation(1782210, "Crab Game");

            // Fallback to browse the folder
            if (string.IsNullOrEmpty(location) || !File.Exists(Path.Combine(location, "Crab Game.exe")))
            {
                location = BrowseGame2();
            }

            return location;
        }


        private static string BrowseGame2()
        {

            VistaFolderBrowserDialog dialog = new()
            {
                Description = "Select Crab Game folder",
                ShowNewFolderButton = false
            };

            // Show dialog and return Path
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.SelectedPath;
            }

            return null;
        }



        public static string FindGameLocation3()
        {
            string location = SteamUtils.GetAppLocation(945360, "Among Us");

            // Fallback to browse the folder
            if (string.IsNullOrEmpty(location) || !File.Exists(Path.Combine(location, "Among Us.exe")))
            {
                location = BrowseGame3();
            }

            return location;
        }




        private static string BrowseGame3()
        {

            VistaFolderBrowserDialog dialog = new()
            {
                Description = "Select AmongUs Game folder",
                ShowNewFolderButton = false
            };

            // Show dialog and return Path
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.SelectedPath;
            }

            return null;
        }




        public static void CopyDir(string sourceFolder, string destFolder)
        {
            if (!Directory.Exists(destFolder))
                Directory.CreateDirectory(destFolder);

            // Get Files & Copy
            string[] files = Directory.GetFiles(sourceFolder);
            foreach (string file in files)
            {
                string name = Path.GetFileName(file);

                // ADD Unique File Name Check to Below!!!!
                string dest = Path.Combine(destFolder, name);
                File.Copy(file, dest, true);
            }

            // Get dirs recursively and copy files
            string[] folders = Directory.GetDirectories(sourceFolder);
            foreach (string folder in folders)
            {
                string name = Path.GetFileName(folder);
                string dest = Path.Combine(destFolder, name);
                CopyDir(folder, dest);
            }
        }

    }
}
