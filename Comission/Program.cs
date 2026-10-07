using System.Data;
using System.Text.Json;
using Comission;

string path = Path.Combine(AppContext.BaseDirectory, "sales.json");

string json = File.ReadAllText(path);

SalesFile salesFile = JsonSerializer.Deserialize<SalesFile>(
    json,
    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
);

ComissionRule? rule = new();
Dictionary<string, decimal> totals = new(StringComparer.OrdinalIgnoreCase);

Console.WriteLine("=== Calculo de Comissões ===");

foreach (Sale sale in salesFile.Sales)
{
    decimal commission = rule.Calculate(sale.Value);

    if (totals.ContainsKey(sale.Seller))
        totals[sale.Seller] += commission;
    else
        totals[sale.Seller] = commission;
}

Console.WriteLine("== Total de comissão por vendedor ==");
foreach (var item in totals)
{
    Console.WriteLine($"{item.Key}: R$ {item.Value:F2}");
}
