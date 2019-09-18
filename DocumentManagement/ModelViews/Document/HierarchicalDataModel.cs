using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.ModelViews.Document
{
    public class HierarchicalDataModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string imageUrl { get; set; }
        public string spriteCssClass { get; set; }
        public List<HierarchicalDataModel> Items { get; set; }
    }
}
