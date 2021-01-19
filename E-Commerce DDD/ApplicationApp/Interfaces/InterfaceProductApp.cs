using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationApp.Interfaces
{
    public interface InterfaceProductApp : InterfaceGenericaApp<Product>
    {
        Task AddProduct(Product product);
        Task UpdateProduct(Product product);

        Task<List<Product>> ListarProdutoUsuario(string userId);

        Task<List<Product>> ListarProdutosComEstoque();

        Task<List<Product>> ListarProdutosCarrinhoUsuario(string userId);

        Task<Product> ObterProdutoCarrinho(int idProdutoCarrinho);
    }
}
