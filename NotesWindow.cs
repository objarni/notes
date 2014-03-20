using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Notes
{
    public partial class NotesWindow : Form
    {
        public NotesWindow(string path)
        {
            this.path = path;
            this.InitializeComponent();
        }

        private bool controlDown = false;

        private string path;

        private void Notes_FormClosing(object sender, FormClosingEventArgs e)
        {
            File.WriteAllText(path, notesBox.Text);
        }

        private void HandleLoad(object sender, EventArgs e)
        {
            try
            {
                notesBox.Text = File.ReadAllText(path);
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
    }
}