#pragma checksum "C:\Users\killme\source\repos\GameInfo\GameInfo\Views\Guides\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "cfe206be9fd6c3f944d4ddea86b27bd3d976c352"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Guides_Details), @"mvc.1.0.view", @"/Views/Guides/Details.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Guides/Details.cshtml", typeof(AspNetCore.Views_Guides_Details))]
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
#line 1 "C:\Users\killme\source\repos\GameInfo\GameInfo\Views\_ViewImports.cshtml"
using GameInfo;

#line default
#line hidden
#line 2 "C:\Users\killme\source\repos\GameInfo\GameInfo\Views\_ViewImports.cshtml"
using GameInfo.Models;

#line default
#line hidden
#line 5 "C:\Users\killme\source\repos\GameInfo\GameInfo\Views\Guides\Details.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#line 6 "C:\Users\killme\source\repos\GameInfo\GameInfo\Views\Guides\Details.cshtml"
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cfe206be9fd6c3f944d4ddea86b27bd3d976c352", @"/Views/Guides/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3a50ec5626bc0f71447e25d817b93ab3f3b5953d", @"/Views/_ViewImports.cshtml")]
    public class Views_Guides_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<GameInfo.Models.ViewModels.GuideDetailsViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Guides", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Delete", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "get", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "C:\Users\killme\source\repos\GameInfo\GameInfo\Views\Guides\Details.cshtml"
  
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(207, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(260, 237, true);
            WriteLiteral("\r\n<br />\r\n<div class=\"container-fluid\" style=\"border: solid 1px white; border-radius: 10px; padding: 6px 6px 20px 6px\">\r\n    <div class=\"row\" style=\"padding: 4px 12px 4px 12px\">\r\n        <div class=\"col-lg-2 text-white\">\r\n            <p>");
            EndContext();
            BeginContext(498, 14, false);
#line 14 "C:\Users\killme\source\repos\GameInfo\GameInfo\Views\Guides\Details.cshtml"
          Write(Model.UserName);

#line default
#line hidden
            EndContext();
            BeginContext(512, 22, true);
            WriteLiteral("</p>\r\n            <img");
            EndContext();
            BeginWriteAttribute("src", " src=\"", 534, "\"", 557, 1);
#line 15 "C:\Users\killme\source\repos\GameInfo\GameInfo\Views\Guides\Details.cshtml"
WriteAttributeValue("", 540, Model.UserAvatar, 540, 17, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(558, 194, true);
            WriteLiteral(" alt=\"Account avatar\" style=\"height: 100px; width: 100px; padding-right:10px\" />\r\n        </div>\r\n        <div class=\"col-md-9 text-white\" style=\"border-left: 1px solid white\">\r\n            <h1>");
            EndContext();
            BeginContext(753, 11, false);
#line 18 "C:\Users\killme\source\repos\GameInfo\GameInfo\Views\Guides\Details.cshtml"
           Write(Model.Title);

#line default
#line hidden
            EndContext();
            BeginContext(764, 117, true);
            WriteLiteral("</h1>\r\n            <hr style=\"background-color: white; height: 1px\" />\r\n            <p style=\"word-wrap: break-word\">");
            EndContext();
            BeginContext(882, 23, false);
#line 20 "C:\Users\killme\source\repos\GameInfo\GameInfo\Views\Guides\Details.cshtml"
                                        Write(Html.Raw(Model.Content));

#line default
#line hidden
            EndContext();
            BeginContext(905, 131, true);
            WriteLiteral("</p>\r\n        </div>\r\n    </div>\r\n    <hr style=\"background-color: white;\" />\r\n    <div class=\"row\">\r\n        <div class=\"col-6\">\r\n");
            EndContext();
#line 26 "C:\Users\killme\source\repos\GameInfo\GameInfo\Views\Guides\Details.cshtml"
             if (SignInManager.IsSignedIn(User) && User.IsInRole("Admin"))
            {

#line default
#line hidden
            BeginContext(1127, 16, true);
            WriteLiteral("                ");
            EndContext();
            BeginContext(1143, 177, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "33d177ef3b37424da7fbc7eb467cbe18", async() => {
                BeginContext(1218, 95, true);
                WriteLiteral("\r\n                    <button type=\"submit\" class=\"btn\">Delete guide</button>\r\n                ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 28 "C:\Users\killme\source\repos\GameInfo\GameInfo\Views\Guides\Details.cshtml"
                                                                    WriteLiteral(Model.Id);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1320, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 31 "C:\Users\killme\source\repos\GameInfo\GameInfo\Views\Guides\Details.cshtml"
            }

#line default
#line hidden
            BeginContext(1337, 68, true);
            WriteLiteral("        </div>\r\n        <div class=\"col-6 text-right\">\r\n            ");
            EndContext();
            BeginContext(1405, 162, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ec24d10a3de94ea9b18850906f9ddb47", async() => {
                BeginContext(1467, 93, true);
                WriteLiteral("\r\n                <button type=\"submit\" class=\"btn\">Back to all guides</button>\r\n            ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1567, 44, true);
            WriteLiteral("\r\n        </div>\r\n    </div>\r\n</div>\r\n<br />");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public SignInManager<GameInfoUser> SignInManager { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<GameInfo.Models.ViewModels.GuideDetailsViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
