using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;
using TargetSystem.Domain;
using TargetSystem.Services;

namespace TargetSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TargetSystemController : ControllerBase
    {
        private readonly ISalesRepository _repo;
        private readonly ICommissionService _commission;
        private readonly IWebHostEnvironment _env;

        public TargetSystemController(ISalesRepository repo, ICommissionService commission, IWebHostEnvironment env)
        {
            _repo = repo;
            _commission = commission;
            _env = env;
        }

        [HttpGet("commissions")]
        public async Task<ActionResult<List<CommissionResult>>> GetCommissions()
        {
            var path = Path.Combine(_env.ContentRootPath, "Mocks", "sales.json");

            if (!System.IO.File.Exists(path))
                return NotFound("Arquivo Mocks/sales.json não encontrado.");

            await using var stream = System.IO.File.OpenRead(path);
            var data = await JsonSerializer.DeserializeAsync<SalesFile>(
                stream,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            var resultado = (data?.Vendas ?? new())
                .GroupBy(v => v.Vendedor, StringComparer.OrdinalIgnoreCase)
                .Select(g => new
                {
                    vendedor = g.Key,
                    quantidadeVendas = g.Count(),
                    totalVendido = g.Sum(v => v.Valor),
                    comissao = Math.Round(g.Sum(v => CalcularComissao(v.Valor)), 2),
                })
                .OrderByDescending(r => r.comissao);

            return Ok(resultado);
        }

        private static decimal CalcularComissao(decimal valor)
        {
            if (valor < 100m)
                return 0m;
            if (valor < 500m)
                return valor * 0.01m;
            return valor * 0.05m;
        }
    }
}
