using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentManagement.Models.Data
{
    public partial class UserInformation
    {
        [Column(TypeName = "int(11)")]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "varchar(45)")]
        public string Name { get; set; }
        [Column(TypeName = "int(11)")]
        public int? Gender { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateOfBirth { get; set; }
        [Column(TypeName = "int(11)")]
        public int? Role { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string MobileNumber { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        [Column(TypeName = "bit(1)")]
        public bool Status { get; set; }
        [Column(TypeName = "int(11)")]
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [Column(TypeName = "varchar(45)")]
        public string Image { get; set; }
    }
}
