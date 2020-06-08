using System;

namespace BirthdayControl.Business
{
    //Classe pessoas
    public class Pessoa
    {
        public int PessoaID { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime DataNascimento { get; set; }

        public Pessoa(int PessoaID, string Nome, string Sobrenome, DateTime DataNascimento)
        {
            this.PessoaID = PessoaID;
            this.Nome = Nome;
            this.Sobrenome = Sobrenome; 
            this.DataNascimento = DataNascimento;
        }
        public Pessoa()
        {
        }
    }   
}
