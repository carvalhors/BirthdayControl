using System;
using System.Collections.Generic;
using System.Linq;
using BirthdayControl.Business;
using BirthdayControl.Data;
using BirthdayControl.Functions;

namespace BirthdayControl
{
    class Program
    {
        //Gerar sequencial para atribuir um ID único em cada cadastro das pessoas
        private static int ContadorIdPessoa = 1;

        //Método estático principal que chama um método privado da classe Program onde apresenta o Menu com opções
        static void Main(string[] args)
        {
            MenuPrincipal();
        }

        //Método privado da classe Program. Menu principal. Coleta-se uma opção e, através de um switch, direciona para o método/opção escolhida
        private static void MenuPrincipal()
        {
            Console.WriteLine("|---------------------------------------|");
            Console.WriteLine("|***** CADASTRO DE ANIVERSARIANTES *****|");
            Console.WriteLine("|---------------------------------------|");
            Console.WriteLine("|Aluno: Rodrigo Carvalho                |");
            Console.WriteLine("|Graduação: GRLADS01C2-M1-L1            |");
            Console.WriteLine("|***************************************|");
            Console.WriteLine("|---------------------------------------|");
            Console.WriteLine("|Escolha uma opção abaixo:              |");
            Console.WriteLine("|                                       |");
            Console.WriteLine("|1 - Cadastrar                          |");
            Console.WriteLine("|2 - Listar                             |");
            Console.WriteLine("|3 - Procurar                           |");
            Console.WriteLine("|0 - Sair                               |");
            Console.WriteLine("|---------------------------------------|");

            do
            {
                try
                {
                    int MenuEscolha = int.Parse(Console.ReadLine());

                    switch (MenuEscolha)
                    {
                        case 1:
                            CadastroPessoa();
                            break;
                        case 2:
                            ListarPessoas();
                            break;

                        case 3:
                            BuscarPessoas();
                            break;
                        case 0:
                            Environment.Exit(0);
                            break;

                        default:
                            Console.WriteLine("Escolha uma opção válida.");
                            break;
                    }
                }
                catch
                {
                    Console.WriteLine("Escolha uma opção válida.");
                }
            } while (true);
        }

        //Método privado da classe Program. Efetua o cadastro das pessoas utilizando o conceito de repositório.
        private static void CadastroPessoa()
        {
            string Nome, Sobrenome, DataNascimento;
            Console.WriteLine("Informe o nome:");
            Nome = Console.ReadLine();
            Console.WriteLine("Informe o sobrenome:");
            Sobrenome = Console.ReadLine();

            bool ValidaDataNascimento;
            do
            {
                Console.WriteLine("Informe a data de nascimento (dd/mm/yyyy):");
                DataNascimento = Console.ReadLine();

                try
                {
                    DateTime DataNascimentoConvertida = Convert.ToDateTime(DataNascimento);
                    TimeSpan Ts = DateTime.Today - DataNascimentoConvertida;
                    DateTime IdadePessoa = (new DateTime() + Ts).AddYears(-1).AddDays(-1);
                    ValidaDataNascimento = true;
                }
                catch (Exception)
                {
                    Console.WriteLine("---------------------------------------------");
                    Console.WriteLine("Atenção: Data inválida. Por favor, verifique.");
                    Console.WriteLine("---------------------------------------------");
                    ValidaDataNascimento = false;
                }

            } while (ValidaDataNascimento == false);

            Console.Clear();
            Console.WriteLine("|-------------------------------------|");
            Console.WriteLine("Os dados abaixo estão corretos?");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Nome: " + Nome + " " + Sobrenome);
            Console.WriteLine("Data de Nascimento: " + Convert.ToString(DataNascimento));
            Console.WriteLine("Idade: " + Funcoes.CalculdarIdade(Convert.ToDateTime(DataNascimento), 1));
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("1) Sim");
            Console.WriteLine("2) Não");
            Console.WriteLine("---------------------------------------");

            int ConfirmaCadastro = int.Parse(Console.ReadLine());

            if (ConfirmaCadastro == 1)
            {
                //após validar, insiro os dados
                PessoaRepository PessoaRepository = new PessoaRepository();
                PessoaApp Pessoa = new PessoaApp(PessoaRepository);
                Pessoa.SalvarPessoa(ContadorIdPessoa, Nome, Sobrenome, Convert.ToDateTime(DataNascimento));
                ContadorIdPessoa++;

                Console.WriteLine("Cadastro realizado com sucesso!");
                Console.WriteLine("-> Pressione qualquer tecla para continuar");
                Console.ReadKey();
                Console.Clear();
                MenuPrincipal();

            }
            else if (ConfirmaCadastro == 2)
            {
                Console.WriteLine("Cadastro não realizado.");
                Console.WriteLine("-> Pressione qualquer tecla para continuar");
                Console.ReadKey();
                Console.Clear();
                MenuPrincipal();
            }
            else
            {
                Console.WriteLine("Opção inválida. Cadastro não realizado.");
                Console.WriteLine("-> Pressione qualquer tecla para continuar");
                Console.ReadKey();
                Console.Clear();
                MenuPrincipal();
            }

        }

        //Método privado da classe Program. Lista todas as pessoas que foram cadastradas e colocadas em um List
        private static void ListarPessoas()
        {
            PessoaRepository ObterPessoas = new PessoaRepository();
            IEnumerable<Pessoa> PessoasRepositorio = ObterPessoas.ObterPessoas();


            foreach (var Item in PessoasRepositorio)
            {
                Console.WriteLine(Item.PessoaID + ") " + Item.Nome + " " + Item.Sobrenome);
            }

            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Informe o ID da pessoa para obter mais detalhes:");
            Console.WriteLine("---------------------------------------");

            int SelecaoPessoaId = int.Parse(Console.ReadLine());

            List<Pessoa> PessoaEscolhida = PessoasRepositorio.Where(x => x.PessoaID.Equals(SelecaoPessoaId)).ToList();

            foreach (Pessoa Item in PessoaEscolhida)
            {
                Console.Clear();
                Console.WriteLine("---------------------------------------");
                Console.WriteLine("Nome: " + Item.Nome + " " + Item.Sobrenome);
                Console.WriteLine("Data de Nascimento: " + Convert.ToString(Item.DataNascimento));
                Console.WriteLine("Idade: " + Funcoes.CalculdarIdade(Convert.ToDateTime(Item.DataNascimento), 1) + " ano(s).");
                Console.WriteLine("Falta(m) " + Funcoes.CalculdarIdade(Convert.ToDateTime(Item.DataNascimento), 2) + " dia(s) para o aniversário.");
                Console.WriteLine("---------------------------------------");
            }

            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
            MenuPrincipal();
        }

        // //Método privado da classe Program. Faz a busca de pessoas utilizando expressões lambda.
        private static void BuscarPessoas()
        {
            PessoaRepository ObterPessoas = new PessoaRepository();
            IEnumerable<Pessoa> PessoasRepositorio = ObterPessoas.ObterPessoas();

            Console.WriteLine("Digite o nome ou parte do nome para encontrar uma pessoa:");
            string NomeBusca = Console.ReadLine();

            var BuscaNome = PessoasRepositorio.Where(x => x.Nome.Contains(NomeBusca.ToUpper())).ToList();
            int QtdeBusca = BuscaNome.Count();


            if (QtdeBusca > 0)
            {
                Console.WriteLine("***** Registro(s) encontrado(s): " + QtdeBusca + " *****");
                Console.WriteLine("---------------------------------------");


                foreach (var Item in BuscaNome)
                {
                    Console.WriteLine(Item.PessoaID + ") " + Item.Nome + " " + Item.Sobrenome);
                }


                Console.WriteLine("---------------------------------------");
                Console.WriteLine("Informe o ID da pessoa para obter mais detalhes:");
                Console.WriteLine("---------------------------------------");

                int IdEscolhaPessoa = int.Parse(Console.ReadLine());
                var DetalhesPessoa = PessoasRepositorio.Where(x => x.PessoaID.Equals(IdEscolhaPessoa)).ToList();

                foreach (var Item in DetalhesPessoa)
                {
                    Console.Clear();
                    Console.WriteLine("---------------------------------------");
                    Console.WriteLine("Nome: " + Item.Nome + " " + Item.Sobrenome);
                    Console.WriteLine("Data de Nascimento: " + Convert.ToString(Item.DataNascimento));
                    Console.WriteLine("Idade: " + Funcoes.CalculdarIdade(Convert.ToDateTime(Item.DataNascimento), 1) + " ano(s).");
                    Console.WriteLine("Falta(m) " + Funcoes.CalculdarIdade(Convert.ToDateTime(Item.DataNascimento), 2) + " dia(s) para o aniversário.");
                    Console.WriteLine("---------------------------------------");
                }

                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
                Console.Clear();
                MenuPrincipal();

            }
            else
            {
                Console.WriteLine("Não há registros para o nome informado.");
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
                Console.Clear();
                MenuPrincipal();
            }

        }


    }
}
