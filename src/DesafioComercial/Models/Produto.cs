namespace DesafioComercial.Models
{
    public class Produto
    {
        public int codigoProduto { get; set; }
        public string descricaoProduto { get; set; } = string.Empty;
        public int estoque { get; set; }
    }
}
