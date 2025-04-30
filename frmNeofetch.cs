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

namespace Neofetch
{
    public partial class frmNeofetch : Form
    {
        public frmNeofetch()
        {
            InitializeComponent();
            this.BackColor = ColorTranslator.FromHtml("#260b36"); // Usar un color oscuro con el valor hexadecimal
            this.Opacity = 0.95; // Ajustar la opacidad del formulario (90%)
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            lblOS.Text = GetOS();              // Muestra el Sistema Operativo
            lblCPU.Text = GetCPU();            // Muestra el CPU
            lblRAM.Text = GetRAM();            // Muestra la RAM
            lblGPU.Text = GetGPU();            // Muestra la GPU
            lblHostModel.Text = GetHostModel(); // Muestra el modelo del host
            lblResolution.Text = GetResolution(); // Muestra la resolución
            lblUptime.Text = GetUptime();       // Muestra el uptime
            lblKernel.Text = GetKernel();       // Muestra la versión del kernel
            lblWM.Text = GetWM();               // Muestra la versión de Explorer
            lblPackages.Text = GetPackages();   // Muestra la cantidad de programas instalados
            lblDiskSpace.Text = GetDiskSpace(); // Espacio en disco
            lblMotherboard.Text = GetMotherboardInfo();   // Placa base
            lblUserInfo.Text = GetUserInfo();

            panelImage.BackColor = Color.Transparent; // Asegúrate de que el Panel sea transparente
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage; // Ajusta la imagen al tamaño del PictureBox
        }

        // Método para obtener el Sistema Operativo
        private string GetOS()
        {
            return System.Environment.OSVersion.ToString();
        }

        // Método para obtener el CPU
        private string GetCPU()
        {
            string cpu = "";
            var searcher = new System.Management.ManagementObjectSearcher("select * from Win32_Processor");
            foreach (var item in searcher.Get())
            {
                cpu = item["Name"].ToString();
            }
            return cpu;
        }

        private string GetDiskSpace()
        {
            DriveInfo drive = new DriveInfo("C"); // Cambia la letra si deseas obtener información de otra unidad
            return $"{drive.TotalSize / (1024 * 1024 * 1024)} GB Total, {drive.TotalFreeSpace / (1024 * 1024 * 1024)} GB Free";
        }

        private string GetMotherboardInfo()
        {
            string info = "";
            var searcher = new System.Management.ManagementObjectSearcher("select * from Win32_BaseBoard");
            foreach (var item in searcher.Get())
            {
                info = $"{item["Manufacturer"]} {item["Product"]}";
            }
            return info;
        }

        // Método para obtener la Resolución
        private string GetResolution()
        {
            return Screen.PrimaryScreen.Bounds.Width.ToString() + "x" + Screen.PrimaryScreen.Bounds.Height.ToString();
        }

        // Método para obtener el Uptime
        private string GetUptime()
        {
            var uptime = TimeSpan.FromMilliseconds(Environment.TickCount);
            return $"{uptime.Days} days, {uptime.Hours} hours, {uptime.Minutes} minutes";
        }

        // Método para obtener la versión del Kernel
        private string GetKernel()
        {
            return System.Environment.OSVersion.VersionString;
        }

        // Método para obtener la versión de Explorer
        private string GetWM()
        {
            return System.Diagnostics.FileVersionInfo.GetVersionInfo(@"C:\Windows\explorer.exe").ProductVersion;
        }

        // Método para obtener la RAM
        private string GetRAM()
        {
            var searcher = new System.Management.ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem");
            foreach (var item in searcher.Get())
            {
                return Math.Round(Convert.ToDouble(item["TotalPhysicalMemory"]) / (1024 * 1024 * 1024), 2).ToString() + " GB";
            }
            return "Unknown";
        }

        // Método para obtener la cantidad de programas instalados
        private string GetPackages()
        {
            int count = 0;
            var searcher = new System.Management.ManagementObjectSearcher("SELECT * FROM Win32_Product");
            foreach (var item in searcher.Get())
            {
                count++;
            }
            return count.ToString() + " Installed Programs";
        }

        // Método para obtener la GPU
        private string GetGPU()
        {
            string gpu = "";
            var searcher = new System.Management.ManagementObjectSearcher("select * from Win32_VideoController");
            foreach (var item in searcher.Get())
            {
                gpu = item["Name"].ToString();
            }
            return gpu;
        }

        // Método para obtener el modelo del host
        private string GetHostModel()
        {
            string model = "";
            var searcher = new System.Management.ManagementObjectSearcher("select * from Win32_ComputerSystem");
            foreach (var item in searcher.Get())
            {
                model = item["Manufacturer"].ToString() + " " + item["Model"].ToString();
            }
            return model;
        }

        private string GetUserInfo()
        {
            string userName = Environment.UserName; // Obtiene el nombre del usuario actual
            string hostName = Environment.MachineName; // Obtiene el nombre de la computadora
            string customDirectory = @"C:\Program Files (x86)"; // Ruta personalizada
            return $"{userName}@{hostName} {customDirectory}";
        }
    }
}
