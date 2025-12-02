using System.Text.Json.Serialization;

namespace DesafioComercial.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum MovimentacaoTipo
    {
        Entrada,
        Saida
    }
}
