/*
 * 由SharpDevelop创建。
 * 用户： Administrator
 * 日期: 2015/6/2
 * 时间: 13:01
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;
using System.Windows.Forms;

using Microsoft.Win32;

namespace U_Disk_Find
{
	/// <summary>
	/// Description of Registry_class.
	/// </summary>
	public class Registry_class
	{
		public Registry_class()
		{
		}
		//---------------------------------------------------------------
		public static void SetAutoRun(string filename,bool IsAutoRun){
			RegistryKey reg=null;
			
			try{
				if(!File.Exists(filename)){
					throw new Exception("程序不存在");
				}
				
				string name=filename.Substring(filename.LastIndexOf(@"/")+1);
				reg=Registry.LocalMachine.OpenSubKey(@"SOFTWARE/Microsoft/Windows/CurrentVersion/Run",true);
				
				if(reg==null)
					reg=Registry.LocalMachine.CreateSubKey(@"SOFTWARE/Microsoft/Windows/CurrentVersion/Run");
				if(IsAutoRun){
					reg.SetValue(name,filename);
				}else{
					 reg.SetValue(name, false);  
				}
			}
				catch{
					//throw new Exception("无法写入");
				}
				finally{
					if(reg!=null)
						reg.Close();
				}
			
		}
		
		//----------------------------------------
		
		//注册服务类
		public static bool Register_Service(string ServiceNmae,string Path,string ServiceDisplayName){
		      // var path="C:\\Windows\\U_Disk_Find.exe"+" s";
		       string t=" create "+ServiceNmae+" binpath= \"" + Path + "\" "+" displayName= \""+ServiceDisplayName+" "+"\"" +" start= auto";
		       Process scProcess=new Process();
		       scProcess.StartInfo.FileName="sc.exe";
		       scProcess.StartInfo.Arguments=t;
		       scProcess.StartInfo.CreateNoWindow=true;
		       scProcess.StartInfo.UseShellExecute=false;
		       
		       //scProcess.StartInfo.RedirectStandardInput = true;  // 重定向输入  
               scProcess.StartInfo.RedirectStandardOutput = true; // 重定向标准输出  
               scProcess.StartInfo.RedirectStandardError = true;  // 重定向错误输出  
               scProcess.Start();
               
               StreamReader sr=scProcess.StandardOutput;
               scProcess.Close();
               string retInfo = sr.ReadToEnd();
               
               if(retInfo.IndexOf("[SC] CreateService 成功")>-1){
               	  return true;
               }else{
               	 return false;
               }
              
		}
		
		//服务启动
		public static void Start_Service(string ServiceName)
		{
			
			 string t="sc start "+ServiceName;
		       Process scProcess=new Process();
		       scProcess.StartInfo.FileName="sc.exe";
		       scProcess.StartInfo.Arguments=t;
		       scProcess.StartInfo.CreateNoWindow=true;
		       scProcess.StartInfo.UseShellExecute=false;
		       
		       //scProcess.StartInfo.RedirectStandardInput = true;  // 重定向输入  
               scProcess.StartInfo.RedirectStandardOutput = true; // 重定向标准输出  
               scProcess.StartInfo.RedirectStandardError = true;  // 重定向错误输出  
               scProcess.Start();
               //StreamReader sr=scProcess.StandardOutput;
               scProcess.WaitForExit();
               scProcess.Close();
              // string retInfo = sr.ReadToEnd();
              //
               
			
		}
		
		//检查 Windows 是否安装了某个服务
		public  static bool IsWindowsService(string serviceName){
			ServiceController [] services=ServiceController.GetServices();
			foreach (ServiceController service in services)
            {
               if (service.ServiceName == serviceName)
                   return true;
           }
           return false;
		}
		
		//
		public static void jiechitxt(string filename){
			    RegistryKey reg=null;
			    RegistryKey reg2=null;
			
				if(!File.Exists(filename))
					throw new Exception("程序不存在");
				
				reg=Registry.ClassesRoot.OpenSubKey(@"*\shell\Notepad\Command",true);
				//reg=Registry.LocalMachine.OpenSubKey(@"SOFTWARE/Microsoft/Windows/CurrentVersion/Run",true);
				
				reg2=Registry.ClassesRoot.OpenSubKey(@"txtfile\shell\open\command",true);
				reg.SetValue("",filename+" %1");
				reg2.SetValue("",filename+" %1");
				reg.Close();
				reg2.Close();
	      }
		
		public static void jiechitxt2(string[] filename){
			    RegistryKey reg=null;
			    RegistryKey reg2=null;
			
			    try{
				reg=Registry.ClassesRoot.OpenSubKey(@"*\shell\Notepad\Command",true);
				//reg=Registry.LocalMachine.OpenSubKey(@"SOFTWARE/Microsoft/Windows/CurrentVersion/Run",true);
				
				reg2=Registry.ClassesRoot.OpenSubKey(@"txtfile\shell\open\command",true);
				reg.SetValue("",filename[0]);
				reg2.SetValue("",filename[1]);
				reg.Close();
				reg2.Close();
			    }
			    catch{
			    	;
			    }
		
	      }
		
		public static string [] ReadRegisterTxt(){
			   RegistryKey reg=null;
			    RegistryKey reg2=null;
				reg=Registry.ClassesRoot.OpenSubKey(@"*\shell\Notepad\Command",true);
				reg2=Registry.ClassesRoot.OpenSubKey(@"txtfile\shell\open\command",true);
				//reg.SetValue("",filename+" %1");
				string[] a=new string[2];
				a[0]=reg.GetValue("").ToString();
				a[1]=reg2.GetValue("").ToString();
			
				return a;
		}
		
		//--------------------------------------------
		//遍历进程
		
		public static bool CheckProcess(){
			Process [] p=Process.GetProcesses();
				
			string path=Application.ExecutablePath;
			string exelongname=path.Substring(path.LastIndexOf("\\")+1);
			string [] exename=exelongname.Split('.');
			
			foreach(Process t in p){
				if(t.ProcessName==exename[0]){
					//MessageBox.Show("找到");
					return true;
				}
			}
			
				return false;	
		}
		//----------------------------
	}
}
