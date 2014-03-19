using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Notes
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var alreadyRunning = CheckIfAlreadyRunning();

            if (alreadyRunning) return;

            string path = GetPersistentFilePath();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new NotesWindow(path));
        }

        private static string GetPersistentFilePath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Notes.txt");
        }

        private static bool CheckIfAlreadyRunning()
        {
            bool alreadyRunning = false;
            var openWindows = OpenWindowGetter.GetOpenWindows();
            foreach (var win in openWindows)
            {
                if (win.Value == "Notes")
                {
                    alreadyRunning = true;
                }
            }
            return alreadyRunning;
        }
    }
}
