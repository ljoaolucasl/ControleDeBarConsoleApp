using Atividade14_ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace Prova01_ControleDeBar.ConsoleApp.ModuloMesa
{
    public class TelaMesa : TelaBase
    {
        private RepositorioMesa repositorioMesa;

        public TelaMesa(RepositorioMesa repositorioMesa)
        {
            this.repositorioMesa = repositorioMesa;
        }

        public override void VisualizarRegistro()
        {
            Console.Clear();

            MostrarCabecalho(80, "Mesas", ConsoleColor.DarkCyan);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            string espacamento = "{0, -5} │ {1, -15} │ {2, -20}";
            Console.WriteLine(espacamento, "ID", "Número", "Setor");
            Console.WriteLine("".PadRight(82, '―'));
            Console.ResetColor();

            foreach (Mesa mesa in repositorioMesa.ObterListaRegistros())
            {
                TextoZebrado();

                Console.WriteLine(espacamento, "#" + mesa.id, mesa.numero, mesa.setor);
            }

            Console.ResetColor();
            zebrado = true;

            PulaLinha();
        }

        protected override EntidadeBase ObterCadastro()
        {
            Mesa mesa = new()
            {
                numero = ObterNumero(),
                setor = ObterSetor(),
            };
            return mesa;
        }

        private int ObterNumero()
        {
            int numero = ValidaNumero("Escreva o Número da Mesa: ");
            return numero;
        }

        private string ObterSetor()
        {
            Mesa mesa = new();
            string setor = mesa.ValidaCampoVazio("Escreva o Setor da Mesa: ");
            return setor;
        }
    }
}
