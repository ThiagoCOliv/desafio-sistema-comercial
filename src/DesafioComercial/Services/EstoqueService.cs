using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using DesafioComercial.Models;

namespace DesafioComercial.Services
{
    public static class EstoqueService
    {
        public static void Run()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "data", "estoque.json");

            if (!File.Exists(path)) 
            { 
                Console.WriteLine($"\nArquivo de estoque não encontrado: {path}"); 
                return; 
            }

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var root = JsonSerializer.Deserialize<EstoqueRoot>(File.ReadAllText(path), options);

            if (root == null || root.estoque.Count == 0) 
            { 
                Console.WriteLine("\nNenhum produto no estoque."); 
                return; 
            }
            
            Console.WriteLine("\nProdutos disponíveis:");
            
            foreach (var prod in root.estoque)
            {
                Console.WriteLine($"\nCódigo: {prod.codigoProduto}");
                Console.WriteLine($"Produto: {prod.descricaoProduto}");
                Console.WriteLine($"Quantidade: {prod.estoque}");
            }
            
            Console.WriteLine("\nInforme o código do produto:");
            
            if (!int.TryParse(Console.ReadLine(), out int codigo))
            { 
                Console.WriteLine("\nCódigo inválido");
                return; 
            }

            var produto = root.estoque.FirstOrDefault(prod => prod.codigoProduto == codigo);
            
            if (produto == null) 
            { 
                Console.WriteLine("\nProduto não encontrado"); 
                return;
            }
            
            Console.WriteLine("\nDescrição da movimentação:");
            Console.WriteLine("1 - Entrada");
            Console.WriteLine("2 - Saida");
            var desc = Console.ReadLine() ?? "";
            Console.WriteLine("\nQuantidade:");

            if(desc != "1" && desc != "2") 
            { 
                Console.WriteLine("\nDescrição inválida"); 
                return; 
            }
            
            if (!int.TryParse(Console.ReadLine(), out int qtd) || qtd <= 0)
            { 
                Console.WriteLine("\nQuantidade inválida"); 
                return;
            }
            
            var id = Guid.NewGuid().ToString();
            produto.estoque += desc == "1" ? qtd : qtd * -1;

            try
            {
                string movPath = Path.Combine(Directory.GetCurrentDirectory(), "data", "movimentacoes.json");
                MovimentacoesRoot movRoot = new MovimentacoesRoot();
                if (File.Exists(movPath))
                {
                    var movJson = File.ReadAllText(movPath);
                    var opt = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    opt.Converters.Add(new JsonStringEnumConverter());
                    var existing = JsonSerializer.Deserialize<MovimentacoesRoot>(movJson, opt);
                    if (existing != null) movRoot = existing;
                }

                var movimento = new Movimentacao
                {
                    id = id,
                    codigoProduto = produto.codigoProduto,
                    tipo = (desc == "1" ? MovimentacaoTipo.Entrada : MovimentacaoTipo.Saida),
                    quantidadeAlterada = qtd,
                    timestamp = DateTime.UtcNow.ToString("o"),
                    estoqueFinal = produto.estoque
                };

                movRoot.movimentacoes.Add(movimento);
                var movOptions = new JsonSerializerOptions { WriteIndented = true };
                movOptions.Converters.Add(new JsonStringEnumConverter());
                File.WriteAllText(movPath, JsonSerializer.Serialize(movRoot, movOptions));
                Console.WriteLine("\nMovimentação gravada em data/movimentacoes.json");

                Console.WriteLine($"\nMovimentação registrada: id={id}");
                Console.WriteLine($"Produto: {produto.codigoProduto} - {produto.descricaoProduto}");
                Console.WriteLine($"Tipo de movimentação: {(desc == "1" ? "Entrada" : "Saída")}");
                Console.WriteLine($"Quantidade alterada: {qtd}");
                Console.WriteLine($"Quantidade final em estoque: {produto.estoque}");

                File.WriteAllText(path, JsonSerializer.Serialize(root, new JsonSerializerOptions { WriteIndented = true }));
                Console.WriteLine("\nArquivo de estoque atualizado.");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nFalha ao gravar movimentação: {ex.Message}");
            }
        }
    }
}
