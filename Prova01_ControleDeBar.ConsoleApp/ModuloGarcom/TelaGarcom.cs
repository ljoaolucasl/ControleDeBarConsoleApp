namespace Prova01_ControleDeBar.ConsoleApp.ModuloGarcom
{
    public class TelaGarcom : TelaBase<RepositorioGarcom, Garcom>
    {
        private RepositorioGarcom repositorioGarcom;

        public TelaGarcom(RepositorioGarcom repositorioGarcom)
        {
            this.repositorioGarcom = repositorioGarcom;
        }

        public override void VisualizarRegistro()
        {
            Console.Clear();

            MostrarCabecalho(80, "Garçons", ConsoleColor.DarkYellow);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            string espacamento = "{0, -5} │ {1, -30} │ {2, -20} │ {3, -18}";
            Console.WriteLine(espacamento, "ID", "Nome", "CPF", "Telefone");
            Console.WriteLine("".PadRight(82, '―'));
            Console.ResetColor();

            foreach (Garcom garcom in repositorioGarcom.ObterListaRegistros())
            {
                TextoZebrado();

                Console.WriteLine(espacamento, "#" + garcom.id, garcom.nome, garcom.cpf, garcom.telefone);
            }

            Console.ResetColor();
            zebrado = true;

            PulaLinha();
        }

        protected override Garcom ObterCadastro()
        {
            Garcom garcom = new()
            {
                nome = ObterNome(),
                cpf = ObterCPF(),
                telefone = ObterTelefone()
            };
            return garcom;
        }

        private string ObterNome()
        {
            Garcom garcom = new();
            string nome = garcom.ValidaCampoVazio("Escreva o Nome: ");
            return nome;
        }

        private string ObterCPF()
        {
            string cpf = ValidaCPF("Escreva o CPF: ");
            return cpf;
        }

        private string ObterTelefone()
        {
            string telefone = ValidaTelefone("Escreva o Telefone: ");
            return telefone;
        }
    }
}
