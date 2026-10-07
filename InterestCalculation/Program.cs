using System.Globalization;

var br = new CultureInfo("pt-BR");
const decimal dailyRate = 0.025m;

Console.WriteLine("=== Calculo de Juros ===");
Console.Write("Valor: ");
if (!decimal.TryParse(Console.ReadLine(), NumberStyles.Number, br, out decimal value) || value <= 0)
{
    Console.WriteLine("Valor inválido.");
    return;
}

Console.Write("Data de vencimento (dd/MM/yyyy): ");
if (
    !DateTime.TryParseExact(
        Console.ReadLine(),
        "dd/MM/yyyy",
        br,
        DateTimeStyles.None,
        out DateTime dueDate
    )
)
{
    Console.WriteLine("Data inválida.");
    return;
}

int daysLate = (DateTime.Today - dueDate.Date).Days;

if (daysLate <= 0)
{
    Console.WriteLine("Não há atraso, portanto não há juros.");
    Console.WriteLine($"Valor a pagar: {value.ToString("C", br)}");
    return;
}

decimal interest = Math.Round(value * dailyRate * daysLate, 2);
decimal total = value + interest;

Console.WriteLine();
Console.WriteLine($"Dias em atraso: {daysLate}");
Console.WriteLine($"Juros: {interest.ToString("C", br)}");
Console.WriteLine($"Total a pagar: {total.ToString("C", br)}");
