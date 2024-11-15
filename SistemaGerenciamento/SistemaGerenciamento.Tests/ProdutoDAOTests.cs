using Moq;
using Xunit;
using SistemaGerenciamento.Models;
using SistemaGerenciamento.Interfaces;

namespace SistemaGerenciamento.Tests
{
    public class ProdutoDAOTests
    {
        private readonly Mock<IProdutoDAO> _produtoDAO;

        public ProdutoDAOTests()
        {
            _produtoDAO = new Mock<IProdutoDAO>();
        }

        [Fact]
        public void Inserir_DeveAdicionarProduto()
        {
            var produto = new Produto { Nome = "Suporte para Monitor", Preco = 100.0M, Estoque = 10 };
            _produtoDAO.Object.Inserir(produto);
            _produtoDAO.Verify(d => d.Inserir(It.IsAny<Produto>()), Times.Once);
        }

        [Fact]
        public void Atualizar_DeveModificarProduto()
        {
            var produto = new Produto { Id = 1, Nome = "Suporte para Monitor", Preco = 100.0M, Estoque = 10 };
            _produtoDAO.Object.Atualizar(produto);
            _produtoDAO.Verify(d => d.Atualizar(It.IsAny<Produto>()), Times.Once);
        }

        [Fact]
        public void Remover_DeveExcluirProduto()
        {
            _produtoDAO.Object.Remover(1);
            _produtoDAO.Verify(d => d.Remover(It.IsAny<int>()), Times.Once);
        }
    }
}
