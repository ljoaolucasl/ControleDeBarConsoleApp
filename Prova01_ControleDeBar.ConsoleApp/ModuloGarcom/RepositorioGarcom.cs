using Atividade14_ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace Prova01_ControleDeBar.ConsoleApp.ModuloGarcom
{
    public class RepositorioGarcom : RepositorioBase
    {
        public void PreCadastrarGarcons()
        {
            Garcom garcom1 = new();
            garcom1.nome = "Rodrigo Lopes";
            garcom1.cpf = "025.456.847-55";
            garcom1.telefone = "9988456";

            Adicionar(garcom1);

            Garcom garcom2 = new();
            garcom2.nome = "Marcos Dos Santos";
            garcom2.cpf = "456.854.125-88";
            garcom2.telefone = "9884562";

            Adicionar(garcom2);

            Garcom garcom3 = new();
            garcom3.nome = "Laís Silvano";
            garcom3.cpf = "552.365.487-56";
            garcom3.telefone = "98886423";

            Adicionar(garcom3);
        }
    }
}
