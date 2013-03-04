using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Yahoo.Yui.Compressor;

namespace WebResourceCompressor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                this.textBox1.Text = folderBrowserDialog1.SelectedPath;
                this.textBox1.Enabled = false;
                label1.Text = "Compressing. Please wait for a while";
                searchDirectory(this.textBox1.Text);
                label1.ForeColor = Color.Green;
                label1.Text = "Successfully compressed the css and js files; Enjoy :)";
            }
        }

        private void searchDirectory(string sDir)
        {
            try
            {
                if (Directory.GetDirectories(sDir, "*.*", SearchOption.AllDirectories).Length > 0)
                {
                    foreach (string d in Directory.GetDirectories(sDir,"*.*",SearchOption.AllDirectories ))
                    {
                        foreach (string f in Directory.GetFiles(d))
                        {

                            compressFile(f);
                            textBox2.Text += f+"\r\n";
                            //Console.WriteLine(f);
                        }
                        //DirSearch(d);
                    }



                }
                //else
                //{
                    foreach (string f in Directory.GetFiles(sDir))
                    {
                        compressFile(f);
                        textBox2.Text += f + "\r\n";
                        //Console.WriteLine(f);
                    }
                //}

            }
            catch (System.Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }
        }

        void compressFile(string f)
        {
            try
            {
                var thefile = File.ReadAllText(f);

                if (Path.GetExtension(f) == ".js")
                {
                    var jsCompressor = new JavaScriptCompressor();
                    var mini = jsCompressor.Compress(thefile);
                    if (mini.ToString() != null)
                        File.WriteAllText(f, mini.ToString());
                }
                else if (Path.GetExtension(f) == ".css")
                {
                    var cssCompressor = new CssCompressor();
                    var mini = cssCompressor.Compress(thefile);
                    if (mini.ToString() != null)
                        File.WriteAllText(f, mini.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }


        }
    }
}
