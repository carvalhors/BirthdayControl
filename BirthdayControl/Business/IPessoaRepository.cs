using System.Collections.Generic;

namespace BirthdayControl.Business
{
    //Definição da interface pessoa com método salvar e listar
    public interface IPessoaRepository
    {
        public void SalvarPessoaLst(Pessoa pessoa);
        public void SalvarPessoaCsv(string pessoa);
        public IEnumerable<Pessoa> ObterPessoasLst();
        public IEnumerable<Pessoa> ObterPessoasCsv();
        public void ExcluirPessoaLst(int PessoaID);
        public void ExcluirPessoaCsv(int PessoaID);
        public void EditarPessoaCsv(string pessoa);

    }
}
