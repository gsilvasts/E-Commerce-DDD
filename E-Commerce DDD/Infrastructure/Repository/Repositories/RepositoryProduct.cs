using Domain.Interfaces.InterfaceProduct;
using Entities.Entities;
using Entities.Entities.Enums;
using Infrastructure.Configuration;
using Infrastructure.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Repositories
{
    public class RepositoryProduct : RepositoryGenerics<Product>, IProduct
    {
        private readonly DbContextOptions<ContextBase> _optionsBuilder;

        public RepositoryProduct()
        {
            _optionsBuilder = new DbContextOptions<ContextBase>();
        }

        public async Task<List<Product>> ListarProdutos(Expression<Func<Product, bool>> exProduct)
        {
            using (var banco = new ContextBase(_optionsBuilder))
            {
                return await banco.Products.Where(exProduct).AsNoTracking().ToListAsync();
            }
        }

     

        public async Task<List<Product>> ListarProdutoUsuario(string userId)
        {
            using (var banco = new ContextBase(_optionsBuilder))
            {
                return await banco.Products.Where(p => p.UserId == userId).AsNoTracking().ToListAsync();
            }

        }

        public async Task<Product> ObterProdutoCarrinho(int idProdutoCarrinho)
        {
            using (var banco = new ContextBase(_optionsBuilder))
            {
                var produtosCarrinhoUsuario = await(from p in banco.Products
                                                    join c in banco.CompraUsuarios on p.Id equals c.IdProduto
                                                    where c.Id.Equals(idProdutoCarrinho) && c.Estado == EnumEstadoCompra.Produto_Carrinho
                                                    select new Product
                                                    {
                                                        Id = p.Id,
                                                        Nome = p.Nome,
                                                        Descricao = p.Descricao,
                                                        Observacao = p.Observacao,
                                                        Valor = p.Valor,
                                                        qtdCompra = p.qtdCompra,
                                                        IdProdutoCarrinho = c.Id
                                                    }).AsNoTracking().FirstOrDefaultAsync();
                return produtosCarrinhoUsuario;
            }
        }

        public async Task<List<Product>> ListarProdutosCarrinhoUsuario(string userId)
        {
            using (var banco = new ContextBase(_optionsBuilder))
            {
                var produtosCarrinhoUsuario = await (from p in banco.Products
                                                     join c in banco.CompraUsuarios on p.Id equals c.IdProduto
                                                     where c.UserId.Equals(userId) && c.Estado == EnumEstadoCompra.Produto_Carrinho
                                                     select new Product
                                                     {
                                                         Id = p.Id,
                                                         Nome = p.Nome,
                                                         Descricao = p.Descricao,
                                                         Observacao = p.Observacao,
                                                         Valor = p.Valor,
                                                         qtdCompra = c.QtdCompra,
                                                         IdProdutoCarrinho = c.Id
                                                     }).AsNoTracking().ToListAsync();
                return produtosCarrinhoUsuario;
            }
        }
    }
}
