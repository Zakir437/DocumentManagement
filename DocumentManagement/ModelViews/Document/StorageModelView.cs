using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.ModelViews.Document
{
    public class StorageModelView
    {
        public string Allocated { get; set; }
        public string Used { get; set; }
        public string Available { get; set; }
        public string Documents { get; set; }
        public string Image { get; set; }
        public string Audio { get; set; }
        public string Video { get; set; }
        public string Files { get; set; }
    }
}
