using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentManagement.Models.Data
{
    public partial class Storage
    {
        [Column(TypeName = "int(11)")]
        public int Id { get; set; }
        [Column(TypeName = "bigint(20)")]
        public long Allowed { get; set; }
        [Column(TypeName = "bigint(20)")]
        public long Used { get; set; }
        [Column(TypeName = "bigint(20)")]
        public long Buffered { get; set; }
        [Column(TypeName = "int(11)")]
        public int UserId { get; set; }
        [Column(TypeName = "int(11)")]
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
    }
}
