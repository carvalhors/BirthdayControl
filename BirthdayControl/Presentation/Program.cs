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
        //Variavel para definir aonde salvar arquivo
        private static int LocalParaGravarDados;
        public static string DescricaoLocalParaGravarDados { get; set; }
        //Definição do repositório
        public static IEnumerable<Pessoa> PessoasRepositorio { get; set; }

        //Método estático principal que chama um método privado da classe Program onde apresenta o Menu com opções
        private static void Main()
        {
            MenuPrincipal();
        }

        //Método privado da classe Program. Menu principal. Coleta-se uma opção e, através de um switch, direciona para o método/opção escolhida
        public static void MenuPrincipal()
        {


            if (LocalParaGravarDados == 1)
            {
                DescricaoLocalParaGravarDados = "Memória";
            }
            else
            {
                DescricaoLocalParaGravarDados = "Arquivo";
            }

            Funcoes.CabecalhoSistema(DescricaoLocalParaGravarDados);

            Console.WriteLine("|---------------------------------------|");
            Console.WriteLine("|Escolha uma opção abaixo:              |");
            Console.WriteLine("|                                       |");
            Console.WriteLine("|1 - Cadastrar                          |");
            Console.WriteLine("|2 - Listar                             |");
            Console.WriteLine("|3 - Procurar                           |");
            Console.WriteLine("|4 - Configurações                      |");
            Console.WriteLine("|5 - Aniversariantes do Dia             |");
            Console.WriteLine("|0 - Sair do Programa                   |");
            Console.WriteLine("|---------------------------------------|");

            do
            {
                try
                {
                    int MenuEscolha = int.Parse(Console.ReadLine());

                    switch (MenuEscolha)
                    {
                        case 1:
                            Console.Clear();
                            Funcoes.CabecalhoSistema(DescricaoLocalParaGravarDados);
                            CadastrarPessoas();
                            break;
                        case 2:
                            Console.Clear();
                            Funcoes.CabecalhoSistema(DescricaoLocalParaGravarDados);
                            ListarPessoas();
                            break;

                        case 3:
                            Console.Clear();
                            Funcoes.CabecalhoSistema(DescricaoLocalParaGravarDados);
                            BuscarPessoas();
                            break;
                        case 4:
                            Console.Clear();
                            Funcoes.CabecalhoSistema(DescricaoLocalParaGravarDados);
                            Configuracoes();
                            break;
                        case 5:
                            Console.Clear();
                            Funcoes.CabecalhoSistema(DescricaoLocalParaGravarDados);
                            AniversariantesDoDia();
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
        private static void Configuracoes()
        {
            Console.WriteLine("|---------------------------------------|");
            Console.WriteLine("|Você está em -> Configurações          |");
            Console.WriteLine("|---------------------------------------|");
            Console.WriteLine("|Escolha um tipo de ambiente para dados:|");
            Console.WriteLine("|---------------------------------------|");
            Console.WriteLine("|                                       |");
            Console.WriteLine("|1) Gravar em Memória                   |");
            Console.WriteLine("|2) Gravar em Disco                     |");
            Console.WriteLine("|---------------------------------------|");

            bool ValidaLocalGravarDados;

            do
            {
                LocalParaGravarDados = Convert.ToInt32(int.Parse(Console.ReadLine()));

                if (LocalParaGravarDados == 1 || LocalParaGravarDados == 2)
                {
                    ValidaLocalGravarDados = true;
                    Console.WriteLine("Tipo de ambiente definido com sucesso.");
                    Console.WriteLine("-> Pressione qualquer tecla para continuar.");
                    Console.ReadKey();
                    Console.Clear();
                    MenuPrincipal();
                }
                else
                {
                    Console.WriteLine("Opção inválida. Por favor, escolha uma das opções acima.");
                    ValidaLocalGravarDados = false;
                }
            } while (ValidaLocalGravarDados == false);
        }
        //Método privado da classe Program. Efetua o cadastro das pessoas utilizando o conceito de repositório.
        private static void CadastrarPessoas()
        {
            Console.WriteLine("|---------------------------------------|");
            Console.WriteLine("|Você está em -> Cadastrar              |");
            Console.WriteLine("|---------------------------------------|");

            string Nome, Sobrenome, DataNascimento;
            Console.WriteLine("-> Informe o nome:");
            Nome = Console.ReadLine();
            Console.WriteLine("-> Informe o sobrenome:");
            Sobrenome = Console.ReadLine();

            bool ValidaDataNascimento;
            do
            {
                Console.WriteLine("-> Informe a data de nascimento (dd/mm/yyyy):");
                DataNascimento = Console.ReadLine();

                try
                {
                    DateTime DataNascimentoConvertida = Convert.ToDateTime(DataNascimento);
                    ValidaDataNascimento = true;
                }
                catch (Exception)
                {
                    Console.WriteLine("---------------------------------------");
                    Console.WriteLine("************ Data inválida ************");
                    Console.WriteLine("---------------------------------------");
                    ValidaDataNascimento = false;
                }

            } while (ValidaDataNascimento == false);

            Console.Clear();
            Console.WriteLine("|--------------------------------------|");
            Console.WriteLine("|Os dados abaixo estão corretos?       |");
            Console.WriteLine("|--------------------------------------|");
            Console.WriteLine("-> Nome: " + Nome + " " + Sobrenome);
            Console.WriteLine("-> Data de Nascimento: " + Convert.ToString(DataNascimento));
            Console.WriteLine("-> Idade: " + Funcoes.CalculdarIdade(Convert.ToDateTime(DataNascimento), 1));
            Console.WriteLine("|--------------------------------------|");
            Console.WriteLine("|1) Sim                                |");
            Console.WriteLine("|2) Não                                |");
            Console.WriteLine("|--------------------------------------|");

            int ConfirmaCadastro = int.Parse(Console.ReadLine());

            switch (ConfirmaCadastro)
            {
                case 1:
                    if (LocalParaGravarDados == 1)
                    {
                        //grava no list
                        PessoaRepository PessoaRepository = new PessoaRepository();
                        PessoaApp Pessoa = new PessoaApp(PessoaRepository);
                        Pessoa.SalvarPessoaLst(ContadorIdPessoa, Nome, Sobrenome, Convert.ToDateTime(DataNascimento));
                        ContadorIdPessoa++;

                        Console.WriteLine("Cadastro realizado com sucesso!");
                        Console.WriteLine("-> Pressione qualquer tecla para continuar.");
                        Console.ReadKey();
                        Console.Clear();
                        MenuPrincipal();
                        break;
                    }
                    else
                    {
                        // grava no csv
                        PessoaRepository PessoaRepository = new PessoaRepository();
                        PessoaApp Pessoa = new PessoaApp(PessoaRepository);
                        Pessoa.SalvarPessoaCsv(Nome, Sobrenome, DataNascimento);

                        Console.WriteLine("Cadastro realizado com sucesso!");
                        Console.WriteLine("-> Pressione qualquer tecla para continuar.");
                        Console.ReadKey();
                        Console.Clear();
                        MenuPrincipal();
                        break;
                    }

                case 2:
                    Console.WriteLine("Você desistiu da operação.");
                    Console.WriteLine("-> Pressione qualquer tecla para continuar.");
                    Console.ReadKey();
                    Console.Clear();
                    MenuPrincipal();
                    break;

                default:
                    Console.WriteLine("Opção inválida. Cadastro não realizado.");
                    Console.WriteLine("-> Pressione qualquer tecla para continuar.");
                    Console.ReadKey();
                    Console.Clear();
                    MenuPrincipal();
                    break;
            }
        }
        //Método privado da classe Program. Lista todas as pessoas que foram cadastradas e colocadas em um List
        private static void ListarPessoas()
        {
            Console.WriteLine("|---------------------------------------|");
            Console.WriteLine("|Você está em -> Listar                 |");
            Console.WriteLine("|---------------------------------------|");

            PessoaRepository Pessoas = new PessoaRepository();

            if (LocalParaGravarDados == 1)
            {
                PessoasRepositorio = Pessoas.ObterPessoasLst();
            }
            else
            {
                PessoasRepositorio = Pessoas.ObterPessoasCsv();
            }

            if (PessoasRepositorio.Any())
            {
                foreach (var Item in PessoasRepositorio.OrderBy(x=>x.PessoaID))
                {
                    Console.WriteLine("|[" + Item.PessoaID + "]-> " + Item.Nome + " " + Item.Sobrenome);
                }

                Console.WriteLine("|---------------------------------------|");
                Console.WriteLine("|Informe o ID para obter mais detalhes: |");
                Console.WriteLine("|---------------------------------------|");

                int SelecaoPessoaId = int.Parse(Console.ReadLine());
                List<Pessoa> PessoaEscolhida = PessoasRepositorio.Where(x => x.PessoaID.Equals(SelecaoPessoaId)).ToList();

                if (PessoaEscolhida.Any())
                {
                    Console.Clear();
                    Funcoes.CabecalhoSistema(DescricaoLocalParaGravarDados);
                    Funcoes.DetalhesDaPessoa(PessoaEscolhida, SelecaoPessoaId, LocalParaGravarDados);
                }
                else
                {
                    Console.WriteLine("ID inválido. ");
                    Console.WriteLine("-> Pressione qualquer tecla para continuar.");
                    Console.ReadKey();
                    Console.Clear();
                    MenuPrincipal();
                }
            }
            else
            {
                Console.WriteLine("Nada para listar. ");
                Console.WriteLine("-> Pressione qualquer tecla para continuar.");
                Console.ReadKey();
                Console.Clear();
                MenuPrincipal();

            }
        }
        // //Método privado da classe Program. Faz a busca de pessoas utilizando expressões lambda.
        private static void BuscarPessoas()
        {
            PessoaRepository ObterPessoas = new PessoaRepository();

            if (LocalParaGravarDados == 1)
            {
                IEnumerable<Pessoa> PessoasRepositorio = ObterPessoas.ObterPessoasLst();
            }
            else
            {
                IEnumerable<Pessoa> PessoasRepositorio = ObterPessoas.ObterPessoasCsv();
            }


            Console.WriteLine("|---------------------------------------|");
            Console.WriteLine("|Informe o nome para pesquisar:         |");
            Console.WriteLine("|---------------------------------------|");
            string NomeBusca = Console.ReadLine();

            var BuscaNome = PessoasRepositorio.Where(x => (x.Nome.ToLower()).Contains(NomeBusca.ToLower())).ToList();
            int QtdeBusca = BuscaNome.Count();


            if (QtdeBusca > 0)
            {
                Console.Clear();
                Funcoes.CabecalhoSistema(DescricaoLocalParaGravarDados);
                Console.WriteLine("|---------------------------------------|");
                Console.WriteLine("|Registro(s) encontrado(s): " + QtdeBusca + "           |");
                Console.WriteLine("|---------------------------------------|");

                foreach (var Item in BuscaNome)
                {
                    Console.WriteLine("|[" + Item.PessoaID + "]-> " + Item.Nome + " " + Item.Sobrenome);
                }

                Console.WriteLine("|---------------------------------------|");
                Console.WriteLine("|Informe o ID para obter mais detalhes: |");
                Console.WriteLine("|---------------------------------------|");

                int IdEscolhaPessoa = int.Parse(Console.ReadLine());
                var DetalhesPessoa = BuscaNome.Where(x => x.PessoaID.Equals(IdEscolhaPessoa)).ToList();

                if (DetalhesPessoa.Any())
                {
                    Console.Clear();
                    Funcoes.CabecalhoSistema(DescricaoLocalParaGravarDados);
                    Funcoes.DetalhesDaPessoa(DetalhesPessoa, IdEscolhaPessoa, LocalParaGravarDados);
                }
                else
                {
                    Console.WriteLine("ID inválido.");
                    Console.WriteLine("-> Pressione qualquer tecla para continuar.");
                    Console.ReadKey();
                    Console.Clear();
                    MenuPrincipal();
                }
            }
            else
            {
                Console.WriteLine("Não há registros para o nome informado.");
                Console.WriteLine("-> Pressione qualquer tecla para continuar.");
                Console.ReadKey();
                Console.Clear();
                MenuPrincipal();
            }
        }
        private static void AniversariantesDoDia()
        {
            PessoaRepository Pessoas = new PessoaRepository();

            Console.WriteLine("|---------------------------------------|");
            Console.WriteLine("|Você está em -> Aniversariantes do Dia |");
            Console.WriteLine("|---------------------------------------|");

            if (LocalParaGravarDados == 1)
            {
                PessoasRepositorio = Pessoas.ObterPessoasLst();
            }
            else
            {
                PessoasRepositorio = Pessoas.ObterPessoasCsv();
            }

            var PessoasFazendoAniversarioHoje = PessoasRepositorio.Where(x => Funcoes.DataNascimentoParaDataPresente(x.DataNascimento) == DateTime.Today).ToList();
            int QtdePessoasFazendoAniversarioHoje = PessoasFazendoAniversarioHoje.Count();

            Console.WriteLine("|Pessoas fazendo aniversário hoje: " + QtdePessoasFazendoAniversarioHoje + "    |");

            foreach(var Item in PessoasFazendoAniversarioHoje)
            {
                Console.WriteLine("");
                Console.WriteLine("|---------------------------------------|");
                Console.WriteLine("|-> Nome: " + Item.Nome);
                Console.WriteLine("|--> Idade:" + Funcoes.CalculdarIdade(Item.DataNascimento, 1) + " ano(s)");
                Console.WriteLine("|---------------------------------------|");
            }

            Console.WriteLine("|---------------------------------------|");
            Console.WriteLine("-> Pressione qualquer tecla para continuar.");
            Console.ReadKey();
            Console.Clear();
            MenuPrincipal();


        }

    }
}
