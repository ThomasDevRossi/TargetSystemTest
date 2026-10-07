namespace TargetSystem.Domain
{
    public class SalesFile
    {
        public List<Sale> Vendas { get; set; } = new();
    }

    public class Sale
    {
        public string Vendedor { get; set; } = string.Empty;
        public decimal Valor { get; set; }
    }

    public class CommissionResult
    {
        public string Vendedor { get; set; } = string.Empty;
        public int QuantidadeVendas { get; set; }
        public int VendasComComissao { get; set; }
        public decimal TotalVendido { get; set; }
        public decimal Comissao { get; set; }
    }
}
