using Atividade14_ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace Prova01_ControleDeBar.ConsoleApp.ModuloEstoque
{
    public class RepositorioEstoque : RepositorioBase<Estoque>
    {
        public void PreCadastrarEstoques()
        {
            Estoque estoque1 = new();
            estoque1.nome = "Gelada Padrão";
            estoque1.quantidade = 100;
            estoque1.valor = 5;

            Adicionar(estoque1);

            Estoque estoque2 = new();
            estoque2.nome = "Bebida Misteriosa";
            estoque2.quantidade = 60;
            estoque2.valor = 60;

            Adicionar(estoque2);

            Estoque estoque3 = new();
            estoque3.nome = "Chopp Galera";
            estoque3.quantidade = 350;
            estoque3.valor = 20;

            Adicionar(estoque3);
        }
    }
}
