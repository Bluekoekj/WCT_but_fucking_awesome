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

        bool tbxinstalled;
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

            if (tbxinstalled)
            {
                // Cleanup
                // tbc

                MessageBox.Show("Note: Since you have installed TaskbarX, you must not delete the tbx folder, since this contains all required files for TaskbarX to operate.\nDeleting the folder will break TaskbarX.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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

        void SetupProgram(string name, string link, string exec, bool reqextract, string zip)
        {
            progressBar.Style = ProgressBarStyle.Marquee;
            progressLabel.Text = "Preparing to configure " + name;
            WebClient wc = new WebClient();
            wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wc_DownloadProgressChanged);
            Install();

            void Install()
            {
                try
                {
                    progressBar.Style = ProgressBarStyle.Blocks;
                    progressLabel.Text = "Downloading " + name;
                    if (reqextract)
                    {
                        wc.DownloadFileAsync(new Uri(link), zip);
                    }
                    else
                    {
                        wc.DownloadFileAsync(new Uri(link), exec);
                    }
                }
                catch
                {
                    wait(1000);
                    if (reqextract)
                    {
                        File.Delete(zip);
                    }
                    else
                    {
                        File.Delete(exec);
                    }
                    Install();
                }
                finally
                {
                    wait(5000);
                    progressBar.Style = ProgressBarStyle.Marquee;
                    progressLabel.Text = "Installing " + name + "...";

                    if (reqextract)
                    {
                        try
                        {
                            if (Directory.Exists(name))
                            {
                                progressLabel.Text = "Deleting \\" + name + "\\";
                                Directory.Delete(name, true);
                            }
                        }
                        catch
                        {
                            MessageBox.Show("Couldn't delete '" + name + "'. Please delete it manually and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Environment.Exit(-1);
                        }
                        wait(1000);
                        progressLabel.Text = "Extracting " + name;
                        Directory.CreateDirectory(name);
                        ZipFile.ExtractToDirectory(zip, name);
                        wait(3500);
                        try
                        {
                            progressLabel.Text = "Starting " + name;
                            Process.Start(name + "\\" + exec);
                        }
                        catch (IOException)
                        {
                            MessageBox.Show("An IO error occured when running " + exec + ", usually meaning the file is currently in use.\nWhen you finish using WCT, run '" + exec + "'manually to finish installation.", "IO Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    else
                    {
                        try
                        {
                            progressLabel.Text = "Starting " + name;
                            Process.Start(exec);
                        }
                        catch (Win32Exception e)
                        {
                            string ex = Convert.ToString(e);
                            if (ex.Contains("being used by another"))
                            {
                                MessageBox.Show("'"+ exec + "' couldn't be executed due to another program using it.\nDon't worry, this can be resolved by launching it yourself when you finish with WCT.\nTechnical error: " + e.Message, "IO Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }

                    wait(3000);
                    progressBar.Style = ProgressBarStyle.Blocks;
                    progressLabel.Text = name + " installed. Ready.";
                }
            }
        }

        public void wait(int milliseconds)
        {
            var timer1 = new System.Windows.Forms.Timer();
            if (milliseconds == 0 || milliseconds < 0) return;

            // Console.WriteLine("start wait timer");
            timer1.Interval = milliseconds;
            timer1.Enabled = true;
            timer1.Start();

            timer1.Tick += (s, e) =>
            {
                timer1.Enabled = false;
                timer1.Stop();
                // Console.WriteLine("stop wait timer");
            };

            while (timer1.Enabled)
            {
                Application.DoEvents();
            }
        }

        private void TbxLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            progressBar.Style = ProgressBarStyle.Marquee;
            progressLabel.Text = "Preparing to configure TaskbarX...";

            var tbx11check = MessageBox.Show("Are you using Windows 11?\nIf yes, ExplorerPatcher will be installed.", "WCT", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
            if (tbx11check == DialogResult.Yes)
            {
                SetupProgram("ExplorerPatcher", "https://github.com/valinet/ExplorerPatcher/releases/download/22000.708.46.6_d042e57/ep_setup.exe", "ep.exe", false, null);
                SetupProgram("TaskbarX", "https://github.com/ChrisAnd1998/TaskbarX/releases/download/1.7.6.0/TaskbarX_1.7.6.0_x64.zip", "TaskbarX Configurator.exe", true, "tbx.zip");
                tbxinstalled = true;

            }

            else if (tbx11check == DialogResult.No) { SetupProgram("TaskbarX", "https://github.com/ChrisAnd1998/TaskbarX/releases/download/1.7.6.0/TaskbarX_1.7.6.0_x64.zip", "TaskbarX Configurator.exe", true, "tbx.zip"); tbxinstalled = true;}

            else if (tbx11check == DialogResult.Cancel)
            {
                progressBar.Style = ProgressBarStyle.Blocks;
                progressLabel.Text = "Ready";
                tbxinstalled = false;

            }
        }

            private void TrTbLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SetupProgram("TranslucentTB", "https://github.com/TranslucentTB/TranslucentTB/releases/download/2021.5/TranslucentTB.appinstaller", "ttb.appinstaller", false, null);
        }

        private void OsLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SetupProgram("Open-Shell", "https://github.com/Open-Shell/Open-Shell-Menu/releases/download/v4.4.170/OpenShellSetup_4_4_170.exe", "os.exe", false, null);
        }

        private void SibLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SetupProgram("StartIsBack++", "https://startisback.sfo3.cdn.digitaloceanspaces.com/StartIsBackPlusPlus_setup.exe", "sib.exe", false, null);
        }


        void wc_DownloadProgressChanged(Object sender, DownloadProgressChangedEventArgs e)

        {
            progressBar.Value = e.ProgressPercentage;
        }

        private void SabLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SetupProgram("StartAllBack", "https://startisback.sfo3.cdn.digitaloceanspaces.com/StartAllBack_3.4.4_setup.exe", "sib.exe", false, null);
        }

        private void TaskbarTab_Click(object sender, EventArgs e)
        {

        }

        private void S10Link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SetupProgram("Start10", "https://stardock.cachefly.net/Start10-sd-setup.exe", "s10.exe", false, null);
        }

        private void S11Link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

                SetupProgram("Start11", "https://github.com/oliverstech/oliverstech/releases/download/start11-wct/Start11-fs-setup_sd.exe", "s11.exe", false, null);
            
        }
    }

}