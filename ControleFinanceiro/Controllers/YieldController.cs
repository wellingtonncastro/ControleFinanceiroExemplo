using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ControleFinanceiro.Models;
using ControleFinanceiro.Models.ViewModels.Yield;
using ControleFinanceiro.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ControleFinanceiro.Controllers
{
    [Authorize]
    public class YieldController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IYieldRepository _yieldRepository;

        public YieldController(IYieldRepository yieldRepository, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _yieldRepository = yieldRepository;
        }

        public async Task<IActionResult> YieldIndex(string id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id não informado" });
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null || user.Id != _userManager.GetUserId(User))
            {
                return RedirectToAction(nameof(Error), new { Message = "Usuario não correspondido" });
            }

            var lista = await _yieldRepository.FindAllByUserAsync(user.Id);
            return View(lista);


        }


        public IActionResult CreateYield() 
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateYield(CreateYieldFormViewModel viewModel) 
        {
            if (!ModelState.IsValid) 
            {
                return View(viewModel);
            }

            try
            {
                var user = await _userManager.GetUserAsync(User);

                Receita yield = new Receita()
                {
                    Descricao = viewModel.Descricao,
                    Valor = viewModel.Valor,
                    User = user,
                    UserId = user.Id,
                    Date = DateTime.Now
                    
                };

                await _yieldRepository.RegisterNewYieldAsync(yield);
                return RedirectToAction(nameof(YieldIndex), new {id = user.Id });
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }

        }


        public async Task<IActionResult> DetailsYield(int? id) 
        {
            if(id == null) 
            {
                return RedirectToAction(nameof(Error), new { message = "Id não informado"});
            }

            var yield = await _yieldRepository.FindYieldByIdAsync(id.Value);

            return View(yield);
        }


        public async Task<IActionResult> EditYield(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id não fornecido" });
            }

            var yield = await _yieldRepository.FindYieldByIdAsync(id.Value);

            if (yield == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Despesa não encontrada" });
            }

            return View(yield);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditYield(Receita viewModel)
        {
            if (!ModelState.IsValid || viewModel.Valor == 0)
            {
                return View(viewModel);
            }
            var user = await _userManager.GetUserAsync(User);

            if (user.Id != viewModel.UserId || user == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não correspondendte" });
            }

            try
            {
                var yield = await _yieldRepository.FindYieldByIdAsync(viewModel.Id);
                
                yield.Valor = viewModel.Valor;
                yield.Descricao= viewModel.Descricao;
                
                await _yieldRepository.UpdateYieldAsync(yield);

                return RedirectToAction(nameof(YieldIndex), new { id = user.Id });
                
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }


        public async Task<IActionResult> DeleteYield(int? id) 
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não informado" });
            }

            var yield = await _yieldRepository.FindYieldByIdAsync(id.Value);

            return View(yield);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteYield(int id) 
        {
            try
            {
                await _yieldRepository.DeleteYieldAsync(id);

                return RedirectToAction(nameof(YieldIndex));

            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Error), new { message = "Erro ao remover a receita, consultar os desenvolvedores" });
            }
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
    }
}