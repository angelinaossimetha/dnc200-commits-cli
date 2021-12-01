using System;
using System.Collections.Generic;
using System.Text;

namespace dnc200_commits_cli
{
    public class Commit
    {
        public string sha { get; set; }
        public Author author { get; set; }
        public string message { get; set; }
        public bool distinct { get; set; }
        public string url { get; set; }
    }
}
