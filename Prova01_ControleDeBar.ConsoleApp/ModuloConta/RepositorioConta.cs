using Atividade14_ControleDeMedicamentos.ConsoleApp.Compartilhado;
using Prova01_ControleDeBar.ConsoleApp.ModuloPedido;

namespace Prova01_ControleDeBar.ConsoleApp.ModuloConta
{
    public class RepositorioConta : RepositorioBase<Conta>
    {
        public void Fechar(Conta conta)
        {
            conta.estado = false;

            FaturaDiaria faturaDiaria = new();
            faturaDiaria.conta = conta;
            faturaDiaria.data = DateTime.Now;

            faturaDiaria.AdicionarFatura(faturaDiaria);
        }

        public List<Conta> ListaOrganizadaPorEstadoAberto()
        {
            List<Conta> listaOrganizada = new();

            foreach (Conta conta in ObterListaRegistros())
            {
                if (conta.estado)
                    listaOrganizada.Add(conta);
            }
            return listaOrganizada;
        }

        public Conta SelecionarIdContasAbertas(int idEscolhido)
        {
            foreach (Conta registro in ListaOrganizadaPorEstadoAberto())
                if (registro.id == idEscolhido)
                    return registro;

            return null;
        }

        public void AdicionarPedido(Pedido pedido, Conta conta)
        {
            conta.pedidos.Add(pedido);
            ObterTotalGastoConta(conta);
        }

        private void ObterTotalGastoConta(Conta conta)
        {
            double valorTotal = 0;

            foreach (Pedido pedido in conta.pedidos)
            {
                valorTotal += pedido.estoque.valor * pedido.quantidade;
            }
            conta.valorTotal = valorTotal;
        }
    }
}
