namespace Prova01_ControleDeBar.ConsoleApp.ModuloMesa
{
    public class RepositorioMesa : RepositorioBase<Mesa>
    {
        public void PreCadastrarMesas()
        {
            Mesa mesa1 = new();
            mesa1.numero = 22;
            mesa1.setor = "Janela";
            mesa1.ocupado = false;

            Adicionar(mesa1);

            Mesa mesa2 = new();
            mesa2.numero = 14;
            mesa2.setor = "Fundos";
            mesa2.ocupado = false;

            Adicionar(mesa2);

            Mesa mesa3 = new();
            mesa3.numero = 15;
            mesa3.setor = "Fundos";
            mesa3.ocupado = false;

            Adicionar(mesa3);
        }
    }
}
