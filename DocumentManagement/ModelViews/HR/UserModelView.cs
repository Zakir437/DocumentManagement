using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.ModelViews.HR
{
    public class UserModelView
    {
        public string EncryptedId { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Status { get; set; }
        public string Image { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<DateTime> DOB { get; set; }
        public string MobileNumber { get; set; }
        public int? RoleId { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
    }
}
