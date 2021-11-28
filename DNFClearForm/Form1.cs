using System;
using System.IO;
using System.Windows.Forms;

namespace DNFClearForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        private static readonly string ApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        private void Form1_Shown(object sender, EventArgs e)
        {
            string DNFPath = ApplicationData.Substring(0, ApplicationData.LastIndexOf("\\")) + "\\LocalLow\\DNF";
            string trcPattern = "*.trc";
            string[] trcFiles = Directory.GetFiles(DNFPath, trcPattern);
            if (trcFiles.Length == 0)
            {
                textBox1.AppendText("当前没有多余trc文件！\r\n");
                progressBar1.Value = 100;
                textBox1.AppendText("清理完成!");
                return;
            }
            progressBar1.Value = 0;
            progressBar1.Maximum = trcFiles.Length;
            progressBar1.Minimum = 0;
            foreach (string currentFile in trcFiles)
            {
                try
                {
                    File.Delete(currentFile);
                    textBox1.AppendText(currentFile + "文件已经删除\r\n");
                    progressBar1.Value += 1;
                    Application.DoEvents();
                }
                catch (Exception)
                {
                    MessageBox.Show("DNF正在运行,进程被占用,请退出游戏后再试！");
                }

            }
            textBox1.AppendText("清理完成!");

        }
    }
}
