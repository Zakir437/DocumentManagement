using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentManagement.Models.Data
{
    public partial class DocumentLog
    {
        [Column(TypeName = "bigint(20)")]
        public long Id { get; set; }
        [Column(TypeName = "bigint(20)")]
        public long? DocumentId { get; set; }
        [Column(TypeName = "varchar(45)")]
        public string TableName { get; set; }
        [Required]
        [Column(TypeName = "varchar(500)")]
        public string Status { get; set; }
        [Column(TypeName = "int(11)")]
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
    }
}
