using System.Collections.Generic;
using BirthdayControl.Business;

namespace BirthdayControl.Data
{
    //Implementação da interface pessoas
    public class PessoaRepository : IPessoaRepository
    {
        private static readonly List<Pessoa> Pessoas = new List<Pessoa>();
          
        void IPessoaRepository.Salvar(Pessoa pessoa)
        {            
            Pessoas.Add(pessoa);
        }

        public IEnumerable<Pessoa> ObterPessoas()
        {
            return Pessoas;
        }
   
    }
}
