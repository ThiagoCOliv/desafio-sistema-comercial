using System;

namespace DesafioComercial.Models
{
    public class Movimentacao
    {
        public string id { get; set; } = string.Empty;
        public int codigoProduto { get; set; }
        public MovimentacaoTipo tipo { get; set; } = MovimentacaoTipo.Entrada;
        public int quantidadeAlterada { get; set; }
        public string timestamp { get; set; } = string.Empty;
        public int estoqueFinal { get; set; }
    }
}
