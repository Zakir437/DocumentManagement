using System;
using System.Security.Claims;
using System.Security.Principal;

namespace DocumentManagement.Services
{
    public static class IdentityExtentions
    {
        //Get FullName of Logged User using (User.Identity.GetUserName();)
        public static string GetUserFullName(this IPrincipal identity)
        {
            ClaimsIdentity claimsIdentity = identity.Identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.FindFirst(ClaimTypes.Name);

            return claim?.Value ?? string.Empty;
        }
        //Get UserName of Logged User
        public static string GetUserName(this IPrincipal identity)
        {
            ClaimsIdentity claimsIdentity = identity.Identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);

            return claim?.Value ?? string.Empty;
        }
        //Get UserId of Logged User as Int32
        public static int GetUserId(this IPrincipal identity)
        {
            ClaimsIdentity claimsIdentity = identity.Identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.FindFirst(ClaimTypes.Sid);

            return Convert.ToInt32(claim?.Value);
        }
        //Get RoleName of Logged User
        public static string GetRoleName(this IPrincipal identity)
        {
            ClaimsIdentity claimsIdentity = identity.Identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.FindFirst(ClaimTypes.Role);

            return claim?.Value ?? string.Empty;
        }
        //Get RoleId of Logged User as Int32
        public static int GetRoleId(this IPrincipal identity)
        {
            ClaimsIdentity claimsIdentity = identity.Identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.FindFirst(ClaimTypes.OtherPhone);

            return Convert.ToInt32(claim?.Value);
        }
    }
}
