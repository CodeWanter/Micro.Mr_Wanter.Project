using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Micro.Wanter.Common.Helper
{
    public class ExcuteBat
    {
        /// <summary>
        /// 启动ElasticSearch服务
        /// </summary>
        /// <param name="path">存放启动ElasticSearch的文件路径</param>
        public static void Bat(string targetDir,string args)
        {
           // string targetDir = string.Format(@"D:\工作\elasticsearch\elasticsearch-2.4.2\bin");//this is where mybatch.bat lies
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WorkingDirectory = targetDir;
            startInfo.FileName = " cmd.exe ";
            startInfo.Arguments = args;
                //string.Format(@"/K D:\工作\elasticsearch\elasticsearch-2.4.2\bin\elasticsearch.bat");
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardInput = false;
            startInfo.RedirectStandardOutput = false;
            startInfo.CreateNoWindow = true;
            process.StartInfo = startInfo;

            process.Start();
            //process.WaitForExit();
            process.Dispose();
        }

        public static void BatWithDos(string targetDir,string fileName)
        {
            Process proc = null;
            try
            {
                //string targetDir = string.Format(@"D:\工作\elasticsearch\elasticsearch-2.4.2\bin");//this is where mybatch.bat lies
                proc = new Process();
                proc.StartInfo.WorkingDirectory = targetDir;
                proc.StartInfo.FileName = fileName;
                    //"elasticsearch.bat";
                proc.StartInfo.CreateNoWindow = true;
                proc.Start();
                proc.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Occurred :{0},{1}", ex.Message, ex.StackTrace.ToString());
            }
        }
    }
}
