#pragma checksum "C:\Users\YudinAA\source\repos\IntegratorEHZ — копия (3)\IntegratorEHZ\Views\HomeOld\_DeviceData.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "686b9b78b49f9f99a6593dfe02bcba2b2aacba56"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_HomeOld__DeviceData), @"mvc.1.0.view", @"/Views/HomeOld/_DeviceData.cshtml")]
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
#nullable restore
#line 1 "C:\Users\YudinAA\source\repos\IntegratorEHZ — копия (3)\IntegratorEHZ\Views\HomeOld\_DeviceData.cshtml"
using IntegratorEHZ.App_Code;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"686b9b78b49f9f99a6593dfe02bcba2b2aacba56", @"/Views/HomeOld/_DeviceData.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"43b269b6c7ef664c14ce8085843fcdafb6c75cc0", @"/Views/_ViewImports.cshtml")]
    public class Views_HomeOld__DeviceData : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IntegratorEHZ.Models.DataSKZ>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<style>
    td {
        font-size: 100%;
        padding: 0px;
    }

    .Headers {
        width: 500px;
        height: 10px;
        font-size: smaller;
        text-align: right;
    }

    .Cell {
        width: 350px;
        font-size: smaller;
        text-align: left;
        margin-left: -40px;
    }

    .PowerModuleStatusImg {
        width: 16px;
        margin: 2px;
    }

    .ParamTelesignal {
        width: 16px;
        margin: 3px;
    }

    .tooltip-inner {
        background-color: #007bff !important;
        /*!important is not necessary if you place custom.css at the end of your css calls. For the purpose of this demo, it seems to be required in SO snippet*/
        color: #fff;
    }

    .tooltip.top .tooltip-arrow {
        border-top-color: #007bff;
    }

    .tooltip.right .tooltip-arrow {
        border-right-color: #007bff;
    }

    .tooltip.bottom .tooltip-arrow {
        border-bottom-color: #007bff;
    }

    .tooltip.le");
            WriteLiteral(@"ft .tooltip-arrow {
        border-left-color: #007bff;
    }

</style>

<div class=""border border-primary rounded"" style=""overflow:auto; height:684px"">
    <div class=""container"">

        <div class=""row"">
            <table class=""table border"">
                <tr><td class=""Headers p-1"">Напряжение питающей сети 1:              </td><td class=""Cell p-1"">");
#nullable restore
#line 63 "C:\Users\YudinAA\source\repos\IntegratorEHZ — копия (3)\IntegratorEHZ\Views\HomeOld\_DeviceData.cshtml"
                                                                                                          Write(Model.PowerLineVoltage1);

#line default
#line hidden
#nullable disable
            WriteLiteral("  </td></tr>\r\n                <tr><td class=\"Headers p-1\">Значение счетчика электроэнергии 1:      </td><td class=\"Cell p-1\">");
#nullable restore
#line 64 "C:\Users\YudinAA\source\repos\IntegratorEHZ — копия (3)\IntegratorEHZ\Views\HomeOld\_DeviceData.cshtml"
                                                                                                          Write(Model.EnergyMeterValue1);

#line default
#line hidden
#nullable disable
            WriteLiteral("  </td></tr>\r\n                <tr><td class=\"Headers p-1\">Напряжение питающей сети 2:              </td><td class=\"Cell p-1\">");
#nullable restore
#line 65 "C:\Users\YudinAA\source\repos\IntegratorEHZ — копия (3)\IntegratorEHZ\Views\HomeOld\_DeviceData.cshtml"
                                                                                                          Write(Model.PowerLineVoltage2);

#line default
#line hidden
#nullable disable
            WriteLiteral("  </td></tr>\r\n                <tr><td class=\"Headers p-1\">Значение счетчика электроэнергии 2:      </td><td class=\"Cell p-1\">");
#nullable restore
#line 66 "C:\Users\YudinAA\source\repos\IntegratorEHZ — копия (3)\IntegratorEHZ\Views\HomeOld\_DeviceData.cshtml"
                                                                                                          Write(Model.EnergyMeterValue2);

#line default
#line hidden
#nullable disable
            WriteLiteral("  </td></tr>\r\n                <tr><td class=\"Headers p-1\">Время наработки:                         </td><td class=\"Cell p-1\">");
#nullable restore
#line 67 "C:\Users\YudinAA\source\repos\IntegratorEHZ — копия (3)\IntegratorEHZ\Views\HomeOld\_DeviceData.cshtml"
                                                                                                          Write(Model.WorkTime);

#line default
#line hidden
#nullable disable
            WriteLiteral("           </td></tr>\r\n                <tr><td class=\"Headers p-1\">Время защиты сооружения:                 </td><td class=\"Cell p-1\">");
#nullable restore
#line 68 "C:\Users\YudinAA\source\repos\IntegratorEHZ — копия (3)\IntegratorEHZ\Views\HomeOld\_DeviceData.cshtml"
                                                                                                          Write(Model.ProtectionTime);

#line default
#line hidden
#nullable disable
            WriteLiteral("     </td></tr>\r\n                <tr><td class=\"Headers p-1\">Выходной ток:                            </td><td class=\"Cell p-1\">");
#nullable restore
#line 69 "C:\Users\YudinAA\source\repos\IntegratorEHZ — копия (3)\IntegratorEHZ\Views\HomeOld\_DeviceData.cshtml"
                                                                                                          Write(Model.OutputCurrent);

#line default
#line hidden
#nullable disable
            WriteLiteral("      </td></tr>\r\n                <tr><td class=\"Headers p-1\">Выходное напряжение:                     </td><td class=\"Cell p-1\">");
#nullable restore
#line 70 "C:\Users\YudinAA\source\repos\IntegratorEHZ — копия (3)\IntegratorEHZ\Views\HomeOld\_DeviceData.cshtml"
                                                                                                          Write(Model.OutputVoltage);

#line default
#line hidden
#nullable disable
            WriteLiteral("      </td></tr>\r\n                <tr><td class=\"Headers p-1\">Защитный потенциал, суммарный:           </td><td class=\"Cell p-1\">");
#nullable restore
#line 71 "C:\Users\YudinAA\source\repos\IntegratorEHZ — копия (3)\IntegratorEHZ\Views\HomeOld\_DeviceData.cshtml"
                                                                                                          Write(Model.SumProtectPotent);

#line default
#line hidden
#nullable disable
            WriteLiteral("   </td></tr>\r\n                <tr><td class=\"Headers p-1\">Защитный потенциал, поляризационный:     </td><td class=\"Cell p-1\">");
#nullable restore
#line 72 "C:\Users\YudinAA\source\repos\IntegratorEHZ — копия (3)\IntegratorEHZ\Views\HomeOld\_DeviceData.cshtml"
                                                                                                          Write(Model.PolProtectPotent);

#line default
#line hidden
#nullable disable
            WriteLiteral("   </td></tr>\r\n                <tr><td class=\"Headers p-1\">Режим управления станцией:               </td><td class=\"Cell p-1\">");
#nullable restore
#line 73 "C:\Users\YudinAA\source\repos\IntegratorEHZ — копия (3)\IntegratorEHZ\Views\HomeOld\_DeviceData.cshtml"
                                                                                                          Write(Model.ControlMode);

#line default
#line hidden
#nullable disable
            WriteLiteral("        </td></tr>\r\n                <tr><td class=\"Headers p-1\">Задание выходного тока:                  </td><td class=\"Cell p-1\">");
#nullable restore
#line 74 "C:\Users\YudinAA\source\repos\IntegratorEHZ — копия (3)\IntegratorEHZ\Views\HomeOld\_DeviceData.cshtml"
                                                                                                          Write(Model.SetOutputCurrent);

#line default
#line hidden
#nullable disable
            WriteLiteral("   </td></tr>\r\n                <tr><td class=\"Headers p-1\">Задание сум. потенциала:                 </td><td class=\"Cell p-1\">");
#nullable restore
#line 75 "C:\Users\YudinAA\source\repos\IntegratorEHZ — копия (3)\IntegratorEHZ\Views\HomeOld\_DeviceData.cshtml"
                                                                                                          Write(Model.SetSumPotential);

#line default
#line hidden
#nullable disable
            WriteLiteral("    </td></tr>\r\n                <tr><td class=\"Headers p-1\">Задание поляр. потенциала:               </td><td class=\"Cell p-1\">");
#nullable restore
#line 76 "C:\Users\YudinAA\source\repos\IntegratorEHZ — копия (3)\IntegratorEHZ\Views\HomeOld\_DeviceData.cshtml"
                                                                                                          Write(Model.SetPolPotential);

#line default
#line hidden
#nullable disable
            WriteLiteral("    </td></tr>\r\n                <tr><td class=\"Headers p-1\">Управление режимами стабилизации станции:</td><td class=\"Cell p-1\">");
#nullable restore
#line 77 "C:\Users\YudinAA\source\repos\IntegratorEHZ — копия (3)\IntegratorEHZ\Views\HomeOld\_DeviceData.cshtml"
                                                                                                          Write(Model.SetStabControlMode);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </td></tr>\r\n            </table>\r\n        </div>\r\n        <div class=\"row justify-content-center\">\r\n            Состояние силомых модулей\r\n        </div>\r\n        <div class=\"row justify-content-center\">\r\n            ");
#nullable restore
#line 84 "C:\Users\YudinAA\source\repos\IntegratorEHZ — копия (3)\IntegratorEHZ\Views\HomeOld\_DeviceData.cshtml"
       Write(Html.CheckStatus((UInt16)Model.PowerModuleState1, 1));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            ");
#nullable restore
#line 85 "C:\Users\YudinAA\source\repos\IntegratorEHZ — копия (3)\IntegratorEHZ\Views\HomeOld\_DeviceData.cshtml"
       Write(Html.CheckStatus((UInt16)Model.PowerModuleState2, 2));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            ");
#nullable restore
#line 86 "C:\Users\YudinAA\source\repos\IntegratorEHZ — копия (3)\IntegratorEHZ\Views\HomeOld\_DeviceData.cshtml"
       Write(Html.CheckStatus((UInt16)Model.PowerModuleState3, 3));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            ");
#nullable restore
#line 87 "C:\Users\YudinAA\source\repos\IntegratorEHZ — копия (3)\IntegratorEHZ\Views\HomeOld\_DeviceData.cshtml"
       Write(Html.CheckStatus((UInt16)Model.PowerModuleState4, 4));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            ");
#nullable restore
#line 88 "C:\Users\YudinAA\source\repos\IntegratorEHZ — копия (3)\IntegratorEHZ\Views\HomeOld\_DeviceData.cshtml"
       Write(Html.CheckStatus((UInt16)Model.PowerModuleState5, 5));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            ");
#nullable restore
#line 89 "C:\Users\YudinAA\source\repos\IntegratorEHZ — копия (3)\IntegratorEHZ\Views\HomeOld\_DeviceData.cshtml"
       Write(Html.CheckStatus((UInt16)Model.PowerModuleState6, 6));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            ");
#nullable restore
#line 90 "C:\Users\YudinAA\source\repos\IntegratorEHZ — копия (3)\IntegratorEHZ\Views\HomeOld\_DeviceData.cshtml"
       Write(Html.CheckStatus((UInt16)Model.PowerModuleState7, 7));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            ");
#nullable restore
#line 91 "C:\Users\YudinAA\source\repos\IntegratorEHZ — копия (3)\IntegratorEHZ\Views\HomeOld\_DeviceData.cshtml"
       Write(Html.CheckStatus((UInt16)Model.PowerModuleState8, 8));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            ");
#nullable restore
#line 92 "C:\Users\YudinAA\source\repos\IntegratorEHZ — копия (3)\IntegratorEHZ\Views\HomeOld\_DeviceData.cshtml"
       Write(Html.CheckStatus((UInt16)Model.PowerModuleState9, 9));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            ");
#nullable restore
#line 93 "C:\Users\YudinAA\source\repos\IntegratorEHZ — копия (3)\IntegratorEHZ\Views\HomeOld\_DeviceData.cshtml"
       Write(Html.CheckStatus((UInt16)Model.PowerModuleState10, 10));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            ");
#nullable restore
#line 94 "C:\Users\YudinAA\source\repos\IntegratorEHZ — копия (3)\IntegratorEHZ\Views\HomeOld\_DeviceData.cshtml"
       Write(Html.CheckStatus((UInt16)Model.PowerModuleState11, 11));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            ");
#nullable restore
#line 95 "C:\Users\YudinAA\source\repos\IntegratorEHZ — копия (3)\IntegratorEHZ\Views\HomeOld\_DeviceData.cshtml"
       Write(Html.CheckStatus((UInt16)Model.PowerModuleState12, 12));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n        <div class=\"row justify-content-center\">\r\n            Параметры телесигнализации\r\n        </div>\r\n        <div class=\"row justify-content-center \">\r\n\r\n            ");
#nullable restore
#line 102 "C:\Users\YudinAA\source\repos\IntegratorEHZ — копия (3)\IntegratorEHZ\Views\HomeOld\_DeviceData.cshtml"
       Write(Html.CheckTheTelesignalizationParameters(Model.UnauthorisedAccess, "Несанкционированный доступ в шкаф станции(блок - бокс)"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            ");
#nullable restore
#line 103 "C:\Users\YudinAA\source\repos\IntegratorEHZ — копия (3)\IntegratorEHZ\Views\HomeOld\_DeviceData.cshtml"
       Write(Html.CheckTheTelesignalizationParameters(Model.StationControllMode, "Режим управления станцией"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            ");
#nullable restore
#line 104 "C:\Users\YudinAA\source\repos\IntegratorEHZ — копия (3)\IntegratorEHZ\Views\HomeOld\_DeviceData.cshtml"
       Write(Html.CheckTheTelesignalizationParameters(Model.StationFailure, "Неисправность станции"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            ");
#nullable restore
#line 105 "C:\Users\YudinAA\source\repos\IntegratorEHZ — копия (3)\IntegratorEHZ\Views\HomeOld\_DeviceData.cshtml"
       Write(Html.CheckTheTelesignalizationParameters(Model.LineDisconnection, "Обрыв измерительных цепей от защищаемого сооружения или от электрода сравнения"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            ");
#nullable restore
#line 106 "C:\Users\YudinAA\source\repos\IntegratorEHZ — копия (3)\IntegratorEHZ\Views\HomeOld\_DeviceData.cshtml"
       Write(Html.CheckTheTelesignalizationParameters(Model.MainOrReserved, "Включение группы основных или резервных силовых модулей (СКЗ)"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            ");
#nullable restore
#line 107 "C:\Users\YudinAA\source\repos\IntegratorEHZ — копия (3)\IntegratorEHZ\Views\HomeOld\_DeviceData.cshtml"
       Write(Html.CheckTheTelesignalizationParameters(Model.DistantPowerControl, "Дистанционное отключение и включение силовых модулей"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n    </div>\r\n</div>\r\n<script>\r\n      $(function () {\r\n        $(\'[data-toggle=\"tooltip\"]\').tooltip()\r\n    })\r\n\r\n</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IntegratorEHZ.Models.DataSKZ> Html { get; private set; }
    }
}
#pragma warning restore 1591
