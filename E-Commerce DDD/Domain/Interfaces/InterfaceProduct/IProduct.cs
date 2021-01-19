using Domain.Interfaces.Generics;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.InterfaceProduct
{
    public interface IProduct : IGeneric<Product>
    {
        Task<List<Product>> ListarProdutoUsuario(string userId);

        Task<List<Product>> ListarProdutos(Expression<Func<Product, bool>> exProduct);

        Task<List<Product>> ListarProdutosCarrinhoUsuario(string userId);

        Task<Product> ObterProdutoCarrinho(int idProdutoCarrinho);

    }
}
