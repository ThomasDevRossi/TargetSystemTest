using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Comission
{
    public class SalesFile
    {
        [JsonPropertyName("vendas")]
        public List<Sale> Sales { get; set; } = new();
    }

    public class Sale
    {
        [JsonPropertyName("vendedor")]
        public string Seller { get; set; } = string.Empty;

        [JsonPropertyName("valor")]
        public decimal Value { get; set; }
    }

    public class ComissionRule
    {
        public decimal MinimumForCommission { get; set; } = 100m;
        public decimal UpperRangeLimit { get; set; } = 500m;
        public decimal LowRate { get; set; } = 0.01m;
        public decimal HighRate { get; set; } = 0.05m;
        public decimal Value { get; set; }

        public decimal Calculate(decimal saleValue)
        {
            if (saleValue < MinimumForCommission)
                return 0m;

            if (saleValue < UpperRangeLimit)
                return saleValue * LowRate;

            return saleValue * HighRate;
        }
    }
}
