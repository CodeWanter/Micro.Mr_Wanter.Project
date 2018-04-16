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
        public static void Bat(string path)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = " cmd.exe ";
            startInfo.Arguments = path;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardInput = false;
            startInfo.RedirectStandardOutput = false;
            startInfo.CreateNoWindow = true;
            process.StartInfo = startInfo;

            process.Start();
            //process.WaitForExit();
            process.Dispose();
        }
    }
}
