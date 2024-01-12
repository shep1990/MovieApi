using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApi.Data
{
    public class CosmosConfig
    {
        public string Database { get; set; }
        public string MovieContainer { get; set; }
        public string MoviePartition { get; set; }
    }
}
