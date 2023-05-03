using Atividade14_ControleDeMedicamentos.ConsoleApp.Compartilhado;
using Prova01_ControleDeBar.ConsoleApp.ModuloPedido;
using System.Collections;

namespace Prova01_ControleDeBar.ConsoleApp.ModuloConta
{
    public class RepositorioConta : RepositorioBase
    {
        public void Fechar(Conta conta)
        {
            conta.estado = false;
        }

        public double CalcularValorTotal(Conta conta)
        {
            double total = 0;

            foreach (Pedido pedido in conta.pedidos)
            {
                total += pedido.estoque.valor * pedido.quantidade;
            }

            return total;
        }

        public ArrayList ListaOrganizadaPorEstadoAberto()
        {
            ArrayList listaOrganizada = new();

            foreach (Conta conta in ObterListaRegistros())
            {
                if (conta.estado)
                    listaOrganizada.Add(conta);
            }
            return listaOrganizada;
        }

        public double ObterTotalFaturado()
        {
            double totalFaturado = 0;

            foreach (Conta conta in ObterListaRegistros())
            {
                if (!conta.estado)
                    totalFaturado += conta.valorTotal;
            }

            return totalFaturado;
        }

        public EntidadeBase SelecionarIdContasAbertas(int idEscolhido)
        {
            foreach (EntidadeBase registro in ListaOrganizadaPorEstadoAberto())
                if (registro.id == idEscolhido)
                    return registro;

            return null;
        }

        public void AddPedido(Pedido pedido, Conta conta)
        {
            conta.pedidos.Add(pedido);
        }
    }
}
