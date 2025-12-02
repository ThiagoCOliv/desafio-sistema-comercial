using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using DesafioComercial.Models;

namespace DesafioComercial.Services
{
    public static class ComissaoService
    {
        public static void Run()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "data", "vendas.json");

            if (!File.Exists(path))
            {
                Console.WriteLine($"Arquivo de vendas não encontrado: {path}");
                return;
            }

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var root = JsonSerializer.Deserialize<VendasRoot>(File.ReadAllText(path), options);

            if (root == null)
            {
                Console.WriteLine("Nenhuma venda encontrada.");
                return;
            }

            var resultado = new Dictionary<string, decimal>();

            foreach (var venda in root.vendas)
            {
                var com = venda.comissao;
                if (!resultado.ContainsKey(venda.vendedor)) resultado[venda.vendedor] = 0;
                resultado[venda.vendedor] += com;
            }

            Console.WriteLine("Comissões por vendedor:");
            foreach (var kv in resultado)
            {
                Console.WriteLine($"{kv.Key}: R$ {kv.Value:F2}");
            }
        }
    }
}
