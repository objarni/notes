using System;
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

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var notesWin = new NotesWindow(GetNotesPath(), GetWindowGeomPath());
            Application.Run(notesWin);
        }

        private static string GetNotesPath()
        {
            return Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.ApplicationData),
                "Notes.txt");
        }

        private static string GetWindowGeomPath()
        {
            return Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.ApplicationData),
                "Notes.ini");
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
