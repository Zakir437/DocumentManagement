#pragma checksum "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\F2DeleteAlert.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f201e9f56e77674a004c3cf2543a9b8a22128db9"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Document_F2DeleteAlert), @"mvc.1.0.view", @"/Views/Document/F2DeleteAlert.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Document/F2DeleteAlert.cshtml", typeof(AspNetCore.Views_Document_F2DeleteAlert))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f201e9f56e77674a004c3cf2543a9b8a22128db9", @"/Views/Document/F2DeleteAlert.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"da19c4b16b29ae1c5018fedbda5ddbae5a75d409", @"/Views/_ViewImports.cshtml")]
    public class Views_Document_F2DeleteAlert : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/Document/f2Delete.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            BeginContext(1, 43, false);
#line 1 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\F2DeleteAlert.cshtml"
Write(Html.Hidden("F2IdInDelete", ViewBag.Id, ""));

#line default
#line hidden
            EndContext();
            BeginContext(44, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(47, 35, false);
#line 2 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\F2DeleteAlert.cshtml"
Write(Html.Hidden("Type", ViewBag.Id, ""));

#line default
#line hidden
            EndContext();
            BeginContext(82, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(85, 49, false);
#line 3 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\F2DeleteAlert.cshtml"
Write(Html.Hidden("IsMultiple", ViewBag.IsMultiple, ""));

#line default
#line hidden
            EndContext();
            BeginContext(134, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(137, 47, false);
#line 4 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\F2DeleteAlert.cshtml"
Write(Html.Hidden("EncryptId", ViewBag.EncryptId, ""));

#line default
#line hidden
            EndContext();
            BeginContext(184, 443, true);
            WriteLiteral(@"
<div class=""k-window-body"">
    <div id=""divF2Delete"" class=""k-window-body-form"">
        <div class=""tiny_loading""></div>
    </div>
</div>
<div class=""k-window-footer"">
    <button type=""button"" class=""btn btn-info waves-effect"" id=""btnF2DeleteOk""><i class=""fa fa-check""></i>&nbsp;OK</button>
    <button type=""button"" class=""btn bg-red waves-effect"" id=""btnF2DeleteCancel""><i class=""fa fa-times""></i>&nbsp;Cancel</button>
</div>
");
            EndContext();
            BeginContext(627, 53, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "94a334a3bfdc49e3a986c429b3dad995", async() => {
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
