#pragma checksum "C:\Projetos\ControleFinanceiro\ControleFinanceiro\Views\Users\DownloadPdfAll.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "584735107284b02322c5a2d9d6b9b00805224740"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Users_DownloadPdfAll), @"mvc.1.0.view", @"/Views/Users/DownloadPdfAll.cshtml")]
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
#line 1 "C:\Projetos\ControleFinanceiro\ControleFinanceiro\Views\_ViewImports.cshtml"
using ControleFinanceiro;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Projetos\ControleFinanceiro\ControleFinanceiro\Views\_ViewImports.cshtml"
using ControleFinanceiro.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"584735107284b02322c5a2d9d6b9b00805224740", @"/Views/Users/DownloadPdfAll.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0fe8908c9ef00d8a2cb6d154b078dc85c3dd6eb0", @"/Views/_ViewImports.cshtml")]
    public class Views_Users_DownloadPdfAll : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ControleFinanceiro.Models.ViewModels.DetailsAllExpenseAndYield>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/bootstrap/dist/css/bootstrap.min.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Projetos\ControleFinanceiro\ControleFinanceiro\Views\Users\DownloadPdfAll.cshtml"
  
    Layout = null;
    var yield = Model.Receitas.Sum(x => x.Valor);
    var expenseFixed = Model.TotalPorCategoriaFixa.Sum(x => x.Total);
    var expenseVariable = Model.TotalPorCategoriaVariavel.Sum(x => x.Total);
    var exenseExtra = Model.TotalPorCategoriaExtra.Sum(x => x.Total);
    var total = yield - (expenseFixed + expenseVariable + exenseExtra);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<!DOCTYPE html>\r\n\r\n<html>\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "584735107284b02322c5a2d9d6b9b008052247404805", async() => {
                WriteLiteral("\r\n    <meta name=\"viewport\" content=\"width=device-width\" />\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "584735107284b02322c5a2d9d6b9b008052247405132", async() => {
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
                WriteLiteral("\r\n    <title>DownloadPdfExpenseFixed</title>\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "584735107284b02322c5a2d9d6b9b008052247407064", async() => {
                WriteLiteral("\r\n    <h1 class=\"text-center\">Controle Financeiro</h1>\r\n    <h2>Saldo: ");
#nullable restore
#line 23 "C:\Projetos\ControleFinanceiro\ControleFinanceiro\Views\Users\DownloadPdfAll.cshtml"
          Write(total.ToString("N2"));

#line default
#line hidden
#nullable disable
                WriteLiteral("</h2>\r\n    <hr />\r\n    <h3>Receita</h3>\r\n    <h4>Total da receita: ");
#nullable restore
#line 26 "C:\Projetos\ControleFinanceiro\ControleFinanceiro\Views\Users\DownloadPdfAll.cshtml"
                     Write(Model.Receitas.Sum(x => x.Valor).ToString("N2"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</h4>
    <table class=""table"">
        <thead class=""bg bg-warning"">
            <tr>
                <th>
                    Descrição
                </th>
                <th>
                    Valor
                </th>
                <th></th>
            </tr>
        </thead>
        <tbody>
");
#nullable restore
#line 40 "C:\Projetos\ControleFinanceiro\ControleFinanceiro\Views\Users\DownloadPdfAll.cshtml"
             foreach (var item in Model.Receitas)
            {

#line default
#line hidden
#nullable disable
                WriteLiteral("                <tr>\r\n                    <td>\r\n                        ");
#nullable restore
#line 44 "C:\Projetos\ControleFinanceiro\ControleFinanceiro\Views\Users\DownloadPdfAll.cshtml"
                   Write(Html.DisplayFor(modelItem => item.Descricao));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
#nullable restore
#line 47 "C:\Projetos\ControleFinanceiro\ControleFinanceiro\Views\Users\DownloadPdfAll.cshtml"
                   Write(Html.DisplayFor(modelItem => item.Valor));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                    </td>\r\n\r\n                </tr>\r\n");
#nullable restore
#line 51 "C:\Projetos\ControleFinanceiro\ControleFinanceiro\Views\Users\DownloadPdfAll.cshtml"
            }

#line default
#line hidden
#nullable disable
                WriteLiteral("        </tbody>\r\n    </table>\r\n\r\n\r\n    <h3>Despesas Fixas</h3>\r\n    <h4>Total das despesas: ");
#nullable restore
#line 57 "C:\Projetos\ControleFinanceiro\ControleFinanceiro\Views\Users\DownloadPdfAll.cshtml"
                       Write(Model.TotalPorCategoriaFixa.Sum(x => x.Total).ToString("N2"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</h4>
    <table class=""table"">
        <thead class=""bg bg-warning"">
            <tr>
                <th>
                    Categoria
                </th>
                <th>
                    Valor
                </th>
                <th></th>
            </tr>
        </thead>
        <tbody>
");
#nullable restore
#line 71 "C:\Projetos\ControleFinanceiro\ControleFinanceiro\Views\Users\DownloadPdfAll.cshtml"
             foreach (var item in Model.TotalPorCategoriaFixa)
            {
                

#line default
#line hidden
#nullable disable
#nullable restore
#line 73 "C:\Projetos\ControleFinanceiro\ControleFinanceiro\Views\Users\DownloadPdfAll.cshtml"
                 if (item.Total > 0)
                {

#line default
#line hidden
#nullable disable
                WriteLiteral("                    <tr>\r\n                        <td>\r\n                            ");
#nullable restore
#line 77 "C:\Projetos\ControleFinanceiro\ControleFinanceiro\Views\Users\DownloadPdfAll.cshtml"
                       Write(Html.DisplayFor(modelItem => item.Categorias.NomeCategoria));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 80 "C:\Projetos\ControleFinanceiro\ControleFinanceiro\Views\Users\DownloadPdfAll.cshtml"
                       Write(Html.DisplayFor(modelItem => item.Total));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                        </td>\r\n\r\n                    </tr>\r\n");
#nullable restore
#line 84 "C:\Projetos\ControleFinanceiro\ControleFinanceiro\Views\Users\DownloadPdfAll.cshtml"
                }

#line default
#line hidden
#nullable disable
#nullable restore
#line 84 "C:\Projetos\ControleFinanceiro\ControleFinanceiro\Views\Users\DownloadPdfAll.cshtml"
                 
            }

#line default
#line hidden
#nullable disable
                WriteLiteral("        </tbody>\r\n    </table>\r\n\r\n\r\n    <h3>Despesas Variáveis</h3>\r\n    <h4>Total das despesas: ");
#nullable restore
#line 91 "C:\Projetos\ControleFinanceiro\ControleFinanceiro\Views\Users\DownloadPdfAll.cshtml"
                       Write(Model.TotalPorCategoriaVariavel.Sum(x => x.Total).ToString("N2"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</h4>
    <table class=""table"">
        <thead class=""bg bg-warning"">
            <tr>
                <th>
                    Categoria
                </th>
                <th>
                    Valor
                </th>
                <th></th>
            </tr>
        </thead>
        <tbody>
");
#nullable restore
#line 105 "C:\Projetos\ControleFinanceiro\ControleFinanceiro\Views\Users\DownloadPdfAll.cshtml"
             foreach (var item in Model.TotalPorCategoriaVariavel)
            {
                

#line default
#line hidden
#nullable disable
#nullable restore
#line 107 "C:\Projetos\ControleFinanceiro\ControleFinanceiro\Views\Users\DownloadPdfAll.cshtml"
                 if (item.Total > 0)
                {

#line default
#line hidden
#nullable disable
                WriteLiteral("                    <tr>\r\n                        <td>\r\n                            ");
#nullable restore
#line 111 "C:\Projetos\ControleFinanceiro\ControleFinanceiro\Views\Users\DownloadPdfAll.cshtml"
                       Write(Html.DisplayFor(modelItem => item.Categorias.NomeCategoria));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 114 "C:\Projetos\ControleFinanceiro\ControleFinanceiro\Views\Users\DownloadPdfAll.cshtml"
                       Write(Html.DisplayFor(modelItem => item.Total));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                        </td>\r\n\r\n                    </tr>\r\n");
#nullable restore
#line 118 "C:\Projetos\ControleFinanceiro\ControleFinanceiro\Views\Users\DownloadPdfAll.cshtml"
                }

#line default
#line hidden
#nullable disable
#nullable restore
#line 118 "C:\Projetos\ControleFinanceiro\ControleFinanceiro\Views\Users\DownloadPdfAll.cshtml"
                 
            }

#line default
#line hidden
#nullable disable
                WriteLiteral("        </tbody>\r\n    </table>\r\n\r\n\r\n    <h3>Despesas Extras</h3>\r\n    <h4>Total das despesas: ");
#nullable restore
#line 125 "C:\Projetos\ControleFinanceiro\ControleFinanceiro\Views\Users\DownloadPdfAll.cshtml"
                       Write(Model.TotalPorCategoriaExtra.Sum(x => x.Total).ToString("N2"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</h4>
    <table class=""table"">
        <thead class=""bg bg-warning"">
            <tr>
                <th>
                    Categoria
                </th>
                <th>
                    Valor
                </th>
                <th></th>
            </tr>
        </thead>
        <tbody>
");
#nullable restore
#line 139 "C:\Projetos\ControleFinanceiro\ControleFinanceiro\Views\Users\DownloadPdfAll.cshtml"
             foreach (var item in Model.TotalPorCategoriaExtra)
            {
                

#line default
#line hidden
#nullable disable
#nullable restore
#line 141 "C:\Projetos\ControleFinanceiro\ControleFinanceiro\Views\Users\DownloadPdfAll.cshtml"
                 if (item.Total > 0)
                {

#line default
#line hidden
#nullable disable
                WriteLiteral("                    <tr>\r\n                        <td>\r\n                            ");
#nullable restore
#line 145 "C:\Projetos\ControleFinanceiro\ControleFinanceiro\Views\Users\DownloadPdfAll.cshtml"
                       Write(Html.DisplayFor(modelItem => item.Categorias.NomeCategoria));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 148 "C:\Projetos\ControleFinanceiro\ControleFinanceiro\Views\Users\DownloadPdfAll.cshtml"
                       Write(Html.DisplayFor(modelItem => item.Total));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                        </td>\r\n\r\n                    </tr>\r\n");
#nullable restore
#line 152 "C:\Projetos\ControleFinanceiro\ControleFinanceiro\Views\Users\DownloadPdfAll.cshtml"
                }

#line default
#line hidden
#nullable disable
#nullable restore
#line 152 "C:\Projetos\ControleFinanceiro\ControleFinanceiro\Views\Users\DownloadPdfAll.cshtml"
                 
            }

#line default
#line hidden
#nullable disable
                WriteLiteral("        </tbody>\r\n    </table>\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</html>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ControleFinanceiro.Models.ViewModels.DetailsAllExpenseAndYield> Html { get; private set; }
    }
}
#pragma warning restore 1591