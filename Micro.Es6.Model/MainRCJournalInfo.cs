using Nest;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Micro.Es6.Model
{
    [ElasticsearchType(IdProperty = "id", Name = "MainRCJournalInfo")]
    [Table("MainRCJournalInfo")]
    public partial class MainRCJournalInfo
    {
        [String(Name = "f_id")]
        public long ID { get; set; }
        [String(Name = "TITLE", Analyzer = "ik_max_word")]
        public string TITLE { get; set; }
        [String(Name = "PUBLISHER")]
        public string PUBLISHER { get; set; }
        [String(Name = "PUBLISHING_PLACE",Index =FieldIndexOption.NotAnalyzed,DocValues =false,IndexOptions =IndexOptions.Docs,NullValue ="")]
        public string PUBLISHING_PLACE { get; set; }
        [String(Name = "PUBLISHING_PERIOD")]
        public string PUBLISHING_PERIOD { get; set; }
        [String(Name = "RESTYPE", Index = FieldIndexOption.No)]
        public string RESTYPE { get; set; }

        [String(Ignore=true)]
        [NotMapped]
        public string Score { get; set; }
    }
}
