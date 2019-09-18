#pragma checksum "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\RecentFileList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4077b5e6c31db760a296b7630039429424989d2b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Document_RecentFileList), @"mvc.1.0.view", @"/Views/Document/RecentFileList.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Document/RecentFileList.cshtml", typeof(AspNetCore.Views_Document_RecentFileList))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4077b5e6c31db760a296b7630039429424989d2b", @"/Views/Document/RecentFileList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"da19c4b16b29ae1c5018fedbda5ddbae5a75d409", @"/Views/_ViewImports.cshtml")]
    public class Views_Document_RecentFileList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<DocumentManagement.ModelViews.Document.RecentFileModelView>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\RecentFileList.cshtml"
 if (Model.Any())
{

#line default
#line hidden
            BeginContext(102, 227, true);
            WriteLiteral("    <table class=\"table table-hover\">\r\n        <thead>\r\n            <tr>\r\n                <th>Name</th>\r\n                <th>Location</th>\r\n                <th>Action</th>\r\n            </tr>\r\n        </thead>\r\n        <tbody>\r\n");
            EndContext();
#line 13 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\RecentFileList.cshtml"
             foreach (var list in Model)
            {

#line default
#line hidden
            BeginContext(386, 48, true);
            WriteLiteral("                <tr>\r\n                    <td>\r\n");
            EndContext();
#line 17 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\RecentFileList.cshtml"
                         if (list.FileType == 1)
                        {

#line default
#line hidden
            BeginContext(511, 80, true);
            WriteLiteral("                            <span class=\"k-icon k-i-image color-image\"></span>\r\n");
            EndContext();
#line 20 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\RecentFileList.cshtml"
                        }
                        else if (list.FileType == 2)
                        {

#line default
#line hidden
            BeginContext(699, 76, true);
            WriteLiteral("                            <span class=\"k-icon k-i-pdf color-pdf\"></span>\r\n");
            EndContext();
#line 24 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\RecentFileList.cshtml"
                        }
                        else if (list.FileType == 3)
                        {

#line default
#line hidden
            BeginContext(883, 78, true);
            WriteLiteral("                            <span class=\"k-icon k-i-word color-word\"></span>\r\n");
            EndContext();
#line 28 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\RecentFileList.cshtml"
                        }
                        else if (list.FileType == 4)
                        {

#line default
#line hidden
            BeginContext(1069, 77, true);
            WriteLiteral("                            <span class=\"k-icon k-i-txt color-text\"></span>\r\n");
            EndContext();
#line 32 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\RecentFileList.cshtml"
                        }
                        else if (list.FileType == 5)
                        {

#line default
#line hidden
            BeginContext(1254, 76, true);
            WriteLiteral("                            <span class=\"k-icon k-i-ppt color-ppt\"></span>\r\n");
            EndContext();
#line 36 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\RecentFileList.cshtml"
                        }
                        else if (list.FileType == 6)
                        {

#line default
#line hidden
            BeginContext(1438, 80, true);
            WriteLiteral("                            <span class=\"k-icon k-i-excel color-excel\"></span>\r\n");
            EndContext();
#line 40 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\RecentFileList.cshtml"
                        }

#line default
#line hidden
            BeginContext(1545, 24, true);
            WriteLiteral("                        ");
            EndContext();
            BeginContext(1570, 9, false);
#line 41 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\RecentFileList.cshtml"
                   Write(list.Name);

#line default
#line hidden
            EndContext();
            BeginContext(1579, 53, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>");
            EndContext();
            BeginContext(1633, 13, false);
#line 43 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\RecentFileList.cshtml"
                   Write(list.Location);

#line default
#line hidden
            EndContext();
            BeginContext(1646, 123, true);
            WriteLiteral("</td>\r\n                    <td>\r\n                        <button class=\"btn btn-info waves-effect btn-sm btnView\" data-id=\"");
            EndContext();
            BeginContext(1770, 16, false);
#line 45 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\RecentFileList.cshtml"
                                                                                     Write(list.ProtectedId);

#line default
#line hidden
            EndContext();
            BeginContext(1786, 13, true);
            WriteLiteral("\" data-type=\"");
            EndContext();
            BeginContext(1800, 9, false);
#line 45 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\RecentFileList.cshtml"
                                                                                                                   Write(list.Type);

#line default
#line hidden
            EndContext();
            BeginContext(1809, 132, true);
            WriteLiteral("\"><i class=\"fa fa-eye\"></i></button>\r\n                        <button class=\"btn btn-danger waves-effect btn-sm btnDelete\" data-id=\"");
            EndContext();
            BeginContext(1942, 7, false);
#line 46 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\RecentFileList.cshtml"
                                                                                         Write(list.Id);

#line default
#line hidden
            EndContext();
            BeginContext(1949, 90, true);
            WriteLiteral("\"><i class=\"fa fa-trash\"></i></button>\r\n                    </td>\r\n                </tr>\r\n");
            EndContext();
#line 49 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\RecentFileList.cshtml"
            }

#line default
#line hidden
            BeginContext(2054, 32, true);
            WriteLiteral("        </tbody>\r\n    </table>\r\n");
            EndContext();
#line 52 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\RecentFileList.cshtml"
}
else
{

#line default
#line hidden
            BeginContext(2098, 44, true);
            WriteLiteral("    <h5>There is no record to display</h5>\r\n");
            EndContext();
#line 56 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\RecentFileList.cshtml"
}

#line default
#line hidden
            BeginContext(2145, 4, true);
            WriteLiteral("\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<DocumentManagement.ModelViews.Document.RecentFileModelView>> Html { get; private set; }
    }
}
#pragma warning restore 1591
