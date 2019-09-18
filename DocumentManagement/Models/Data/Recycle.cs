using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentManagement.Models.Data
{
    public partial class Recycle
    {
        [Column(TypeName = "int(11)")]
        public int Id { get; set; }
        [Column(TypeName = "bigint(20)")]
        public long OwnerId { get; set; }
        [Required]
        [Column(TypeName = "varchar(200)")]
        public string Name { get; set; }
        [Column(TypeName = "bit(1)")]
        public bool IsFile { get; set; }
        [Column(TypeName = "int(11)")]
        public int Type { get; set; }
        [Column(TypeName = "int(11)")]
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        [Column(TypeName = "bit(1)")]
        public bool? IsPermanent { get; set; }
    }
}
