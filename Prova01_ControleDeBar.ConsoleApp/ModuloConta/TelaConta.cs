using Atividade14_ControleDeMedicamentos.ConsoleApp.Compartilhado;
using Prova01_ControleDeBar.ConsoleApp.ModuloEstoque;
using Prova01_ControleDeBar.ConsoleApp.ModuloGarcom;
using Prova01_ControleDeBar.ConsoleApp.ModuloMesa;
using Prova01_ControleDeBar.ConsoleApp.ModuloPedido;

namespace Prova01_ControleDeBar.ConsoleApp.ModuloConta
{
    public class TelaConta : TelaBase<RepositorioConta, Conta>
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

        public override void MostrarMenu(string tipo, ConsoleColor cor, RepositorioConta tipoRepositorio)
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
                PulaLinha();

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine($"Controle de Pedidos");
                Console.ResetColor();
                PulaLinha();
                Console.WriteLine($"(7)Registrar Pedido");
                Console.WriteLine($"(8)Visualizar Pedidos");
                PulaLinha();
                Console.WriteLine($"(9)Visualizar Total Faturado Hoje");
                PulaLinha();
                Console.WriteLine("(S)Sair");
                PulaLinha();
                Console.Write("Escolha: ");

                continuar = InicializarOpcaoEscolhida(tipoRepositorio);
            }
        }

        public override void VisualizarRegistro()
        {
            Console.Clear();

            MostrarCabecalho(90, "Contas", ConsoleColor.DarkYellow);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            string espacamento = "{0, -5} │ {1, -15} │ {2, -30} │ {3, -15} │ ";
            Console.Write(espacamento, "ID", "Número da Mesa", "Garçom", "Valor Total");
            Console.WriteLine("{0, -15}", "Estado");
            Console.WriteLine("".PadRight(92, '―'));
            Console.ResetColor();

            foreach (Conta conta in repositorioConta.ObterListaRegistros())
            {
                TextoZebrado();

                Console.Write(espacamento, "#" + conta.id, conta.mesa.numero, conta.garcom.nome, "R$" + conta.valorTotal);

                if (conta.estado) { Console.ForegroundColor = ConsoleColor.DarkYellow; }

                else { Console.ForegroundColor = ConsoleColor.Green; }

                Console.WriteLine("{0, -15}", conta.estado == true ? "ABERTO" : "FATURADO");

                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.ResetColor();
            zebrado = true;

            PulaLinha();
        }

        protected override bool InicializarOpcaoEscolhida(RepositorioConta tipoRepositorio)
        {
            string entrada = Console.ReadLine();

            switch (entrada.ToUpper())
            {
                case "1": VisualizarRegistro(); Console.ReadLine(); break;
                case "2": AdicionarRegistro(tipoRepositorio); break;
                case "3": EditarRegistro(tipoRepositorio); break;
                case "4": ExcluirRegistro(tipoRepositorio); break;
                case "5": FecharConta(); break;
                case "6": VisualizarContaEmAberto(); Console.ReadLine(); break;
                case "7": RegistrarPedido(); break;
                case "8": VisualizarPedidos(); Console.ReadLine(); break;
                case "9": VisualizarFaturamento(); Console.ReadLine(); break;
                case "S": return false;
                default: break;
            }
            return true;
        }

        protected override void AdicionarRegistro(RepositorioConta tipoRepositorio)
        {
            VisualizarRegistro();

            RepositorioConta repositorio = tipoRepositorio;

            Conta conta = ObterCadastro();

            if (ValidaValorNull(conta))
            {
                repositorio.Adicionar(conta);

                conta.mesa.ocupado = true;

                VisualizarRegistro();

                MensagemColor($"\nCadastro adicionado com sucesso!", ConsoleColor.Green);
            }
            else
                MensagemColor($"\nCadastro incompleto, retornando ao Menu . . .", ConsoleColor.DarkYellow);

            Console.ReadLine();
        }

        protected override void EditarRegistro(RepositorioConta tipoRepositorio)
        {
            VisualizarRegistro();

            if (ValidaListaVazia(tipoRepositorio.ObterListaRegistros()))
            {
                Conta contaAntiga = ObterId(tipoRepositorio, "Digite o ID do Item que deseja editar: ");

                Conta contaAtualizada = ObterCadastro();

                if (ValidaValorNull(contaAtualizada))
                {
                    foreach (Pedido pedido in contaAntiga.pedidos)
                    {
                        pedido.estoque.quantidade += pedido.quantidade;
                    }

                    contaAntiga.mesa.ocupado = false;

                    tipoRepositorio.Editar(contaAntiga, contaAtualizada);

                    foreach (Pedido pedido in contaAtualizada.pedidos)
                    {
                        pedido.estoque.quantidade -= pedido.quantidade;
                    }

                    contaAtualizada.mesa.ocupado = true;

                    VisualizarRegistro();

                    MensagemColor("\nItem editado com sucesso!", ConsoleColor.Green);
                }
                else
                    MensagemColor($"\nCadastro incompleto, retornando ao Menu . . .", ConsoleColor.DarkYellow);
            }

            Console.ReadLine();
        }

        protected override void ExcluirRegistro(RepositorioConta tipoRepositorio)
        {
            VisualizarRegistro();

            if (ValidaListaVazia(tipoRepositorio.ObterListaRegistros()))
            {
                Conta conta = ObterId(tipoRepositorio, "Digite o ID do Item que deseja excluir: ");

                foreach (Pedido pedido in conta.pedidos)
                {
                    pedido.estoque.quantidade += pedido.quantidade;
                }

                conta.mesa.ocupado = false;

                tipoRepositorio.Excluir(conta);

                VisualizarRegistro();

                MensagemColor("\nItem excluído com sucesso!", ConsoleColor.Green);
            }

            Console.ReadLine();
        }

        protected override Conta ObterCadastro()
        {
            Pedido pedido = new();

            Conta conta = new()
            {
                mesa = ObterMesa(),
                garcom = ObterGarcom(),
                pedido = pedido
            };

            return conta;
        }

        private void FecharConta()
        {
            VisualizarContaEmAberto();

            if (ValidaListaVazia(repositorioConta.ListaOrganizadaPorEstadoAberto()))
            {
                Conta conta = ObterIdContasAbertas("Digite o ID da Conta que deseja fechar: ");

                conta.mesa.ocupado = false;

                repositorioConta.Fechar(conta);

                VisualizarContaEmAberto();

                MensagemColor("\nConta fechada com sucesso!", ConsoleColor.Green);
            }

            Console.ReadLine();
        }

        private Conta ObterIdContasAbertas(string mensagem)
        {
            Conta conta;

            if (ValidaListaVazia(repositorioConta.ListaOrganizadaPorEstadoAberto()))
            {
                while (true)
                {
                    int idEscolhido = ValidaNumero(mensagem);

                    conta = repositorioConta.SelecionarIdContasAbertas(idEscolhido);

                    if (conta == null)
                        MensagemColor("Atenção, apenas ID`s existentes\n", ConsoleColor.Red);

                    else
                        return conta;
                }
            }
            return null;
        }

        private void RegistrarPedido()
        {
            Conta conta = ObterConta();

            Pedido pedido = new();

            pedido.estoque = ObterEstoque();
            do
            {
                pedido.quantidade = ObterQuantidade();

                if (pedido.quantidade > pedido.estoque.quantidade)
                    MensagemColor($"\nQuantidade pedida foi acima do nosso estoque . . . (qtd. disponível: {pedido.estoque.quantidade})\n", ConsoleColor.DarkYellow);

            } while (pedido.quantidade > pedido.estoque.quantidade);

            if (conta != null)
            {
                repositorioConta.AdicionarPedido(pedido, conta);
                pedido.estoque.quantidade -= pedido.quantidade;
                MensagemColor($"\nPedido adicionado com sucesso!", ConsoleColor.Green);
            }
            else
                MensagemColor($"\nNão foi selecionado uma conta, verifique se há ao menos uma conta em Aberto\n", ConsoleColor.DarkYellow);

            Console.ReadLine();
        }

        private void VisualizarPedidos()
        {
            Conta conta = ObterConta();

            if (conta != null)
            {
                Console.Clear();

                MostrarCabecalho(80, $"Pedidos Mesa #{conta.mesa.numero}", ConsoleColor.DarkCyan);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                string espacamento = "{0, -20} │ {1, -15} │ {2, -15} │ {3, -15}";
                Console.WriteLine(espacamento, "Produto", "Quantidade", "Valor Uni.", "Valor Total");
                Console.WriteLine("".PadRight(82, '―'));
                Console.ResetColor();

                foreach (Pedido pedido in conta.pedidos)
                {
                    double totalQtdValor = pedido.estoque.valor * pedido.quantidade;

                    TextoZebrado();

                    Console.WriteLine(espacamento, pedido.estoque.nome, pedido.quantidade, "R$" + pedido.estoque.valor, "R$" + totalQtdValor);
                }

                Console.ResetColor();
                zebrado = true;

                PulaLinha();
                Console.WriteLine("TOTAL: R$" + conta.valorTotal);

                PulaLinha();
            }
            else
                MensagemColor($"\nNão foi selecionado uma conta, verifique se há ao menos uma conta cadastrada\n", ConsoleColor.DarkYellow);
        }

        private void VisualizarFaturamento()
        {
            Console.Clear();

            FaturaDiaria faturaDiaria = new();

            DateTime data = ValidaData("Digite uma Data para ver o Total Faturado: ");

            MostrarCabecalho(74, "Faturamento", ConsoleColor.DarkMagenta);
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            string espacamento = "{0, -12} │ {1, -10} │ {2, -30} │ {3, -15}";
            Console.WriteLine(espacamento, "Data", "Mesa", "Garçom", "Valor Total");
            Console.WriteLine("".PadRight(76, '―'));
            Console.ResetColor();

            foreach (FaturaDiaria fatura in faturaDiaria.ObterListaFatura())
            {
                if (data.ToString("d") == fatura.data.ToString("d"))
                {
                    TextoZebrado();

                    Console.WriteLine(espacamento, fatura.data.ToString("d"), fatura.conta.mesa.numero, fatura.conta.garcom.nome, "R$" + fatura.conta.valorTotal);
                }
            }

            Console.ResetColor();
            zebrado = true;

            PulaLinha();

            Console.WriteLine("Total: R$" + faturaDiaria.ObterTotalFaturado(data));
        }

        private void VisualizarContaEmAberto()
        {
            Console.Clear();

            MostrarCabecalho(90, "Contas", ConsoleColor.DarkYellow);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            string espacamento = "{0, -5} │ {1, -15} │ {2, -30} │ {3, -15} │ ";
            Console.Write(espacamento, "ID", "Número da Mesa", "Garçom", "Valor Total");
            Console.WriteLine("{0, -15}", "Estado");
            Console.WriteLine("".PadRight(92, '―'));
            Console.ResetColor();

            foreach (Conta conta in repositorioConta.ListaOrganizadaPorEstadoAberto())
            {
                TextoZebrado();

                Console.Write(espacamento, "#" + conta.id, conta.mesa.numero, conta.garcom.nome, "R$" + conta.valorTotal);

                if (conta.estado) { Console.ForegroundColor = ConsoleColor.DarkYellow; }

                else { Console.ForegroundColor = ConsoleColor.Green; }

                Console.WriteLine("{0, -15}", conta.estado == true ? "ABERTO" : "FATURADO");

                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.ResetColor();
            zebrado = true;

            PulaLinha();
        }

        private Estoque ObterEstoque()
        {
            telaEstoque.VisualizarRegistro();
            Estoque estoque = null;

            if (telaEstoque.ValidaListaVazia(repositorioEstoque.ObterListaRegistros()))
            {
                estoque = telaEstoque.ObterId(repositorioEstoque, "Digite o ID do Produto: ");
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

            if (telaMesa.ValidaListaVazia(repositorioMesa.ObterListaRegistros()))
            {
                mesa = telaMesa.ObterId(repositorioMesa, "Digite o ID do Mesa: ");
            }
            return mesa;
        }

        private Garcom ObterGarcom()
        {
            telaGarcom.VisualizarRegistro();

            Garcom garcom = null;

            if (telaGarcom.ValidaListaVazia(repositorioGarcom.ObterListaRegistros()))
            {
                garcom = telaGarcom.ObterId(repositorioGarcom, "Digite o ID do Garcom: ");
            }
            return garcom;
        }

        private Conta ObterConta()
        {
            VisualizarContaEmAberto();

            Conta conta = null;

            if (ValidaListaVazia(repositorioConta.ListaOrganizadaPorEstadoAberto()))
            {
                conta = ObterIdContasAbertas("Digite o ID da Conta: ");
            }
            return conta;
        }
    }
}
