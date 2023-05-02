﻿using Atividade14_ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace Prova01_ControleDeBar.ConsoleApp.ModuloEstoque
{
    public class TelaEstoque : TelaBase
    {
        private RepositorioEstoque repositorioEstoque;

        public TelaEstoque(RepositorioEstoque repositorioEstoque)
        {
            this.repositorioEstoque = repositorioEstoque;
        }

        public override void VisualizarRegistro()
        {
            Console.Clear();

            MostrarCabecalho(80, "Estoque", ConsoleColor.DarkCyan);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            string espacamento = "{0, -5} │ {1, -30} │ {2, -15} │ {3, -18}";
            Console.WriteLine(espacamento, "ID", "Nome", "Quantidade", "Valor");
            Console.WriteLine("".PadRight(82, '―'));
            Console.ResetColor();

            foreach (Estoque estoque in repositorioEstoque.ObterListaRegistros())
            {
                TextoZebrado();

                Console.WriteLine(espacamento, "#" + estoque.id, estoque.nome, estoque.quantidade, "R$" + estoque.valor);
            }

            Console.ResetColor();
            zebrado = true;

            PulaLinha();
        }

        protected override EntidadeBase ObterCadastro()
        {
            Estoque estoque = new()
            {
                nome = ObterNome(),
                quantidade = ObterQuantidade(),
                valor = ObterValor()
            };
            return estoque;
        }

        private string ObterNome()
        {
            Estoque estoque = new();
            string nome = estoque.ValidaCampoVazio("Escreva o Nome: ");
            return nome;
        }

        private int ObterQuantidade()
        {
            int quantidade = ValidaNumero("Escreva a Quantidade: ");
            return quantidade;
        }

        private double ObterValor()
        {
            double valor = ValidaNumeroFlutuante("Escreva o Valor: ");
            return valor;
        }
    }
}