#pragma checksum "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\HR\_UserInfoEdit.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "06feda01fb7f2fdcaf4ef0768a73de3830c8a4c8"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_HR__UserInfoEdit), @"mvc.1.0.view", @"/Views/HR/_UserInfoEdit.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/HR/_UserInfoEdit.cshtml", typeof(AspNetCore.Views_HR__UserInfoEdit))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"06feda01fb7f2fdcaf4ef0768a73de3830c8a4c8", @"/Views/HR/_UserInfoEdit.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"da19c4b16b29ae1c5018fedbda5ddbae5a75d409", @"/Views/_ViewImports.cshtml")]
    public class Views_HR__UserInfoEdit : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<DocumentManagement.ModelViews.HR.UserModelView>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/HR/infoEditPartial.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(55, 135, true);
            WriteLiteral("<label for=\"MobileNumber\">Mobile Number</label>\r\n<div class=\"form-group\">\r\n    <input type=\"text\" id=\"MobileNumber\" name=\"MobileNumber\"");
            EndContext();
            BeginWriteAttribute("value", " value=\"", 190, "\"", 217, 1);
#line 4 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\HR\_UserInfoEdit.cshtml"
WriteAttributeValue("", 198, Model.MobileNumber, 198, 19, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(218, 37, true);
            WriteLiteral(" placeholder=\"Mobile number\" />\r\n    ");
            EndContext();
            BeginContext(256, 74, false);
#line 5 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\HR\_UserInfoEdit.cshtml"
Write(Html.ValidationMessage("MobileNumber", "", new { @class = "text-Danger" }));

#line default
#line hidden
            EndContext();
            BeginContext(330, 76, true);
            WriteLiteral("\r\n</div>\r\n<label for=\"DOB\">Date of Birth</label>\r\n<div class=\"form-group\">\r\n");
            EndContext();
#line 9 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\HR\_UserInfoEdit.cshtml"
     if (Model.DOB != null)
    {

#line default
#line hidden
            BeginContext(442, 34, true);
            WriteLiteral("        <input id=\"DOB\" name=\"DOB\"");
            EndContext();
            BeginWriteAttribute("value", " value=\"", 476, "\"", 524, 1);
#line 11 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\HR\_UserInfoEdit.cshtml"
WriteAttributeValue("", 484, Model.DOB.Value.ToString("MMM dd yyyy"), 484, 40, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(525, 131, true);
            WriteLiteral(" required placeholder=\"Date of Birth\" data-required-msg=\"Please enter date of birth.\" style=\"width:100%\" title=\"Date of Birth\" />\r\n");
            EndContext();
#line 12 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\HR\_UserInfoEdit.cshtml"
    }
    else
    {

#line default
#line hidden
            BeginContext(680, 165, true);
            WriteLiteral("        <input id=\"DOB\" name=\"DOB\" required data-required-msg=\"Please enter date of birth.\" placeholder=\"Date of Birth\" style=\"width:100%\" title=\"Date of Birth\" />\r\n");
            EndContext();
#line 16 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\HR\_UserInfoEdit.cshtml"
    }

#line default
#line hidden
            BeginContext(852, 163, true);
            WriteLiteral("    <span class=\"k-widget k-invalid-msg\" data-for=\"DOB\"></span>\r\n</div>\r\n<label for=\"Role\">Role</label>\r\n<div class=\"form-group\">\r\n    <input id=\"Role\" name=\"Role\"");
            EndContext();
            BeginWriteAttribute("value", " value=\"", 1015, "\"", 1036, 1);
#line 21 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\HR\_UserInfoEdit.cshtml"
WriteAttributeValue("", 1023, Model.RoleId, 1023, 13, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1037, 160, true);
            WriteLiteral(" style=\"width:100%\" required data-required-msg=\"Please select role.\" />\r\n    <span class=\"k-widget k-invalid-msg\" data-for=\"orderTypeDropdown\"></span>\r\n</div>\r\n");
            EndContext();
            BeginContext(1197, 54, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "cbff87431f614e8f853e8929680b7814", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<DocumentManagement.ModelViews.HR.UserModelView> Html { get; private set; }
    }
}
#pragma warning restore 1591
