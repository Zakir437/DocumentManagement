#pragma checksum "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\CabinetDocList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "711d268b4e6ff500e5d8ff724eb5016c4e651284"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Document_CabinetDocList), @"mvc.1.0.view", @"/Views/Document/CabinetDocList.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Document/CabinetDocList.cshtml", typeof(AspNetCore.Views_Document_CabinetDocList))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"711d268b4e6ff500e5d8ff724eb5016c4e651284", @"/Views/Document/CabinetDocList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"da19c4b16b29ae1c5018fedbda5ddbae5a75d409", @"/Views/_ViewImports.cshtml")]
    public class Views_Document_CabinetDocList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<DocumentManagement.ModelViews.Document.CabinetTileModelView>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/css/Document/folder.min.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            BeginContext(81, 62, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "5e09d5a42a934504b0a34f9e745abd84", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(143, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\CabinetDocList.cshtml"
 if (Model.Any())
{
    foreach (var doc in Model)
    {
        if (doc.IsFile == false)
        {

#line default
#line hidden
            BeginContext(251, 109, true);
            WriteLiteral("            <div class=\"ContentBox\" oncontextmenu=\"return false\">\r\n                <div class=\"box\" data-id=\"");
            EndContext();
            BeginContext(361, 12, false);
#line 10 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\CabinetDocList.cshtml"
                                     Write(doc.FolderId);

#line default
#line hidden
            EndContext();
            BeginContext(373, 28, true);
            WriteLiteral("\" data-type=\"1\" data-count=\"");
            EndContext();
            BeginContext(402, 6, false);
#line 10 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\CabinetDocList.cshtml"
                                                                              Write(doc.Id);

#line default
#line hidden
            EndContext();
            BeginContext(408, 158, true);
            WriteLiteral("\">\r\n                    <div class=\"divActionBtn\"></div>\r\n                    <span class=\"k-icon k-i-folder color-folder\"></span><br />\r\n                    ");
            EndContext();
            BeginContext(567, 8, false);
#line 13 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\CabinetDocList.cshtml"
               Write(doc.Name);

#line default
#line hidden
            EndContext();
            BeginContext(575, 46, true);
            WriteLiteral("\r\n                </div>\r\n            </div>\r\n");
            EndContext();
#line 16 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\CabinetDocList.cshtml"
        }
        else
        {

#line default
#line hidden
            BeginContext(657, 109, true);
            WriteLiteral("            <div class=\"ContentBox\" oncontextmenu=\"return false\">\r\n                <div class=\"box\" data-id=\"");
            EndContext();
            BeginContext(767, 10, false);
#line 20 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\CabinetDocList.cshtml"
                                     Write(doc.FileId);

#line default
#line hidden
            EndContext();
            BeginContext(777, 14, true);
            WriteLiteral("\" data-encid=\"");
            EndContext();
            BeginContext(792, 17, false);
#line 20 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\CabinetDocList.cshtml"
                                                              Write(doc.EncryptFildId);

#line default
#line hidden
            EndContext();
            BeginContext(809, 72, true);
            WriteLiteral("\" data-type=\"2\">\r\n                    <div class=\"divActionBtn\"></div>\r\n");
            EndContext();
#line 22 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\CabinetDocList.cshtml"
                     if (doc.FileType == 1)
                    {

#line default
#line hidden
            BeginContext(949, 76, true);
            WriteLiteral("                        <span class=\"k-icon k-i-image color-image\"></span>\r\n");
            EndContext();
#line 25 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\CabinetDocList.cshtml"
                    }
                    else if (doc.FileType == 2)
                    {

#line default
#line hidden
            BeginContext(1120, 72, true);
            WriteLiteral("                        <span class=\"k-icon k-i-pdf color-pdf\"></span>\r\n");
            EndContext();
#line 29 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\CabinetDocList.cshtml"
                    }
                    else if (doc.FileType == 3)
                    {

#line default
#line hidden
            BeginContext(1287, 74, true);
            WriteLiteral("                        <span class=\"k-icon k-i-word color-word\"></span>\r\n");
            EndContext();
#line 33 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\CabinetDocList.cshtml"
                    }
                    else if (doc.FileType == 4)
                    {

#line default
#line hidden
            BeginContext(1456, 73, true);
            WriteLiteral("                        <span class=\"k-icon k-i-txt color-text\"></span>\r\n");
            EndContext();
#line 37 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\CabinetDocList.cshtml"
                    }
                    else if (doc.FileType == 5)
                    {

#line default
#line hidden
            BeginContext(1624, 72, true);
            WriteLiteral("                        <span class=\"k-icon k-i-ppt color-ppt\"></span>\r\n");
            EndContext();
#line 41 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\CabinetDocList.cshtml"
                    }
                    else if (doc.FileType == 6)
                    {

#line default
#line hidden
            BeginContext(1791, 76, true);
            WriteLiteral("                        <span class=\"k-icon k-i-excel color-excel\"></span>\r\n");
            EndContext();
#line 45 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\CabinetDocList.cshtml"
                    }

#line default
#line hidden
            BeginContext(1890, 48, true);
            WriteLiteral("                    <br />\r\n                    ");
            EndContext();
            BeginContext(1939, 8, false);
#line 47 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\CabinetDocList.cshtml"
               Write(doc.Name);

#line default
#line hidden
            EndContext();
            BeginContext(1947, 46, true);
            WriteLiteral("\r\n                </div>\r\n            </div>\r\n");
            EndContext();
#line 50 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\CabinetDocList.cshtml"
        }
    }
}
else
{

#line default
#line hidden
            BeginContext(2023, 89, true);
            WriteLiteral("    <div class=\"col-md-12\">\r\n        <h5>There is no record to display</h5>\r\n    </div>\r\n");
            EndContext();
#line 58 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\CabinetDocList.cshtml"
}

#line default
#line hidden
            BeginContext(2115, 122, true);
            WriteLiteral("<script>\r\n    $(document).ready(function () {\r\n        $(\'[data-toggle=\"tooltip\"]\').tooltip();\r\n    });\r\n</script>\r\n\r\n\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<DocumentManagement.ModelViews.Document.CabinetTileModelView>> Html { get; private set; }
    }
}
#pragma warning restore 1591
