using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace Micro.Mr_Wanter.MVC.Scripts.Plugin.layui_v2._2._5.layui.server
{
    /// <summary>
    /// editimgup 的摘要说明
    /// </summary>
    public class editimgup : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string end = "{\"code\": 1,\"msg\": \"服务器故障\",\"data\": {\"src\": \"\"}}"; //返回的json  

            var file = context.Request.Files[0]; //获取选中文件  
            Stream stream = file.InputStream;    //将文件转为流  

            Image img = Image.FromStream(stream);//将流中的图片转换为Image图片对象  

            Random ran = new Random((int)DateTime.Now.Ticks);//利用时间种子解决伪随机数短时间重复问题  

            //文件保存位置及命名，精确到毫秒并附带一组随机数，防止文件重名，数据库保存路径为此变量  
            string serverPath = "/imgUploads/" + DateTime.Now.ToString("yyyyMMddhhmmssms") + ran.Next(99999) + ".jpg";

            //路径映射为绝对路径  
            string path = context.Server.MapPath(serverPath);

            try
            {
                img.Save(path, ImageFormat.Jpeg);//图片保存，JPEG格式图片较小  

                //保存成功后的json  
                end = "{\"code\": 0,\"msg\": \"成功\",\"data\": {\"src\": \"" + serverPath + "\"}}";
            }
            catch { }

            context.Response.Write(end);//输出结果  
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}