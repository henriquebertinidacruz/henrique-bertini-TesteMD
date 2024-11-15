﻿using SistemaGerenciamento.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaGerenciamento
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormClientes formClientes = new FormClientes();
            formClientes.FormClosed += (s, args) => this.Show();
            formClientes.Show();
        }

        private void btnProdutos_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormProdutos formProdutos = new FormProdutos();
            formProdutos.FormClosed += (s, args) => this.Show();
            formProdutos.Show();
        }

        private void btnVendas_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormVendas formVendas = new FormVendas();
            formVendas.FormClosed += (s, args) => this.Show();
            formVendas.Show();
        }

        private void btnRelatorios_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormRelatorios formRelatorios = new FormRelatorios();
            formRelatorios.FormClosed += (s, args) => this.Show();
            formRelatorios.Show();
        }
    }
}
