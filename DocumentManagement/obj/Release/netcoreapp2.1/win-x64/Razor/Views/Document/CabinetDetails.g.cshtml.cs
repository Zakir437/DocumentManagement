#pragma checksum "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\CabinetDetails.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "da169e232175ec5efeea445b274b66ef9361483d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Document_CabinetDetails), @"mvc.1.0.view", @"/Views/Document/CabinetDetails.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Document/CabinetDetails.cshtml", typeof(AspNetCore.Views_Document_CabinetDetails))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"da169e232175ec5efeea445b274b66ef9361483d", @"/Views/Document/CabinetDetails.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"da19c4b16b29ae1c5018fedbda5ddbae5a75d409", @"/Views/_ViewImports.cshtml")]
    public class Views_Document_CabinetDetails : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<DocumentManagement.ModelViews.Document.CabinetModelView>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/css/bg-page-content.min.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/css/fileUpload.min.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/Document/cabinetDetails.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            BeginContext(65, 34, false);
#line 2 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\CabinetDetails.cshtml"
Write(Html.HiddenFor(m => m.EncryptedId));

#line default
#line hidden
            EndContext();
            BeginContext(99, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\CabinetDetails.cshtml"
  
    ViewData["Title"] = "Cabinet Details";
    Layout = Context.Request.Headers["x-requested-with"] == "XMLHttpRequest" ? null : "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(272, 62, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "f63ef620832e4687a15ef4837e4a938f", async() => {
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
            BeginContext(334, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(336, 57, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "d56f0c5cffd64edd92ec6a542886e6b6", async() => {
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
            BeginContext(393, 289, true);
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
            BeginWriteAttribute("onclick", " onclick=\"", 682, "\"", 771, 6);
            WriteAttributeValue("", 692, "onAjaxLoad(\'Cabinet", 692, 19, true);
            WriteAttributeValue(" ", 711, "Details\',", 712, 10, true);
            WriteAttributeValue(" ", 721, "\'/Document/CabinetDetails?q=", 722, 29, true);
#line 17 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\CabinetDetails.cshtml"
WriteAttributeValue("", 750, Model.EncryptedId, 750, 18, false);

#line default
#line hidden
            WriteAttributeValue("", 768, "\'", 768, 1, true);
            WriteAttributeValue(" ", 769, ")", 770, 2, true);
            EndWriteAttribute();
            BeginContext(772, 265, true);
            WriteLiteral(@">Cabinet Details</a>
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
            BeginContext(1038, 17, false);
#line 26 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\CabinetDetails.cshtml"
                                                 Write(Model.CabinetName);

#line default
#line hidden
            EndContext();
            BeginContext(1055, 352, true);
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
                <dt>Status</dt>
                <dd>
");
            EndContext();
#line 39 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\CabinetDetails.cshtml"
                     if (Model.Status == 1)
                    {

#line default
#line hidden
            BeginContext(1475, 92, true);
            WriteLiteral("                        <span class=\"status-label status-green shadow-style\">Active</span>\r\n");
            EndContext();
#line 42 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\CabinetDetails.cshtml"
                    }

#line default
#line hidden
            BeginContext(1590, 80, true);
            WriteLiteral("                </dd>\r\n                <dt>Created By</dt>\r\n                <dd>");
            EndContext();
            BeginContext(1671, 15, false);
#line 45 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\CabinetDetails.cshtml"
               Write(Model.CreatedBy);

#line default
#line hidden
            EndContext();
            BeginContext(1686, 66, true);
            WriteLiteral("</dd>\r\n                <dt>Created Date</dt>\r\n                <dd>");
            EndContext();
            BeginContext(1753, 73, false);
#line 47 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\CabinetDetails.cshtml"
               Write(Convert.ToDateTime(Model.CreatedDate).ToString("MMM dd-yyyy hh:mm:ss tt"));

#line default
#line hidden
            EndContext();
            BeginContext(1826, 353, true);
            WriteLiteral(@"</dd>
            </dl>
        </div>
    </div>
    <div class=""card"">
        <div class=""header"">
            <div class=""row"">
                <div class=""col-xs-3"">
                    <h4>Folders</h4>
                </div>
                <div class=""col-xs-9"">
                    <div class=""pull-right"">
                        <a");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 2179, "\"", 2230, 2);
            WriteAttributeValue("", 2186, "/Document/CabinetCreate?q=", 2186, 26, true);
#line 59 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\CabinetDetails.cshtml"
WriteAttributeValue("", 2212, Model.EncryptedId, 2212, 18, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(2231, 2754, true);
            WriteLiteral(@" class=""btn btn-primary waves-effect btn-sm"" data-toggle=""tooltip"" id=""btnCreateFolder"" title=""Add Master Folder""><i class=""fa fa-plus""></i> Add Folder</a>
                    </div>
                </div>
            </div>
        </div>
        <div class=""body"">
            <div id=""divF1List"">
                <div class=""tiny_loading""></div>
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
                </div");
            WriteLiteral(@">
            </div>
        </div>
        <div class=""body"">
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
<div id=""divF1EditWin""></div>
<div id=""divDeleteWin""></div>
<script id=""temp_win_file_delete_entry"" type=""text/x-kendo-template"">
    <div style=""padding:1em;""");
            WriteLiteral(@">
        <p style=""font-size: 16px; padding: 0px"" class=""col-red"">
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
            BeginContext(4985, 59, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "41f83bf7c23d4afba9039df45e98c9f0", async() => {
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<DocumentManagement.ModelViews.Document.CabinetModelView> Html { get; private set; }
    }
}
#pragma warning restore 1591
