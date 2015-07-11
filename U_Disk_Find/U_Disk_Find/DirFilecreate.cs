/*
 * 由SharpDevelop创建。
 * 用户： Administrator
 * 日期: 2015/5/31
 * 时间: 19:17
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.IO;
using System.Threading;

namespace U_Disk_Find
{
	/// <summary>
	/// Description of DirFilecreate.
	/// </summary>
	/// 
	
	public class DirFileCreate
	{
		public DirFileCreate()
		{
		}
		
		public string [ ] File__Extension={"jpg","doc","docx","xls","xlsx","ppt","pptx"};
		
		public string DirCreate(){
			//复制到电脑单位文件夹名
			string dir=DateTime.Now.Year+"-"+DateTime.Now.Month+"-"+DateTime.Now.Day+" "+
				       DateTime.Now.Hour+"-"+DateTime.Now.Minute+" U盘复制的文件";
				
		   DriveInfo[] s= DriveInfo.GetDrives();
		   
		   string basedir=s[0].Name+"\\"+dir;
		   
		   if(!Directory.Exists(basedir)){
		     	Directory.CreateDirectory(basedir);
		   }
		   if(Directory.Exists(basedir)){
		   	  foreach(string dir_ex in File__Extension)
		   	  {
		   	  	Directory.CreateDirectory(basedir+"\\"+dir_ex);
		   	  }
		   }
          //返回总目录名
          return basedir; 		   
		   
		}
		
		//复制文件函数  dedir 目标目录 例子：C:\\a
		//             Or_filename 原文件名（完整）   例子： K:\\b\\1.txt
		public void copyFile(string de_dir,string Or_filename){
			
			Random ro=new Random();
			string file_num=ro.Next(0,3666).ToString();
			
			if(!Directory.Exists(de_dir)){
				Directory.CreateDirectory(de_dir);	
			}
			
			FileInfo fi=new FileInfo(Or_filename);
			try{
			   fi.CopyTo(de_dir+"\\"+fi.Name);
			}catch{
				 fi.CopyTo(de_dir+"\\"+file_num+fi.Name);;
			}
			
			
		}
		
		
		public void multiCopyFile(string de_dir,string Or_filename){
		  FileInfo fi=new FileInfo(Or_filename);	
		  using (FileStream outStream = new FileStream(de_dir+"\\"+fi.Name, FileMode.Create))
           {
               using (FileStream fs = new FileStream(fi.FullName, FileMode.Open))
               {
                   //缓冲区太小的话，速度慢而且伤硬盘
                   //声明一个4兆字节缓冲区大小，比如迅雷也有一个缓冲区，如果没有缓冲区的话，
                   //每下载一个字节都要往磁盘进行写，非常伤磁盘，所以，先往内存的缓冲区写字节，当
                   //写够了一定容量之后，再往磁盘进行写操作，减低了磁盘操作。
                   byte[] bytes = new byte[1024 * 1024 *3];
                   int readBytes;
                   //第二个参数Offset表示当前位置的偏移量，一般都传0
                   while ((readBytes = fs.Read(bytes, 0, bytes.Length)) > 0) //读取的位置自动往后挪动。
                   {
                       //readBytes为实际读到的byte数，因为最后一次可能不会读满。
                       outStream.Write(bytes, 0, readBytes);
                   }
               }
           }
		}
		//--------------------------------------------
		
		
	}
}
