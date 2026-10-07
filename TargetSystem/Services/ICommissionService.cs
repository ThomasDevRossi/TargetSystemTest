using TargetSystem.Domain;

namespace TargetSystem.Services
{
    public interface ICommissionService
    {
        List<CommissionResult> Calculate(IEnumerable<Sale> sales);
    }

    public class CommissionService : ICommissionService
    {
        private const decimal MinimoParaComissao = 100m;
        private const decimal LimiteFaixaAlta = 500m;
        private const decimal TaxaBaixa = 0.01m;
        private const decimal TaxaAlta = 0.05m;

        public static decimal CalculateSaleCommission(decimal valor) =>
            valor switch
            {
                < MinimoParaComissao => 0m,
                < LimiteFaixaAlta => valor * TaxaBaixa,
                _ => valor * TaxaAlta,
            };

        public List<CommissionResult> Calculate(IEnumerable<Sale> sales) =>
            sales
                .GroupBy(s => s.Vendedor, StringComparer.OrdinalIgnoreCase)
                .Select(g => new CommissionResult
                {
                    Vendedor = g.Key,
                    QuantidadeVendas = g.Count(),
                    VendasComComissao = g.Count(s => s.Valor >= MinimoParaComissao),
                    TotalVendido = g.Sum(s => s.Valor),
                    Comissao = Math.Round(g.Sum(s => CalculateSaleCommission(s.Valor)), 2),
                })
                .OrderByDescending(r => r.Comissao)
                .ToList();
    }
}
