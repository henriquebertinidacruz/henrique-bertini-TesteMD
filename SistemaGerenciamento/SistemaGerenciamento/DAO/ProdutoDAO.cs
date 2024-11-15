using Npgsql;
using SistemaGerenciamento.Models;
using SistemaGerenciamento.Database;
using System.Collections.Generic;
using SistemaGerenciamento.Interfaces;

namespace SistemaGerenciamento.DAO
{
    public class ProdutoDAO : IProdutoDAO
    {
        public void Inserir(Produto produto)
        {
            using (var connection = new NpgsqlConnection(Connection.ConnectionDatabase))
            {
                connection.Open();
                var query = "INSERT INTO Produtos (Nome, Preco, Estoque) VALUES (@Nome, @Preco, @Estoque)";
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nome", produto.Nome);
                    command.Parameters.AddWithValue("@Preco", produto.Preco);
                    command.Parameters.AddWithValue("@Estoque", produto.Estoque);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Atualizar(Produto produto)
        {
            using (var connection = new NpgsqlConnection(Connection.ConnectionDatabase))
            {
                connection.Open();
                var query = "UPDATE Produtos SET Nome = @Nome, Preco = @Preco, Estoque = @Estoque WHERE Id = @Id";
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", produto.Id);
                    command.Parameters.AddWithValue("@Nome", produto.Nome);
                    command.Parameters.AddWithValue("@Preco", produto.Preco);
                    command.Parameters.AddWithValue("@Estoque", produto.Estoque);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Remover(int id)
        {
            using (var connection = new NpgsqlConnection(Connection.ConnectionDatabase))
            {
                connection.Open();
                var query = "DELETE FROM Produtos WHERE Id = @Id";
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Produto> ConsultarTodos()
        {
            var produtos = new List<Produto>();
            using (var connection = new NpgsqlConnection(Connection.ConnectionDatabase))
            {
                connection.Open();
                var query = "SELECT Id, Nome, Preco, Estoque FROM Produtos";
                using (var command = new NpgsqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        produtos.Add(new Produto
                        {
                            Id = reader.GetInt32(0),
                            Nome = reader.GetString(1),
                            Preco = reader.GetDecimal(2),
                            Estoque = reader.GetInt32(3)
                        });
                    }
                }
            }
            return produtos;
        }

        public Produto ConsultarPorId(int id)
        {
            Produto produto = null;
            using (var connection = new NpgsqlConnection(Connection.ConnectionDatabase))
            {
                connection.Open();
                var query = "SELECT Id, Nome, Preco, Estoque FROM Produtos WHERE Id = @Id";
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            produto = new Produto
                            {
                                Id = reader.GetInt32(0),
                                Nome = reader.GetString(1),
                                Preco = reader.GetDecimal(2),
                                Estoque = reader.GetInt32(3)
                            };
                        }
                    }
                }
            }
            return produto;
        }
    }
}
