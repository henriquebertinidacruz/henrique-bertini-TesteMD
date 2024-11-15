using System.Collections.Generic;
using SistemaGerenciamento.Models;

namespace SistemaGerenciamento.Interfaces
{
    public interface IVendaDAO
    {
        int InserirVenda(Venda venda);
        void InserirItemVenda(ItemVenda itemVenda);
        void AtualizarVenda(Venda venda);
        void RemoverVenda(int id);
        List<Venda> ConsultarVendas();
        Venda ConsultarVendaPorId(int id);
    }
}
