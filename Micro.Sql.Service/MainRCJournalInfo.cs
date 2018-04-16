namespace Micro.Sql.Service
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MainRCJournalInfo")]
    public partial class MainRCJournalInfo
    {
        public long ID { get; set; }

        public string TITLE { get; set; }

        public string PUBLISHER { get; set; }

        public string PUBLISHING_PLACE { get; set; }

        public string PUBLISHING_PERIOD { get; set; }

        public string RESTYPE { get; set; }
    }
}
