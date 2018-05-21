using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace resourceEdge.webUi.Infrastructure.SystemManagers
{
    public class FileManager
    {
        public bool ValidateFileSize(HttpPostedFileBase File)
        {
            var Size = Math.Round(File.ContentLength / (decimal)1024, 2);
            if (Size > 500)
            {
                return false;
            }
            return true;
        }

        public bool ValidateResume(HttpPostedFileBase File)
        {
            var Size = Math.Round(File.ContentLength / (decimal)1024, 2);
            if (Size > 100)
            {
                return false;
            }
            return true;
        }
    }
}