using System;
using System.IO;
using System.Collections.Generic;
using BirthdayControl.Business;
using BirthdayControl.Data;

namespace BirthdayControl.Functions
{
    public class Funcoes
    {
        public static void CabecalhoSistema(string DescricaoLocalParaGravarDados)
        {
            Console.WriteLine("|***************************************|");
            Console.WriteLine("|***** CADASTRO DE ANIVERSARIANTES *****|");
            Console.WriteLine("|***************************************|");
            Console.WriteLine("|Aluno: Rodrigo Carvalho                |");
            Console.WriteLine("|Graduação: GRLADS01C2-M1-L1            |");
            Console.WriteLine("|***************************************|");
            Console.WriteLine("|---------------------------------------|");
            Console.WriteLine("|Tipo de ambiente: " + DescricaoLocalParaGravarDados + "              |");
            Console.WriteLine("|---------------------------------------|");
        }

        public static int CalculdarIdade(DateTime DataNascimento, int TipoCalculo)
        {
            switch (TipoCalculo)
            {
                // idade
                case 1:

                    // Diferença entre data considerando o ano.
                    int IdadePessoa = DateTime.Now.Year - DataNascimento.Year;

                    // Verifica se o dia do ano atual é menor que o dia da data de nascimento.
                    // Se for, é porque ainda não fez aniversário e tenho que tirar 1 ano.
                    if (DateTime.Now.DayOfYear < DataNascimento.DayOfYear)
                    {
                        IdadePessoa -= 1;
                    }

                    return IdadePessoa;

                // dias para aniversario
                case 2:

                    int Mes = DataNascimento.Month;
                    int Dia = DataNascimento.Day;
                    int Ano = DateTime.Today.Year;
                    // Determina a data de aniversario do ano corrente.
                    DateTime AniversarioPessoa = new DateTime(Ano, Mes, Dia);

                    // Verifica se a pessoa já fez aniversário. Caso positivo, incrementa +1 no ano, ou seja, só faz aniversário ano que vem.
                    if (DateTime.Today > AniversarioPessoa)
                    {
                        DateTime ProximoAniversario = new DateTime(Ano + 1, Mes, Dia);
                        int DiasParaAniversario = Convert.ToInt32(ProximoAniversario.Subtract(DateTime.Today).TotalDays);

                        return DiasParaAniversario;
                    }
                    else
                    {
                        DateTime ProximoAniversario = new DateTime(Ano, Mes, Dia);
                        int DiasParaAniversario = Convert.ToInt32(ProximoAniversario.Subtract(DateTime.Today).TotalDays);

                        return DiasParaAniversario;
                    }
                default:
                    return 0;
            }
        }
        public static DateTime DataNascimentoParaDataPresente(DateTime DataNascimento)
        {

            int AnoAtual = DateTime.Today.Year;
            int MesDeAniversario = DataNascimento.Month;
            int DiaDeAniversario = DataNascimento.Day;

            // Determina a data de aniversario do ano corrente.
            DateTime AniversarioPessoaDataPresente = new DateTime(AnoAtual, MesDeAniversario, DiaDeAniversario);

            return AniversarioPessoaDataPresente;

        }
        public static string CriarArquivoSenaoExiste()
        {
            string NomeArquivo = "ListaDeAniversariantes.csv";
            if (!File.Exists(NomeArquivo))
            {
                var Arquivo = File.CreateText(NomeArquivo);
                Arquivo.Close();
                Arquivo.Dispose();
            }

            return NomeArquivo;
        }
        public static void ExcluirArquivoListaDeAniversariantes()
        {
            string NomeArquivo = "ListaDeAniversariantes.csv";
            if (File.Exists(NomeArquivo))
            {
                File.Delete(NomeArquivo);
            }

        }
        public static void DetalhesDaPessoa(List<Pessoa> PessoaEscolhida, int SelecaoPessoaId, int LocalParaGravarDados)
        {
            foreach (Pessoa Item in PessoaEscolhida)
            {
                Console.WriteLine("|---------------------------------------|");
                Console.WriteLine("|Você está em -> Detalhes da pessoa     |");
                Console.WriteLine("|---------------------------------------|");
                Console.WriteLine("|---------------------------------------|");
                Console.WriteLine("|Id: " + Item.PessoaID);
                Console.WriteLine("|Nome: " + Item.Nome + " " + Item.Sobrenome);
                Console.WriteLine("|Data de Nascimento: " + Convert.ToString(Item.DataNascimento));
                Console.WriteLine("|Idade: " + Funcoes.CalculdarIdade(Convert.ToDateTime(Item.DataNascimento), 1) + " ano(s).");
                Console.WriteLine("|Falta(m) " + Funcoes.CalculdarIdade(Convert.ToDateTime(Item.DataNascimento), 2) + " dia(s) para o aniversário.");
                Console.WriteLine("|---------------------------------------|");



                Console.WriteLine("");
                Console.WriteLine("O que você deseja fazer?");
                Console.WriteLine("1-Excluir | 2-Alterar | 0-Voltar");
                PessoaRepository PessoaRepository = new PessoaRepository();
                do
                {
                    try
                    {
                        int OpcoesDetalhesPessoas = int.Parse(Console.ReadLine());
                        PessoaApp Pessoa = new PessoaApp(PessoaRepository);

                        switch (OpcoesDetalhesPessoas)
                        {
                            case 1:                                

                                if (LocalParaGravarDados == 1)
                                {
                                    Pessoa.ExcluirPessoaLst(SelecaoPessoaId);
                                }
                                else
                                {
                                    Pessoa.ExcluirPessoaCsv(SelecaoPessoaId);
                                }

                                Console.WriteLine("-> Pessoa excluída com sucesso.");
                                Console.WriteLine("Pressione qualquer tecla para continuar...");
                                Console.ReadKey();
                                Console.Clear();
                                Program.MenuPrincipal();
                                break;

                            case 2:
                                Console.Clear();
                                CabecalhoSistema(Program.DescricaoLocalParaGravarDados);
                                Console.WriteLine("|---------------------------------------|");
                                Console.WriteLine("|Você está em -> Alterar                |");
                                Console.WriteLine("|---------------------------------------|");
                                Console.WriteLine("Editando: " + Item.PessoaID + ") " + Item.Nome + " " + Item.Sobrenome);
                                Console.WriteLine("|---------------------------------------|");                                
                                Console.WriteLine("Informe o nome:");
                                string NovoNome = Console.ReadLine();
                                Console.WriteLine("Informe o sobrenome:");
                                string NovoSobrenome = Console.ReadLine();


                                if (LocalParaGravarDados == 1)
                                {
                                    Item.Nome = NovoNome;
                                    Item.Sobrenome = NovoSobrenome;
                                    Console.WriteLine("Pessoa alterada com sucesso.");
                                    Console.WriteLine("-> Pressione qualquer tecla para continuar...");
                                    Console.ReadKey();
                                    Console.Clear();
                                    Program.MenuPrincipal();
                                }
                                else
                                {
                                    Pessoa.ExcluirPessoaCsv(Item.PessoaID);
                                    Pessoa.EditarPessoaCsv(Item.PessoaID, NovoNome, NovoSobrenome, Convert.ToString(Item.DataNascimento));
                                    Console.WriteLine("Pessoa alterada com sucesso.");
                                    Console.WriteLine("-> Pressione qualquer tecla para continuar...");
                                    Console.ReadKey();
                                    Console.Clear();
                                    Program.MenuPrincipal();
                                }

                                break;
                            case 0:
                                Console.Clear();
                                Program.MenuPrincipal();
                                break;

                            default:
                                Console.WriteLine("Escolha uma opção válida.");
                                break;
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Escolha uma opção válida.");
                        break;
                    }
                } while (true);

            }

            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
            Program.MenuPrincipal();
        }

    }
}


