using System.Collections.Generic;

namespace DesafioComercial.Models
{
    public class EstoqueRoot
    {
        public List<Produto> estoque { get; set; } = new();
    }
}
