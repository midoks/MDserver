using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;

namespace MDService
{
    public partial class MDService : ServiceBase
    {

        private static string BaseDir = System.AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "/");

        public MDService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // TODO: 在此处添加代码以启动服务。
            System.Timers.Timer timer = new System.Timers.Timer(1000);
            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_main_Tick);
            timer.Enabled = true;
            timer.AutoReset = false;
        }

        protected override void OnStop()
        {
            // TODO: 在此处添加代码以执行停止服务所需的关闭操作。
        }

        //执行cmd命令
        private void Wcmd(string cmdtext)
        {
            Process Tcmd = new Process();
            Tcmd.StartInfo.FileName = "cmd.exe";//设定程序名 
            Tcmd.StartInfo.UseShellExecute = false;//关闭Shell的使用 
            Tcmd.StartInfo.RedirectStandardInput = true; //重定向标准输入 
            Tcmd.StartInfo.RedirectStandardOutput = true;//重定向标准输出 
            Tcmd.StartInfo.RedirectStandardError = true;//重定向错误输出 
            Tcmd.StartInfo.CreateNoWindow = true;//设置不显示窗口 
            Tcmd.StartInfo.Arguments = "/c " + cmdtext;
            //Tcmd.StandardInput.WriteLine("exit");
            //Tcmd.WaitForExit();
            Tcmd.Start();//执行VER命令 
            //string str = Tcmd.StandardOutput.ReadToEnd();
            //MessageBox.Show(str);
            Tcmd.Close();
        }

        private void timer_main_Tick(object sender, EventArgs e)
        {

            Process[] localByName = Process.GetProcessesByName("MDserver.exe");
            if (0 == localByName.Length)
            {
                Process.Start(BaseDir + "MDserver.exe");
            }
            //System.Diagnostics.Process.Start(BaseDir + "MDserver.exe");
            //Wcmd(BaseDir + "MDserver.exe");
        }


    }
}
