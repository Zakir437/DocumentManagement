﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentManagement.Models.Data
{
    public partial class F1
    {
        [Column(TypeName = "bigint(20)")]
        public long Id { get; set; }
        [Column(TypeName = "int(11)")]
        public int CabinetId { get; set; }
        [Required]
        [Column(TypeName = "varchar(45)")]
        public string Name { get; set; }
        [Column(TypeName = "int(11)")]
        public int Status { get; set; }
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
