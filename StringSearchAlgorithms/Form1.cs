using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.IO;

namespace StringSearchAlgorithms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        public void Run(string algorithmName, string filepath, string textToFind, Func<string,string,IEnumerable<int>> func)
        {
            new Task(() =>
            {
                Invoke(new Action(() =>
                {
                    toolStripStatusLabel1.Text = "Searching, please wait...";
                    toolStripProgressBar1.Style = ProgressBarStyle.Marquee;
                    menuStrip1.Enabled = false;
                    txtResults.Clear();
                    txtResults.AppendText(string.Format("Starting {0}...\r\n", algorithmName));
                    txtResults.AppendText(string.Format("Filepath: {0}\r\n", filepath));
                    txtResults.AppendText(string.Format("Text to find: {0}\r\n\r\n", textToFind));
                }));

                var stopWatch = new Stopwatch();
                stopWatch.Start();
                var count = 0;

                try
                {
                   
                    foreach (var d in func(filepath, textToFind))
                    {
                        count++;
                        Invoke(new Action(() =>
                        {
                            txtResults.AppendText(string.Format("Match found: {0}\r\n", d));
                        }));
                    }
                }
                catch(Exception ex)
                {
                    Invoke(new Action(() =>
                    {
                        txtResults.AppendText(string.Format("EXCEPTION OCCURRED: {0}\r\n", ex.Message));
                    }));
                }

                stopWatch.Stop();

                Invoke(new Action(() =>
                {
                    toolStripStatusLabel1.Text = "Ready";
                    toolStripProgressBar1.Style = ProgressBarStyle.Blocks;
                    menuStrip1.Enabled = true;
                    txtResults.AppendText(string.Format("\r\nFINISHED. Total matches Found: {0}. Ellapsed milliseconds: {1} ms", count, stopWatch.ElapsedMilliseconds.ToString()));
                }));

            }).Start();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void searchToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            var searchDialog = new SearchDialog();
            if (searchDialog.ShowDialog() == DialogResult.OK)
            {
                switch (searchDialog.Algorithm)
                {
                    case "Naive":
                        Run(searchDialog.Algorithm, searchDialog.Filepath, searchDialog.TextToFind, StringSearchAlgorithms.Naive);
                        break;
                    case "Knuth-Morris-Pratt":
                        Run(searchDialog.Algorithm, searchDialog.Filepath, searchDialog.TextToFind, StringSearchAlgorithms.KnuthMorrisPratt);
                        break;
                    case "Rabin-Karp":
                        Run(searchDialog.Algorithm, searchDialog.Filepath, searchDialog.TextToFind, StringSearchAlgorithms.RabinKarp);
                        break;
                    default:
                        MessageBox.Show("Unknown algorithm");
                        break;
                }
            }
        }
    }
}
