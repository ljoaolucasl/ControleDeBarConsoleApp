using Prova01_ControleDeBar.ConsoleApp.ModuloConta;
using Prova01_ControleDeBar.ConsoleApp.ModuloEstoque;
using Prova01_ControleDeBar.ConsoleApp.ModuloGarcom;
using Prova01_ControleDeBar.ConsoleApp.ModuloMesa;
using Prova01_ControleDeBar.ConsoleApp.ModuloPedido;

namespace Prova01_ControleDeBar.ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            RepositorioEstoque repositorioEstoque = new();
            TelaEstoque telaEstoque = new(repositorioEstoque);

            RepositorioGarcom repositorioGarcom = new();
            TelaGarcom telaGarcom = new(repositorioGarcom);

            RepositorioMesa repositorioMesa = new();
            TelaMesa telaMesa = new(repositorioMesa);

            RepositorioConta repositorioConta = new();
            TelaConta telaConta = new(repositorioConta, repositorioGarcom, repositorioMesa, repositorioEstoque, telaMesa, telaGarcom, telaEstoque);

            repositorioGarcom.PreCadastrarGarcons();

            repositorioEstoque.PreCadastrarEstoques();

            repositorioMesa.PreCadastrarMesas();

            bool continuar = true;

            while (continuar)
            {
                MostrarMenuPrincipal();

                switch (ObterEscolha().ToUpper())
                {
                    case "1": telaConta.MostrarMenu("Contas", ConsoleColor.DarkCyan, repositorioConta); break;
                    case "2": telaMesa.MostrarMenu("Mesas", ConsoleColor.DarkGreen, repositorioMesa); break;
                    case "3": telaGarcom.MostrarMenu("Garçom", ConsoleColor.DarkYellow, repositorioGarcom); break;
                    case "4": telaEstoque.MostrarMenu("Estoque", ConsoleColor.DarkMagenta, repositorioEstoque); break;
                    case "S": continuar = false; break;
                    default: break;
                }
            }
        }

        private static void MostrarMenuPrincipal()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("╔════════════════════════╗");
            Console.WriteLine("║     Bar da Galera      ║");
            Console.WriteLine("╚════════════════════════╝");
            Console.ResetColor();
            PulaLinha();
            Console.WriteLine("Controles:");
            PulaLinha();
            Console.WriteLine("(1)Controle de Contas");
            Console.WriteLine("(2)Controle de Mesas");
            Console.WriteLine("(3)Controle de Garçons");
            Console.WriteLine("(4)Controle de Estoque");
            PulaLinha();
            Console.WriteLine("(S)Sair");
            PulaLinha();
            Console.Write("Escolha: ");
        }

        private static string ObterEscolha()
        {
            string entrada = Console.ReadLine();

            return entrada;
        }

        private static void PulaLinha()
        {
            Console.WriteLine();
        }
    }
}