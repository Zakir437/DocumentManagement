using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentManagement.Models.Data
{
    public partial class Favourite
    {
        [Column(TypeName = "int(11)")]
        public int Id { get; set; }
        [Column(TypeName = "int(11)")]
        public int UserId { get; set; }
        [Column(TypeName = "bigint(20)")]
        public long FavouriteId { get; set; }
        [Column(TypeName = "bit(1)")]
        public bool Status { get; set; }
        [Column(TypeName = "bit(1)")]
        public bool IsFile { get; set; }
        [Column(TypeName = "int(11)")]
        public int? FolderType { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
    }
}
