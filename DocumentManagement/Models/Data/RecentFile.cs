using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentManagement.Models.Data
{
    public partial class RecentFile
    {
        [Column(TypeName = "int(11)")]
        public int Id { get; set; }
        [Column(TypeName = "bigint(20)")]
        public long FileId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }
    }
}
