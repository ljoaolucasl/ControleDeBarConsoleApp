namespace Prova01_ControleDeBar.ConsoleApp.ModuloConta
{
    public class FaturaDiaria
    {
        private TelaConta telaConta;

        private static List<FaturaDiaria> faturasDiarias = new();

        public Conta conta;
        public DateTime data;

        public void AdicionarFatura(FaturaDiaria faturaDiaria)
        {
            faturasDiarias.Add(faturaDiaria);
        }

        public List<FaturaDiaria> ObterListaFatura()
        {
            return faturasDiarias;
        }

        public double ObterTotalFaturado(DateTime data)
        {
            double totalFaturado = 0;

            foreach (FaturaDiaria fatura in faturasDiarias)
            {
                if (data.ToString("d") == fatura.data.ToString("d"))
                    totalFaturado += fatura.conta.valorTotal;
            }

            return totalFaturado;
        }
    }
}
