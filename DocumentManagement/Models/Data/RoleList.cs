using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentManagement.Models.Data
{
    public partial class RoleList
    {
        [Column(TypeName = "int(11)")]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "varchar(45)")]
        public string RoleName { get; set; }
        [Column(TypeName = "bit(1)")]
        public bool? Status { get; set; }
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
