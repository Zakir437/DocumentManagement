#pragma checksum "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\F1Add.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5e94d57c837300663d91e7507ed9c8597da3b598"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Document_F1Add), @"mvc.1.0.view", @"/Views/Document/F1Add.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Document/F1Add.cshtml", typeof(AspNetCore.Views_Document_F1Add))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5e94d57c837300663d91e7507ed9c8597da3b598", @"/Views/Document/F1Add.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"da19c4b16b29ae1c5018fedbda5ddbae5a75d409", @"/Views/_ViewImports.cshtml")]
    public class Views_Document_F1Add : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<DocumentManagement.ModelViews.Document.F1ModelView>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/css/Document/cabinetCreate.min.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/Document/f1Add.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 2 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\F1Add.cshtml"
Write(Html.HiddenFor(m => m.F1Id));

#line default
#line hidden
            EndContext();
            BeginContext(87, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(90, 32, false);
#line 3 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\F1Add.cshtml"
Write(Html.HiddenFor(m => m.C_countId));

#line default
#line hidden
            EndContext();
            BeginContext(122, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(125, 33, false);
#line 4 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\F1Add.cshtml"
Write(Html.HiddenFor(m => m.F1_CountId));

#line default
#line hidden
            EndContext();
            BeginContext(158, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 5 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\F1Add.cshtml"
  
    ViewData["Title"] = "F1 Add";
    Layout = Context.Request.Headers["x-requested-with"] == "XMLHttpRequest" ? null : "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(322, 69, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "f3cbae42f6024fe7b4aa3be9845dddc5", async() => {
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
            BeginContext(391, 293, true);
            WriteLiteral(@"
<div class=""page-bar"">
    <ul class=""page-breadcrumb"">
        <li>
            <i class=""fa fa-list-ul""></i>
            <a onclick=""onAjaxLoad('Cabinet', '/Document/CabinetTile' )"">Cabinet</a>
            <i class=""fa fa-angle-right""></i>
        </li>
        <li>
            <a");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 684, "\"", 775, 6);
            WriteAttributeValue("", 694, "onAjaxLoad(\'Cabinet", 694, 19, true);
            WriteAttributeValue(" ", 713, "Details\',", 714, 10, true);
            WriteAttributeValue(" ", 723, "\'/Document/CabinetTileDetails?q=", 724, 33, true);
#line 18 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\F1Add.cshtml"
WriteAttributeValue("", 756, Model.CabinetId, 756, 16, false);

#line default
#line hidden
            WriteAttributeValue("", 772, "\'", 772, 1, true);
            WriteAttributeValue(" ", 773, ")", 774, 2, true);
            EndWriteAttribute();
            BeginContext(776, 1, true);
            WriteLiteral(">");
            EndContext();
            BeginContext(778, 17, false);
#line 18 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\F1Add.cshtml"
                                                                                                      Write(Model.CabinetName);

#line default
#line hidden
            EndContext();
            BeginContext(795, 96, true);
            WriteLiteral("</a>\r\n            <i class=\"fa fa-angle-right\"></i>\r\n        </li>\r\n        <li>\r\n            <a");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 891, "\"", 967, 6);
            WriteAttributeValue("", 901, "onAjaxLoad(\'F1", 901, 14, true);
            WriteAttributeValue(" ", 915, "Details\',", 916, 10, true);
            WriteAttributeValue(" ", 925, "\'/Document/F1TileDetails?q=", 926, 28, true);
#line 22 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\F1Add.cshtml"
WriteAttributeValue("", 953, Model.F1Id, 953, 11, false);

#line default
#line hidden
            WriteAttributeValue("", 964, "\'", 964, 1, true);
            WriteAttributeValue(" ", 965, ")", 966, 2, true);
            EndWriteAttribute();
            BeginContext(968, 1, true);
            WriteLiteral(">");
            EndContext();
            BeginContext(970, 10, false);
#line 22 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\F1Add.cshtml"
                                                                                       Write(Model.Name);

#line default
#line hidden
            EndContext();
            BeginContext(980, 96, true);
            WriteLiteral("</a>\r\n            <i class=\"fa fa-angle-right\"></i>\r\n        </li>\r\n        <li>\r\n            <a");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 1076, "\"", 1140, 6);
            WriteAttributeValue("", 1086, "onAjaxLoad(\'F1", 1086, 14, true);
            WriteAttributeValue(" ", 1100, "Add\',", 1101, 6, true);
            WriteAttributeValue(" ", 1106, "\'/Document/F1Add?q=", 1107, 20, true);
#line 26 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\F1Add.cshtml"
WriteAttributeValue("", 1126, Model.F1Id, 1126, 11, false);

#line default
#line hidden
            WriteAttributeValue("", 1137, "\'", 1137, 1, true);
            WriteAttributeValue(" ", 1138, ")", 1139, 2, true);
            EndWriteAttribute();
            BeginContext(1141, 175, true);
            WriteLiteral(">Add</a>\r\n        </li>\r\n    </ul>\r\n</div>\r\n<div class=\"portal-content\">\r\n    <div class=\"card\">\r\n        <div class=\"header\">\r\n            <h4 class=\"col-deep-green\"><strong>");
            EndContext();
            BeginContext(1317, 10, false);
#line 33 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\F1Add.cshtml"
                                          Write(Model.Name);

#line default
#line hidden
            EndContext();
            BeginContext(1327, 293, true);
            WriteLiteral(@"</strong></h4>
        </div>
        <div class=""body"" id=""divForm"">
            <hr />
            <div class=""row form-group"">
                <div class=""col-md-5"">
                    <dl class=""dl-horizontal"">
                        <dt>Cabinet</dt>
                        <dd>");
            EndContext();
            BeginContext(1621, 17, false);
#line 41 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\F1Add.cshtml"
                       Write(Model.CabinetName);

#line default
#line hidden
            EndContext();
            BeginContext(1638, 78, true);
            WriteLiteral("</dd>\r\n                        <dt>Status</dt>\r\n                        <dd>\r\n");
            EndContext();
#line 44 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\F1Add.cshtml"
                             if (Model.Status == 1)
                            {

#line default
#line hidden
            BeginContext(1800, 100, true);
            WriteLiteral("                                <span class=\"status-label status-green shadow-style\">Active</span>\r\n");
            EndContext();
#line 47 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\F1Add.cshtml"
                            }

#line default
#line hidden
            BeginContext(1931, 104, true);
            WriteLiteral("                        </dd>\r\n                        <dt>Created By</dt>\r\n                        <dd>");
            EndContext();
            BeginContext(2036, 15, false);
#line 50 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\F1Add.cshtml"
                       Write(Model.CreatedBy);

#line default
#line hidden
            EndContext();
            BeginContext(2051, 82, true);
            WriteLiteral("</dd>\r\n                        <dt>Created Date</dt>\r\n                        <dd>");
            EndContext();
            BeginContext(2134, 73, false);
#line 52 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\F1Add.cshtml"
                       Write(Convert.ToDateTime(Model.CreatedDate).ToString("MMM dd-yyyy hh:mm:ss tt"));

#line default
#line hidden
            EndContext();
            BeginContext(2207, 1262, true);
            WriteLiteral(@"</dd>
                    </dl>
                </div>
            </div>
            <hr />
            <div class=""row"">
                <div class=""col-xs-3""><h4>Folder</h4></div>
                <div class=""col-xs-9 text-right"">
                    <button class=""btn btn-primary waves-effect"" id=""btnAdd"" data-toggle=""tooltip"" title=""Add folder""><i class=""fa fa-plus""></i> Add</button>
                </div>
            </div>
            <hr />
            <div class=""row form-group"">
                <div class=""col-md-6 col-md-offset-1"" id=""divFolder"">
                    <h4>Please add folder</h4>
                </div>
            </div>
            <div class=""row p-30"">
                <div class=""col-md-6 col-md-offset-1"">
                    <button type=""button"" class=""btn bg-green waves-effect"" id=""btnSave""><i class=""fa fa-check""></i>&nbsp;Save</button>
                    <button type=""reset"" class=""btn  btn-info waves-effect"" id=""btnRefresh""><i class=""fa fa-refresh""></i>&nbsp");
            WriteLiteral(";Refresh</button>\r\n                    <button type=\"button\" class=\"btn bg-red waves-effect\" id=\"btnCancel\"><i class=\"fa fa-times\"></i>&nbsp;Cancel</button>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n");
            EndContext();
            BeginContext(3469, 50, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "50a3b5447ea44130958a73021ba09dd6", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
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
