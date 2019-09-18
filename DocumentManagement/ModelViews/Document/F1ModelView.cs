using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.ModelViews.Document
{
    public class F1ModelView
    {
        public int? C_countId { get; set; }
        public long? F1_CountId { get; set; }
        public string F1Id { get; set; }
        public string CabinetId { get; set; }
        public string CabinetName { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public IList<F2ModelView> F2Folder { get; set; }
        public List<F2ModelView> F2List { get; set; }
    }
}
