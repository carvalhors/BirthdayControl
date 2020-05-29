using System;

namespace BirthdayControl.Functions
{
    public class Funcoes
    {
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
                        IdadePessoa = IdadePessoa - 1;
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
    }
}
