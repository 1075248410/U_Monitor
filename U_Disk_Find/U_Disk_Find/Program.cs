/*
 * 由SharpDevelop创建。
 * 用户： Administrator
 * 日期: 2015/6/1
 * 时间: 18:28
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Windows.Forms;
using System.ServiceProcess;
using System.Diagnostics;


namespace U_Disk_Find
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	internal sealed class Program
	{
		/// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			
			if (args.Length > 0 && args[0] == "s")
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] 
                { 
                    new  U_Disk_Find_Service(),
                };
                ServiceBase.Run(ServicesToRun);
            }
			else{
			    Application.EnableVisualStyles();
			    Application.SetCompatibleTextRenderingDefault(false);
			    Application.Run(new MainForm());
			}
			
		  
		
		}
	   }
}
