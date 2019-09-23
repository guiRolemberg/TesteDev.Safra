using System;

namespace TesteDev.Safra
{
    public class CreditoImobiliario
    {
        public enum EnumTipoCredito
        {
            Direto = 1,
            Consignado = 2,
            PessoaJuridica = 3,
            PessoaFisica = 4,
            Imobiliario = 5
        }


        public double ValorEmprestimo { get; set; }
        public int QuantidadeParcelas { get; set; }
        public DateTime DataVencimento { get; set; }


        public double ValorTotalEmprestimo { get; set; }
        public bool Status { get; set; }
        public EnumTipoCredito TipoCredito { get; set; }

        public double TaxaCredito
        {
            get
            {
                double tx = 0;
                switch (TipoCredito)
                {
                    case EnumTipoCredito.Direto: tx = 2; break;
                    case EnumTipoCredito.Consignado: tx = 1; break;
                    case EnumTipoCredito.PessoaJuridica: tx = 5; break;
                    case EnumTipoCredito.PessoaFisica: tx = 3; break;
                    case EnumTipoCredito.Imobiliario: tx = (double)9 / (double)12; break;//divisão para "transformar" a a.a em a.m
                }

                return tx;
            }
        }
        //Validação para o Valor Máximo do Crédito
        public bool ValidarValorMaximoEmprestimo => ValorEmprestimo <= 1000000;
        //Validação para a Quantidade de Parcelas
        public bool ValidarQuantidadeMaxMinParcelas => (QuantidadeParcelas <= 72 && QuantidadeParcelas >= 5);
        //Validação para o Valor Mínimo do Crédito quando Pessoa Juridica
        public bool ValidarValorEmprestimoMinPJ => ValorEmprestimo >= 15000;

        public double ValorTotalJuros => ValorTotalEmprestimo - ValorEmprestimo;

        public bool ValidarDataVencimentoPrimeiraParcela()
        {
            TimeSpan diferenca = DataVencimento.Date - DateTime.Now.Date;

            return diferenca.Days >= 15 && diferenca.Days <= 40;
        }

        public void CalucarValorTotalEmprestimo()
        {
            double valorJuros = (TaxaCredito / 100) * ValorEmprestimo;
            double valorMensal = (ValorEmprestimo / QuantidadeParcelas) + valorJuros;

            for (int i = 0; i < QuantidadeParcelas; i++)
            {
                ValorTotalEmprestimo += valorMensal;
            }
        }


    }//class
}//nanmespace
