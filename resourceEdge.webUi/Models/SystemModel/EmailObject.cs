using resourceEdge.Domain.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace resourceEdge.webUi.Models
{
    public class EmailObject
    {
        public string FullName { get; set; }  
        public string Sender { get; set; }
        public string Reciever { get; set; }
        public string Subject { get; set; }
        public string Footer { get; set; }
        public  string Body { get; set; }
        public string Signature { get; set; }
        public MailType Type { get; set; }
    }
}