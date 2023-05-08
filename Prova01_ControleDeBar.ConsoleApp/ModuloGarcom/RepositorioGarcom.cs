namespace Prova01_ControleDeBar.ConsoleApp.ModuloGarcom
{
    public class RepositorioGarcom : RepositorioBase<Garcom>
    {
        public void PreCadastrarGarcons()
        {
            Garcom garcom1 = new();
            garcom1.nome = "Rodrigo Lopes";
            garcom1.cpf = "025.456.847-55";
            garcom1.telefone = "(49)99998-8456";

            Adicionar(garcom1);

            Garcom garcom2 = new();
            garcom2.nome = "Marcos Dos Santos";
            garcom2.cpf = "456.854.125-88";
            garcom2.telefone = "(49)99884-5624";

            Adicionar(garcom2);

            Garcom garcom3 = new();
            garcom3.nome = "Laís Silvano";
            garcom3.cpf = "552.365.487-56";
            garcom3.telefone = "(48)99888-6423";

            Adicionar(garcom3);
        }
    }
}
