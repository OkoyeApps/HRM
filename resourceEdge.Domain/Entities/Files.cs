using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Entities
{
   public class Files
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FileName { get; set; }
        public FileType FileType { get; set; }
        public string FilePath { get; set; }

    }
    public enum FileType
    {
        Avatar = 1,
        Assets
    }
}
