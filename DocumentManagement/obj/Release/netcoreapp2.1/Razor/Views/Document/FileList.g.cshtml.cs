#pragma checksum "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\FileList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "eba18c5985316101280b29d044c92f2e7f669c7a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Document_FileList), @"mvc.1.0.view", @"/Views/Document/FileList.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Document/FileList.cshtml", typeof(AspNetCore.Views_Document_FileList))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"eba18c5985316101280b29d044c92f2e7f669c7a", @"/Views/Document/FileList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"da19c4b16b29ae1c5018fedbda5ddbae5a75d409", @"/Views/_ViewImports.cshtml")]
    public class Views_Document_FileList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<DocumentManagement.Models.Data.Files>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\FileList.cshtml"
  
    var count = 0;

#line default
#line hidden
#line 5 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\FileList.cshtml"
 if (Model.Any())
{

#line default
#line hidden
            BeginContext(107, 262, true);
            WriteLiteral(@"    <table class=""table table-hover table-striped"">
        <thead>
            <tr>
                <th width=""50px"">#</th>
                <th>Name</th>
                <th width=""143px"">Actions</th>
            </tr>
        </thead>
        <tbody>
");
            EndContext();
#line 16 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\FileList.cshtml"
             foreach (var list in Model)
            {
                count++;

#line default
#line hidden
            BeginContext(452, 46, true);
            WriteLiteral("                <tr>\r\n                    <td>");
            EndContext();
            BeginContext(499, 5, false);
#line 20 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\FileList.cshtml"
                   Write(count);

#line default
#line hidden
            EndContext();
            BeginContext(504, 34, true);
            WriteLiteral(".</td>\r\n                    <td>\r\n");
            EndContext();
#line 22 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\FileList.cshtml"
                         if (list.FileType == 1)
                        {

#line default
#line hidden
            BeginContext(615, 92, true);
            WriteLiteral("                            <i class=\"k-icon k-i-image file-icon-size img-icon-color\"></i>\r\n");
            EndContext();
#line 25 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\FileList.cshtml"
                        }
                        else if (list.FileType == 2)
                        {

#line default
#line hidden
            BeginContext(815, 95, true);
            WriteLiteral("                            <i class=\"k-icon k-i-file-pdf file-icon-size pdf-icon-color\"></i>\r\n");
            EndContext();
#line 29 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\FileList.cshtml"
                        }
                        else if (list.FileType == 3)
                        {

#line default
#line hidden
            BeginContext(1018, 97, true);
            WriteLiteral("                            <i class=\"k-icon k-i-file-word file-icon-size word-icon-color\"></i>\r\n");
            EndContext();
#line 33 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\FileList.cshtml"
                        }
                        else if (list.FileType == 4)
                        {

#line default
#line hidden
            BeginContext(1223, 95, true);
            WriteLiteral("                            <i class=\"k-icon k-i-file-txt file-icon-size txt-icon-color\"></i>\r\n");
            EndContext();
#line 37 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\FileList.cshtml"
                        }
                        else if (list.FileType == 5)
                        {

#line default
#line hidden
            BeginContext(1426, 94, true);
            WriteLiteral("                            <i class=\"k-icon k-i-file-ppt file-icon-size pp-icon-color\"></i>\r\n");
            EndContext();
#line 41 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\FileList.cshtml"
                        }
                        else if (list.FileType == 6)
                        {

#line default
#line hidden
            BeginContext(1628, 99, true);
            WriteLiteral("                            <i class=\"k-icon k-i-file-excel file-icon-size excel-icon-color\"></i>\r\n");
            EndContext();
#line 45 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\FileList.cshtml"
                        }

#line default
#line hidden
            BeginContext(1754, 24, true);
            WriteLiteral("                        ");
            EndContext();
            BeginContext(1779, 17, false);
#line 46 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\FileList.cshtml"
                   Write(list.OriginalName);

#line default
#line hidden
            EndContext();
            BeginContext(1796, 140, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        <input type=\"file\" class=\"display-none replaceFile\" data-id=\"");
            EndContext();
            BeginContext(1937, 7, false);
#line 49 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\FileList.cshtml"
                                                                                Write(list.Id);

#line default
#line hidden
            EndContext();
            BeginContext(1944, 1, true);
            WriteLiteral("\"");
            EndContext();
            BeginWriteAttribute("name", " name=\"", 1945, "\"", 1972, 2);
            WriteAttributeValue("", 1952, "replaceFile_", 1952, 12, true);
#line 49 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\FileList.cshtml"
WriteAttributeValue("", 1964, list.Id, 1964, 8, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginWriteAttribute("id", " id=\"", 1973, "\"", 1998, 2);
            WriteAttributeValue("", 1978, "replaceFile_", 1978, 12, true);
#line 49 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\FileList.cshtml"
WriteAttributeValue("", 1990, list.Id, 1990, 8, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1999, 56, true);
            WriteLiteral(" style=\"display:none\" />\r\n                        <label");
            EndContext();
            BeginWriteAttribute("for", " for=\"", 2055, "\"", 2081, 2);
            WriteAttributeValue("", 2061, "replaceFile_", 2061, 12, true);
#line 50 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\FileList.cshtml"
WriteAttributeValue("", 2073, list.Id, 2073, 8, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(2082, 197, true);
            WriteLiteral(" class=\"btn btn-warning btn-sm waves-effect  margin-bottom\" data-toggle=\"tooltip\" title=\"File replace\"><i class=\"fa fa-retweet\"></i></label>\r\n                        <button type=\"button\" data-id=\"");
            EndContext();
            BeginContext(2280, 7, false);
#line 51 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\FileList.cshtml"
                                                  Write(list.Id);

#line default
#line hidden
            EndContext();
            BeginContext(2287, 153, true);
            WriteLiteral("\" class=\"btn btn-danger btn-sm waves-effect btnFileDelete margin-bottom\" data-toggle=\"tooltip\" title=\"File delete\"><i class=\"fa fa-trash\"></i></button>\r\n");
            EndContext();
#line 52 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\FileList.cshtml"
                         if (list.IsArchive == true)
                        {

#line default
#line hidden
            BeginContext(2521, 59, true);
            WriteLiteral("                            <button type=\"button\" data-id=\"");
            EndContext();
            BeginContext(2581, 7, false);
#line 54 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\FileList.cshtml"
                                                      Write(list.Id);

#line default
#line hidden
            EndContext();
            BeginContext(2588, 155, true);
            WriteLiteral("\" class=\"btn btn-info btn-sm waves-effect btnViewArchive margin-bottom\" data-toggle=\"tooltip\" title=\"Archive list\"><i class=\"fa fa-archive\"></i></button>\r\n");
            EndContext();
#line 55 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\FileList.cshtml"
                        }

#line default
#line hidden
            BeginContext(2770, 50, true);
            WriteLiteral("                    </td>\r\n                </tr>\r\n");
            EndContext();
#line 58 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\FileList.cshtml"
            }

#line default
#line hidden
            BeginContext(2835, 32, true);
            WriteLiteral("        </tbody>\r\n    </table>\r\n");
            EndContext();
#line 61 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\FileList.cshtml"
}
else
{

#line default
#line hidden
            BeginContext(2879, 44, true);
            WriteLiteral("    <h5>There is no record to display</h5>\r\n");
            EndContext();
#line 65 "E:\Data File\Project\DocumentManagement\DocumentManagement\Views\Document\FileList.cshtml"
}

#line default
#line hidden
            BeginContext(2926, 119, true);
            WriteLiteral("<script>\r\n    $(document).ready(function () {\r\n        $(\'[data-toggle=\"tooltip\"]\').tooltip();\r\n    })\r\n</script>\r\n\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<DocumentManagement.Models.Data.Files>> Html { get; private set; }
    }
}
#pragma warning restore 1591
