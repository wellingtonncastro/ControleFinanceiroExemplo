using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ControleFinanceiro.Models;
using ControleFinanceiro.Models.ViewModels;
using ControleFinanceiro.Models.ViewModels.User;
using ControleFinanceiro.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Rotativa.AspNetCore;

namespace ControleFinanceiro.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmail _email;
        private readonly ILogger<UsersController> _logger;
        private readonly IAnothersExpenseRepository _anothersExpenseRepository;
        private readonly IExpenseRepository _expenseRepository;
        private readonly IYieldRepository _yieldRepository;
        public UsersController(IYieldRepository yieldRepository,IExpenseRepository expenseRepository,IAnothersExpenseRepository anothersExpenseRepository,
                                ILogger<UsersController> logger, IEmail email, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _email = email;
            _logger = logger;
            _anothersExpenseRepository = anothersExpenseRepository;
            _expenseRepository = expenseRepository;
            _yieldRepository = yieldRepository;
        }

        
        public async Task<IActionResult> IndexTotalMonthly(string id)
        {
            await _anothersExpenseRepository.VerifyDateForDeleteDataAsync(id);
            
            var user = await _userManager.FindByIdAsync(id);
            var expenseFixed = await _expenseRepository.FindAllExpenseFixedByUserAsync(id);
            var expenseVariable = await _anothersExpenseRepository.FindAllExpenseByUserAndTypeAsync(id, 2);
            var expenseExtra = await _anothersExpenseRepository.FindAllExpenseByUserAndTypeAsync(id, 3);
            var yield = await _yieldRepository.FindAllByUserAsync(id);

            TotalMonthlyFormViewModel totalMensal = new TotalMonthlyFormViewModel()
            {
                TotalDespesaFixa = expenseFixed.Sum(ex => ex.Valor),
                TotalDespesaVariavel = expenseVariable.Sum(ex => ex.Valor),
                TotalDespesaExtra = expenseExtra.Sum(ex => ex.Valor),
                
                Saldo = yield.Sum(ex => ex.Valor) - (expenseFixed.Sum(ex => ex.Valor) + expenseVariable.Sum(ex => ex.Valor) + expenseExtra.Sum(ex => ex.Valor)),
                
                TotalReceita = yield.Sum(ex => ex.Valor),
                
                ValorGuardar = 0.1m * yield.Sum(ex => ex.Valor),

                MembroDaFamilia = (yield.Sum(ex => ex.Valor) - (expenseFixed.Sum(ex => ex.Valor) + expenseVariable.Sum(ex => ex.Valor) + expenseExtra.Sum(ex => ex.Valor))) / user.QuantidadeMembroFamilia,

                TotalDespesa = expenseFixed.Sum(ex => ex.Valor) + expenseVariable.Sum(ex => ex.Valor) + expenseExtra.Sum(ex => ex.Valor)
            };

            return View(totalMensal);
        }


        [AllowAnonymous]
        public IActionResult Create() 
        {
            return View();
        }

        
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserFormViewModel viewModel)
        {
            if (!ModelState.IsValid ) 
            {
                return View(viewModel);
            }
            else 
            {
                try
                {
                    var user = new User()
                    {
                        Email = viewModel.Email,
                        PasswordHash = viewModel.PasswordHash,
                        UserName = viewModel.UserName,
                        QuantidadeMembroFamilia = viewModel.QuantidadeMembroFamilia,
                        DespesasExcluidas = false
                    };

                    var result = await _userManager.CreateAsync(user, user.PasswordHash);

                    if (result.Succeeded)
                    {
                        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        var LinkConfirmacao = Url.Action("ConfirmEmail", "Users",
                            new { UserId = user.Id, Token = token }, Request.Scheme);

                        await SendConfirmationEmailAsync(token, user.Email, LinkConfirmacao, 1);

                        return RedirectToAction(nameof(SuccessfullyAdded));
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description.ToString());
                        }
                    }
                }
                catch (Exception e)
                {

                    return RedirectToAction(nameof(Error), new { message = e.Message });
                }
            }
            return View(viewModel);
        }

        
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string UserId, string Token) 
        {
            if(UserId == null || Token == null) 
            {
                return RedirectToAction(nameof(Error), new { Message = " Usuario ou Token vazio, contactar os desenvolvedores" });
            }

            var user = await _userManager.FindByIdAsync(UserId);

            if (user == null) 
            {
                return RedirectToAction(nameof(Error), new { Message = " O Id do usuario é invalido" });
            }

            var result = await _userManager.ConfirmEmailAsync(user, Token);
            
            if (result.Succeeded) 
            {
                return View();
            }

            ViewBag.ErrorTittle = "Email não pode ser confirmado, por favor, contactar os desenvolvedores";
            return View("Error");
        }


        private async Task SendConfirmationEmailAsync(string token, string email, string linkConfirmation, int opc) 
        {
            switch (opc) 
            {
                case 1:
                    _logger.Log(LogLevel.Warning, linkConfirmation);
                    _logger.LogInformation("SendEmail");

                    string subject = "Confirmação de cadastro";
                    string menssage = "Obrigado por se cadastrar em nossa plataforma, " +
                        "esperamos que você possa disfrutar o melhor do nosso serviço. Para finalizar seu cadastro, basta clicar no link a seguir:  " + linkConfirmation;

                    await _email.SendEmailAsync(email, subject, menssage);
                    break;
                case 2:
                    _logger.Log(LogLevel.Warning, linkConfirmation);
                    _logger.LogInformation("SendEmail");

                    string subjectPassword = "Troca de senha";
                    string messagePassword = "Clique no link a seguir para resetar sua senha de acesso: " + linkConfirmation;

                    await _email.SendEmailAsync(email, subjectPassword, messagePassword);
                    break;
                case 3:
                    _logger.Log(LogLevel.Warning, linkConfirmation);
                    _logger.LogInformation("SendEmail");

                    string subjectDataChaged = "Alteração de dados";
                    string messageDataChanged = "Alguns dados podem ter sido alterado, qualquer duvida, contatar o suporte " + linkConfirmation;

                    await _email.SendEmailAsync(email, subjectDataChaged, messageDataChanged);
                    break;
            }
        }


        public async Task<IActionResult> ChangePassword(string? id) 
        {
            if(id == null) 
            {
                return RedirectToAction(nameof(Error), new { message = "Id não fornecido"});
            }

            var user = await _userManager.FindByIdAsync(id);

            if(user == null) 
            {
                return RedirectToAction(nameof(Error), new { message = "Usuario não encontrado"});
            }

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ConfirmPasswordFormViewModel viewModel) 
        {
            if (!ModelState.IsValid) 
            {
                return View(viewModel);
            }

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Usuario não encontrado" });
            }

            PasswordHasher<User> passwordHasher = new PasswordHasher<User>();

            if (passwordHasher.VerifyHashedPassword(user, user.PasswordHash, viewModel.CurrencyPasswordHash) != PasswordVerificationResult.Failed)
            {
                var result = await _userManager.ChangePasswordAsync(user, viewModel.CurrencyPasswordHash, viewModel.NewPasswordHash);

                if (!result.Succeeded)
                {
                    return View(nameof(Error), new { message = "Erro ao mudar senha, tente novamente novamente ou mais tarde. Se o problema persistir, por favor, consulte ao administradores" });
                }

                await _signInManager.RefreshSignInAsync(user);

                await SendConfirmationEmailAsync("",user.Email,"", 3);

                return RedirectToAction(nameof(IndexTotalMonthly), new { id = user.Id });
            }
            return View(viewModel);

        }


        public async Task<IActionResult> EditNumberMemberFamily(string? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error));
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return RedirectToAction(nameof(Error));
            }

            return View(user);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditNumberMemberFamily(string? id, User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            if(id != user.Id) 
            {
                return RedirectToAction(nameof(Error), new { message = "Ids não correspondem" });
            }

            if (user == null)
            {
                return RedirectToAction(nameof(Error));
            }

            try
            {   var userChaged = await _userManager.FindByIdAsync(id);
                userChaged.QuantidadeMembroFamilia = user.QuantidadeMembroFamilia;

                await _userManager.UpdateAsync(userChaged);
                return RedirectToAction(nameof(IndexTotalMonthly), new { id = user.Id });
            }
            catch (ApplicationException e)
            {

                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }
       

        public IActionResult Configuration() 
        {
            return View();
        }


        [AllowAnonymous]
        public IActionResult SuccessfullyAdded()
        {
            return View();
        }

        
        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            if (User.Identity.IsAuthenticated) 
            {
                var user = await _userManager.GetUserAsync(User);
                return RedirectToAction(nameof(IndexTotalMonthly), new { id = user.Id }) ;
            }
            _logger.LogInformation("Entrando na pagina de Login");
            return View();
        }


        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login(LoginFormViewModel viewModel) 
        {
            if (!ModelState.IsValid) 
            {
                ModelState.AddModelError("", "Email e/ou senha inválido(s)");
            }

            PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
            var user = await _userManager.FindByEmailAsync(viewModel.Email);

            if(user != null && user.EmailConfirmed) 
            {
                if (passwordHasher.VerifyHashedPassword(user, user.PasswordHash,viewModel.Password) != PasswordVerificationResult.Failed) 
                {
                    await _signInManager.SignInAsync(user, false);

                    return RedirectToAction(nameof(IndexTotalMonthly), new { id = user.Id });
                }

                ModelState.AddModelError("", "Email e/ou senha inválido(s)");
            }

            ModelState.AddModelError("", "Email ainda não confirmado, por favor, verificar na sua caixa de entrada");
            return View(viewModel);
        }


        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(Login));
        }


        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var user = await _userManager.FindByEmailAsync(viewModel.Email);

            if (user != null && await _userManager.IsEmailConfirmedAsync(user))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                var passwordResetLink = Url.Action("ResetPassword", "Users",
                    new { email = viewModel.Email, Token = token }, Request.Scheme);


                await SendConfirmationEmailAsync(token, user.Email, passwordResetLink, 2);

                return View(nameof(ForgotPasswordConfirmation));
            }

            return View(nameof(Error),new {Message = "Email não encontrado"});
        }
        

        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string token, string email) 
        {
            if(token == null || email == null) 
            {
                ModelState.AddModelError("", "Token invalido para resetar a senha, contactar os desenvolvedores");
            }
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordFormViewModel viewModel) 
        {
            if (!ModelState.IsValid) 
            {
                return View(viewModel);
            }

            var user = await _userManager.FindByEmailAsync(viewModel.Email);
            
            if(user != null) 
            {
                var result = await _userManager.ResetPasswordAsync(user, viewModel.Token, viewModel.PasswordHash);

                if (result.Succeeded) 
                {
                    return View(nameof(ResetPasswordConfirmation));
                }
            }
            return RedirectToAction(nameof(Error), new { Message = "Usuario não existe" });
        }


        public IActionResult ResetPasswordConfirmation() 
        {
            return View();
        }


        public async Task<IActionResult> DownloadPdfGeneral() 
        {
            var user = await _userManager.GetUserAsync(User);
            DetailsAllExpenseAndYield all = new DetailsAllExpenseAndYield()
            {
                TotalPorCategoriaFixa = await _expenseRepository.GetTotalCategoryPerUserIdAsync(user.Id),
                TotalPorCategoriaExtra = await _anothersExpenseRepository.GetTotalCategoryPerUserIdAnTypeIdAsync(user.Id, 3),
                TotalPorCategoriaVariavel = await _anothersExpenseRepository.GetTotalCategoryPerUserIdAnTypeIdAsync(user.Id, 2),
                Receitas = await _yieldRepository.FindAllByUserAsync(user.Id)
                
            };

            return new ViewAsPdf("DownloadPdfAll", all) { FileName = "relatorio_do_mes.pdf" };
        }


        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }


        [AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyEmail(string email)
        {
            var userEmail = await _userManager.FindByEmailAsync(email);
            
            if (userEmail != null)
            {
                //ViewBag.statusEmail = "Email em uso";
                return Json($"Email: {email} ja está em uso");
            }

            return Json(true);
        }


        [AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyUserName(string userName)
        {
            var name = await _userManager.FindByNameAsync(userName);

            if (name != null)
            {
                //ViewBag.statusEmail = "Email em uso";
                return Json($"O Apelido: {name} ja está em uso");
            }

            return Json(true);
        }
    }
}