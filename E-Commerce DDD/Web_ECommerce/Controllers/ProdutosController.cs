using ApplicationApp.Interfaces;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_ECommerce.Controllers
{
    [Authorize]
    public class ProdutosController : Controller
    {
        public readonly UserManager<ApplicationUser> _userManager;

        public readonly InterfaceProductApp _interfaceProductApp;

        public readonly InterfaceCompraUsuarioApp _interfaceCompraUsuarioApp;

        public ProdutosController(InterfaceProductApp interfaceProductApp, UserManager<ApplicationUser> userManager, InterfaceCompraUsuarioApp interfaceCompraUsuarioApp)
        {
            _interfaceProductApp = interfaceProductApp;
            _userManager = userManager;
            _interfaceCompraUsuarioApp = interfaceCompraUsuarioApp;
        }
        // GET: ProdutosController
        public async Task<IActionResult> Index()
        {
            var idUsuario = await RetornarUsuarioLogado();

            return View(await _interfaceProductApp.ListarProdutoUsuario(idUsuario));
        }

        // GET: ProdutosController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            return View(await _interfaceProductApp.GetEntityById(id));
        }

        // GET: ProdutosController/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: ProdutosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            try
            {
                var idUsuario = await RetornarUsuarioLogado();

                product.UserId = idUsuario;

                await _interfaceProductApp.AddProduct(product);
                if (product.Notitycoes.Any())
                {
                    foreach (var item in product.Notitycoes)
                    {
                        ModelState.AddModelError(item.NomePropriedade, item.mensagem);
                    }
                    return View("Create", product);
                }
                
            }
            catch
            {
                return View("Create", product);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: ProdutosController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            return View(await _interfaceProductApp.GetEntityById(id));
        }

        // POST: ProdutosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            try
            {
                await _interfaceProductApp.UpdateProduct(product);
                if (product.Notitycoes.Any())
                {
                    foreach (var item in product.Notitycoes)
                    {
                        ModelState.AddModelError(item.NomePropriedade, item.mensagem);
                    }

                    ViewBag.Alerta = true;
                    ViewBag.Mensagem = "verifique, ocorreu algum erro!";

                    return View("Edit", product);
                }

            }
            catch
            {
                return View("Edit", product);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: ProdutosController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            return View(await _interfaceProductApp.GetEntityById(id));
        }

        // POST: ProdutosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Product product)
        {
            try
            {
                var produtoDeletar = await _interfaceProductApp.GetEntityById(id);
                await _interfaceProductApp.Delete(produtoDeletar);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private async Task<string> RetornarUsuarioLogado()
        {
            var idUsuario = await _userManager.GetUserAsync(User);

            return idUsuario.Id;
        }

        [AllowAnonymous]
        [HttpGet("/api/ListarProdutosComEstoque")]
        public async Task<JsonResult> ListarProdutosComEstoque()
        {
            return Json(await _interfaceProductApp.ListarProdutosComEstoque());
        }

        public async Task<IActionResult> ListarProdutosCarrinhoUsuario()
        {
            var idUsuario = await RetornarUsuarioLogado();
            return View(await _interfaceProductApp.ListarProdutosCarrinhoUsuario(idUsuario));
        }

        public async Task<IActionResult> RemoverCarrinho(int id)
        {
            return View(await _interfaceProductApp.ObterProdutoCarrinho(id));
        }

        // POST: ProdutosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoverCarrinho(int id, Product product)
        {
            try
            {
                var produtoDeletar = await _interfaceCompraUsuarioApp.GetEntityById(id);
                await _interfaceCompraUsuarioApp.Delete(produtoDeletar);

                return RedirectToAction(nameof(ListarProdutosCarrinhoUsuario));
            }
            catch
            {
                return View();
            }
        }
    }
}
