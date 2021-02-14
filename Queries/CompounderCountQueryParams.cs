using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using FmpDataContext.Model;

namespace FmpDataContext.Queries
{
    public class CompounderCountQueryParams : HistoryQueryParams
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

        public List<string> SelectedFmpExchanges
        {
            get
            {
                return Exchanges.Where(e => e.Selected).SelectMany(s => s.ExchangesFmp).ToList();
            }
        }

    }
}
