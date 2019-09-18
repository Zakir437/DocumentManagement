using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentManagement.Models.Data
{
    public partial class FileAuditLog
    {
        [Column(TypeName = "bigint(20)")]
        public long Id { get; set; }
        [Column(TypeName = "bigint(20)")]
        public long FileId { get; set; }
        [Required]
        [Column(TypeName = "varchar(200)")]
        public string FileName { get; set; }
        [Required]
        [Column(TypeName = "varchar(500)")]
        public string Message { get; set; }
        [Required]
        [Column(TypeName = "varchar(45)")]
        public string Operation { get; set; }
        [Column(TypeName = "bit(1)")]
        public bool Status { get; set; }
        [Column(TypeName = "int(11)")]
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
    }
}
