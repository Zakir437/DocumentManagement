using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentManagement.Models.Data
{
    public partial class FrequentFolder
    {
        [Column(TypeName = "bigint(20)")]
        public long Id { get; set; }
        [Column(TypeName = "bigint(20)")]
        public long FolderId { get; set; }
        [Column(TypeName = "int(11)")]
        public int Type { get; set; }
        [Column(TypeName = "int(11)")]
        public int Count { get; set; }
        [Column(TypeName = "bit(1)")]
        public bool IsPined { get; set; }
    }
}
