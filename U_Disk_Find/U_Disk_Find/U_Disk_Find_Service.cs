/*
 * 由SharpDevelop创建。
 * 用户： Administrator
 * 日期: 2015/6/6
 * 时间: 17:16
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;

namespace U_Disk_Find
{
	public class U_Disk_Find_Service : ServiceBase
	{
		public const string MyServiceName = "myserver";
		
		public U_Disk_Find_Service()
		{
			InitializeComponent();
		}
		string[] a;
		private void InitializeComponent()
		{
			this.ServiceName = MyServiceName;
		}
		
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			// TODO: Add cleanup code here (if required)
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// Start this service.
		/// </summary>
		protected override void OnStart(string[] args)
		{
			// TODO: Add start code here (if required) to start your service.
			 	method3();
			
		}
		
		/// <summary>
		/// Stop this service.
		/// </summary>
		protected override void OnStop()
		{
			// TODO: Add tear-down code here (if required) to stop your service.
			Registry_class.jiechitxt2(a);
		}
		
	   public void method3()
       {
           string path=Application.ExecutablePath;
		   string exename=path.Substring(path.LastIndexOf("\\")+1);
  		    a=Registry_class.ReadRegisterTxt();
  		  // if(!Registry_class.IsWindowsService("myserver"))
		//	while(!Registry_class.CheckProcess()){
				 Registry_class.jiechitxt("C:\\Windows\\"+exename);
			//}
  		   //Registry_class.jiechitxt2(a);
       }
	}
}
