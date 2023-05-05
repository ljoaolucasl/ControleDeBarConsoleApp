using Atividade14_ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace Prova01_ControleDeBar.ConsoleApp.ModuloEstoque
{
    public class TelaEstoque : TelaBase<RepositorioEstoque, Estoque>
    {
        private RepositorioEstoque repositorioEstoque;

        public TelaEstoque(RepositorioEstoque repositorioEstoque)
        {
            this.repositorioEstoque = repositorioEstoque;
        }

        public override void VisualizarRegistro()
        {
            Console.Clear();

            MostrarCabecalho(75, "Estoque", ConsoleColor.DarkMagenta);
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            string espacamento = "{0, -5} │ {1, -30} │ {2, -15} │ {3, -18}";
            Console.WriteLine(espacamento, "ID", "Nome", "Quantidade", "Valor");
            Console.WriteLine("".PadRight(77, '―'));
            Console.ResetColor();

            foreach (Estoque estoque in repositorioEstoque.ObterListaRegistros())
            {
                TextoZebrado();

                Console.WriteLine(espacamento, "#" + estoque.id, estoque.nome, estoque.quantidade, "R$" + estoque.valor);
            }

            Console.ResetColor();
            zebrado = true;

            PulaLinha();
        }

        protected override Estoque ObterCadastro()
        {
            Estoque estoque = new()
            {
                nome = ObterNome(),
                quantidade = ObterQuantidade(),
                valor = ObterValor()
            };
            return estoque;
        }

        private string ObterNome()
        {
            Estoque estoque = new();
            string nome = estoque.ValidaCampoVazio("Escreva o Nome: ");
            return nome;
        }

        private int ObterQuantidade()
        {
            int quantidade = ValidaNumero("Escreva a Quantidade: ");
            return quantidade;
        }

        private double ObterValor()
        {
            double valor = ValidaNumeroFlutuante("Escreva o Valor: ");
            return valor;
        }
    }
}
