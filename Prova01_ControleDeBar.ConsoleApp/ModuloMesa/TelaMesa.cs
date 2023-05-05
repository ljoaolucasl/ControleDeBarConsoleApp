using Atividade14_ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace Prova01_ControleDeBar.ConsoleApp.ModuloMesa
{
    public class TelaMesa : TelaBase<RepositorioMesa, Mesa>
    {
        private RepositorioMesa repositorioMesa;

        public TelaMesa(RepositorioMesa repositorioMesa)
        {
            this.repositorioMesa = repositorioMesa;
        }

        public override void VisualizarRegistro()
        {
            Console.Clear();

            MostrarCabecalho(67, "Mesas", ConsoleColor.DarkCyan);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            string espacamento = "{0, -5} │ {1, -15} │ {2, -20} │ ";
            Console.Write(espacamento, "ID", "Número", "Setor");
            Console.WriteLine("{0, -20}", "Estado");
            Console.WriteLine("".PadRight(69, '―'));
            Console.ResetColor();

            foreach (Mesa mesa in repositorioMesa.ObterListaRegistros())
            {
                TextoZebrado();

                Console.Write(espacamento, "#" + mesa.id, mesa.numero, mesa.setor);

                if(mesa.ocupado)
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                else
                    Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine("{0, -20}", mesa.ocupado ? "OCUPADO" : "VAGO");

                Console.ForegroundColor = ConsoleColor.White;

            }

            Console.ResetColor();
            zebrado = true;

            PulaLinha();
        }

        protected override Mesa ObterCadastro()
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
