using Atividade14_ControleDeMedicamentos.ConsoleApp.Compartilhado;
using Microsoft.Win32;
using Prova01_ControleDeBar.ConsoleApp.ModuloEstoque;
using Prova01_ControleDeBar.ConsoleApp.ModuloGarcom;
using Prova01_ControleDeBar.ConsoleApp.ModuloMesa;
using Prova01_ControleDeBar.ConsoleApp.ModuloPedido;

namespace Prova01_ControleDeBar.ConsoleApp.ModuloConta
{
    public class TelaConta : TelaBase
    {
        private RepositorioConta repositorioConta;
        private RepositorioGarcom repositorioGarcom;
        private RepositorioMesa repositorioMesa;
        private RepositorioEstoque repositorioEstoque;

        private TelaMesa telaMesa;
        private TelaGarcom telaGarcom;
        private TelaEstoque telaEstoque;

        public TelaConta(RepositorioConta repositorioConta, RepositorioGarcom repositorioGarcom, RepositorioMesa repositorioMesa, RepositorioEstoque repositorioEstoque, TelaMesa telaMesa, TelaGarcom telaGarcom, TelaEstoque telaEstoque)
        {
            this.repositorioConta = repositorioConta;
            this.repositorioGarcom = repositorioGarcom;
            this.repositorioMesa = repositorioMesa;
            this.repositorioEstoque = repositorioEstoque;
            this.telaMesa = telaMesa;
            this.telaGarcom = telaGarcom;
            this.telaEstoque = telaEstoque;
        }

        public override void MostrarMenu(string tipo, ConsoleColor cor, RepositorioBase tipoRepositorio)
        {
            bool continuar = true;

            while (continuar)
            {
                Console.Clear();

                Console.ForegroundColor = cor;
                Console.WriteLine($"Controle de {tipo}");
                Console.ResetColor();
                PulaLinha();
                Console.WriteLine($"(1)Visualizar {tipo}");
                Console.WriteLine($"(2)Adicionar {tipo}");
                Console.WriteLine($"(3)Editar {tipo}");
                Console.WriteLine($"(4)Excluir {tipo}");
                Console.WriteLine($"(5)Fechar {tipo}");
                Console.WriteLine($"(6)Visualizar {tipo} em Aberto");
                Console.WriteLine($"(7)Visualizar Total Faturado Hoje");
                PulaLinha();
                Console.WriteLine("(S)Sair");
                PulaLinha();
                Console.Write("Escolha: ");

                continuar = InicializarOpcaoEscolhida(tipoRepositorio);
            }
        }

        protected override bool InicializarOpcaoEscolhida(RepositorioBase tipoRepositorio)
        {
            string entrada = Console.ReadLine();

            switch (entrada.ToUpper())
            {
                case "1": VisualizarRegistro(); Console.ReadLine(); break;
                case "2": AdicionarRegistro(tipoRepositorio); break;
                case "3": EditarRegistro(tipoRepositorio); break;
                case "4": ExcluirRegistro(tipoRepositorio); break;
                case "5": FecharConta(tipoRepositorio); break;
                case "6": VisualizarContaEmAberto(); Console.ReadLine(); break;
                case "7": VisualizarTotalFaturadoDia(); Console.ReadLine(); break;
                case "S": return false;
                default: break;
            }
            return true;
        }

        private void VisualizarTotalFaturadoDia()
        {
            Console.Clear();

            MostrarCabecalho(80, "Total Faturado", ConsoleColor.White);
            Console.WriteLine("".PadRight(82, '―'));
            Console.ResetColor();

            Console.WriteLine("O Total Faturado hoje foi R$" + repositorioConta.ObterTotalFaturado());
        }

        private void VisualizarContaEmAberto()
        {
            Console.Clear();

            MostrarCabecalho(80, "Contas", ConsoleColor.White);
            Console.ForegroundColor = ConsoleColor.White;
            string espacamento = "{0, -5} │ {1, -15} │ {2, -30} │ {3, -15} │ {4, -15}";
            Console.WriteLine(espacamento, "ID", "Número da Mesa", "Garçom", "Valor Total", "Estado");
            Console.WriteLine("".PadRight(82, '―'));
            Console.ResetColor();

            foreach (Conta conta in repositorioConta.ListaOrganizadaPorEstado())
            {
                conta.valorTotal = repositorioConta.CalcularValorTotal(conta);

                TextoZebrado();

                Console.WriteLine(espacamento, "#" + conta.id, conta.mesa.numero, conta.garcom.nome, "R$" + conta.valorTotal, conta.estado == true ? "ABERTO" : "FECHADO");
            }

            Console.ResetColor();
            zebrado = true;

            PulaLinha();
        }

        public override void VisualizarRegistro()
        {
            Console.Clear();

            MostrarCabecalho(80, "Contas", ConsoleColor.White);
            Console.ForegroundColor = ConsoleColor.White;
            string espacamento = "{0, -5} │ {1, -15} │ {2, -30} │ {3, -15} │ {4, -15}";
            Console.WriteLine(espacamento, "ID", "Número da Mesa", "Garçom", "Valor Total", "Estado");
            Console.WriteLine("".PadRight(82, '―'));
            Console.ResetColor();

            foreach (Conta conta in repositorioConta.ObterListaRegistros())
            {
                conta.valorTotal = repositorioConta.CalcularValorTotal(conta);

                TextoZebrado();

                Console.WriteLine(espacamento, "#" + conta.id, conta.mesa.numero, conta.garcom.nome, "R$" + conta.valorTotal, conta.estado == true ? "ABERTO" : "FECHADO");
            }

            Console.ResetColor();
            zebrado = true;

            PulaLinha();
        }

        protected override void AdicionarRegistro(RepositorioBase tipoRepositorio)
        {
            VisualizarRegistro();

            RepositorioBase repositorio = tipoRepositorio;

            EntidadeBase registro = ObterCadastro();

            if (ValidaValorNull(registro))
            {
                if (ValidaLimiteEstoque((Conta)registro))
                {
                    repositorio.Adicionar(registro);

                    VisualizarRegistro();

                    MensagemColor($"\nCadastro adicionado com sucesso!", ConsoleColor.Green);
                }
            }
            else
                MensagemColor($"\nCadastro incompleto, retornando ao Menu . . .", ConsoleColor.DarkYellow);

            Console.ReadLine();
        }

        protected override void EditarRegistro(RepositorioBase tipoRepositorio)
        {
            VisualizarRegistro();

            if (ValidaListaVazia(tipoRepositorio.ObterListaRegistros()))
            {
                EntidadeBase registroAntigo = ObterId(tipoRepositorio, "Digite o ID do Item que deseja editar: ");

                EntidadeBase registroAtualizado = ObterCadastro();

                if (ValidaValorNull(registroAtualizado))
                {
                    if (ValidaLimiteEstoque((Conta)registroAtualizado))
                    {
                        tipoRepositorio.Editar(registroAntigo, registroAtualizado);

                        VisualizarRegistro();

                        MensagemColor("\nItem editado com sucesso!", ConsoleColor.Green);
                    }
                }
                else
                    MensagemColor($"\nCadastro incompleto, retornando ao Menu . . .", ConsoleColor.DarkYellow);
            }

            Console.ReadLine();
        }

        protected virtual void FecharConta(RepositorioBase tipoRepositorio)
        {
            VisualizarRegistro();

            if (ValidaListaVazia(repositorioConta.ObterListaRegistros()))
            {
                Conta conta = (Conta)ObterId(repositorioConta, "Digite o ID da Conta que deseja fechar: ");

                repositorioConta.Fechar(conta);

                VisualizarRegistro();

                MensagemColor("\nConta fechada com sucesso!", ConsoleColor.Green);
            }

            Console.ReadLine();
        }

        protected override EntidadeBase ObterCadastro()
        {
            Pedido pedido = new()
            {
                estoque = ObterEstoque(),
                quantidade = ObterQuantidade()
            };

            Conta conta = new()
            {
                mesa = ObterMesa(),
                garcom = ObterGarcom(),
                pedido = pedido
            };

            return conta;
        }

        private Estoque ObterEstoque()
        {
            telaEstoque.VisualizarRegistro();
            Estoque estoque = null;

            if (ValidaListaVazia(repositorioEstoque.ObterListaRegistros()))
            {
                estoque = (Estoque)ObterId(repositorioEstoque, "Digite o ID do Produto: ");
            }
            return estoque;
        }

        private int ObterQuantidade()
        {
            int quantidade = ValidaNumero("Escreva a Quantidade: ");
            return quantidade;
        }

        private Mesa ObterMesa()
        {
            telaMesa.VisualizarRegistro();

            Mesa mesa = null;

            if (ValidaListaVazia(repositorioMesa.ObterListaRegistros()))
            {
                mesa = (Mesa)ObterId(repositorioMesa, "Digite o ID do Mesa: ");
            }
            return mesa;
        }

        private Garcom ObterGarcom()
        {
            telaGarcom.VisualizarRegistro();

            Garcom garcom = null;

            if (ValidaListaVazia(repositorioGarcom.ObterListaRegistros()))
            {
                garcom = (Garcom)ObterId(repositorioGarcom, "Digite o ID do Garcom: ");
            }
            return garcom;
        }

        private bool ValidaLimiteEstoque(Conta conta)
        {
            if (conta.pedido.quantidade > conta.pedido.estoque.quantidade)
            {
                MensagemColor($"\nQuantidade pedida foi acima do nosso Estoque . . . (qtd. disponível: {conta.pedido.estoque.quantidade})", ConsoleColor.DarkYellow);
                return false;
            }
            else
                return true;
        }
    }
}
