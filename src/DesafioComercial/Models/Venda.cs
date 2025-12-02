using System;
namespace DesafioComercial.Models
{
    public class Venda
    {
        public string vendedor { get; set; } = string.Empty;
        public decimal valor { get; set; }

        public decimal comissao => CalcularComissao();

        public decimal CalcularComissao()
        {
            if (valor < 100m) return 0m;
            if (valor < 500m) return decimal.Round(valor * 0.01m, 2);
            return decimal.Round(valor * 0.05m, 2);
        }
    }
}
