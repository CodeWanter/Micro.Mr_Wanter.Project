using Elasticsearch.Net;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Micro.Es6.Service
{
   public  class ElasticsearchHelper
    {
        public ElasticClient client;
        public ElasticsearchHelper()
        {
            client = GetElasticClient(null);
        }
        public static ElasticClient GetElasticClient(string indexName)
        {
            var connectString = "http://127.0.0.1:9200";
            var nodesStr = connectString.Split('|');
            var nodes = nodesStr.Select(s => new Uri(s)).ToList();
            var connectionPool = new SniffingConnectionPool(nodes);
            var settings = new ConnectionSettings(connectionPool).RequestTimeout(TimeSpan.FromMinutes(1));

            if (!string.IsNullOrWhiteSpace(indexName))
            {
                settings.DefaultIndex(indexName.ToLower());
            }
            var client = new ElasticClient(settings);
            return client;

        }
    }
}
