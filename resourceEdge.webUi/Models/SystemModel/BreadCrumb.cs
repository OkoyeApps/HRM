using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace resourceEdge.webUi.Models
{
    public class BreadCrumb
    {
        public string Controller { get; set; }
        public string Action { get; set; }
        public string  ActionUrl { get; set; }
        public string   ControllerName { get; set; }

    }
}