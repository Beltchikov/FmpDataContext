using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FmpDataContext.Model
{
    /// <summary>
    /// Stock
    /// </summary>
    [Index(nameof(Name), IsUnique = true)]
    public class Stock
    {
        [Key]
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("price")]
        public double Price { get; set; }

        [JsonPropertyName("exchange")]
        public string Exchange { get; set; }

        public FmpSymbolCompany ToFmpSymbolCompany()
        {
            return new FmpSymbolCompany
            {
                Symbol = this.Symbol,
                Name = this.Name,
                Price = this.Price,
                Exchange = this.Exchange
            };
        }
    }
}

