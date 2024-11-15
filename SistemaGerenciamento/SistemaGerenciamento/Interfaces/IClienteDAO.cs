using SistemaGerenciamento.Models;
using System.Collections.Generic;

namespace SistemaGerenciamento.Interfaces
{
    public interface IClienteDAO
    {
        void Inserir(Cliente cliente);
        void Atualizar(Cliente cliente);
        void Remover(int id);
        List<Cliente> ConsultarTodos();
        Cliente ConsultarPorId(int id);
    }
}
