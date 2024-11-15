using SistemaGerenciamento.DAO;
using SistemaGerenciamento.Models;
using SistemaGerenciamento.Database;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SistemaGerenciamento.Forms
{
    public partial class FormProdutos : Form
    {
        private ProdutoDAO produtoDAO;
        private int? produtoIdSelecionado;

        public FormProdutos()
        {
            InitializeComponent();
            produtoDAO = new ProdutoDAO();
            CarregarProdutos();
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
        }


        private void CarregarProdutos()
        {
            List<Produto> produtos = produtoDAO.ConsultarTodos();
            dgvProdutos.DataSource = produtos;
        }

        private void btnSalvarProduto_Click(object sender, EventArgs e)
        {
            Produto produto = new Produto
            {
                Nome = txtNomeProduto.Text,
                Preco = decimal.Parse(txtPrecoProduto.Text),
                Estoque = int.Parse(txtEstoqueProduto.Text)
            };

            produtoDAO.Inserir(produto);
            MessageBox.Show("Produto salvo com sucesso!");

            CarregarProdutos();
            LimparCampos();
        }

        private void btnAtualizarProduto_Click(object sender, EventArgs e)
        {
            if (!produtoIdSelecionado.HasValue)
            {
                MessageBox.Show("Selecione um produto para atualizar.");
                return;
            }

            Produto produto = new Produto
            {
                Id = produtoIdSelecionado.Value,
                Nome = txtNomeProduto.Text,
                Preco = decimal.Parse(txtPrecoProduto.Text),
                Estoque = int.Parse(txtEstoqueProduto.Text)
            };

            produtoDAO.Atualizar(produto);
            MessageBox.Show("Produto atualizado com sucesso!");

            CarregarProdutos();
            LimparCampos();
        }

        private void btnExcluirProduto_Click(object sender, EventArgs e)
        {
            if (!produtoIdSelecionado.HasValue)
            {
                MessageBox.Show("Selecione um produto para excluir.");
                return;
            }

            produtoDAO.Remover(produtoIdSelecionado.Value);
            MessageBox.Show("Produto excluído com sucesso!");

            CarregarProdutos();
            LimparCampos();
        }

        private void btnLimparProduto_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        private void LimparCampos()
        {
            produtoIdSelecionado = null;
            txtNomeProduto.Clear();
            txtPrecoProduto.Clear();
            txtEstoqueProduto.Clear();
            dgvProdutos.ClearSelection();
        }

        private void dgvProdutos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvProdutos.Rows[e.RowIndex];

                produtoIdSelecionado = Convert.ToInt32(row.Cells["Id"].Value);
                txtNomeProduto.Text = row.Cells["Nome"].Value.ToString();
                txtPrecoProduto.Text = row.Cells["Preco"].Value.ToString();
                txtEstoqueProduto.Text = row.Cells["Estoque"].Value.ToString();
            }
        }
    }
}
