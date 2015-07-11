/*
 * 由SharpDevelop创建。
 * 用户： Administrator
 * 日期: 2015/6/1
 * 时间: 18:29
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;

namespace U_Disk_Find
{
	/// <summary>
	/// Description of U_Disk_Info.
	/// </summary>
	public class U_Disk_Info
	{
		public   U_Disk_Info()
		{
//			this.name="unkoown";
//			this.VolumnLabel="aaa";
//			this.IsCopy=false;
		}
		
		public string name{get;set;}        //盘名
		public string VolumnLabel{get;set;} //卷标名
		public bool IsCopy{get;set;}        //是否已经被复制
		
		//构造函数
		public U_Disk_Info(string rename,string revolumnlabel,bool iscopy){
			this.name=rename;
			this.VolumnLabel=revolumnlabel;
			this.IsCopy=iscopy;
		}
		
		
		//判断U盘信息类两个实例是否相同
		public static bool ObjectEquel(U_Disk_Info obj1, U_Disk_Info obj2)
        {
            Type type1 = obj1.GetType();
            Type type2 = obj2.GetType();

            System.Reflection.PropertyInfo[] properties1 = type1.GetProperties();
            System.Reflection.PropertyInfo[] properties2 = type2.GetProperties();

            bool IsMatch = true;
            for (int i = 0; i < properties1.Length-1; i++)
            {
                //string s = properties1[i].DeclaringType.Name;
                if (properties1[i].GetValue(obj1, null).ToString() != properties2[i].GetValue(obj2, null).ToString())
                {
                    IsMatch = false;
                    break;
                }
            }

            return IsMatch;
        }
		//------------------------------------------
		//U盘类数组对比
		//after   
		public static U_Disk_Info[] compare( U_Disk_Info[] after, U_Disk_Info[] before){
			
			U_Disk_Info [] temp=new U_Disk_Info[50];
			for(int j=0;j<50;j++){
				temp[j]=new U_Disk_Info();
			}
			
			int a_legth=length(after);
			int b_length=length(before);
			
			int i=0;
			for(int j=0;j<b_length;j++){
				temp[i]=new U_Disk_Info();
				temp[i].name=before[j].name;
				temp[i].VolumnLabel=before[j].VolumnLabel;
				temp[i].IsCopy=before[j].IsCopy;
				i++;
			}
			
			for(int k=0;k<a_legth;k++){	
				if(!find(after[k],before)){
				   temp[i]=new U_Disk_Info();
				   temp[i].name=after[k].name;
				   temp[i].VolumnLabel=after[k].VolumnLabel;
				   temp[i].IsCopy=after[k].IsCopy;
				}
				
			}
			
			
			return temp;
		}
		//--------------------------------------------
		
		public static int length(U_Disk_Info[] u_disk){
			int i=0;
			while(u_disk[i].name!=null && u_disk[i].VolumnLabel!=null){
				i++;
			}
			return i;
		}
		
		public static bool find(U_Disk_Info find ,U_Disk_Info [] t){
			int t_legth=length(t);
			for(int i=0;i<t_legth;i++){
				if(find.name==t[i].name && find.VolumnLabel==t[i].VolumnLabel){
					return true;
				}
			}
			return false;
		}
	}
}
