using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.ModelViews.Document
{
    public class FileAuditLogModelView
    {
        public long FileId { get; set; }
        public string FileName  { get; set; }
        public string Message { get; set; }
        public string Operation { get; set; }
        public bool Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        
    }
}
