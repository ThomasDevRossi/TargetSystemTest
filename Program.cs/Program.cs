using System.Text.Json;

string caminho = Path.Combine(AppContext.BaseDirectory, "comissions.json");

string json = File.ReadAllText(caminho);

var pessoas = JsonSerializer.Deserialize<List<Pessoa>>(json);

foreach (var pessoa in pessoas)
{
    Console.WriteLine($"Nome: {pessoa.Nome} - Idade: {pessoa.Idade}");
}

class Pessoa
{
    public string Nome { get; set; }
    public int Idade { get; set; }
}