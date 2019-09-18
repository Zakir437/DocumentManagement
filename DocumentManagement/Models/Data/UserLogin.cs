using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentManagement.Models.Data
{
    public partial class UserLogin
    {
        [Column(TypeName = "int(11)")]
        public int Id { get; set; }
        [Column(TypeName = "int(11)")]
        public int UserId { get; set; }
        [Required]
        [Column(TypeName = "varchar(45)")]
        public string UserName { get; set; }
        [Required]
        [Column(TypeName = "varchar(45)")]
        public string Password { get; set; }
        [Column(TypeName = "bit(1)")]
        public bool IsConfirmed { get; set; }
        [Column(TypeName = "bit(1)")]
        public bool IsLoginBefore { get; set; }
        [Column("IsOTPEnable", TypeName = "bit(1)")]
        public bool IsOtpenable { get; set; }
        [Column("OTPCode", TypeName = "int(6)")]
        public int? Otpcode { get; set; }
    }
}
