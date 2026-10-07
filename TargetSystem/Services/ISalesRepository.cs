using System.Text.Json;
using TargetSystem.Domain;

namespace TargetSystem.Services
{
    public interface ISalesRepository
    {
        Task<List<Sale>> GetAllAsync();
    }

    public class JsonSalesRepository : ISalesRepository
    {
        private readonly string _path;
        private static readonly JsonSerializerOptions _options = new()
        {
            PropertyNameCaseInsensitive = true,
        };

        public JsonSalesRepository(IWebHostEnvironment env)
        {
            _path = Path.Combine(env.ContentRootPath, "Mocks", "sales.json");
        }

        public async Task<List<Sale>> GetAllAsync()
        {
            await using var stream = File.OpenRead(_path);
            var data = await JsonSerializer.DeserializeAsync<SalesFile>(stream, _options);
            return data?.Vendas ?? new();
        }
    }
}
