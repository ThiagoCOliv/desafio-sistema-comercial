using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using DesafioComercial.Models;
using DesafioComercial.Services;

namespace DesafioComercial
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Desafio - Sistema Comercial (C#)");

            while (true)
            {
                Console.WriteLine("\nEscolha uma opção:");
                Console.WriteLine("1 - Cálculo de comissões");
                Console.WriteLine("2 - Movimentações de estoque");
                Console.WriteLine("3 - Cálculo de juros por atraso");
                Console.WriteLine("0 - Sair");

                var opt = Console.ReadLine();

                if (opt == "1") ComissaoService.Run();
                else if (opt == "2") EstoqueService.Run();
                else if (opt == "3") JurosService.Run();
                else if (opt == "0") break;
                else Console.WriteLine("Opção inválida.");
            }
        }
    }
}