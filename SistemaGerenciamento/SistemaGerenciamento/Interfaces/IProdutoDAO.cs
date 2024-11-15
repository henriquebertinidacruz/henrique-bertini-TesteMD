using SistemaGerenciamento.Models;
using System.Collections.Generic;

namespace SistemaGerenciamento.Interfaces
{
    public interface IProdutoDAO
    {
        void Inserir(Produto produto);
        void Atualizar(Produto produto);
        void Remover(int id);
        List<Produto> ConsultarTodos();
        Produto ConsultarPorId(int id);
    }
}
