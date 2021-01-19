using ApplicationApp.Interfaces;
using Domain.Interfaces.InterfaceProduct;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationApp.OpenApp
{
    public class AppProduct : InterfaceProductApp
    {
        IProduct _IProduct;
        IServiceProduct _IServiceProduct;

        public AppProduct(IProduct IProduct, IServiceProduct IServiceProduct)
        {
            _IProduct = IProduct;
            _IServiceProduct = IServiceProduct;
        }

        public async Task<List<Product>> ListarProdutoUsuario(string userId)
        {
            return await _IProduct.ListarProdutoUsuario(userId);
        }
        public async Task AddProduct(Product product)
        {
            await _IServiceProduct.AddProduct(product);
        }

        public async Task UpdateProduct(Product product)
        {
            await _IServiceProduct.UpdateProduct(product);
        }

        public async Task Add(Product Objeto)
        {
            await _IProduct.Add(Objeto);
        }

        public async Task Delete(Product Objeto)
        {
            await _IProduct.Delete(Objeto);
        }

        public async Task<Product> GetEntityById(int id)
        {
            return await _IProduct.GetEntityById(id);
        }

        public async Task<List<Product>> List()
        {
            return await _IProduct.List();
        }

        public async Task Update(Product Objeto)
        {
            await _IProduct.Update(Objeto);
        }

        public async Task<List<Product>> ListarProdutosComEstoque()
        {
            return await _IServiceProduct.ListarProdutosComEstoque();
        }

        public async Task<List<Product>> ListarProdutosCarrinhoUsuario(string userId)
        {
            return await _IProduct.ListarProdutosCarrinhoUsuario(userId);
        }

        public async Task<Product> ObterProdutoCarrinho(int idProdutoCarrinho)
        {
            return await _IProduct.ObterProdutoCarrinho(idProdutoCarrinho);
        }
    }
}
