using SistemaGerenciamento.DAO;
using SistemaGerenciamento.Models;
using SistemaGerenciamento.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SistemaGerenciamento.Forms
{
    public partial class FormVendas : Form
    {
        private ProdutoDAO produtoDAO;
        private ClienteDAO clienteDAO;
        private VendaDAO vendaDAO;
        private List<ItemVenda> itensVenda;

        public FormVendas()
        {
            InitializeComponent();

            produtoDAO = new ProdutoDAO();
            clienteDAO = new ClienteDAO();
            vendaDAO = new VendaDAO();

            itensVenda = new List<ItemVenda>();

            CarregarClientes();
            CarregarProdutos();
            this.MaximizeBox = false;
            AtualizarTotalVenda();
            this.StartPosition = FormStartPosition.CenterScreen;
        }


        private void CarregarClientes()
        {
            List<Cliente> clientes = clienteDAO.ConsultarTodos();
            cbCliente.DataSource = clientes;
            cbCliente.DisplayMember = "Nome";
            cbCliente.ValueMember = "Id";
        }

        private void CarregarProdutos()
        {
            List<Produto> produtos = produtoDAO.ConsultarTodos();
            cbProduto.DataSource = produtos;
            cbProduto.DisplayMember = "Nome";
            cbProduto.ValueMember = "Id";
        }

        private void btnAdicionarProduto_Click(object sender, EventArgs e)
        {
            if (cbProduto.SelectedItem == null || string.IsNullOrEmpty(txtQuantidade.Text))
            {
                MessageBox.Show("Selecione um produto e informe a quantidade.");
                return;
            }

            Produto produtoSelecionado = (Produto)cbProduto.SelectedItem;
            int quantidade = int.Parse(txtQuantidade.Text);

            if (quantidade > produtoSelecionado.Estoque)
            {
                MessageBox.Show("Quantidade solicitada maior que o estoque disponível.");
                return;
            }

            decimal precoUnitario = produtoSelecionado.Preco;

            ItemVenda itemVenda = new ItemVenda
            {
                ProdutoId = produtoSelecionado.Id,
                Produto = produtoSelecionado,
                Quantidade = quantidade,
                PrecoUnitario = precoUnitario
            };

            itensVenda.Add(itemVenda);

            AtualizarItensVenda();
            AtualizarTotalVenda();
            LimparCamposProduto();
        }


        private void btnRemoverProduto_Click(object sender, EventArgs e)
        {
            if (dgvItensVenda.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione um item para remover.");
                return;
            }

            int index = dgvItensVenda.SelectedRows[0].Index;
            itensVenda.RemoveAt(index);

            AtualizarItensVenda();
            AtualizarTotalVenda();
        }

        private void btnFinalizarVenda_Click(object sender, EventArgs e)
        {
            if (cbCliente.SelectedItem == null)
            {
                MessageBox.Show("Selecione um cliente para finalizar a venda.");
                return;
            }

            if (itensVenda.Count == 0)
            {
                MessageBox.Show("Adicione pelo menos um produto à venda.");
                return;
            }

            Cliente clienteSelecionado = (Cliente)cbCliente.SelectedItem;

            Venda venda = new Venda
            {
                ClienteId = clienteSelecionado.Id,
                DataVenda = DateTime.Now,
            };

            int vendaId = vendaDAO.InserirVenda(venda);

            foreach (var item in itensVenda)
            {
                item.VendaId = vendaId;
                vendaDAO.InserirItemVenda(item);

                Produto produto = produtoDAO.ConsultarPorId(item.ProdutoId);
                produto.Estoque -= item.Quantidade;
                produtoDAO.Atualizar(produto);
            }

            MessageBox.Show("Venda finalizada com sucesso!");

            LimparVenda();
        }

        private void btnLimparVenda_Click(object sender, EventArgs e)
        {
            LimparVenda();
        }

        private void AtualizarItensVenda()
        {
            dgvItensVenda.DataSource = null;
            dgvItensVenda.DataSource = itensVenda.Select(i => new
            {
                Produto = i.Produto.Nome,
                Quantidade = i.Quantidade,
                PrecoUnitario = i.PrecoUnitario,
                Total = i.Quantidade * i.PrecoUnitario
            }).ToList();
        }

        private void AtualizarTotalVenda()
        {
            decimal total = itensVenda.Sum(i => i.Quantidade * i.PrecoUnitario);
            lblTotal.Text = $"{total:C}";
        }

        private void LimparCamposProduto()
        {
            cbProduto.SelectedIndex = -1;
            txtQuantidade.Clear();
        }

        private void LimparVenda()
        {
            itensVenda.Clear();
            cbCliente.SelectedIndex = -1;
            AtualizarItensVenda();
            AtualizarTotalVenda();
            LimparCamposProduto();
        }

        private void dgvItensVenda_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= itensVenda.Count)
            {
                return;
            }

            var itemSelecionado = itensVenda[e.RowIndex];

            cbProduto.SelectedItem = itemSelecionado.Produto;

            txtQuantidade.Text = itemSelecionado.Quantidade.ToString();

            dgvItensVenda.Tag = e.RowIndex;
        }
    }
}
