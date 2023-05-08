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
            return ObterListaRegistros().FindAll(c => c.estado);
        }

        public Conta SelecionarIdContasAbertas(int idEscolhido)
        {
            return ListaOrganizadaPorEstadoAberto().Find(e => e.id == idEscolhido);
        }

        public void AdicionarPedido(Pedido pedido, Conta conta)
        {
            conta.pedidos.Add(pedido);
            ObterTotalGastoConta(conta);
        }

        private void ObterTotalGastoConta(Conta conta)
        {
            conta.valorTotal = conta.pedidos.Sum(p => p.estoque.valor * p.quantidade);
        }
    }
}
