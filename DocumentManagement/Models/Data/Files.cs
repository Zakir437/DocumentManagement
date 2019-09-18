using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentManagement.Models.Data
{
    public partial class Files
    {
        [Column(TypeName = "bigint(20)")]
        public long Id { get; set; }
        [Column(TypeName = "int(11)")]
        public int? CabinetId { get; set; }
        [Column("F1Id", TypeName = "bigint(20)")]
        public long? F1id { get; set; }
        [Column("F2Id", TypeName = "bigint(20)")]
        public long? F2id { get; set; }
        [Column("F3Id", TypeName = "bigint(20)")]
        public long? F3id { get; set; }
        [Required]
        [Column(TypeName = "varchar(45)")]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "varchar(200)")]
        public string OriginalName { get; set; }
        [Column(TypeName = "int(11)")]
        public int Type { get; set; }
        [Column(TypeName = "int(11)")]
        public int FileType { get; set; }
        [Column(TypeName = "bigint(20)")]
        public long Size { get; set; }
        [Column(TypeName = "bit(1)")]
        public bool Status { get; set; }
        [Column(TypeName = "int(11)")]
        public int ListCount { get; set; }
        [Column(TypeName = "bit(1)")]
        public bool? IsArchive { get; set; }
        [Column(TypeName = "int(11)")]
        public int? DeleteType { get; set; }
        [Column(TypeName = "int(11)")]
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        [Column(TypeName = "int(11)")]
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
    }
}
