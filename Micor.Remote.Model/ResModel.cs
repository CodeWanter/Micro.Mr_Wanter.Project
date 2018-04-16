using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Micor.Remote.Model
{
    public class ResModel
    {
        public int f_id { get; set; }

        public string mid { get; set; }

        public string Title { get; set; }
        public string Author { get; set; }
        public string Bzjg { get; set; }
        public string Bzsj { get; set; }
        public string Cbdw { get; set; }
        public string Cbsj { get; set; }
        public string ISBN { get; set; }
        public string Xklb { get; set; }
        public string Qwen { get; set; }
        public string Introduce { get; set; }
        public string TextAddress { get; set; }
        public string PicAddress { get; set; }
        public string Pagecount { get; set; }
        public string Type { get; set; }
        public string EXT1 { get; set; }
        public string EXT2 { get; set; }
        public string EXT3 { get; set; }
        /// <summary>
        /// 标题转码后
        /// </summary>
        public string Titlegbk { get; set; }
    }
}
