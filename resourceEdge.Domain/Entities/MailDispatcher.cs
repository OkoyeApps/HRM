using resourceEdge.Domain.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Entities
{
    public class MailDispatcher
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Subject { get; set; }
        public string Reciever { get; set; }
        public string Sender { get; set; }
        public string Message { get; set; }
        public DateTime? TimeToSend { get; set; }
        public MailType Type { get; set; }
        public string GroupName { get; set; }
        public bool Delivered { get; set; }
    }
}
