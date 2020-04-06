using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ControleFinanceiro.Models;
using ControleFinanceiro.Models.ViewModels.AnothersExpenses;
using ControleFinanceiro.Models.ViewModels.Expense;
using ControleFinanceiro.Services.Interfaces;
using ControleFinanceiro.Services.Repository.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;

namespace ControleFinanceiro.Controllers
{
    public class AnothersExpenseController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAnothersExpenseRepository _anothersExpenseRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITypeRepository _typeRepository;

        public AnothersExpenseController(UserManager<User> userManager, SignInManager<User> signInManager, ITypeRepository typeRepository, 
                                            IAnothersExpenseRepository anothersExpenseRepository,ICategoryRepository categoryRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _anothersExpenseRepository = anothersExpenseRepository;
            _categoryRepository = categoryRepository;
            _typeRepository = typeRepository;

        }

        public async Task<IActionResult> VariableExpenseIndex(string id)
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

            DetailsAnothersExpenseFormViewModel viewModel = new DetailsAnothersExpenseFormViewModel()
            {
                Despesas = await _anothersExpenseRepository.FindAllExpenseByUserAndTypeAsync(user.Id, 2),
                TotalPorCategoria = await _anothersExpenseRepository.GetTotalCategoryPerUserIdAnTypeIdAsync(user.Id,2)
            };

            return View(viewModel);
        }


        public async Task<IActionResult> ExtraExpenseIndex(string id)
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

            DetailsAnothersExpenseFormViewModel viewModel = new DetailsAnothersExpenseFormViewModel()
            {
                Despesas = await _anothersExpenseRepository.FindAllExpenseByUserAndTypeAsync(user.Id, 3),
                TotalPorCategoria = await _anothersExpenseRepository.GetTotalCategoryPerUserIdAnTypeIdAsync(user.Id, 3)
            };
            return View(viewModel);
        }


        public async Task<IActionResult> CreateVariableExpense()
        {
            var list = await _categoryRepository.FindAll();
            CreateAnotherExpenseFormViewModel viewModel = new CreateAnotherExpenseFormViewModel()
            {
                Categorias = list,
                TipoId = 2
            };

            return View(viewModel);
        }


        public async Task<IActionResult> CreateExtraExpense()
        {
            var list = await _categoryRepository.FindAll();
            CreateAnotherExpenseFormViewModel viewModel = new CreateAnotherExpenseFormViewModel()
            {
                Categorias = list,
                TipoId = 3
            };

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAnotherExpense(CreateAnotherExpenseFormViewModel viewModel) 
        {
            if (!ModelState.IsValid || viewModel.Valor == 0) 
            {
                return View(viewModel);
            }
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var category = await _categoryRepository.FindByIdAsync(viewModel.CategoriaId);
                var type = await _typeRepository.FindTypeById(viewModel.TipoId);

               Despesa expense = new Despesa()
               {
                   Categoria = category,
                   CategoriaId = category.Id,
                   Data = DateTime.UtcNow,
                   Descricao = viewModel.Descricao,
                   Tipo = type,
                   TipoId = type.Id,
                   User = user,
                   UserId = user.Id,
                   Valor = viewModel.Valor
               };
                
                await _anothersExpenseRepository.RegisterNewExpenseAsync(expense);
                if(expense.TipoId == 2) 
                {
                    return RedirectToAction(nameof(VariableExpenseIndex), new { id = user.Id});
                }
                else 
                {
                    return RedirectToAction(nameof(ExtraExpenseIndex), new { id = user.Id });
                }
            }
            catch (Exception e)
            {

                return RedirectToAction(nameof(Error), new { message = "Erro ao cadastrar" });
            }
        }


        public async Task<IActionResult> DetailsAnotherExpense(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id não fornecido" });
            }

            var expense = await _anothersExpenseRepository.FindExpenseByIdAsync(id.Value);

            if (expense == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Despesa não encontrada" });
            }

            return View(expense);
        }


        public async Task<IActionResult> EditAnotherExpense(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id não fornecido" });
            }

            var expense = await _anothersExpenseRepository.FindExpenseByIdAsync(id.Value);

            if (expense == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Despesa não encontrada" });
            }

            var category = await _categoryRepository.FindAll();
            EditexpenseFixedFormViewModel viewModel = new EditexpenseFixedFormViewModel()
            {   
                CategoriaId = expense.CategoriaId,
                Categorias = category,
                Descricao = expense.Descricao,
                Id = expense.Id,
                Valor = expense.Valor,
                UserId = expense.UserId
            };

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAnotherExpense(EditexpenseFixedFormViewModel viewModel)
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
                var expense = await _anothersExpenseRepository.FindExpenseByIdAsync(viewModel.Id);

                expense.CategoriaId = viewModel.CategoriaId;
                expense.Descricao = viewModel.Descricao;
                expense.UserId = viewModel.UserId;
                expense.User = user;
                expense.Valor = viewModel.Valor;

                await _anothersExpenseRepository.UpdateExpenseAync(expense);
                
                if (expense.TipoId == 2)
                {
                    return RedirectToAction(nameof(VariableExpenseIndex), new { id = user.Id });
                }
                else
                {
                    return RedirectToAction(nameof(ExtraExpenseIndex), new { id = user.Id });
                }

            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }


        public async Task<IActionResult> DeleteAnotherExpensed(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id não fornecido" });
            }

            var expense = await _anothersExpenseRepository.FindExpenseByIdAsync(id.Value);

            if (expense == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Despesa não encontrada" });
            }

            return View(expense);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAnotherExpensed(int id)
        {
            try
            {
                var expense = await _anothersExpenseRepository.FindExpenseByIdAsync(id);

                int type = expense.TipoId;

                await _anothersExpenseRepository.DeleteExpenseAync(id);

                var user = await _userManager.GetUserAsync(User);

                if (type == 2)
                {
                    return RedirectToAction(nameof(VariableExpenseIndex), new { id = user.Id });
                }
                else
                {
                    return RedirectToAction(nameof(ExtraExpenseIndex), new { id = user.Id });
                }
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = "Impossivel excluir despesas" });
            }
        }


        public async Task<IActionResult> DownloadPdfExpenseFixed(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            DetailsAnothersExpenseFormViewModel expenseFixed = new DetailsAnothersExpenseFormViewModel()
            {
                Despesas = await _anothersExpenseRepository.FindAllExpenseByUserAndTypeAsync(user.Id, id),
                TotalPorCategoria = await _anothersExpenseRepository.GetTotalCategoryPerUserIdAnTypeIdAsync(user.Id, id)
            };

            return new ViewAsPdf("DownloadPdfAnotherExpense", expenseFixed) { FileName = "Demais_despesas.pdf" };
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