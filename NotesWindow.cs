using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Notes
{
    public partial class NotesWindow : Form, IWindowGeom
    {
        public NotesWindow(string notesPath, string winGeomPath)
        {
            this.notesPath = notesPath;
            this.winGeomPath = winGeomPath;
            this.InitializeComponent();
        }

        private bool controlDown = false;

        private string notesPath, winGeomPath;

        private void Notes_FormClosing(object sender, FormClosingEventArgs e)
        {
            File.WriteAllText(notesPath, notesBox.Text);
            SaveConfig(Left, Top, Width, Height);
        }

        private void HandleLoad(object sender, EventArgs e)
        {
            try
            {
                notesBox.Text = File.ReadAllText(notesPath);
                var cfg = LoadConfig();
                Left = cfg[0];
                Top = cfg[1];
                Width = cfg[2];
                Height = cfg[3];
            }
            catch
            {

            }

            notesBox.Select(0, 0);
        }

        private void HandleKeyDown(object sender, KeyEventArgs e)
        {
            controlDown = e.Control;

            if (e.Control && e.KeyCode == Keys.A)
            {
                notesBox.SelectAll();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }

            if (e.Control && e.KeyCode == Keys.PageDown)
                this.EnlargeFont();

            if (e.Control && e.KeyCode == Keys.PageUp)
                this.ShrinkFont();
        }

        private void EnlargeFont()
        {
            var oldFont = this.notesBox.Font;
            if (oldFont.Size > 30) return;

            this.ResizeFont(oldFont, 1.25);
        }

        private void ShrinkFont()
        {
            var oldFont = this.notesBox.Font;
            if (oldFont.Size < 8) return;

            this.ResizeFont(oldFont, 0.8);
        }

        private void ResizeFont(Font oldFont, double factor)
        {
            var biggerFont = new Font(oldFont.FontFamily, oldFont.Size * (float)factor);
            this.notesBox.Font = biggerFont;
        }

        public int[] LoadConfig()
        {
            try
            {

                List<int> rows = new List<int>();
                foreach (var row in File.ReadAllLines(winGeomPath))
                {
                    int i;
                    int.TryParse(row, out i);
                    rows.Add(i);
                }
                return rows.ToArray();
            }
            catch
            {
                return new int[] { 0, 0, 300, 200 };
            }
        }

        public void SaveConfig(int x, int y, int w, int h)
        {
            try
            {
                List<string> rows = new List<string>();
                rows.Add(x.ToString());
                rows.Add(y.ToString());
                rows.Add(w.ToString());
                rows.Add(h.ToString());
                string txt = string.Join("\n", rows.ToArray());
                File.WriteAllText(winGeomPath, txt);
            }
            catch
            {

            }
        }
    }
}