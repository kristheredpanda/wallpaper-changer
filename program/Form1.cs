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
using System.Threading;
using System.Runtime.InteropServices;

namespace Wallpaper_Changer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public class wallpaper
        {
            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            private static extern Int32 SystemParametersInfo(UInt32 action, UInt32 uParam, string vParam, UInt32 winIni);

            public static void change(string path)
            {
                SystemParametersInfo(0x14, 0, path, 0x01 | 0x02);
            }
        }

        public void loadfwscriptstage1()
        {
            if (!Directory.Exists(Application.StartupPath + "\\scripts"))
            {
                Directory.CreateDirectory(Application.StartupPath + "\\scripts");
                loadfwscriptstage2();
            }
            else if (Directory.Exists(Application.StartupPath + "\\scripts"))
            {
                loadfwscriptstage2();
            }
        }

        public void loadfwscriptstage2()
        {
            string fwscriptlocation = Application.StartupPath + "\\scripts\\findwallpaper.vbs";

            File.Create(fwscriptlocation).Close();

            StreamWriter sw = new StreamWriter(fwscriptlocation);
            sw.WriteLine("Const HKCU = &H80000001 'HKEY_CURRENT_USER");
            sw.WriteLine("");
            sw.WriteLine("sComputer = \".\"");
            sw.WriteLine("");
            sw.WriteLine("Set oReg=GetObject(\"winmgmts:{impersonationLevel=impersonate}!\\\\\" _");
            sw.WriteLine("            & sComputer & \"\\root\\default:StdRegProv\")");
            sw.WriteLine("");
            sw.WriteLine("sKeyPath = \"Control Panel\\Desktop\\\"");
            sw.WriteLine("sValueName = \"TranscodedImageCache\"");
            sw.WriteLine("oReg.GetBinaryValue HKCU, sKeyPath, sValueName, sValue");
            sw.WriteLine("");
            sw.WriteLine("");
            sw.WriteLine("sContents = \"\"");
            sw.WriteLine("");
            sw.WriteLine("For i = 24 To UBound(sValue)");
            sw.WriteLine("  vByte = sValue(i)");
            sw.WriteLine("  If vByte <> 0 And vByte <> \"\" Then");
            sw.WriteLine("    sContents = sContents & Chr(vByte)");
            sw.WriteLine("  End If");
            sw.WriteLine("Next");
            sw.WriteLine("");
            sw.WriteLine("Set fso = CreateObject(\"Scripting.FileSystemObject\")");
            sw.WriteLine("Set fl = fso.OpenTextFile(\"" + Application.StartupPath + "\\currentwallpaperoutput.txt\", 2, True)");
            sw.WriteLine("fl.Write(sContents)");
            sw.WriteLine("fl.Close : Set fl = Nothing");
            sw.WriteLine("Set fso = Nothing");

            sw.Close();

            System.Diagnostics.Process.Start(Application.StartupPath + "\\scripts\\findwallpaper.vbs");
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            loadfwscriptstage3();
            timer1.Stop();
        }

        public void loadfwscriptstage3()
        {
            string cwolocation = Application.StartupPath + "\\currentwallpaperoutput.txt";

            StreamReader sr = new StreamReader(cwolocation);
            string wallpaperdirectory = sr.ReadLine();

            sr.Close();

            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;

            pictureBox1.Image = Image.FromFile(wallpaperdirectory);
        }

        private void PopulateListBox(ListBox lsb, string Folder, string FileType)
        {
            DirectoryInfo dinfo = new DirectoryInfo(Folder);
            FileInfo[] Files = dinfo.GetFiles(FileType);
            foreach (FileInfo file in Files)
            {
                lsb.Items.Add(file.Name);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadfwscriptstage1();
            PopulateListBox(listBox1, Application.StartupPath + "\\wallpapers", "*.jpg");
            PopulateListBox(listBox1, Application.StartupPath + "\\wallpapers", "*.png");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            PopulateListBox(listBox1, Application.StartupPath + "\\wallpapers", "*.jpg");
            PopulateListBox(listBox1, Application.StartupPath + "\\wallpapers", "*.png");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string wallpapersfolder = Application.StartupPath + "\\wallpapers";

            if (listBox1.SelectedIndex == 0)
            {
                wallpaper.change(wallpapersfolder + "\\wallpaper1.jpg");
                pictureBox1.Image = Image.FromFile(wallpapersfolder + "\\wallpaper1.jpg");
            }

            if (listBox1.SelectedIndex == 1)
            {
                wallpaper.change(wallpapersfolder + "\\wallpaper2.jpg");
                pictureBox1.Image = Image.FromFile(wallpapersfolder + "\\wallpaper2.jpg");
            }

            if (listBox1.SelectedIndex == 2)
            {
                wallpaper.change(wallpapersfolder + "\\wallpaper3.jpg");
                pictureBox1.Image = Image.FromFile(wallpapersfolder + "\\wallpaper3.jpg");
            }

            if (listBox1.SelectedIndex == 3)
            {
                wallpaper.change(wallpapersfolder + "\\wallpaper4.jpg");
                pictureBox1.Image = Image.FromFile(wallpapersfolder + "\\wallpaper4.jpg");
            }

            if (listBox1.SelectedIndex == 4)
            {
                wallpaper.change(wallpapersfolder + "\\wallpaper5.jpg");
                pictureBox1.Image = Image.FromFile(wallpapersfolder + "\\wallpaper5.jpg");
            }

            if (listBox1.SelectedIndex == 5)
            {
                wallpaper.change(wallpapersfolder + "\\wallpaper6.png");
                pictureBox1.Image = Image.FromFile(wallpapersfolder + "\\wallpaper6.png");
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string wallpapersfolder = Application.StartupPath + "\\wallpapers";

            if (listBox1.SelectedIndex == 0)
            {
                pictureBox2.Image = Image.FromFile(wallpapersfolder + "\\wallpaper1.jpg");
            }

            if (listBox1.SelectedIndex == 1)
            {
                pictureBox2.Image = Image.FromFile(wallpapersfolder + "\\wallpaper2.jpg");
            }

            if (listBox1.SelectedIndex == 2)
            {
                pictureBox2.Image = Image.FromFile(wallpapersfolder + "\\wallpaper3.jpg");
            }

            if (listBox1.SelectedIndex == 3)
            {
                pictureBox2.Image = Image.FromFile(wallpapersfolder + "\\wallpaper4.jpg");
            }

            if (listBox1.SelectedIndex == 4)
            {
                pictureBox2.Image = Image.FromFile(wallpapersfolder + "\\wallpaper5.jpg");
            }

            if (listBox1.SelectedIndex == 5)
            {
                pictureBox2.Image = Image.FromFile(wallpapersfolder + "\\wallpaper6.png");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/kristheredpanda/wallpaper-changer");
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
        }
    }
}
