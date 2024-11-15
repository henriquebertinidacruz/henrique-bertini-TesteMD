using SistemaGerenciamento.DAO;
using SistemaGerenciamento.Models;
using SistemaGerenciamento.Database;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SistemaGerenciamento.Forms
{
    public partial class FormClientes : Form
    {
        private ClienteDAO clienteDAO;
        private int? clienteIdSelecionado;

        public FormClientes()
        {
            InitializeComponent();
            clienteDAO = new ClienteDAO();
            CarregarClientes();
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
        }


        private void CarregarClientes()
        {
            List<Cliente> clientes = clienteDAO.ConsultarTodos();
            dgvClientes.DataSource = clientes;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Cliente cliente = new Cliente
            {
                Nome = txtNome.Text,
                Endereco = txtEndereco.Text,
                Telefone = txtTelefone.Text,
                Email = txtEmail.Text
            };

            clienteDAO.Inserir(cliente);
            MessageBox.Show("Cliente salvo com sucesso!");

            CarregarClientes();
            LimparCampos();
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            if (!clienteIdSelecionado.HasValue)
            {
                MessageBox.Show("Selecione um cliente para atualizar.");
                return;
            }

            Cliente cliente = new Cliente
            {
                Id = clienteIdSelecionado.Value,
                Nome = txtNome.Text,
                Endereco = txtEndereco.Text,
                Telefone = txtTelefone.Text,
                Email = txtEmail.Text
            };

            clienteDAO.Atualizar(cliente);
            MessageBox.Show("Cliente atualizado com sucesso!");

            CarregarClientes();
            LimparCampos();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (!clienteIdSelecionado.HasValue)
            {
                MessageBox.Show("Selecione um cliente para excluir.");
                return;
            }

            clienteDAO.Remover(clienteIdSelecionado.Value);
            MessageBox.Show("Cliente excluído com sucesso!");

            CarregarClientes();
            LimparCampos();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        private void LimparCampos()
        {
            clienteIdSelecionado = null;
            txtNome.Clear();
            txtEndereco.Clear();
            txtTelefone.Clear();
            txtEmail.Clear();
            dgvClientes.ClearSelection();
        }

        private void dgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvClientes.Rows[e.RowIndex];

                clienteIdSelecionado = Convert.ToInt32(row.Cells["Id"].Value);
                txtNome.Text = row.Cells["Nome"].Value.ToString();
                txtEndereco.Text = row.Cells["Endereco"].Value.ToString();
                txtTelefone.Text = row.Cells["Telefone"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
            }
        }
    }
}
