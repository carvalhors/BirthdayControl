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

        //Método para salvar uma pessoa
        public void SalvarPessoa(int PessoaID, string Nome, string Sobrenome, DateTime DataNascimento)
        {
            Pessoa pessoa = new Pessoa(PessoaID ,Nome, Sobrenome, DataNascimento);
            RepositorioPessoa.Salvar(pessoa);
        }
      
    }
}
