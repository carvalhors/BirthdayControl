using System.Collections.Generic;

namespace BirthdayControl.Business
{
    //Definição da interface pessoa com método salvar e listar
    public interface IPessoaRepository
    {
        public void Salvar(Pessoa pessoa);
        public IEnumerable<Pessoa> ObterPessoas();
    }
}
