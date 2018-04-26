namespace Micro.Wanter.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class S_User
    {
        public string ConnectionID { get; set; }
        public S_User(string UserName, string connectionId)
        {
            this.UserName = UserName;
            this.ConnectionID = connectionId;
        }

        public S_User()
        {
        }

        public int id { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        public DateTime? CreateTime { get; set; }
    }
}
