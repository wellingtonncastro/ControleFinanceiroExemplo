using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ControleFinanceiro.Models;
using ControleFinanceiro.Models.ViewModels.Expense;
using ControleFinanceiro.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;

namespace ControleFinanceiro.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IExpenseRepository _expenseRepository;
        private readonly ICategoryRepository _categoryRepository;
        
        public ExpenseController(ICategoryRepository categoryRepository,IExpenseRepository expenseRepository, UserManager<User> userManager, 
                                    SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _expenseRepository = expenseRepository;
            _categoryRepository = categoryRepository;
        }


        public async Task<IActionResult> ExpenseFixedIndex(string id)
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

            DetailsExpenseFixedFormViewModel viewModel = new DetailsExpenseFixedFormViewModel()
            {
                DespesaFixa = await _expenseRepository.FindAllExpenseFixedByUserAsync(user.Id),
                TotalPorCategoria = await _expenseRepository.GetTotalCategoryPerUserIdAsync(user.Id)
            };

            return View(viewModel);
        }


        public async Task<IActionResult> CreateExpenseFixed()
        {
            var list = await _categoryRepository.FindAll();
            CreateExpenseFixedFormViewModel viewModel = new CreateExpenseFixedFormViewModel()
            {
                Categorias = list
            };

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateExpenseFixed(CreateExpenseFixedFormViewModel viewModel)
        {
            if (!ModelState.IsValid || viewModel.Valor == 0)
            {
                return View(viewModel);
            }

            try
            {
                var user = await _userManager.GetUserAsync(User);
                var category = await _categoryRepository.FindByIdAsync(viewModel.CategoriaId);

                DespesaFixa expenseFixed = new DespesaFixa()
                {   Categoria = category,
                    CategoriaId = viewModel.CategoriaId,
                    Descricao = viewModel.Descricao,
                    User = user,
                    UserId = user.Id,
                    Valor = viewModel.Valor,
                    
                };

                await _expenseRepository.RegisterNewExpenseFixedAsync(expenseFixed);
                
                return RedirectToAction(nameof(ExpenseFixedIndex),new { id = user.Id});
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }


        public async Task<IActionResult> DetailsExpenseFixed(int? id) 
        {
            if(id == null) 
            {
                return RedirectToAction(nameof(Error), new { Message = "Id não fornecido" });
            }

            var despesa = await _expenseRepository.FindExpenseFixedByIdAsync(id.Value);
            
            if (despesa == null) 
            {
                return RedirectToAction(nameof(Error), new { Message = "Despesa não encontrada" });
            }

            return View(despesa);
        }


        public async Task<IActionResult> EditExpenseFixed(int? id) 
        {
            if(id == null) 
            {
                return RedirectToAction(nameof(Error), new { Message = "Id não fornecido" });
            }
            
            var expense = await _expenseRepository.FindExpenseFixedByIdAsync(id.Value);

            if (expense == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Despesa não encontrada" });
            }

            var categorias = await _categoryRepository.FindAll();
            EditexpenseFixedFormViewModel viewModel = new EditexpenseFixedFormViewModel()
            {
                CategoriaId = expense.CategoriaId,
                Categorias = categorias,
                Descricao = expense.Descricao,
                Id = expense.Id,
                Valor = expense.Valor,
                UserId = expense.UserId

            };

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> EditExpenseFixed(EditexpenseFixedFormViewModel viewModel) 
        {
            if (!ModelState.IsValid) 
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
                var expense = await _expenseRepository.FindExpenseFixedByIdAsync(viewModel.Id);

                expense.CategoriaId = viewModel.CategoriaId;
                expense.Descricao = viewModel.Descricao;
                expense.UserId = viewModel.UserId;
                expense.User = user;
                expense.Valor = viewModel.Valor;

                await _expenseRepository.UpdateExpenseFixedAync(expense);
                return RedirectToAction(nameof(ExpenseFixedIndex), new { id = user.Id });
            
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }


        public async Task<IActionResult> DeleteExpensedFixed(int? id) 
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id não fornecido" });
            }

            var expense = await _expenseRepository.FindExpenseFixedByIdAsync(id.Value);

            if (expense == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Despesa não encontrada" });
            }

            return View(expense);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteExpensedFixed(int id) 
        {
            await _expenseRepository.DeleteExpenseFixedAync(id);
            var user = await _userManager.GetUserAsync(User);
            return RedirectToAction(nameof(ExpenseFixedIndex), new { id = user.Id });
        }


        public async Task<IActionResult> DownloadPdfExpenseFixed()
        {
            var user = await _userManager.GetUserAsync(User);

            DetailsExpenseFixedFormViewModel expenseFixed = new DetailsExpenseFixedFormViewModel()
            {
                DespesaFixa = await _expenseRepository.FindAllExpenseFixedByUserAsync(user.Id),
                TotalPorCategoria = await _expenseRepository.GetTotalCategoryPerUserIdAsync(user.Id)

            };

            return new ViewAsPdf("DownloadPdfExpenseFixed", expenseFixed) { FileName = "Despesas_fixas.pdf" };
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