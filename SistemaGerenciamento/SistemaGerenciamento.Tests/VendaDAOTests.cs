﻿using Moq;
using Xunit;
using SistemaGerenciamento.Models;
using SistemaGerenciamento.DAO;
using System;

namespace SistemaGerenciamento.Tests
{
    public class VendaDAOTests
    {
        private readonly Mock<VendaDAO> _vendaDAO;

        public VendaDAOTests()
        {
            _vendaDAO = new Mock<VendaDAO>();
        }

        [Fact]
        public void RegistrarVenda_DeveRetornarIdDaVenda()
        {
            var venda = new Venda { ClienteId = 1, DataVenda = DateTime.Now };
            _vendaDAO.Setup(d => d.InserirVenda(It.IsAny<Venda>())).Returns(1);

            int vendaId = _vendaDAO.Object.InserirVenda(venda);

            Assert.Equal(1, vendaId);
            _vendaDAO.Verify(d => d.InserirVenda(It.IsAny<Venda>()), Times.Once);
        }

        [Fact]
        public void InserirItemVenda_DeveAdicionarItemVenda()
        {
            var itemVenda = new ItemVenda { VendaId = 1, ProdutoId = 1, Quantidade = 5, PrecoUnitario = 20.0M };
            _vendaDAO.Object.InserirItemVenda(itemVenda);
            _vendaDAO.Verify(d => d.InserirItemVenda(It.IsAny<ItemVenda>()), Times.Once);
        }
    }
}
