using Domain.Interfaces.InterfaceProduct;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class ServiceProduct : IServiceProduct
    {
        private readonly IProduct _IProduct;

        public ServiceProduct(IProduct IProduct)
        {
            _IProduct = IProduct;
        }

        public async Task AddProduct(Product product)
        {
            var validaNome = product.ValidarPropriedadeString(product.Nome, "Nome");

            var validaValor = product.ValidarPropriedadeDecimal(product.Valor, "Valor");

            var validaQtdEstoque = product.ValidarPropriedadeInt(product.QtdEstoque, "QtdEstoque");

            if (validaNome && validaValor && validaQtdEstoque)
            {
                product.DataCadastro = DateTime.Now;
                product.DataAlteracao = DateTime.Now;
                product.Estado = true;
                await _IProduct.Add(product);
            }
        }

        public async Task<List<Product>> ListarProdutosComEstoque()
        {
            return await _IProduct.ListarProdutos(p => p.QtdEstoque > 0);
        }

        public async Task UpdateProduct(Product product)
        {
            var validaNome = product.ValidarPropriedadeString(product.Nome, "Nome");

            var validaValor = product.ValidarPropriedadeDecimal(product.Valor, "Valor");

            var validaQtdEstoque = product.ValidarPropriedadeInt(product.QtdEstoque, "QtdEstoque");

            if (validaNome && validaValor && validaQtdEstoque)
            {
                product.DataAlteracao = DateTime.Now;
                await _IProduct.Update(product);
            }
        }
    }
}
