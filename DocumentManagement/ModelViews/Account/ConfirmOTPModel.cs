using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.ModelViews.Account
{
    public class ConfirmOTPModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string IsLoginBefore { get; set; }
        public string RememberMe { get; set; }
        public string ReturnUrl { get; set; }
        public string MobileNumber { get; set; }
        public bool Error { get; set; }
        public string Last4Digit { get; set; }
        public int VarificationCode { get; set; }
        public bool SaveBrowser { get; set; }
    }
}
