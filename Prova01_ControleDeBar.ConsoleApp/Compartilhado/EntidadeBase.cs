namespace Atividade14_ControleDeMedicamentos.ConsoleApp.Compartilhado
{
    public abstract class EntidadeBase
    {
        public int id;

        public string ValidaCampoVazio(string mensagem)
        {
            bool validaPalavra;
            string entrada;

            do
            {
                Console.Write(mensagem);

                entrada = Console.ReadLine();

                validaPalavra = string.IsNullOrEmpty(entrada) || string.IsNullOrWhiteSpace(entrada);

                if (validaPalavra)
                {
                    MensagemColor("Atenção, campo obrigatório\n", ConsoleColor.Red);
                }

            } while (validaPalavra);

            return entrada;
        }

        private void MensagemColor(string mensagem, ConsoleColor cor)
        {
            Console.ForegroundColor = cor;
            Console.Write(mensagem);
            Console.ResetColor();
        }
    }
}
