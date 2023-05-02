using Atividade14_ControleDeMedicamentos.ConsoleApp.Compartilhado;
using Prova01_ControleDeBar.ConsoleApp.ModuloEstoque;

namespace Prova01_ControleDeBar.ConsoleApp.ModuloPedido
{
    public class Pedido : EntidadeBase
    {
        public Estoque estoque;
        public int quantidade;
    }
}
