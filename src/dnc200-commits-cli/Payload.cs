using System;
using System.Collections.Generic;
using System.Text;

namespace dnc200_commits_cli
{
    public class Payload
    {
        public object push_id { get; set; }
        public int size { get; set; }
        public int distinct_size { get; set; }
        public string @ref { get; set; }
        public string head { get; set; }
        public string before { get; set; }
        public List<Commit> commits { get; set; }
        public string ref_type { get; set; }
        public string master_branch { get; set; }
        public object description { get; set; }
        public string pusher_type { get; set; }
    }
}
