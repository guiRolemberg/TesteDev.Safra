using System;

namespace TesteDev.Safra
{
    internal class Program
    {
        private static CreditoImobiliario creditoImobiliario;
        private static bool valido = false;

        private static void Main(string[] args)
        {

            creditoImobiliario = new CreditoImobiliario();

            Console.WriteLine("Teste Desenvolvedor - Banco Safra \n");

            valido = false;
            ValidarInputTipoCredito();

            valido = false;
            ValidarInputValorEmprestimo();

            valido = false;
            ValidarInputQuantidadeParcelas();

            valido = false;
            ValidarInputDataVencimento();

            creditoImobiliario.CalucarValorTotalEmprestimo();

            Console.WriteLine("\nStatus do Crédto: Aprovado");

            Console.WriteLine("Valor Total do Empréstimo com Juros: {0}", creditoImobiliario.ValorTotalEmprestimo.ToString("C", new System.Globalization.CultureInfo("pt-BR")));
            Console.WriteLine("Valor Total do Juros: {0}", creditoImobiliario.ValorTotalJuros.ToString("C", new System.Globalization.CultureInfo("pt-BR")));

            Console.ReadKey();
        }

        private static void ValidarInputTipoCredito()
        {
            while (!valido)
            {
                Console.WriteLine("Favor informar o Tipo do Crédito");
                Console.WriteLine("1 - Crédito Direto, 2 - Crédito Consignado, 3 - Crédito Pessoa Juridica, 4 - Credito Pessoa Fisica, 5 - Crédito Imobiliario");

                if (!int.TryParse(Console.ReadLine(), out int valor))
                {
                    Console.WriteLine("O Tipo do Crédito deve ser numérico");
                }
                else
                {
                    if (valor > 5)
                    {
                        Console.WriteLine("O Tipo de Crédito deve ser de acordo com os tipos informados");
                    }
                    else
                    {
                        creditoImobiliario.TipoCredito = (CreditoImobiliario.EnumTipoCredito)valor;
                        valido = true;
                    }
                }
            }
        }

        private static void ValidarInputValorEmprestimo()
        {
            while (!valido)
            {
                Console.WriteLine("Favor informar o Valor do Crédito");

                if (!double.TryParse(Console.ReadLine(), out double valor))
                {
                    Console.WriteLine("O Valor do Empréstimo deve ser numérico");
                }
                else
                {
                    creditoImobiliario.ValorEmprestimo = valor;

                    if (!creditoImobiliario.ValidarValorMaximoEmprestimo)
                    {
                        Console.WriteLine("Status do Crédito: Recusado \nO Valor Máximo para o Empréstimo é de: R$ 1.000.000,00");
                    }
                    else if (creditoImobiliario.TipoCredito == CreditoImobiliario.EnumTipoCredito.PessoaJuridica && !creditoImobiliario.ValidarValorEmprestimoMinPJ)
                    {
                        Console.WriteLine("Status do Crédito: Recusado \nO Valor Mínimo para o Crédito Pessoa Juridica é de: R$ 15.000,00");
                    }
                    else
                    {
                        valido = true;
                    }
                }
            }
        }

        private static void ValidarInputQuantidadeParcelas()
        {
            while (!valido)
            {
                Console.WriteLine("Favor informar a Quantidade de Parcelas");

                if (!int.TryParse(Console.ReadLine(), out int valor))
                {
                    Console.WriteLine("Status do Crédito: Recusado \nA Quantidade de Parcelas deve ser numérico");
                }
                else
                {
                    creditoImobiliario.QuantidadeParcelas = valor;

                    if (!creditoImobiliario.ValidarQuantidadeMaxMinParcelas)
                    {
                        Console.WriteLine("Status do Crédito: Recusado \nA Quantidade de Parcelas deve ser entre 5 e 72");
                    }
                    else
                    {
                        valido = true;
                    }
                }
            }
        }

        private static void ValidarInputDataVencimento()
        {
            while (!valido)
            {
                Console.WriteLine("Favor informar a Data do Primeiro Vencimento");

                if (!DateTime.TryParse(Console.ReadLine(), out DateTime data))
                {
                    Console.WriteLine("Status do Crédito: Recusado \nA Data do Primeiro Vencimento deve ser no formato: DD/MM/AAAA");
                }
                else
                {
                    creditoImobiliario.DataVencimento = data;

                    if (!creditoImobiliario.ValidarDataVencimentoPrimeiraParcela())
                    {
                        Console.WriteLine("Status do Crédito: Recusado \nA Primeira Data de Vecimento deve ser no mínimo D+15 e no méximo D+ 40");
                    }
                    else
                    {
                        valido = true;
                    }
                }
            }
        }


    }//class
}//namespace
