#pragma checksum "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\F2Rename.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "429f2d64d3bbdc45f20bb53fbf48a499b9b6abd1"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Document_F2Rename), @"mvc.1.0.view", @"/Views/Document/F2Rename.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Document/F2Rename.cshtml", typeof(AspNetCore.Views_Document_F2Rename))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"429f2d64d3bbdc45f20bb53fbf48a499b9b6abd1", @"/Views/Document/F2Rename.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"da19c4b16b29ae1c5018fedbda5ddbae5a75d409", @"/Views/_ViewImports.cshtml")]
    public class Views_Document_F2Rename : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/Document/f2Rename.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            BeginContext(1, 35, false);
#line 1 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\F2Rename.cshtml"
Write(Html.Hidden("f2Id", ViewBag.Id, ""));

#line default
#line hidden
            EndContext();
            BeginContext(36, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(39, 37, false);
#line 2 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\F2Rename.cshtml"
Write(Html.Hidden("f1Id", ViewBag.F1id, ""));

#line default
#line hidden
            EndContext();
            BeginContext(76, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(79, 35, false);
#line 3 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\F2Rename.cshtml"
Write(Html.Hidden("Cid", ViewBag.Cid, ""));

#line default
#line hidden
            EndContext();
            BeginContext(114, 588, true);
            WriteLiteral(@"
<div class=""k-window-body"">
    <div id=""divF2RenameForm"" class=""k-window-body-form"">
        <div class=""tiny_loading""></div>
    </div>
</div>
<div class=""k-window-footer"">
    <button type=""button"" class=""btn  btn-primary waves-effect"" id=""btnF2RenameOk""><i class=""fa fa-check""></i>&nbsp;Ok</button>
    <button type=""reset"" class=""btn  btn-info waves-effect"" id=""btnF2RenameRefresh""><i class=""fa fa-refresh""></i>&nbsp;Refresh</button>
    <button type=""button"" class=""btn bg-red waves-effect"" id=""btnF2RenameCancel""><i class=""fa fa-times""></i>&nbsp;Cancel</button>
</div>
");
            EndContext();
            BeginContext(702, 53, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6d26e43798dc419bab34fc2cfaaafec3", async() => {
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
