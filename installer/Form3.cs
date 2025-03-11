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
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace Wallpaper_Changer_Installer
{
    public partial class Form3 : Form
    {
        [ComImport]
        [Guid("00021401-0000-0000-C000-000000000046")]
        internal class ShellLink
        {
        }

        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid("000214F9-0000-0000-C000-000000000046")]
        internal interface IShellLink
        {
            void GetPath([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszFile, int cchMaxPath, out IntPtr pfd, int fFlags);
            void GetIDList(out IntPtr ppidl);
            void SetIDList(IntPtr pidl);
            void GetDescription([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszName, int cchMaxName);
            void SetDescription([MarshalAs(UnmanagedType.LPWStr)] string pszName);
            void GetWorkingDirectory([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszDir, int cchMaxPath);
            void SetWorkingDirectory([MarshalAs(UnmanagedType.LPWStr)] string pszDir);
            void GetArguments([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszArgs, int cchMaxPath);
            void SetArguments([MarshalAs(UnmanagedType.LPWStr)] string pszArgs);
            void GetHotkey(out short pwHotkey);
            void SetHotkey(short wHotkey);
            void GetShowCmd(out int piShowCmd);
            void SetShowCmd(int iShowCmd);
            void GetIconLocation([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszIconPath, int cchIconPath, out int piIcon);
            void SetIconLocation([MarshalAs(UnmanagedType.LPWStr)] string pszIconPath, int iIcon);
            void SetRelativePath([MarshalAs(UnmanagedType.LPWStr)] string pszPathRel, int dwReserved);
            void Resolve(IntPtr hwnd, int fFlags);
            void SetPath([MarshalAs(UnmanagedType.LPWStr)] string pszFile);
        }

        public Form3()
        {
            InitializeComponent();
        }

        string programinstalldirectory = "C:\\Program Files\\";
        private void Form3_Load(object sender, EventArgs e)
        {
            label2.Text = "Extracting files to temp folder...";

            string tempfolder = "C:\\Users\\" + Environment.UserName + "\\AppData\\Local\\Temp";
            Directory.CreateDirectory(tempfolder + "\\wallpaperchangerinstallertemporaryfiles");
            string resourcefolder = tempfolder + "\\wallpaperchangerinstallertemporaryfiles";

            string programfilesfolder = "C:\\Program Files";
            Directory.CreateDirectory(programfilesfolder + "\\wallpaper-changer-v1.0");

            File.WriteAllBytes(resourcefolder + "\\app.exe", Properties.Resources.app);
            File.WriteAllBytes(resourcefolder + "\\wallpaper1.jpg", Properties.Resources.wallpaper1);
            File.WriteAllBytes(resourcefolder + "\\wallpaper2.jpg", Properties.Resources.wallpaper2);
            File.WriteAllBytes(resourcefolder + "\\wallpaper3.jpg", Properties.Resources.wallpaper3);
            File.WriteAllBytes(resourcefolder + "\\wallpaper4.jpg", Properties.Resources.wallpaper4);
            File.WriteAllBytes(resourcefolder + "\\wallpaper5.jpg", Properties.Resources.wallpaper5);
            File.WriteAllBytes(resourcefolder + "\\wallpaper6.png", Properties.Resources.wallpaper6);

            timer1.Start();
            timer2.Start();
        }

        public void createshortcut()
        {
            IShellLink link = (IShellLink)new ShellLink();

            link.SetDescription("Choose from wallpapers included with the program.");
            link.SetPath("C:\\Program Files\\wallpaper-changer-v1.0\\app.exe");

            IPersistFile file = (IPersistFile)link;
            string desktopPath = "C:\\Users\\" + Environment.UserName + "\\Desktop";
            file.Save(Path.Combine(desktopPath, "Wallpaper Changer.lnk"), false);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value < 100)
            {
                progressBar1.Value = progressBar1.Value + 1;
                label1.Text = "Installing " + progressBar1.Value + "%";
                this.Text = "Installing " + progressBar1.Value + "%";
            }
            else if (progressBar1.Value == 100)
            {
                createshortcut();
                label1.Text = "Installation finished.";
                progressBar1.Visible = false;
                label2.Visible = false;
                button1.Visible = true;
                timer1.Stop();
            }
        }

        public void movetoinstalldirectory()
        {
            string resourcefolder = "C:\\Users\\" + Environment.UserName + "\\AppData\\Local\\Temp\\wallpaperchangerinstallertemporaryfiles";
            string programinstalldirectory = "C:\\Program Files\\wallpaper-changer-v1.0";

            File.Move(resourcefolder + "\\app.exe", programinstalldirectory + "\\app.exe");
            Directory.CreateDirectory(programinstalldirectory + "\\wallpapers");
            File.Move(resourcefolder + "\\wallpaper1.jpg", programinstalldirectory + "\\wallpapers\\wallpaper1.jpg");
            File.Move(resourcefolder + "\\wallpaper2.jpg", programinstalldirectory + "\\wallpapers\\wallpaper2.jpg");
            File.Move(resourcefolder + "\\wallpaper3.jpg", programinstalldirectory + "\\wallpapers\\wallpaper3.jpg");
            File.Move(resourcefolder + "\\wallpaper4.jpg", programinstalldirectory + "\\wallpapers\\wallpaper4.jpg");
            File.Move(resourcefolder + "\\wallpaper5.jpg", programinstalldirectory + "\\wallpapers\\wallpaper5.jpg");
            File.Move(resourcefolder + "\\wallpaper6.png", programinstalldirectory + "\\wallpapers\\wallpaper6.png");
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            string programinstalldirectory = "C:\\Program Files\\wallpaper-changer-v1.0";

            label2.Text = "Copying files to " + programinstalldirectory;
            timer3.Start();
            movetoinstalldirectory();
            timer2.Stop();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            label2.Text = "Deleting temporary files...";
            timer3.Stop();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
