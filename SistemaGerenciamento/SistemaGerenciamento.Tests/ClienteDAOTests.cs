using Moq;
using Xunit;
using SistemaGerenciamento.Models;
using SistemaGerenciamento.Interfaces;

namespace SistemaGerenciamento.Tests
{
    public class ClienteDAOTests
    {
        private readonly Mock<IClienteDAO> _clienteDAO;

        public ClienteDAOTests()
        {
            _clienteDAO = new Mock<IClienteDAO>();
        }

        [Fact]
        public void Inserir_DeveAdicionarCliente()
        {
            var cliente = new Cliente { Nome = "Henrique Bertini", Endereco = "Rua Teste", Telefone = "123456789", Email = "henriquebertini@example.com" };

            _clienteDAO.Object.Inserir(cliente);

            _clienteDAO.Verify(d => d.Inserir(It.IsAny<Cliente>()), Times.Once);
        }

        [Fact]
        public void Atualizar_DeveModificarCliente()
        {
            var cliente = new Cliente { Id = 1, Nome = "Henrique Bertini", Endereco = "Rua Teste", Telefone = "123456789", Email = "henriquebertini@example.com" };

            _clienteDAO.Object.Atualizar(cliente);

            _clienteDAO.Verify(d => d.Atualizar(It.IsAny<Cliente>()), Times.Once);
        }

        [Fact]
        public void Remover_DeveExcluirCliente()
        {
            _clienteDAO.Object.Remover(1);

            _clienteDAO.Verify(d => d.Remover(It.IsAny<int>()), Times.Once);
        }
    }
}
