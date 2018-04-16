using Microsoft.Office.Interop.Excel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Micro.Wanter.Common.ViewResult
{
    public class ExcelResult<T> : ActionResult
    {
        private List<T> _data;
        private const int OLDOFFICEVESION = -4143;
        private const int NEWOFFICEVESION = 56;
   
        public ExcelResult(List<T> data)
        {
            _data = data;
        }
        //public override void ExecuteResult(ControllerContext context)
        //{
        //    //保存excel文件的格式
        //    int FormatNum;
        //    //excel版本号
        //    string Version;

        //    //启动应用
        //    Application xlApp = new Application();

        //    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        //    Workbooks workbooks = xlApp.Workbooks;
        //    //创建文件
        //    Workbook workbook = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
        //    //创建sheet
        //    Worksheet worksheet = (Worksheet)workbook.Worksheets[1];
        //    //获取你使用的excel 的版本号
        //    Version = xlApp.Version;
        //    //设置禁止弹出保存和覆盖的询问提示框   
        //    xlApp.DisplayAlerts = false;
        //    xlApp.AlertBeforeOverwriting = false;
        //    //使用Excel 97-2003
        //    if (Convert.ToDouble(Version) < 12)
        //    {
        //        FormatNum = OLDOFFICEVESION;
        //    }
        //    //使用 excel 2007或更新
        //    else
        //    {
        //        FormatNum = NEWOFFICEVESION;
        //    }

        //    Type type = typeof(T);
        //    PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
        //    try
        //    {
        //        for (int i = 0; i < _data.Count; i++)
        //        {
        //            for (int j = 0; j < properties.Length; j++)
        //            {
        //                PropertyInfo item = properties[j];
        //                string name = item.Name;
        //                object value = item.GetValue(_data[i], null);
        //                if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
        //                {
        //                    if (i == 0)
        //                        worksheet.Cells[1, j + 1] = name;
        //                    worksheet.Cells[i + 2, j + 1] = value;
        //                }
        //            }
        //        }
        //        //删除已存在的excel文件，否则会无法保存创建的excel文件
        //        if (File.Exists(@"D:\testExcell.xlsx"))
        //        {
        //            try
        //            {
        //                File.Delete(@"D:\testExcell.xlsx");
        //            }
        //            catch (IOException e)
        //            {
        //                Console.WriteLine(e.Message);
        //            }
        //        }
        //        //保存，这里必须指定FormatNum文件的格式，否则无法打开创建的excel文件
        //        workbook.Save();
        //        //显示创建的excel文件
        //        xlApp.Visible = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}
        public override void ExecuteResult(ControllerContext context)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table>");
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            try
            {
                for (int i = 0; i < _data.Count; i++)
                {
                    sb.Append("<tr>");
                    for (int j = 0; j < properties.Length; j++)
                    {
                        PropertyInfo item = properties[j];
                        string name = item.Name;
                        object value = item.GetValue(_data[i], null);
                        if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                        {
                            if (i == 0)
                            {
                                sb.Append("<td>");
                                sb.Append(name);
                                sb.Append("</td>");
                            }
                            else
                            {
                                sb.Append("<td>");
                                sb.Append(value);
                                sb.Append("</td>");
                            }
                        }
                    }
                    sb.Append("</tr>");
                }
                sb.Append("</table>");
                var response = context.HttpContext.Response;
                response.ContentType = "application/vnd.ms-excel";
                response.AddHeader("Content-Disposition", "attachment; filename=统计.xls");
                response.Write(sb);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public ArrayList GetProperties(int m)
        {
            ArrayList ret = new ArrayList();
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            if (properties.Length <= 0) { return null; }
            for (int i = 0; i < properties.Length; i++)
            {
                PropertyInfo item = properties[i];
                string name = item.Name;
                object value = item.GetValue(_data[m], null);
                if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                {
                    ret.Insert(i, value);
                }
            }
            return ret;
        }
    }
}
