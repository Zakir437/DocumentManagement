#pragma checksum "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\F1Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "68eb062d8e0d0510e26dec29ff0e4e82ec3537f7"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Document_F1Details), @"mvc.1.0.view", @"/Views/Document/F1Details.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Document/F1Details.cshtml", typeof(AspNetCore.Views_Document_F1Details))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"68eb062d8e0d0510e26dec29ff0e4e82ec3537f7", @"/Views/Document/F1Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"da19c4b16b29ae1c5018fedbda5ddbae5a75d409", @"/Views/_ViewImports.cshtml")]
    public class Views_Document_F1Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<DocumentManagement.ModelViews.Document.F1ModelView>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/css/Document/cabinetDetails.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/css/fileUpload.min.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/Document/f1Details.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            BeginContext(60, 27, false);
#line 2 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\F1Details.cshtml"
Write(Html.HiddenFor(m => m.F1Id));

#line default
#line hidden
            EndContext();
            BeginContext(87, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\F1Details.cshtml"
  
    ViewData["Title"] = "F1 Details";
    Layout = Context.Request.Headers["x-requested-with"] == "XMLHttpRequest" ? null : "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(255, 66, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "b04ea66d672941e9b08cca77c2d6958d", async() => {
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
            BeginContext(321, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(323, 57, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "a7ee98d895c14738a37f2ebdedbe88e6", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(380, 289, true);
            WriteLiteral(@"
<div class=""page-bar"">
    <ul class=""page-breadcrumb"">
        <li>
            <i class=""fa fa-list-ul""></i>
            <a onclick=""onAjaxLoad('Cabinet', '/Document/Cabinet' )"">Cabinet</a>
            <i class=""fa fa-angle-right""></i>
        </li>
        <li>
            <a");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 669, "\"", 756, 6);
            WriteAttributeValue("", 679, "onAjaxLoad(\'Cabinet", 679, 19, true);
            WriteAttributeValue(" ", 698, "Details\',", 699, 10, true);
            WriteAttributeValue(" ", 708, "\'/Document/CabinetDetails?q=", 709, 29, true);
#line 17 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\F1Details.cshtml"
WriteAttributeValue("", 737, Model.CabinetId, 737, 16, false);

#line default
#line hidden
            WriteAttributeValue("", 753, "\'", 753, 1, true);
            WriteAttributeValue(" ", 754, ")", 755, 2, true);
            EndWriteAttribute();
            BeginContext(757, 112, true);
            WriteLiteral(">Cabinet Details</a>\r\n            <i class=\"fa fa-angle-right\"></i>\r\n        </li>\r\n        <li>\r\n            <a");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 869, "\"", 941, 6);
            WriteAttributeValue("", 879, "onAjaxLoad(\'F1", 879, 14, true);
            WriteAttributeValue(" ", 893, "Details\',", 894, 10, true);
            WriteAttributeValue(" ", 903, "\'/Document/F1Details?q=", 904, 24, true);
#line 21 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\F1Details.cshtml"
WriteAttributeValue("", 927, Model.F1Id, 927, 11, false);

#line default
#line hidden
            WriteAttributeValue("", 938, "\'", 938, 1, true);
            WriteAttributeValue(" ", 939, ")", 940, 2, true);
            EndWriteAttribute();
            BeginContext(942, 260, true);
            WriteLiteral(@">F1 Details</a>
        </li>
    </ul>
</div>
<div class=""portal-content"">
    <div class=""card"">
        <div class=""header"">
            <div class=""row"">
                <div class=""col-xs-6"">
                    <h4><label class=""col-deep-green"">");
            EndContext();
            BeginContext(1203, 10, false);
#line 30 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\F1Details.cshtml"
                                                 Write(Model.Name);

#line default
#line hidden
            EndContext();
            BeginContext(1213, 351, true);
            WriteLiteral(@"</label></h4>
                </div>
                <div class=""col-xs-6"">
                    <div class=""pull-right"">

                    </div>
                </div>
            </div>
        </div>
        <div class=""col-md-12 clearfix"">
            <dl class=""dl-horizontal"">
                <dt>Cabinet</dt>
                <dd>");
            EndContext();
            BeginContext(1565, 17, false);
#line 42 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\F1Details.cshtml"
               Write(Model.CabinetName);

#line default
#line hidden
            EndContext();
            BeginContext(1582, 62, true);
            WriteLiteral("</dd>\r\n                <dt>Status</dt>\r\n                <dd>\r\n");
            EndContext();
#line 45 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\F1Details.cshtml"
                     if (Model.Status == 1)
                    {

#line default
#line hidden
            BeginContext(1712, 92, true);
            WriteLiteral("                        <span class=\"status-label status-green shadow-style\">Active</span>\r\n");
            EndContext();
#line 48 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\F1Details.cshtml"
                    }

#line default
#line hidden
            BeginContext(1827, 80, true);
            WriteLiteral("                </dd>\r\n                <dt>Created By</dt>\r\n                <dd>");
            EndContext();
            BeginContext(1908, 15, false);
#line 51 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\F1Details.cshtml"
               Write(Model.CreatedBy);

#line default
#line hidden
            EndContext();
            BeginContext(1923, 66, true);
            WriteLiteral("</dd>\r\n                <dt>Created Date</dt>\r\n                <dd>");
            EndContext();
            BeginContext(1990, 73, false);
#line 53 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\F1Details.cshtml"
               Write(Convert.ToDateTime(Model.CreatedDate).ToString("MMM dd-yyyy hh:mm:ss tt"));

#line default
#line hidden
            EndContext();
            BeginContext(2063, 350, true);
            WriteLiteral(@"</dd>
            </dl>
        </div>
    </div>
    <div class=""card"">
        <div class=""header"">
            <div class=""row"">
                <div class=""col-xs-3"">
                    <h4>List</h4>
                </div>
                <div class=""col-xs-9"">
                    <div class=""pull-right"">
                        <a");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 2413, "\"", 2449, 2);
            WriteAttributeValue("", 2420, "/Document/F1Add?q=", 2420, 18, true);
#line 65 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\F1Details.cshtml"
WriteAttributeValue("", 2438, Model.F1Id, 2438, 11, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(2450, 2701, true);
            WriteLiteral(@" class=""btn btn-primary waves-effect btn-sm"" data-toggle=""tooltip"" id=""btnCreateFolder"" title=""Add new Folder""><i class=""fa fa-plus""></i> Add Folder</a>
                    </div>
                </div>
            </div>
        </div>
        <div class=""body"">
            <div id=""divF2List"">
            </div>
        </div>
    </div>
    <div class=""card"">
        <div class=""header"">
            <div class=""row"">
                <div class=""col-xs-3"">
                    <h4>Files</h4>
                </div>
                <div class=""col-xs-9"">
                    <div class=""pull-right"">
                        <input type=""file"" class=""displayNone"" name=""files"" id=""files"" multiple style=""display:none"" />
                        <label for=""files"" class=""btn btn-primary waves-effect btn-sm"" id="""" data-toggle=""tooltip"" title=""Add file""><i class=""fa fa-plus""></i> Add Files</label>
                    </div>
                </div>
            </div>
        </div>
        <div c");
            WriteLiteral(@"lass=""body"">
            <div class=""row display-none"" id=""divFileUpload"">
                <div id=""divSelectedFile""></div>
                <div class=""col-md-12"">
                    <div class=""pull-right"">
                        <button type=""button"" id=""btnFileUpload"" class=""btn btn-info btn-sm margin-bottom"" data-toggle=""tooltip"" title=""Upload selected file""><i class=""fa fa-upload""></i> Upload</button>
                        <button type=""button"" id=""btnFileUploadCancel"" class=""btn btn-default btn-sm margin-bottom"" data-toggle=""tooltip"" title=""Upload cancel""><i class=""fa fa-times""></i> Cancel</button>
                    </div>
                </div>
            </div>
            <div id=""divFileList""></div>
        </div>
    </div>
</div>
<div id=""divFileArchiveWin""></div>
<div id=""divF2EditWin""></div>
<div id=""divDeleteWin""></div>
<script id=""temp_win_file_delete_entry"" type=""text/x-kendo-template"">
    <div style=""padding:1em;"">
        <p style=""font-size: 16px; padding: 0px"" c");
            WriteLiteral(@"lass=""col-red"">
            #=msg# <br />
            <div class=""checkbox"">
                <label style=""font-size:15px;""><input type=""checkbox"" id=""keepCheckBox"" name=""keepCheckBox"" style=""width:20px"">Keep Document</label>
            </div>
        </p>
        <div style=""text-align: right;"">
            <button type=""button"" class=""btn btn-info waves-effect"" id=""btn_file_delete_Entry_noty_ok""><i class=""fa fa-check""></i>&nbsp;OK</button>
            <button type=""button"" class=""btn btn-danger waves-effect"" id=""btn_file_delete_Entry_noty_cancel""><i class=""fa fa-times""></i>&nbsp;Cancel</button>
        </div>
    </div>
</script>
");
            EndContext();
            BeginContext(5151, 54, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "9ada2ce72aaf4cbab8ca7632705a80bc", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<DocumentManagement.ModelViews.Document.F1ModelView> Html { get; private set; }
    }
}
#pragma warning restore 1591
