using Microsoft.Reporting.WinForms;
using SistemaGerenciamento.DAO;
using SistemaGerenciamento.Models;
using SistemaGerenciamento.Database;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SistemaGerenciamento.Forms
{
    public partial class FormRelatorios : Form
    {
        public FormRelatorios()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void FormRelatorios_Load(object sender, EventArgs e)
        {
            cbRelatorio.Items.Add("Relatório de Vendas");
            cbRelatorio.Items.Add("Relatório de Produtos");
            cbRelatorio.Items.Add("Relatório de Clientes");
            this.reportViewer.RefreshReport();
        }

        private void btnGerarRelatorio_Click(object sender, EventArgs e)
        {
            string tipoRelatorio = cbRelatorio.SelectedItem?.ToString();
            if (tipoRelatorio == null)
            {
                MessageBox.Show("Selecione um tipo de relatório.");
                return;
            }

            switch (tipoRelatorio)
            {
                case "Relatório de Vendas":
                    GerarRelatorioVendas();
                    break;
                case "Relatório de Produtos":
                    GerarRelatorioProdutos();
                    break;
                case "Relatório de Clientes":
                    GerarRelatorioClientes();
                    break;
                default:
                    MessageBox.Show("Selecione um tipo de relatório válido.");
                    break;
            }
        }

        private void GerarRelatorioVendas()
        {
            reportViewer.LocalReport.ReportPath = @"C:\Dev_GitHub\henrique-bertini-TesteMD\SistemaGerenciamento\SistemaGerenciamento\Reports\RelatorioVendas.rdlc";

            List<Venda> vendas = ObterVendas();

            ReportDataSource dataSource = new ReportDataSource("VendasDataSet", vendas);
            reportViewer.LocalReport.DataSources.Clear();
            reportViewer.LocalReport.DataSources.Add(dataSource);

            reportViewer.RefreshReport();
        }

        private void GerarRelatorioProdutos()
        {
            reportViewer.LocalReport.ReportPath = @"C:\Dev_GitHub\henrique-bertini-TesteMD\SistemaGerenciamento\SistemaGerenciamento\Reports\RelatorioProdutos.rdlc";

            List<Produto> produtos = ObterProdutos();

            ReportDataSource dataSource = new ReportDataSource("ProdutosDataSet", produtos);
            reportViewer.LocalReport.DataSources.Clear();
            reportViewer.LocalReport.DataSources.Add(dataSource);

            reportViewer.RefreshReport();
        }

        private void GerarRelatorioClientes()
        {
            reportViewer.LocalReport.ReportPath = @"C:\Dev_GitHub\henrique-bertini-TesteMD\SistemaGerenciamento\SistemaGerenciamento\Reports\RelatorioClientes.rdlc";

            List<Cliente> clientes = ObterClientes();

            ReportDataSource dataSource = new ReportDataSource("ClientesDataSet", clientes);
            reportViewer.LocalReport.DataSources.Clear();
            reportViewer.LocalReport.DataSources.Add(dataSource);

            reportViewer.RefreshReport();
        }

        private List<Venda> ObterVendas()
        {
            var vendaDAO = new VendaDAO();
            return vendaDAO.ConsultarVendas();
        }

        private List<Produto> ObterProdutos()
        {
            var produtoDAO = new ProdutoDAO();
            return produtoDAO.ConsultarTodos();
        }

        private List<Cliente> ObterClientes()
        {
            var clienteDAO = new ClienteDAO();
            return clienteDAO.ConsultarTodos();
        }
    }
}
