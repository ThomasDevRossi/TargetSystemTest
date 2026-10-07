using System.Text.Json;
using StockControl;

string path = Path.Combine(AppContext.BaseDirectory, "stock.json");

string json = File.ReadAllText(path);


StockFile stockFile = JsonSerializer.Deserialize<StockFile>(
    json,
    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
)!;

var movements = new List<Movement>();
bool running = true;

while (running)
{
    Console.WriteLine("=== Controle de Estoque ===");
    Console.WriteLine("1 - Entrada de mercadoria");
    Console.WriteLine("2 - Saída de mercadoria");
    Console.WriteLine("3 - Ver estoque atual");
    Console.WriteLine("4 - Ver histórico de movimentações");
    Console.WriteLine("0 - Sair");
    Console.Write("Opção: ");
    string? option = Console.ReadLine();
    Console.WriteLine();

    switch (option)
    {
        case "0":
            running = false;
            break;

        case "1":
        case "2":
        {
            bool isEntry = option == "1";

            Console.Write("Código do produto: ");
            if (!int.TryParse(Console.ReadLine(), out int code))
            {
                Console.WriteLine("Código inválido.\n");
                continue;
            }

            Product? product = stockFile.Products.FirstOrDefault(p => p.Code == code);
            if (product == null)
            {
                Console.WriteLine("Produto não encontrado.\n");
                continue;
            }

            Console.Write("Quantidade: ");
            if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0)
            {
                Console.WriteLine("Quantidade inválida.\n");
                continue;
            }

            if (!isEntry && quantity > product.Quantity)
            {
                Console.WriteLine($"Estoque insuficiente. Disponível: {product.Quantity}\n");
                continue;
            }

            Console.Write("Descrição da movimentação (ex: Compra, Venda, Devolução, Perda): ");
            string description = Console.ReadLine() ?? string.Empty;

            product.Quantity += isEntry ? quantity : -quantity;

            var movement = new Movement
            {
                ProductCode = product.Code,
                Type = isEntry ? "Entrada" : "Saída",
                Description = description,
                Quantity = quantity,
                FinalStock = product.Quantity,
            };
            movements.Add(movement);

            Console.WriteLine();
            Console.WriteLine(
                $"Movimentação {movement.Id} registrada ({movement.Type}: {description})."
            );
            Console.WriteLine($"Estoque final de {product.Description}: {product.Quantity} un.");
            Console.WriteLine();
            break;
        }

        case "3":
            foreach (Product p in stockFile.Products)
                Console.WriteLine($"{p.Code} - {p.Description}: {p.Quantity} un.");
            Console.WriteLine();
            break;

        case "4":
            if (movements.Count == 0)
                Console.WriteLine("Nenhuma movimentação lançada.");

            foreach (Movement m in movements)
                Console.WriteLine(
                    $"{m.Id} | {m.Date:dd/MM/yyyy HH:mm} | {m.Type} | Produto {m.ProductCode} | "
                        + $"Qtde: {m.Quantity} | {m.Description} | Estoque final: {m.FinalStock}"
                );
            Console.WriteLine();
            break;

        default:
            Console.WriteLine("Opção inválida.\n");
            break;
    }
}
