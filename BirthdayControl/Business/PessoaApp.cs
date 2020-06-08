using System;

namespace BirthdayControl.Business
{
    public class PessoaApp
    {
        //Define o repositótio como privado e somente leitura
        private readonly IPessoaRepository RepositorioPessoa;

        //Instancia-se o repositótio
        public PessoaApp(IPessoaRepository RepositorioPessoa)
        {
            this.RepositorioPessoa = RepositorioPessoa;
        }

        //Método para salvar uma pessoa na memoria
        public void SalvarPessoaLst(int PessoaID, string Nome, string Sobrenome, DateTime DataNascimento)
        {
            Pessoa pessoa = new Pessoa(PessoaID ,Nome, Sobrenome, DataNascimento);
            RepositorioPessoa.SalvarPessoaLst(pessoa);
        }
        //Método para salvar uma pessoa no disco
        public void SalvarPessoaCsv(string Nome, string Sobrenome, string DataNascimento)
        {
            string ConcatenaPessoa = Nome + ";" + Sobrenome + ";" + DataNascimento;
            RepositorioPessoa.SalvarPessoaCsv(ConcatenaPessoa);
        }
        public void ExcluirPessoaCsv(int PessoaID)
        {
            RepositorioPessoa.ExcluirPessoaCsv(PessoaID);
        }
        public void ExcluirPessoaLst(int PessoaID)
        {
            RepositorioPessoa.ExcluirPessoaLst(PessoaID);
        }
        public void EditarPessoaCsv(int PessoaId, string Nome, string Sobrenome, string DataNascimento)
        {
            string ConcatenaPessoa = PessoaId + ";" + Nome + ";" + Sobrenome + ";" + DataNascimento;
            RepositorioPessoa.EditarPessoaCsv(ConcatenaPessoa);
        }

    }
}
