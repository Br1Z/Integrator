#pragma checksum "C:\Users\YudinAA\source\repos\IntegratorEHZ — копия (3)\IntegratorEHZ\Views\HomeOld\_ListDevice.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "76c5912ea498dcf247ae0d9712e0ecb60170ede9"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_HomeOld__ListDevice), @"mvc.1.0.view", @"/Views/HomeOld/_ListDevice.cshtml")]
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
#nullable restore
#line 1 "C:\Users\YudinAA\source\repos\IntegratorEHZ — копия (3)\IntegratorEHZ\Views\_ViewImports.cshtml"
using IntegratorEHZ;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\YudinAA\source\repos\IntegratorEHZ — копия (3)\IntegratorEHZ\Views\_ViewImports.cshtml"
using IntegratorEHZ.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"76c5912ea498dcf247ae0d9712e0ecb60170ede9", @"/Views/HomeOld/_ListDevice.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"43b269b6c7ef664c14ce8085843fcdafb6c75cc0", @"/Views/_ViewImports.cshtml")]
    public class Views_HomeOld__ListDevice : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<IntegratorEHZ.Models.Device>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/jquery/dist/jquery.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
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
            WriteLiteral("    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "76c5912ea498dcf247ae0d9712e0ecb60170ede93588", async() => {
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
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\YudinAA\source\repos\IntegratorEHZ — копия (3)\IntegratorEHZ\Views\HomeOld\_ListDevice.cshtml"
 if (Model.Count() > 0)
{
    

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\YudinAA\source\repos\IntegratorEHZ — копия (3)\IntegratorEHZ\Views\HomeOld\_ListDevice.cshtml"
     foreach (var item in Model)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <button type=\"button\" data-url=\"");
#nullable restore
#line 7 "C:\Users\YudinAA\source\repos\IntegratorEHZ — копия (3)\IntegratorEHZ\Views\HomeOld\_ListDevice.cshtml"
                                   Write(Url.Action("TabDevice","Home",new { id= item.Id }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\" class=\"list-group-item list-group-item-action deviceButton\">");
#nullable restore
#line 7 "C:\Users\YudinAA\source\repos\IntegratorEHZ — копия (3)\IntegratorEHZ\Views\HomeOld\_ListDevice.cshtml"
                                                                                                                                                    Write(item.IMEI);

#line default
#line hidden
#nullable disable
            WriteLiteral("</button>\r\n");
#nullable restore
#line 8 "C:\Users\YudinAA\source\repos\IntegratorEHZ — копия (3)\IntegratorEHZ\Views\HomeOld\_ListDevice.cshtml"
    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\YudinAA\source\repos\IntegratorEHZ — копия (3)\IntegratorEHZ\Views\HomeOld\_ListDevice.cshtml"
     
}
else
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <p>Не одно устройство ещё не создано</p>\r\n");
#nullable restore
#line 13 "C:\Users\YudinAA\source\repos\IntegratorEHZ — копия (3)\IntegratorEHZ\Views\HomeOld\_ListDevice.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<script>\r\n    $(\".deviceButton\").click(function () {\r\n        var url = $(this).data(\"url\");\r\n        $(\'#ValuesList\').load(url);\r\n    });\r\n</script>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<IntegratorEHZ.Models.Device>> Html { get; private set; }
    }
}
#pragma warning restore 1591
