#pragma checksum "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\_StorageInfo.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2eb5516b452e4e728712cd6cab390aa1d3b31ae3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Document__StorageInfo), @"mvc.1.0.view", @"/Views/Document/_StorageInfo.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Document/_StorageInfo.cshtml", typeof(AspNetCore.Views_Document__StorageInfo))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2eb5516b452e4e728712cd6cab390aa1d3b31ae3", @"/Views/Document/_StorageInfo.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"da19c4b16b29ae1c5018fedbda5ddbae5a75d409", @"/Views/_ViewImports.cshtml")]
    public class Views_Document__StorageInfo : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<DocumentManagement.ModelViews.Document.StorageModelView>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(64, 60, true);
            WriteLiteral("<dl class=\"dl-horizontal\">\r\n    <dt>Allocated</dt>\r\n    <dd>");
            EndContext();
            BeginContext(125, 15, false);
#line 4 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\_StorageInfo.cshtml"
   Write(Model.Allocated);

#line default
#line hidden
            EndContext();
            BeginContext(140, 34, true);
            WriteLiteral("</dd>\r\n    <dt>Used</dt>\r\n    <dd>");
            EndContext();
            BeginContext(175, 10, false);
#line 6 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\_StorageInfo.cshtml"
   Write(Model.Used);

#line default
#line hidden
            EndContext();
            BeginContext(185, 39, true);
            WriteLiteral("</dd>\r\n    <dt>Available</dt>\r\n    <dd>");
            EndContext();
            BeginContext(225, 15, false);
#line 8 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\_StorageInfo.cshtml"
   Write(Model.Available);

#line default
#line hidden
            EndContext();
            BeginContext(240, 82, true);
            WriteLiteral("</dd>\r\n</dl>\r\n<hr />\r\n<dl class=\"dl-horizontal\">\r\n    <dt>Documents</dt>\r\n    <dd>");
            EndContext();
            BeginContext(323, 15, false);
#line 13 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\_StorageInfo.cshtml"
   Write(Model.Documents);

#line default
#line hidden
            EndContext();
            BeginContext(338, 35, true);
            WriteLiteral("</dd>\r\n    <dt>Image</dt>\r\n    <dd>");
            EndContext();
            BeginContext(374, 11, false);
#line 15 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\_StorageInfo.cshtml"
   Write(Model.Image);

#line default
#line hidden
            EndContext();
            BeginContext(385, 35, true);
            WriteLiteral("</dd>\r\n    <dt>Audio</dt>\r\n    <dd>");
            EndContext();
            BeginContext(421, 11, false);
#line 17 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\_StorageInfo.cshtml"
   Write(Model.Audio);

#line default
#line hidden
            EndContext();
            BeginContext(432, 35, true);
            WriteLiteral("</dd>\r\n    <dt>Video</dt>\r\n    <dd>");
            EndContext();
            BeginContext(468, 11, false);
#line 19 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\_StorageInfo.cshtml"
   Write(Model.Video);

#line default
#line hidden
            EndContext();
            BeginContext(479, 35, true);
            WriteLiteral("</dd>\r\n    <dt>Files</dt>\r\n    <dd>");
            EndContext();
            BeginContext(515, 11, false);
#line 21 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\_StorageInfo.cshtml"
   Write(Model.Files);

#line default
#line hidden
            EndContext();
            BeginContext(526, 12, true);
            WriteLiteral("</dd>\r\n</dl>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<DocumentManagement.ModelViews.Document.StorageModelView> Html { get; private set; }
    }
}
#pragma warning restore 1591