using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.ModelViews.Document
{
    public class SearchResultModelView
    {
        public string ProtectedId { get; set; }
        public string Name { get; set; }
        public bool IsFile { get; set; }
        public int Type { get; set; }
        public int FileType { get; set; }
        public string Location { get; set; }
    }
}
