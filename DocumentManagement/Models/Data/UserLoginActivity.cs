using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentManagement.Models.Data
{
    public partial class UserLoginActivity
    {
        [Column(TypeName = "bigint(20)")]
        public long Id { get; set; }
        [Column(TypeName = "int(11)")]
        public int UserId { get; set; }
        [Required]
        [Column(TypeName = "varchar(45)")]
        public string Browser { get; set; }
        [Required]
        [Column("DeviceOS", TypeName = "varchar(45)")]
        public string DeviceOs { get; set; }
        [Required]
        [Column("IPAddress", TypeName = "varchar(45)")]
        public string Ipaddress { get; set; }
        [Column(TypeName = "varchar(45)")]
        public string City { get; set; }
        [Column(TypeName = "varchar(45)")]
        public string Country { get; set; }
        [Column(TypeName = "varchar(45)")]
        public string UniqueId { get; set; }
        [Column(TypeName = "bit(1)")]
        public bool? Status { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LoginDate { get; set; }
    }
}
