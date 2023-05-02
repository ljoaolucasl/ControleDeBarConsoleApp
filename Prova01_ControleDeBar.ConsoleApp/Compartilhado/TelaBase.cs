using Microsoft.Win32;
using System.Collections;
using System.Text.RegularExpressions;

namespace Atividade14_ControleDeMedicamentos.ConsoleApp.Compartilhado
{
    public abstract class TelaBase
    {
        public virtual void MostrarMenu(string tipo, ConsoleColor cor, RepositorioBase tipoRepositorio)
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
                PulaLinha();
                Console.WriteLine("(S)Sair");
                PulaLinha();
                Console.Write("Escolha: ");

                continuar = InicializarOpcaoEscolhida(tipoRepositorio);
            }
        }

        public abstract void VisualizarRegistro();

        protected virtual bool InicializarOpcaoEscolhida(RepositorioBase tipoRepositorio)
        {
            string entrada = Console.ReadLine();

            switch (entrada.ToUpper())
            {
                case "1": VisualizarRegistro(); Console.ReadLine(); break;
                case "2": AdicionarRegistro(tipoRepositorio); break;
                case "3": EditarRegistro(tipoRepositorio); break;
                case "4": ExcluirRegistro(tipoRepositorio); break;
                case "S": return false;
                default: break;
            }
            return true;
        }

        protected virtual void AdicionarRegistro(RepositorioBase tipoRepositorio)
        {
            VisualizarRegistro();

            RepositorioBase repositorio = tipoRepositorio;

            EntidadeBase registro = ObterCadastro();

            if (ValidaValorNull(registro))
            {
                repositorio.Adicionar(registro);

                VisualizarRegistro();

                MensagemColor($"\nCadastro adicionado com sucesso!", ConsoleColor.Green);
            }
            else
                MensagemColor($"\nCadastro incompleto, retornando ao Menu . . .", ConsoleColor.DarkYellow);

            Console.ReadLine();
        }

        protected virtual void EditarRegistro(RepositorioBase tipoRepositorio)
        {
            VisualizarRegistro();

            if (ValidaListaVazia(tipoRepositorio.ObterListaRegistros()))
            {
                EntidadeBase registroAntigo = ObterId(tipoRepositorio, "Digite o ID do Item que deseja editar: ");

                EntidadeBase registroAtualizado = ObterCadastro();

                if (ValidaValorNull(registroAtualizado))
                {
                    tipoRepositorio.Editar(registroAntigo, registroAtualizado);

                    VisualizarRegistro();

                    MensagemColor("\nItem editado com sucesso!", ConsoleColor.Green);
                }
                else
                    MensagemColor($"\nCadastro incompleto, retornando ao Menu . . .", ConsoleColor.DarkYellow);
            }

            Console.ReadLine();
        }

        protected virtual void ExcluirRegistro(RepositorioBase tipoRepositorio)
        {
            VisualizarRegistro();

            if (ValidaListaVazia(tipoRepositorio.ObterListaRegistros()))
            {
                EntidadeBase registroEscolhido = ObterId(tipoRepositorio, "Digite o ID do Item que deseja excluir: ");

                tipoRepositorio.Excluir(registroEscolhido);

                VisualizarRegistro();

                MensagemColor("\nItem excluído com sucesso!", ConsoleColor.Green);
            }

            Console.ReadLine();
        }

        protected abstract EntidadeBase ObterCadastro();

        protected EntidadeBase ObterId(RepositorioBase tipoRepositorio, string mensagem)
        {
            EntidadeBase registro;

            if (ValidaListaVazia(tipoRepositorio.ObterListaRegistros()))
            {
                while (true)
                {
                    int idEscolhido = ValidaNumero(mensagem);

                    registro = tipoRepositorio.SelecionarId(idEscolhido);

                    if (registro == null)
                        MensagemColor("Atenção, apenas ID`s existentes\n", ConsoleColor.Red);

                    else
                        return registro;
                }
            }
            return null;
        }

        protected bool ValidaValorNull(EntidadeBase entidade)
        {
            if (entidade != null)
            {
                Type tipo = entidade.GetType();

                foreach (var atributo in tipo.GetFields())
                {
                    if (atributo.GetValue(entidade) == null)
                    {
                        MensagemColor($"Necessário o cadastro de \"{atributo.Name}\" para continuar", ConsoleColor.Red);
                        return false;
                    }
                }
            }
            return true;
        }

        protected int ValidaNumero(string mensagem)
        {
            bool validaNumero;
            string entrada;
            int numero;

            do
            {
                Console.Write(mensagem);

                entrada = Console.ReadLine();

                validaNumero = int.TryParse(entrada, out numero);

                if (!validaNumero)
                {
                    MensagemColor("Atenção, apenas números\n", ConsoleColor.Red);
                }

            } while (!validaNumero);

            return numero;
        }

        protected double ValidaNumeroFlutuante(string mensagem)
        {
            bool validaNumero;
            string entrada;
            double numero;

            do
            {
                Console.Write(mensagem);

                entrada = Console.ReadLine();

                validaNumero = double.TryParse(entrada, out numero);

                if (!validaNumero)
                {
                    MensagemColor("Atenção, apenas números\n", ConsoleColor.Red);
                }

            } while (!validaNumero);

            return numero;
        }

        protected string ValidaTelefone(string mensagem)
        {
            bool validaTelefone;
            string entrada;
            string telefoneFormato = @"^\((\d{2})\)(\d{5})\-(\d{4})$|^\((\d{2})\)(\d{4})\-(\d{4})$|^(\d{2})(\d{4})(\d{4})$|^(\d{2})(\d{5})(\d{4})$";

            do
            {
                Console.Write(mensagem);

                entrada = Console.ReadLine();

                validaTelefone = Regex.IsMatch(entrada, telefoneFormato);

                if (!validaTelefone)
                    MensagemColor("Atenção, telefone inválido. Ex: (ddd)00000-0000 (Traços e Parênteses não necessários)\n", ConsoleColor.Red);

            } while (!validaTelefone);

            if (Regex.IsMatch(entrada, @"^(\d{2})(\d{4})(\d{4})$"))
                entrada = Regex.Replace(entrada, @"^(\d{2})(\d{4})(\d{4})$", "($1)$2-$3");

            else if (Regex.IsMatch(entrada, @"^(\d{2})(\d{5})(\d{4})$"))
                entrada = Regex.Replace(entrada, @"^(\d{2})(\d{5})(\d{4})$", "($1)$2-$3");

            return entrada;
        }

        protected string ValidaCPF(string mensagem)
        {
            bool validaCPF;
            string entrada;
            string cpfFormato = @"^(\d{3})\.(\d{3})\.(\d{3})\-(\d{2})$|^(\d{3})(\d{3})(\d{3})(\d{2})$";

            do
            {
                Console.Write(mensagem);

                entrada = Console.ReadLine();

                validaCPF = Regex.IsMatch(entrada, cpfFormato);

                if (!validaCPF)
                    MensagemColor("Atenção, CPF inválido. Ex: 000.000.000-00 (Traços e Pontos não necessários)\n", ConsoleColor.Red);

            } while (!validaCPF);

            if (Regex.IsMatch(entrada, @"(\d{3})(\d{3})(\d{3})(\d{2})"))
                entrada = Regex.Replace(entrada, @"(\d{3})(\d{3})(\d{3})(\d{2})", "$1.$2.$3-$4");

            return entrada;
        }

        protected string ValidaCNPJ(string mensagem)
        {
            bool validaCPF;
            string entrada;
            string cpfFormato = @"^(\d{2})\.(\d{3})\.(\d{3})\/(\d{4})\-(\d{2})$|^(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})$";

            do
            {
                Console.Write(mensagem);

                entrada = Console.ReadLine();

                validaCPF = Regex.IsMatch(entrada, cpfFormato);

                if (!validaCPF)
                {
                    MensagemColor("Atenção, CNPJ inválido. Ex: 00.000.000/0000-00 (Traços e Pontos não necessários)\n", ConsoleColor.Red);
                }

            } while (!validaCPF);

            if (Regex.IsMatch(entrada, @"(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})"))
                entrada = Regex.Replace(entrada, @"(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})", "$1.$2.$3/$4-$5");

            return entrada;
        }

        protected DateTime ValidaData(string mensagem)
        {
            bool validaData;
            string entrada;
            DateTime dataAbertura;

            do
            {
                Console.Write(mensagem);

                entrada = Console.ReadLine();

                validaData = DateTime.TryParse(entrada, out dataAbertura);

                if (!validaData)
                {
                    MensagemColor("Atenção, escreva uma data válida\n", ConsoleColor.Red);
                }

            } while (!validaData);

            return dataAbertura;
        }

        protected bool ValidaListaVazia(ArrayList lista)
        {
            if (lista.Count == 0)
            {
                MensagemColor("A Lista está vazia . . .", ConsoleColor.DarkYellow);
                return false;
            }
            else
                return true;
        }

        protected void MostrarCabecalho(int espaco, string texto, ConsoleColor cor)
        {
            Console.ForegroundColor = cor;
            Console.WriteLine("╔" + "".PadRight(espaco, '═') + "╗");
            Console.WriteLine("║" + CentralizarTexto(espaco, texto) + "║");
            Console.WriteLine("╚" + "".PadRight(espaco, '═') + "╝");
            PulaLinha();
            Console.ResetColor();
        }

        protected string CentralizarTexto(int espaco, string texto)
        {
            int calculoLeft = (espaco + texto.Length) / 2;

            string textoCentralizado = $"{texto.PadLeft(calculoLeft, ' ').PadRight(espaco, ' ')}";

            return textoCentralizado;
        }

        protected void MensagemColor(string mensagem, ConsoleColor cor)
        {
            Console.ForegroundColor = cor;
            Console.Write(mensagem);
            Console.ResetColor();
        }

        protected void TextoZebrado()
        {
            if (zebrado)
            {
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.ForegroundColor = ConsoleColor.Black;
                zebrado = false;
            }
            else { Console.ResetColor(); zebrado = true; }
        }

        protected bool zebrado = true;

        protected void PulaLinha()
        {
            Console.WriteLine();
        }
    }
}
