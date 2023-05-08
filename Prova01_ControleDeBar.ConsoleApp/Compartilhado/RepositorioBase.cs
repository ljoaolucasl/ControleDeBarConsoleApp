namespace Prova01_ControleDeBar.ConsoleApp.Compartilhado
{
    public abstract class RepositorioBase<TEntidade> where TEntidade : EntidadeBase
    {
        private List<TEntidade> listaRegistros = new();

        private int id = 1;

        public void Adicionar(TEntidade registro)
        {
            registro.id = id; id++;
            listaRegistros.Add(registro);
        }

        public void Editar(TEntidade registroAntigo, TEntidade registroNovo)
        {
            if (registroAntigo != null)
            {
                Type tipo = registroAntigo.GetType();

                foreach (var atributo in tipo.GetFields())
                {
                    if (atributo.Name != "id")
                        atributo.SetValue(registroAntigo, atributo.GetValue(registroNovo));
                }
            }
        }

        public void Excluir(TEntidade registroSelecionado)
        {
            if (registroSelecionado != null)
            {
                listaRegistros.Remove(registroSelecionado);
            }
        }

        public TEntidade SelecionarId(int idEscolhido)
        {
            return listaRegistros.Find(e => e.id == idEscolhido);
        }

        public List<TEntidade> ObterListaRegistros()
        {
            return listaRegistros;
        }

        public void PreCadastrarTestes(TEntidade entidade)
        {
            Type tipoEntidade = entidade.GetType();

            foreach (var atributo in tipoEntidade.GetFields())
            {
                if (atributo.Name != "id" && atributo.FieldType == typeof(string))
                    atributo.SetValue(entidade, "TESTE");

                else if (atributo.Name != "id" && atributo.FieldType != typeof(bool))
                    atributo.SetValue(entidade, 22);
            }

            Adicionar(entidade);
        }
    }
}
