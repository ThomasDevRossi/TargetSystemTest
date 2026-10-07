using System.Text.Json.Serialization;

namespace StockControl;

public class StockFile
{
    [JsonPropertyName("estoque")]
    public List<Product> Products { get; set; } = new();
}

public class Product
{
    [JsonPropertyName("codigoProduto")]
    public int Code { get; set; }

    [JsonPropertyName("descricaoProduto")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("estoque")]
    public int Quantity { get; set; }
}

public class Movement
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int ProductCode { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public int FinalStock { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
}