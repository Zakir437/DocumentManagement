using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.ModelViews.Document
{
    public class CabinetModelView
    {
        public int Id { get; set; }
        public int? CountId { get; set; }
        public string EncryptedId { get; set; }
        public int Status { get; set; }
        public string CabinetName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public IList<F1ModelView> F1Folder { get; set; }
        public List<F1ModelView> F1List { get; set; }
    }
}
