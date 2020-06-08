using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using BirthdayControl.Business;
using BirthdayControl.Functions;


namespace BirthdayControl.Data
{
    //Implementação da interface pessoas
    public class PessoaRepository : IPessoaRepository
    {
        private static readonly List<Pessoa> Pessoas = new List<Pessoa>();
        private readonly List<Pessoa> PessoasCsv = new List<Pessoa>();
        private static List<Pessoa> PessoasRepositorio { get; set; }
        private static int UltimoIdPessoa { get; set; }

        void IPessoaRepository.SalvarPessoaLst(Pessoa pessoa)
        {
            Pessoas.Add(pessoa);
        }

        void IPessoaRepository.SalvarPessoaCsv(string ConcatenaPessoa)
        {

            string NomeArquivo = Funcoes.CriarArquivoSenaoExiste();
            UltimoIdPessoa = 0;

            using StreamReader Ler = new StreamReader(NomeArquivo, Encoding.UTF8, true);
            {
                while (true)
                {
                    string LinhaArquivo = Ler.ReadLine();
                    if (LinhaArquivo == null) break;

                    List<string> ListaPessoas = LinhaArquivo.Split(";").ToList();
                    UltimoIdPessoa = Convert.ToInt32(ListaPessoas.First());
                }
                Ler.Close();
            }

            using StreamWriter Escrever = new StreamWriter(NomeArquivo, true);
            {

                if (UltimoIdPessoa == 0)
                {
                    UltimoIdPessoa = 1;
                }
                else
                {
                    UltimoIdPessoa += 1;
                }

                Escrever.WriteLine(UltimoIdPessoa + ";" + ConcatenaPessoa);
                Escrever.Close();
                Escrever.Dispose();
            }
        }

        public IEnumerable<Pessoa> ObterPessoasLst()
        {
            return Pessoas;
        }

        public IEnumerable<Pessoa> ObterPessoasCsv()
        {
            string NomeArquivo = Funcoes.CriarArquivoSenaoExiste();
            var TodasAsPessoas = File.ReadAllLines(NomeArquivo).ToList();
   
            foreach (string Item in TodasAsPessoas)
            {
                var Valor = Item.Split(";");


                PessoasCsv.Add(new Pessoa()
                {
                    PessoaID = Convert.ToInt32(Valor[0]),
                    Nome = Valor[1],
                    Sobrenome = Valor[2],
                    DataNascimento = Convert.ToDateTime(Valor[3])
                });
            }
            return PessoasCsv;
        }
        public void ExcluirPessoaCsv(int PessoaID)
        {

            PessoaRepository Pessoas = new PessoaRepository();
            PessoasRepositorio = (List<Pessoa>)Pessoas.ObterPessoasCsv();

            PessoasRepositorio.Remove(PessoasRepositorio.First(x => x.PessoaID == PessoaID));

            Funcoes.ExcluirArquivoListaDeAniversariantes();
            string NomeArquivo = Funcoes.CriarArquivoSenaoExiste();

            using StreamWriter Escrever = new StreamWriter(NomeArquivo, true);
            {
                foreach (var Item in PessoasRepositorio)
                {

                    string IdPessoa = Item.PessoaID.ToString();
                    string Nome = Item.Nome;
                    string Sobrenome = Item.Sobrenome;
                    string DataNascimento = Item.DataNascimento.ToString("dd/MM/yyyy");

                    Escrever.WriteLine(IdPessoa + ";" + Nome + ";" + Sobrenome + ";" + DataNascimento);
                }

                Escrever.Close();
                Escrever.Dispose();
            }

        }
        public void ExcluirPessoaLst(int PessoaID)

        {
            PessoaRepository Pessoas = new PessoaRepository();
            PessoasRepositorio = (List<Pessoa>)Pessoas.ObterPessoasLst();

            PessoasRepositorio.Remove(PessoasRepositorio.First(x => x.PessoaID == PessoaID));
        }

        void IPessoaRepository.EditarPessoaCsv(string ConcatenaPessoa)
        {
            string NomeArquivo = Funcoes.CriarArquivoSenaoExiste();

            using StreamWriter Escrever = new StreamWriter(NomeArquivo, true);
            {
                Escrever.WriteLine(ConcatenaPessoa);
                Escrever.Close();
                Escrever.Dispose();
            }
        }

    }
}
