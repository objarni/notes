﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace AlwaysOnTop
{
    public partial class Notes : Form
    {
        public Notes(string path)
        {
            this.path = path;
            this.InitializeComponent();
        }

        private string path;

        private void Notes_FormClosing(object sender, FormClosingEventArgs e)
        {
            File.WriteAllText(path, notesBox.Text);
        }

        private void Notes_Load(object sender, EventArgs e)
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

        private void notesBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                notesBox.SelectAll();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

    }
}