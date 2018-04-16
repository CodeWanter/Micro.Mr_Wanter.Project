namespace Micro.Wanter.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class S_User
    {
        public int id { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        public DateTime? CreateTime { get; set; }
    }
}
