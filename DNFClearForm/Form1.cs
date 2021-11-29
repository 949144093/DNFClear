using System;
using System.IO;
using System.Windows.Forms;
using System.Configuration;

namespace DNFClearForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        private static readonly string ApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);



        #region 直接删除指定目录下的所有文件及文件夹(保留目录)
        /// <summary>
        /// 直接删除指定目录下的所有文件及文件夹(保留目录)
        /// </summary>
        /// <param name="strPath">文件夹路径</param>
        /// <returns>执行结果</returns>
        public bool DeleteDir(string strPath)
        {
            try
            {
                // 清除空格
                 strPath = @strPath.Trim().ToString();
                // 判断文件夹是否存在
                if (System.IO.Directory.Exists(strPath))
                {
                    // 获得文件夹数组
                    string[] strDirs = System.IO.Directory.GetDirectories(strPath);
                    // 获得文件数组
                    string[] strFiles = System.IO.Directory.GetFiles(strPath);
                    // 遍历所有子文件夹
                    foreach (string strFile in strFiles)
                    {
                        // 删除文件
                        if (!strFile.EndsWith("DNF.cfg"))
                        {
                            System.IO.File.Delete(strFile);
                            textBox1.AppendText(strFile + "文件已经删除\r\n");
                        }
                    }
                    // 遍历所有文件
                    foreach (string strdir in strDirs)
                    {
                        // 删除文件夹
                        System.IO.Directory.Delete(strdir, true);
                        textBox1.AppendText(strdir + "文件夹已经删除\r\n");
                    }
                }
                // 成功
                return true;
            }
            catch (Exception Exp) // 异常处理
            {
                // 异常信息
                MessageBox.Show("文件占用，请关闭游戏和wegame后重试！");
                // 失败
                return false;
            }
        }
        public void DeleteDir(string strPath,string strPattern)
        {
            try
            {
                string[] strFiles = Directory.GetFiles(strPath, strPattern);
                if (strFiles.Length != 0)
                {
                    foreach (string item in strFiles)
                    {
                        try
                        {
                            File.Delete(item);
                            textBox1.AppendText(item + "文件已经删除\r\n");
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("文件占用，请关闭游戏和wegame后重试！");
                        }

                    }
                }
            }
            catch (Exception e)
            {
                textBox1.AppendText("当前路径为空，无需清理。\r\n");
            }
            
        }
        #endregion
        private void Clear()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (Directory.Exists(config.AppSettings.Settings["folder"].Value))//判断配置的路径是否存在
            {
                dialog.SelectedPath = config.AppSettings.Settings["folder"].Value;//若路径存在则自动获取该路径
                textBox2.Text = dialog.SelectedPath.ToString();
            }
            //第一次打开软件需配置DNF安装目录
            else
            {
                if (dialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    string _path = dialog.SelectedPath;//将用户选取的路径值赋值给变量       
                    config.AppSettings.Settings["folder"].Value = _path; //将用户选取的路径_path赋给app.config中的_path(名称自取)     
                    config.Save(ConfigurationSaveMode.Modified);       //将配置保存
                    textBox2.Text = dialog.SelectedPath.ToString();                              //
                }
                System.Configuration.ConfigurationManager.RefreshSection("appSettings");//刷新配置文件  
            }

            string DNFClearPath1 = ApplicationData.Substring(0, ApplicationData.LastIndexOf("\\")) + "\\LocalLow\\DNF";
            string DNFClearPath2 = ApplicationData.Substring(0, ApplicationData.LastIndexOf("\\")) + "\\Roaming\\Tencent\\dnf_asura\\config";
            string DNFClearPath3 = ApplicationData.Substring(0, ApplicationData.LastIndexOf("\\")) + "\\Roaming\\Tencent\\Logs";
            string DNFClearPath4 = ApplicationData.Substring(0, ApplicationData.LastIndexOf("\\")) + "\\Local\\Temp\\qbcore\\cache";

            
            string DnfSetupPath = textBox2.Text;
            try
            {
                DirectoryInfo di = new DirectoryInfo("C:\\Program Files\\AntiCheatExpert");
                di.Delete(true);
                textBox1.AppendText("C:\\Program Files\\AntiCheatExpert文件夹已经删除\r\n");
            }
            catch (Exception e)
            {
                textBox1.AppendText("C:\\Program Files\\AntiCheatExpert文件夹删除失败或为空，请以管理员身份启动软件！\r\n");
            }


            DeleteDir(DNFClearPath1);
            DeleteDir(DNFClearPath2);
            DeleteDir(DNFClearPath3);
            DeleteDir(DNFClearPath4);


            DeleteDir(DnfSetupPath, "*.log");
            DeleteDir(DnfSetupPath, "*_tmp.dat");
            DeleteDir(DnfSetupPath, "*_tmp.dat - journal");
            DeleteDir(DnfSetupPath, "Thread *.z");
            DeleteDir(DnfSetupPath, "debug.log");
            DeleteDir(DnfSetupPath, "BugTrace.log");
            DeleteDir(DnfSetupPath, "file.txt");
            DeleteDir(DnfSetupPath, "LagLog.txt");

            DeleteDir(DnfSetupPath, "gameloader.log");
            DeleteDir(DnfSetupPath, "CrashDNF2.cra");
            DeleteDir(DnfSetupPath, "NetworkDump.dmp");
            DeleteDir(DnfSetupPath, "vsddrvdll *.org");

            DeleteDir(DnfSetupPath + "\\AntiCheatExpert\\InGame\\x64", "*.rc");
            DeleteDir(DnfSetupPath + "\\AntiCheatExpert\\InGame\\x64", "*.org");
            DeleteDir(DnfSetupPath + "\\AntiCheatExpert\\InGame\\x64", "*.bh");
            DeleteDir(DnfSetupPath + "\\start\\Cross\\Log","*.*");

            DeleteDir(DnfSetupPath + "\\start\\Cross\\Core\\Stable\\log", "tgp_web_log.tlg");
            DeleteDir(DnfSetupPath + "\\start\\Cross\\Core\\Stable\\Logs", "CrossProxy.tlg.*");
            DeleteDir(DnfSetupPath + "\\start\\TenProtect\\patchs","*.*");

            DeleteDir(DnfSetupPath + "\\TCLS\\TenProtect", "*.dmp.z");
            DeleteDir(DnfSetupPath + "\\TCLS\\TenProtect\\TP", "Installer.log");
            DeleteDir(DnfSetupPath + "\\TCLS\\TenProtect\\TP\\TPHelper", "*.dmp.z");
            DeleteDir(DnfSetupPath + "\\TCLS\\ALog", "*.log");
            DeleteDir(DnfSetupPath + "\\TCLS\\tLog", "*.log");
            DeleteDir(DnfSetupPath + "\\TCLS\\tLog\\Repair", "*.log");
            DeleteDir(DnfSetupPath + "\\TCLS\\log", "*.*");


            textBox1.AppendText("清理完成!\r\n"); 
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            Clear();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (dialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                string _path = dialog.SelectedPath;//将用户选取的路径值赋值给变量       
                config.AppSettings.Settings["folder"].Value = _path; //将用户选取的路径_path赋给app.config中的_path(名称自取)     
                config.Save(ConfigurationSaveMode.Modified);       //将配置保存
                textBox2.Text = dialog.SelectedPath.ToString();                              //
            }
            System.Configuration.ConfigurationManager.RefreshSection("appSettings");//刷新配置文件 

            Clear();
        }
    }
}
