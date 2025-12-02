using System;
using System.Globalization;

namespace DesafioComercial.Services
{
    public static class JurosService
    {
        public static void Run()
        {
            Console.WriteLine("\nInforme o valor:");
            Console.Write("R$ ");
            var raw = Console.ReadLine() ?? string.Empty;
            raw = raw.Trim();
            
            if (raw.Contains("."))
            {
                raw = raw.Replace(",", ".");
            }

            if (!decimal.TryParse(raw, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal valor))
            {
                Console.WriteLine("\nValor inválido");
                return;
            }

            Console.WriteLine("\nInforme a data de vencimento (DD/MM/YYYY):");
            var dateRaw = Console.ReadLine() ?? string.Empty;
            dateRaw = dateRaw.Trim();
            
            if (!DateTime.TryParseExact(dateRaw, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime venc))
            {
                Console.WriteLine("\nData inválida. Use o formato DD/MM/YYYY");
                return;
            }

            var dias = (DateTime.Today - venc).Days;
            
            if (dias <= 0) 
            { 
                Console.WriteLine("\nNenhum juros. Valor a pagar: R$ {0:F2}", valor); 
                return; 
            }
            
            decimal taxaDia = 0.025m;
            decimal juros = valor * taxaDia * dias;
            decimal total = valor + juros;

            Console.WriteLine($"\nDias de atraso: {dias}");
            Console.WriteLine($"Juros acumulados: R$ {juros:F2}");
            Console.WriteLine($"Total a pagar: R$ {total:F2}");
        }
    }
}
