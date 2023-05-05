namespace Atividade14_ControleDeMedicamentos.ConsoleApp.Compartilhado
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
                int idIndex = listaRegistros.IndexOf(registroSelecionado);

                listaRegistros.RemoveAt(idIndex);
            }
        }

        public TEntidade SelecionarId(int idEscolhido)
        {
            foreach (TEntidade registro in listaRegistros)
                if (registro.id == idEscolhido)
                    return registro;

            return null;
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
