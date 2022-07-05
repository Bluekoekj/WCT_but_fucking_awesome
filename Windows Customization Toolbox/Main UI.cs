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
using Microsoft.Win32;
namespace Windows_Customization_Toolbox
{
    public partial class Main_UI : Form
    {

        public Main_UI()
        {
            InitializeComponent();
        }

        bool tbxinstalled;
        bool rainmeterinstalled;
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


        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Process.Start("https://short.oliverstech.tk/WtcGit");
        }

        private void pictureBox5_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.Hand;
            ImageToolTip.SetToolTip(pictureBox5, "View project on GitHub");
        }

        void RmSkinSetup(string link, string name, string output)
        {
            if (rainmeterinstalled == false)
            {
                var setupskinchoice = MessageBox.Show("Rainmeter hasn't been installed with WCT, but may have been installed otherwise. Would you like to install the skin anyway?", "Rainmeter Skin Installation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (setupskinchoice == DialogResult.Yes)
                {
                    DownloadFile(link, name, output);
                    wait(2000);

                    progressBar.Style = ProgressBarStyle.Marquee;
                    progressLabel.Text = "Installing " + name + " skin";
                    Process.Start(output);
                    progressBar.Style = ProgressBarStyle.Blocks;
                    progressLabel.Text = "Installed " + name + " skin. Ready.";
                }

            }
            else
            {
                DownloadFile(link, name, output);
                wait(2000);

                progressBar.Style = ProgressBarStyle.Marquee;
                progressLabel.Text = "Installing " + name + "skin";
                Process.Start(output);
                progressBar.Style = ProgressBarStyle.Blocks;
                progressLabel.Text = "Installed " + name + "skin. Ready.";
            }
        }

        void DownloadFile(string link, string name, string output)
        {
            progressBar.Style = ProgressBarStyle.Marquee;
            progressLabel.Text = "Preparing to download " + name;
            WebClient wc = new WebClient();
            wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wc_DownloadProgressChanged);

            progressBar.Style = ProgressBarStyle.Blocks;
            progressLabel.Text = "Downloading " + name;
            wc.DownloadFileAsync(new Uri(link), output);

        }

        void SetupProgram(string name, string link, string exec, bool reqextract, string zip)
        {

            // Using alternate download scheme than method due to ZIP file checks...
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
                                MessageBox.Show("'" + exec + "' couldn't be executed due to another program using it.\nDon't worry, this can be resolved by launching it yourself when you finish with WCT.\nTechnical error: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            else if (tbx11check == DialogResult.No) { SetupProgram("TaskbarX", "https://github.com/ChrisAnd1998/TaskbarX/releases/download/1.7.6.0/TaskbarX_1.7.6.0_x64.zip", "TaskbarX Configurator.exe", true, "tbx.zip"); tbxinstalled = true; }

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

            SetupProgram("Start11", "https://github.com/oliverstech/oliverstech/releases/download/start11-wct/Start11-fs-setup_sd.exe", "s11.exe", false, null); //using link from another repo > releases, since github has a file size of 25MB in the direct repo :P

        }

        private void pictureBox12_Move(object sender, EventArgs e)
        {

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SetupProgram("Rainmeter", "https://github.com/rainmeter/rainmeter/releases/download/v4.5.13.3632/Rainmeter-4.5.13.exe", "rmtr.exe", false, null);
            rainmeterinstalled = true;
        }

        private void Desktop_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox12_MouseMove(object sender, MouseEventArgs e)
        {
            ImageToolTip.SetToolTip(pictureBox12, "Supports Windows 7 and above");
            ImageToolTip.SetToolTip(pictureBox13, "Supports Windows 7 and above");
            ImageToolTip.SetToolTip(pictureBox14, "Supports Windows 7 and above");
            ImageToolTip.SetToolTip(pictureBox15, "Supports Windows 7 and above");
        }

        private void WphLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://wallpaperhub.app");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://visualskins.com/");
        }

        private void pictureBox17_MouseMove(object sender, MouseEventArgs e)
        {

            ImageToolTip.SetToolTip(pictureBox19, "Supports Windows 7 and above");
            ImageToolTip.SetToolTip(pictureBox18, "Supports Windows 7 and above");
            ImageToolTip.SetToolTip(pictureBox17, "Supports Windows 7 and above");
            ImageToolTip.SetToolTip(pictureBox16, "Supports Windows 7 and above");

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("rundll32.exe", "shell32.dll,Control_RunDLL desk.cpl,,2");
        }

        private void pictureBox20_MouseMove(object sender, MouseEventArgs e)
        {
            ImageToolTip.SetToolTip(pictureBox20, "Supports Windows 7 and above");
            ImageToolTip.SetToolTip(pictureBox21, "Supports Windows 7 and above");
            ImageToolTip.SetToolTip(pictureBox22, "Supports Windows 7 and above");
            ImageToolTip.SetToolTip(pictureBox23, "Supports Windows 7 and above");
        }

        private void pictureBox26_MouseMove(object sender, MouseEventArgs e)
        {
            ImageToolTip.SetToolTip(pictureBox26, "Supports Windows 7-10");
            ImageToolTip.SetToolTip(pictureBox25, "Supports Windows 7-10");
            ImageToolTip.SetToolTip(pictureBox24, "Supports Windows 7-10");
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SetupProgram("Sidebar Diagnostics", "https://github.com/ArcadeRenegade/SidebarDiagnostics/releases/download/v3.6.2/Setup.exe", "sdiag.exe", false, null);
        }

        private void linkLabel2_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("desk.cpl", " ,5");
        }

        int tweakadded = 0;
        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            // Delete any existing files
            if (File.Exists("DisplayVersionShow.reg")) { File.Delete("DisplayVersionShow.reg"); };
            if (File.Exists("DisplayVersionHide.reg")) { File.Delete("DisplayVersionHide.reg"); };
            if (File.Exists("EnterPush.exe")) { File.Delete("DisplayVersionShow.exe"); };


            progressBar.Style = ProgressBarStyle.Marquee;
            progressLabel.Text = "Writing tweak to registry";

            MessageBox.Show("After pressing OK, please don't press any buttons until you see UAC. After that, press Yes and OK on all message boxes.", "Note", MessageBoxButtons.OK, MessageBoxIcon.Information);

            DownloadFile("https://github.com/oliverstech/WindowsCustomizationToolbox/raw/main/files/EnterPush.exe", "EnterPusher", "EnterPush.exe");

            if (tweakadded == 0) 
            {
                
                DownloadFile("https://github.com/oliverstech/WindowsCustomizationToolbox/raw/main/files/DisplayVersionShow.reg", "DisplayVersionShower", "DisplayVersionShow.reg");                

                wait(2000);
                Process.Start("explorer", "/select,"+Directory.GetCurrentDirectory()+"\\DisplayVersionShow.reg"); // Show reg file in explorer; select it
                Random rnd = new Random();
                wait(rnd.Next(1000, 7500)); // waits a random length between 2000-7500ms (to give HDDs a chance)
                Process.Start("EnterPush.exe"); // run it by pushing enter
                tweakadded = 1;

                progressLabel.Text = "Version info now showing. Ready";
            }
            else {

                DownloadFile("https://github.com/oliverstech/WindowsCustomizationToolbox/raw/main/files/DisplayVersionHide.reg", "DisplayVersionHider", "DisplayVersionHide.reg");
                
                wait(2000);
                Process.Start("explorer", "/select,"+Directory.GetCurrentDirectory()+"\\DisplayVersionHide.reg");
                Random rnd = new Random();
                wait(rnd.Next(1000, 7500));
                Process.Start("EnterPush.exe");

                tweakadded = 0;

                progressLabel.Text = "Version info no longer showing. Ready.";
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click_1(object sender, EventArgs e)
        {

        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RmSkinSetup("https://github.com/oliverstech/WindowsCustomizationToolbox/raw/main/files/Bluesh_.rmskin", "Blueish", "blueish.rmskin");
        }



        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RmSkinSetup("https://visualskins.com/media/p/667/cold.rmskin", "Cold", "cold.rmskin");
        }

        private void linkLabel9_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RmSkinSetup("https://github.com/tjmarkham/win10widgets/releases/download/1.0.0/Win10.Widgets_1.0.0.rmskin", "Win10Widgets", "win10widgets.rmskin");
        }

        int logoclicked = 1;
        private void pictureBox34_DoubleClick(object sender, EventArgs e)
        {
            // sneaky easter egg??                       

            if (logoclicked == 1 || logoclicked == 2 || logoclicked == 3)
            {
                MessageBox.Show("A project by Oliver's Tech Corner. :)");
            }

            if (logoclicked == 4)
            {
                MessageBox.Show("There's no point in you still clicking here lmao");
            }
            if (logoclicked == 5)
            {
                MessageBox.Show("Why are you still clicking this?");
            }
            if (logoclicked == 6)
            {
                MessageBox.Show("Don't you have something better to do with your life?");
            }
            if (logoclicked == 7)
            {
                MessageBox.Show("Click this again, I dare you.");
            }
            if (logoclicked == 8)
            {
                MessageBox.Show("...oh");
            }
            if (logoclicked == 9)
            {
                MessageBox.Show("STOPPPPP");
            }
            if (logoclicked == 10)
            {
                Process.Start("http://gg.gg/11nz22");
                logoclicked = 0;
                MessageBox.Show("You asked for this...");
            }

            logoclicked++;
        }
    }
}