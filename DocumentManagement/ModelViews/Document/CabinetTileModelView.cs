using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.ModelViews.Document
{
    public class CabinetTileModelView
    {
        public long Id { get; set; }
        public long FileId { get; set; }
        public string EncryptFildId { get; set; }
        public int? FileType { get; set; }
        public string FolderId { get; set; }
        public int? FolderType { get; set; }
        public string Name { get; set; }
        public bool IsFile { get; set; }
        public bool IsPined { get; set; }
        public int Status { get; set; }
    }
}
