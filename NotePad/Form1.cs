using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;

namespace NotePad
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static bool change = false;

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Icon.ExtractAssociatedIcon(AppDomain.CurrentDomain.FriendlyName);
                this.Text = "New Text Document.txt - NotePad";
                RegistryKey regis = Registry.ClassesRoot.OpenSubKey(@"Directory\background\shell", true);
                regis.CreateSubKey("NotePad\\command");
                regis = Registry.ClassesRoot.OpenSubKey(@"Directory\background\shell\NotePad\\command", true);
                regis.SetValue("", AppDomain.CurrentDomain.BaseDirectory+AppDomain.CurrentDomain.FriendlyName);
                regis.Close();
            }
            catch (Exception)
            {

                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }


        private void button3_Click(object sender, EventArgs e)
        {
            
        }
        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            change = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (change)
            {
                string m = MessageBox.Show("Do you want to save changes ?", "NotePad", MessageBoxButtons.YesNo).ToString();
                if (m == "Yes")
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    string[] split_name_forme = this.Text.Split('-');
                    sfd.FileName = split_name_forme[0].Trim() +".txt";
                    sfd.Title = "Save your file ";
                    sfd.Filter = "Text Documents|*.txt|All Files|*.*";
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        string stpath = sfd.FileName;
                        if (Path.GetExtension(stpath).ToLower() != ".txt")
                        {
                            stpath += ".txt";
                        }
                        StreamWriter sw = new StreamWriter(stpath);
                        sw.WriteLine(txtnote.Text);
                        sw.Close();

                    }

                }
                else
                    MessageBox.Show("**********Goodbay*********", "NotePad");
                //if (m == "Cancel")
                //{
                //    new CancelEventArgs().Cancel = true;
                //    MessageBox.Show("Welcome back");
                //}
                /////// C is capital


            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                string[] split_name_forme = this.Text.Split('-');
                sfd.FileName = split_name_forme[0].Trim() + ".txt";
                sfd.Title = "Save your file ";
                sfd.Filter = "Text Documents (*.txt)|*.txt|All Files|*.*";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string stpath = sfd.FileName;
                    if (Path.GetExtension(stpath).ToLower() != ".txt")
                    {
                        stpath += ".txt";
                    }
                    StreamWriter sw = new StreamWriter(stpath);
                    sw.WriteLine(txtnote.Text);
                    sw.Close();
                    txtnote.Text = "";
                    change = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtnote.Text = "";
            this.Text = "New Text Document.txt - NotePad";
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Text Documents (*.txt)|*.txt|All Files|*.*";
            open.Title = "Open";
            if (open.ShowDialog() == DialogResult.OK)
            {
                txtnote.Text = "";
                change = false;
                StreamReader sw = new StreamReader(open.FileName.ToString());
                txtnote.Text= sw.ReadToEnd();
                sw.Close();
                
                this.Text = open.SafeFileName.ToString() + " - NotePad";
                change = false;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] split_name_forme = this.Text.Split('-');
            StreamWriter sw = new StreamWriter(split_name_forme[0]);
            sw.WriteLine(txtnote.Text);
            sw.Close();
            change = false;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (change)
            {
                DialogResult m = MessageBox.Show("Do you want to save changes ?", "NotePad", MessageBoxButtons.YesNoCancel);
                if (m == DialogResult.Yes)
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    string[] split_name_forme = this.Text.Split('-');
                    sfd.FileName = split_name_forme[0].Trim() + ".txt";
                    sfd.Title = "Save your file ";
                    sfd.Filter = "Text Documents|*.txt|All Files|*.*";
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        string stpath = sfd.FileName;
                        if (Path.GetExtension(stpath).ToLower() != ".txt")
                        {
                            stpath += ".txt";
                        }
                        StreamWriter sw = new StreamWriter(stpath);
                        sw.WriteLine(txtnote.Text);
                        sw.Close();
                    }
                }
                else if (m == DialogResult.No)
                {
                    change = false;
                    this.Close();
                    
                }
                else if (m == DialogResult.Cancel)
                {
                    return;
                }
            }
            else
            {
                this.Close();
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtnote.Undo();
        }

        private void selectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtnote.Focus();
            txtnote.SelectAll();
        }

        private void dateTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtnote.Paste(DateTime.Now.ToString());
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtnote.SelectedText);
            txtnote.SelectedText = "";
        }

        private void pastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtnote.Paste(Clipboard.GetText());
        }

        private void copeToolStripMenuItem_Click(object sender, EventArgs e)
        { 
            Clipboard.SetText(txtnote.SelectedText);
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (txtnote.SelectionLength > 0)
            {
                copeToolStripMenuItem.Enabled = true;
                deleteToolStripMenuItem.Enabled = true;
                cutToolStripMenuItem.Enabled = true;
            }
            else { copeToolStripMenuItem.Enabled = false;
                deleteToolStripMenuItem.Enabled = false;
                cutToolStripMenuItem.Enabled = false;
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtnote.SelectedText = "";
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = fontDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                txtnote.Font = fontDialog1.Font;
                

            }
        }
    }
}
