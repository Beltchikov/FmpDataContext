using FmpDataContext.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FmpDataContext.Queries
{
    /// <summary>
    /// CompounderQueryParams
    /// </summary>
    public class CompounderQueryParams : HistoryQueryParams
    {
        public int YearTo { get; set; }
        public double? RoeFrom { get; set; }
        public double? RoeTo { get; set; }
        public double? ReinvestmentRateFrom { get; set; }
        public double? ReinvestmentRateTo { get; set; }
        public int? RoeGrowthKoef { get; set; }
        public int? RevenueGrowthKoef { get; set; }
        public int? EpsGrowthKoef { get; set; }
        public double? DebtEquityRatioFrom { get; set; }
        public double? DebtEquityRatioTo { get; set; }
        public List<Exchange> Exchanges { get; set; }
        public string[] ExchangesAsArray { get; set; }

        public void SetExchanges(string[] exchangesArray)
        {
            if (exchangesArray == null)
            {
                Exchanges = new List<Exchange>();
            }
            else
            {
                Exchanges = Exchange.FromArray(exchangesArray);
            }
        }

        public List<string> SelectedFmpExchanges
        {
            get
            {
                if (Exchanges == null)
                {
                    return new List<string>();
                }
                else
                {
                    return Exchanges.Where(e => e.Selected).SelectMany(s => s.ExchangesFmp).ToList();
                }
            }
        }
        public string OrderBy { get; set; }
        public bool Descending { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int MaxResultCount { get; set; }

    }
}
