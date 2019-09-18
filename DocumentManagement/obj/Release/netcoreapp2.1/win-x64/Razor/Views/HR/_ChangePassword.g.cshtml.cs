#pragma checksum "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\HR\_ChangePassword.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f36b20a0801239aacc21276b1c553125b36ec10b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_HR__ChangePassword), @"mvc.1.0.view", @"/Views/HR/_ChangePassword.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/HR/_ChangePassword.cshtml", typeof(AspNetCore.Views_HR__ChangePassword))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\_ViewImports.cshtml"
using DocumentManagement;

#line default
#line hidden
#line 2 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\_ViewImports.cshtml"
using DocumentManagement.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f36b20a0801239aacc21276b1c553125b36ec10b", @"/Views/HR/_ChangePassword.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"da19c4b16b29ae1c5018fedbda5ddbae5a75d409", @"/Views/_ViewImports.cshtml")]
    public class Views_HR__ChangePassword : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 386, true);
            WriteLiteral(@"<label for=""password"">New Password</label>
<div class=""form-group"">
    <input type=""password"" id=""password"" name=""password"" pattern=""(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,}"" data-required-msg=""Please enter new password"" required validationMessage=""Must contain at least one number and one uppercase and lowercase letter, and at least 6 or more characters"" placeholder=""Password"" />
    ");
            EndContext();
            BeginContext(387, 70, false);
#line 4 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\HR\_ChangePassword.cshtml"
Write(Html.ValidationMessage("password", "", new { @class = "text-Danger" }));

#line default
#line hidden
            EndContext();
            BeginContext(457, 260, true);
            WriteLiteral(@"
</div>
<label for=""confirmPassword"">Confirm Password</label>
<div class=""form-group"">
    <input type=""password"" id=""confirmPassword"" name=""confirmPassword"" required data-required-msg=""Please enter confirm password"" placeholder=""Confirm Password"" />
    ");
            EndContext();
            BeginContext(718, 70, false);
#line 9 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\HR\_ChangePassword.cshtml"
Write(Html.ValidationMessage("password", "", new { @class = "text-Danger" }));

#line default
#line hidden
            EndContext();
            BeginContext(788, 8, true);
            WriteLiteral("\r\n</div>");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
