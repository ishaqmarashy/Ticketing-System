#pragma checksum "C:\Users\ishaq\source\repos\Ticketing System Solution\Ticketing System\Views\Ticket\AdminAllTickets.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "cecee3a874f22737fc334a50a203baae56b79e6b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Ticket_AdminAllTickets), @"mvc.1.0.view", @"/Views/Ticket/AdminAllTickets.cshtml")]
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
#line 1 "C:\Users\ishaq\source\repos\Ticketing System Solution\Ticketing System\Views\_ViewImports.cshtml"
using Ticketing_System;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\ishaq\source\repos\Ticketing System Solution\Ticketing System\Views\_ViewImports.cshtml"
using Ticketing_System.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\Users\ishaq\source\repos\Ticketing System Solution\Ticketing System\Views\Ticket\AdminAllTickets.cshtml"
using System.Data;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\ishaq\source\repos\Ticketing System Solution\Ticketing System\Views\Ticket\AdminAllTickets.cshtml"
using System.Net;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\ishaq\source\repos\Ticketing System Solution\Ticketing System\Views\Ticket\AdminAllTickets.cshtml"
using Newtonsoft.Json.Linq;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\ishaq\source\repos\Ticketing System Solution\Ticketing System\Views\Ticket\AdminAllTickets.cshtml"
using Ticketing_System.Controllers;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cecee3a874f22737fc334a50a203baae56b79e6b", @"/Views/Ticket/AdminAllTickets.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2d49796796fc6e2fb0a6dbb186ddab45aaf040e3", @"/Views/_ViewImports.cshtml")]
    public class Views_Ticket_AdminAllTickets : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Ticketing_System.Models.ErrorViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "SearchTicket", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/js/tableAdminButton.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<h4>My Tickets</h4>\r\n<p>");
#nullable restore
#line 7 "C:\Users\ishaq\source\repos\Ticketing System Solution\Ticketing System\Views\Ticket\AdminAllTickets.cshtml"
Write(TempData["update"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n<p>");
#nullable restore
#line 8 "C:\Users\ishaq\source\repos\Ticketing System Solution\Ticketing System\Views\Ticket\AdminAllTickets.cshtml"
Write(ViewData["id"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n<hr />\r\n<div class=\"row\">\r\n    ");
#nullable restore
#line 11 "C:\Users\ishaq\source\repos\Ticketing System Solution\Ticketing System\Views\Ticket\AdminAllTickets.cshtml"
Write(Html.Raw(@WebUtility.HtmlDecode(ViewData["data"].ToString())));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n</div>\r\n\r\n<div>\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "cecee3a874f22737fc334a50a203baae56b79e6b5864", async() => {
                WriteLiteral("Back to Search");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</div>\r\n\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "cecee3a874f22737fc334a50a203baae56b79e6b7041", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
#nullable restore
#line 18 "C:\Users\ishaq\source\repos\Ticketing System Solution\Ticketing System\Views\Ticket\AdminAllTickets.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion = true;

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-append-version", __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Ticketing_System.Models.ErrorViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
