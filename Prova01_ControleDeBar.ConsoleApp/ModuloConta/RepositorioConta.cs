using Atividade14_ControleDeMedicamentos.ConsoleApp.Compartilhado;
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
            return conta.pedido.estoque.valor * conta.pedido.quantidade;
        }

        public ArrayList ListaOrganizadaPorEstado()
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
            foreach (EntidadeBase registro in ListaOrganizadaPorEstado())
                if (registro.id == idEscolhido)
                    return registro;

            return null;
        }
    }
}
