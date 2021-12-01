using System;
using System.Collections.Generic;
using System.Text;

namespace dnc200_commits_cli
{
    public class Event { 
  
            public string id { get; set; }
            public string type { get; set; }
            public Actor actor { get; set; }
            public Repo repo { get; set; }
            public Payload payload { get; set; }
            public bool @public { get; set; }
            public DateTime created_at { get; set; }
       


    }
}
