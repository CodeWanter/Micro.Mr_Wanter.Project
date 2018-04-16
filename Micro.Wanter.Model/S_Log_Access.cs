namespace Micro.Wanter.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class S_Log_Access
    {
        public int id { get; set; }

        public int? UserId { get; set; }

        public DateTime? LoginTime { get; set; }

        public string Browser { get; set; }

        [StringLength(50)]
        public string Ip { get; set; }

        public string OS { get; set; }
    }
}
