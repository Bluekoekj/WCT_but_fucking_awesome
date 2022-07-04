using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.IO.Compression;
using System.Threading;
using System.Windows.Forms;

namespace Windows_Customization_Toolbox
{
    public partial class Main_UI : Form
    {
        
        public Main_UI()
        {
            InitializeComponent();
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //...
        }

        private void Main_UI_Load(object sender, EventArgs e)
        {
            //...
        }

        private void Main_UI_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Confirms all forms are closed when the program is exited
            Environment.Exit(0);
        }

        // Tab 1 Support Image Hover tooltips - start
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {            
            ImageToolTip.SetToolTip(pictureBox1, "Supports Windows 10 and 11");
            ImageToolTip.SetToolTip(pictureBox2, "Supports Windows 10 and 11");
        }

        private void pictureBox3_MouseMove(object sender, MouseEventArgs e)
        {
            ImageToolTip.SetToolTip(pictureBox3, "Supports Windows 10");
        }

        private void pictureBox7_MouseMove(object sender, MouseEventArgs e)
        {
            ImageToolTip.SetToolTip(pictureBox4, "Supports Windows 7, 8, 8.1 and 10");
            ImageToolTip.SetToolTip(pictureBox7, "Supports Windows 7, 8, 8.1 and 10");
            ImageToolTip.SetToolTip(pictureBox6, "Supports Windows 7, 8, 8.1 and 10");
        }

        private void pictureBox8_MouseMove(object sender, MouseEventArgs e)
        {
            ImageToolTip.SetToolTip(pictureBox8, "Supports Windows 10");
        }

        private void pictureBox9_MouseMove(object sender, MouseEventArgs e)
        {
            ImageToolTip.SetToolTip(pictureBox9, "Supports Windows 11");
        }

        private void pictureBox10_MouseMove(object sender, MouseEventArgs e)
        {
            ImageToolTip.SetToolTip(pictureBox10, "Supports Windows 10");
        }

        private void pictureBox11_MouseMove(object sender, MouseEventArgs e)
        {
            ImageToolTip.SetToolTip(pictureBox11, "Supports Windows 11");
        }
        // Tooltip segment end


        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Process.Start("https://short.oliverstech.tk/WtcGit");
        }

        private void pictureBox5_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.Hand;
            ImageToolTip.SetToolTip(pictureBox5, "View project on GitHub");
        }

        // Tab 1 Link Click responses - start
        private void TbxLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            progressBar.Style = ProgressBarStyle.Marquee;
            progressLabel.Text = "Preparing to configure TaskbarX...";

            var tbx11check = MessageBox.Show("Are you using Windows 11?\nIf yes, ExplorerPatcher will be installed.", "WCT", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
            if (tbx11check == DialogResult.Yes)
            {
                progressLabel.Text = "Getting ready to install ExplorerPatcher...";

                void EpSetup()
                {
                    WebClient wc = new WebClient();
                    progressBar.Style = ProgressBarStyle.Blocks;
                    wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wc_DownloadProgressChanged);
                    try
                    {
                        progressLabel.Text = "Downloading ExplorerPatcher...";
                        wc.DownloadFileAsync(new Uri("https://github.com/valinet/ExplorerPatcher/releases/download/22000.708.46.6_d042e57/ep_setup.exe"), "epsetup.exe");
                    }
                    catch (Exception)
                    {
                        File.Delete("epsetup.exe");
                        EpSetup();
                    }
                    finally
                    {
                        progressBar.Style = ProgressBarStyle.Marquee;
                        progressLabel.Text = "Installing ExplorerPatcher...";
                        Process.Start("epsetup.exe");
                        Thread.Sleep(3000);
                    }

                    InstallTbx();
                }
            }
            else if (tbx11check == DialogResult.No) { InstallTbx(); }

            else if (tbx11check == DialogResult.Cancel) {
                progressBar.Style = ProgressBarStyle.Blocks;
                progressLabel.Text = "Ready";
            }
            void InstallTbx()
            {
                progressBar.Style = ProgressBarStyle.Blocks;
                progressLabel.Text = "Downloading TaskbarX...";
                WebClient wc = new WebClient();
                wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wc_DownloadProgressChanged);
                try
                {       
                    wc.DownloadFileAsync(new Uri("https://github.com/ChrisAnd1998/TaskbarX/releases/download/1.7.6.0/TaskbarX_1.7.6.0_x64.zip"), "tbx.zip");
                }
                catch (Exception) {
                    File.Delete("tbx.zip");
                    InstallTbx();
                }
                finally
                {
                    progressBar.Style = ProgressBarStyle.Marquee;
                    progressLabel.Text = "Extracting TaskbarX...";
                    if (Directory.Exists("tbx")) { Directory.Delete("tbx", true); }                    
                    Thread.Sleep(1000);
                    ZipFile.ExtractToDirectory("tbx.zip", "tbx");

                    progressLabel.Text = "Starting TaskbarX...";
                    Process.Start("tbx\\TaskbarX.exe");
                    progressLabel.Text = "Starting TaskbarX Configurator...";
                    Process.Start("tbx\\TaskbarX Configurator.exe");

                    progressBar.Style = ProgressBarStyle.Blocks;
                    progressLabel.Text = "TaskbarX installed. Ready.";
                }

            }
        }
        void wc_DownloadProgressChanged(Object sender, DownloadProgressChangedEventArgs e)

        {
            progressBar.Value = e.ProgressPercentage;
        }

        private void TrTbLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            progressBar.Style = ProgressBarStyle.Marquee;
            progressLabel.Text = "Preparing to install TranslucentTB...";
            WebClient wc = new WebClient();
            wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wc_DownloadProgressChanged);
            InstallTrtb();
            void InstallTrtb()
            {
                try
                {
                    progressBar.Style = ProgressBarStyle.Blocks;
                    progressLabel.Text = "Downloading TranslucentTB...";
                    wc.DownloadFileAsync(new Uri("https://github.com/TranslucentTB/TranslucentTB/releases/download/2021.5/TranslucentTB.appinstaller"), "trtb.appinstaller");
                }
                catch
                {
                    File.Delete("trtb.appinstaller");
                    InstallTrtb();
                }
                finally
                {
                    Thread.Sleep(1000);
                    progressBar.Style = ProgressBarStyle.Marquee;
                    progressLabel.Text = "Installing TranslucentTB...";
                    Process.Start("trtb.appinstaller");

                    progressBar.Style = ProgressBarStyle.Blocks;
                    progressLabel.Text = "TranslucentTB installed. Ready.";
                }
            }

        }
    }
}