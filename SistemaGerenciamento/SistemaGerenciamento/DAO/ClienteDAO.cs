using Npgsql;
using SistemaGerenciamento.Models;
using SistemaGerenciamento.Database;
using SistemaGerenciamento.Interfaces;
using System;
using System.Collections.Generic;

namespace SistemaGerenciamento.DAO
{
    public class ClienteDAO : IClienteDAO
    {
        public void Inserir(Cliente cliente)
        {
            using (var connection = new NpgsqlConnection(Connection.ConnectionDatabase))
            {
                connection.Open();
                var query = "INSERT INTO Clientes (Nome, Endereco, Telefone, Email) VALUES (@Nome, @Endereco, @Telefone, @Email)";
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nome", cliente.Nome);
                    command.Parameters.AddWithValue("@Endereco", cliente.Endereco ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Telefone", cliente.Telefone ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Email", cliente.Email ?? (object)DBNull.Value);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Atualizar(Cliente cliente)
        {
            using (var connection = new NpgsqlConnection(Connection.ConnectionDatabase))
            {
                connection.Open();
                var query = "UPDATE Clientes SET Nome = @Nome, Endereco = @Endereco, Telefone = @Telefone, Email = @Email WHERE Id = @Id";
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", cliente.Id);
                    command.Parameters.AddWithValue("@Nome", cliente.Nome);
                    command.Parameters.AddWithValue("@Endereco", cliente.Endereco ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Telefone", cliente.Telefone ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Email", cliente.Email ?? (object)DBNull.Value);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Remover(int id)
        {
            using (var connection = new NpgsqlConnection(Connection.ConnectionDatabase))
            {
                connection.Open();
                var query = "DELETE FROM Clientes WHERE Id = @Id";
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Cliente> ConsultarTodos()
        {
            var clientes = new List<Cliente>();
            using (var connection = new NpgsqlConnection(Connection.ConnectionDatabase))
            {
                connection.Open();
                var query = "SELECT Id, Nome, Endereco, Telefone, Email FROM Clientes";
                using (var command = new NpgsqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        clientes.Add(new Cliente
                        {
                            Id = reader.GetInt32(0),
                            Nome = reader.GetString(1),
                            Endereco = reader.IsDBNull(2) ? null : reader.GetString(2),
                            Telefone = reader.IsDBNull(3) ? null : reader.GetString(3),
                            Email = reader.IsDBNull(4) ? null : reader.GetString(4)
                        });
                    }
                }
            }
            return clientes;
        }

        public Cliente ConsultarPorId(int id)
        {
            Cliente cliente = null;
            using (var connection = new NpgsqlConnection(Connection.ConnectionDatabase))
            {
                connection.Open();
                var query = "SELECT Id, Nome, Endereco, Telefone, Email FROM Clientes WHERE Id = @Id";
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            cliente = new Cliente
                            {
                                Id = reader.GetInt32(0),
                                Nome = reader.GetString(1),
                                Endereco = reader.IsDBNull(2) ? null : reader.GetString(2),
                                Telefone = reader.IsDBNull(3) ? null : reader.GetString(3),
                                Email = reader.IsDBNull(4) ? null : reader.GetString(4)
                            };
                        }
                    }
                }
            }
            return cliente;
        }
    }
}
