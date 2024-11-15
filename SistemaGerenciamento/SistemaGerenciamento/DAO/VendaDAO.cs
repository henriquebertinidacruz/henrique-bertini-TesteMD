using Npgsql;
using SistemaGerenciamento.Models;
using SistemaGerenciamento.Database;
using SistemaGerenciamento.Interfaces;
using System;
using System.Collections.Generic;

namespace SistemaGerenciamento.DAO
{
    public class VendaDAO : IVendaDAO
    {
        public int InserirVenda(Venda venda)
        {
            int vendaId;
            using (var connection = new NpgsqlConnection(Connection.ConnectionDatabase))
            {
                connection.Open();
                var query = "INSERT INTO Vendas (ClienteId, DataVenda) VALUES (@ClienteId, @DataVenda) RETURNING Id";
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ClienteId", venda.ClienteId);
                    command.Parameters.AddWithValue("@DataVenda", venda.DataVenda);
                    vendaId = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            return vendaId;
        }

        public void InserirItemVenda(ItemVenda itemVenda)
        {
            using (var connection = new NpgsqlConnection(Connection.ConnectionDatabase))
            {
                connection.Open();
                var query = "INSERT INTO ItensVenda (VendaId, ProdutoId, Quantidade, PrecoUnitario) VALUES (@VendaId, @ProdutoId, @Quantidade, @PrecoUnitario)";
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@VendaId", itemVenda.VendaId);
                    command.Parameters.AddWithValue("@ProdutoId", itemVenda.ProdutoId);
                    command.Parameters.AddWithValue("@Quantidade", itemVenda.Quantidade);
                    command.Parameters.AddWithValue("@PrecoUnitario", itemVenda.PrecoUnitario);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void AtualizarVenda(Venda venda)
        {
            using (var connection = new NpgsqlConnection(Connection.ConnectionDatabase))
            {
                connection.Open();
                var query = "UPDATE Vendas SET ClienteId = @ClienteId, DataVenda = @DataVenda WHERE Id = @Id";
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", venda.Id);
                    command.Parameters.AddWithValue("@ClienteId", venda.ClienteId);
                    command.Parameters.AddWithValue("@DataVenda", venda.DataVenda);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void RemoverVenda(int id)
        {
            using (var connection = new NpgsqlConnection(Connection.ConnectionDatabase))
            {
                connection.Open();
                var query = "DELETE FROM Vendas WHERE Id = @Id";
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Venda> ConsultarVendas()
        {
            var vendas = new List<Venda>();
            using (var connection = new NpgsqlConnection(Connection.ConnectionDatabase))
            {
                connection.Open();
                var query = "SELECT v.Id, v.ClienteId, v.DataVenda, c.Nome AS ClienteNome FROM Vendas v JOIN Clientes c ON v.ClienteId = c.Id";
                using (var command = new NpgsqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var venda = new Venda
                        {
                            Id = reader.GetInt32(0),
                            ClienteId = reader.GetInt32(1),
                            DataVenda = reader.GetDateTime(2),
                            Cliente = new Cliente { Nome = reader.GetString(3) }
                        };
                        vendas.Add(venda);
                    }
                }
            }
            return vendas;
        }

        public Venda ConsultarVendaPorId(int id)
        {
            Venda venda = null;
            using (var connection = new NpgsqlConnection(Connection.ConnectionDatabase))
            {
                connection.Open();
                var query = "SELECT v.Id, v.ClienteId, v.DataVenda, c.Nome AS ClienteNome FROM Vendas v JOIN Clientes c ON v.ClienteId = c.Id WHERE v.Id = @Id";
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("Id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            venda = new Venda
                            {
                                Id = reader.GetInt32(0),
                                ClienteId = reader.GetInt32(1),
                                DataVenda = reader.GetDateTime(2),
                                Cliente = new Cliente { Nome = reader.GetString(3) }
                            };
                        }
                    }
                }
            }
            return venda;
        }
    }
}
