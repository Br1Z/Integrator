#pragma checksum "C:\Users\YudinAA\source\repos\IntegratorEHZ — копия (3)\IntegratorEHZ\Views\HomeOld\_TabDevice.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4bbe9433bf5f4cead5ec15f05842c886b3fd9ba3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_HomeOld__TabDevice), @"mvc.1.0.view", @"/Views/HomeOld/_TabDevice.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4bbe9433bf5f4cead5ec15f05842c886b3fd9ba3", @"/Views/HomeOld/_TabDevice.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"43b269b6c7ef664c14ce8085843fcdafb6c75cc0", @"/Views/_ViewImports.cshtml")]
    public class Views_HomeOld__TabDevice : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IntegratorEHZ.Models.AboutDevices>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/css/DeviceSKZStyle.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            WriteLiteral("\r\n\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "4bbe9433bf5f4cead5ec15f05842c886b3fd9ba33922", async() => {
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
            WriteLiteral("\r\n<div class=\"row\">\r\n    <div class=\"col-10\"> <h1 class=\"display-4\">");
#nullable restore
#line 6 "C:\Users\YudinAA\source\repos\IntegratorEHZ — копия (3)\IntegratorEHZ\Views\HomeOld\_TabDevice.cshtml"
                                          Write(Model.dataSKZ.device.IMEI);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1> </div>\r\n    <div class=\"col-2\"> <div id=\"LedIndication\"");
            BeginWriteAttribute("class", " class=\"", 258, "\"", 292, 1);
#nullable restore
#line 7 "C:\Users\YudinAA\source\repos\IntegratorEHZ — копия (3)\IntegratorEHZ\Views\HomeOld\_TabDevice.cshtml"
WriteAttributeValue("", 266, ViewBag.IsDeviceConnected, 266, 26, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@"></div> </div>
</div>

<ul class=""nav nav-tabs"" id=""myTab"" role=""tablist"">
    <li class=""nav-item"">
        <a class=""nav-link active"" id=""DeviceData-tab"" data-toggle=""tab"" href=""#DeviceData"" role=""tab"" aria-controls=""DeviceData"" aria-selected=""true"">Данные устройства</a>
    </li>
    <li class=""nav-item"">
        <a class=""nav-link"" id=""DeviceDataJournal-tab"" data-toggle=""tab"" href=""#DeviceDataJournal"" role=""tab"" aria-controls=""DeviceDataJournal"" aria-selected=""false"">Журнал</a>
    </li>
    <li>
        <button type=""button"" class=""btn btn-success"" id=""ModalCall"" data-url=""");
#nullable restore
#line 18 "C:\Users\YudinAA\source\repos\IntegratorEHZ — копия (3)\IntegratorEHZ\Views\HomeOld\_TabDevice.cshtml"
                                                                          Write(Url.Action("UpdatingRegister","Home",new { Id = Model.dataSKZ.device.Id }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\" style=\"margin-left:460px\">\r\n            Изменить\r\n        </button>\r\n    </li>\r\n</ul>\r\n\r\n<div class=\"tab-content\">\r\n    <div class=\"tab-pane fade show active\" id=\"DeviceData\" role=\"tabpanel\" aria-labelledby=\"DeviceData-tab\"> ");
#nullable restore
#line 25 "C:\Users\YudinAA\source\repos\IntegratorEHZ — копия (3)\IntegratorEHZ\Views\HomeOld\_TabDevice.cshtml"
                                                                                                        Write(await Html.PartialAsync("_DeviceData",Model.dataSKZ));

#line default
#line hidden
#nullable disable
            WriteLiteral(" </div>\r\n    <div class=\"tab-pane fade\" id=\"DeviceDataJournal\" role=\"tabpanel\" aria-labelledby=\"DeviceDataJournal-tab\">");
#nullable restore
#line 26 "C:\Users\YudinAA\source\repos\IntegratorEHZ — копия (3)\IntegratorEHZ\Views\HomeOld\_TabDevice.cshtml"
                                                                                                         Write(await Html.PartialAsync("_DeviceDataJournal", Model.dataSKZ));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</div>
</div>



<div class=""modal fade"" id=""formModal"" tabindex=""-1"" role=""dialog"" aria-labelledby=""exampleModalLabel"" aria-hidden=""true"">
    <div class=""modal-dialog modal-lg"" role=""document"">
        <div class=""modal-content"">
            <div class=""modal-header"">
                <h5 class=""modal-title"" id=""exampleModalLabel"">");
#nullable restore
#line 35 "C:\Users\YudinAA\source\repos\IntegratorEHZ — копия (3)\IntegratorEHZ\Views\HomeOld\_TabDevice.cshtml"
                                                          Write(Model.dataSKZ.device.IMEI);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h5>
                <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
                    <span aria-hidden=""true"">&times;</span>
                </button>
            </div>
            <div class=""modal-body"">
                
            </div>
        </div>
    </div>
</div>

<script>
    $(document).ready(function () {

        var connection = new signalR.HubConnectionBuilder().withUrl(""/integratorHub"").build();
        connection.start()
        .then(function () {
            console.log('connection started');
        })
        .catch(error => {
            console.error(error.message);
        });

        connection.on(""GetAllDevices"", function (message) {
            $('#DeviceData').load('");
#nullable restore
#line 60 "C:\Users\YudinAA\source\repos\IntegratorEHZ — копия (3)\IntegratorEHZ\Views\HomeOld\_TabDevice.cshtml"
                              Write(Url.Action("DeviceData","Home", new { Id=Model.dataSKZ.device.Id}));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"');
       });

        connection.on(""OnConnectionIndication"", function (isConnected) {
            console.log(isConnected);
            if (isConnected)
            {
                LedIndication.className = ""led-green"";
            }
            else
            {
                LedIndication.className = ""led-red"";
            }
        });


        $(""#ModalCall"").click(function () {
            $.ajax({
                type: ""GET"",
                url: $(this).data('url'),
                success: function (result) {
                    $('#formModal .modal-body').html(result);
                    $('#formModal').modal('show');
                }

            });
        });
    });



</script>
");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IntegratorEHZ.Models.AboutDevices> Html { get; private set; }
    }
}
#pragma warning restore 1591