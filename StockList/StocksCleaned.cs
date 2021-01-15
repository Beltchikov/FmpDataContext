using FmpDataContext.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FmpDataContext.StockList
{
    /// <summary>
    /// StocksCleaned
    /// </summary>
    public class StocksCleaned : StockListBase
    {
        protected List<FmpSymbolCompany> _symbolCompany;
        protected StocksDistinct _stocksDistinct;

        /// <summary>
        /// StocksCleaned
        /// </summary>
        /// <param name="stockList"></param>
        internal StocksCleaned(List<Stock> stockList, List<string> dates, DataContext dataContext) : base(stockList, dates, dataContext)
        {
            var companiesWithMultipleSymbols = CompaniesWithMultipleSymbols(_stockList);
            _stockList = _stockList.Except(companiesWithMultipleSymbols, new CompanyNameComparer()).ToList();
            var symbolsWithMultipleCompanyName = SymbolsWithMultipleCompanyName(_stockList);
            _stockList = _stockList.Except(symbolsWithMultipleCompanyName).ToList();
            _stocksDistinct = new StocksDistinct(_stockList, dates, dataContext);

            _symbolCompany = companiesWithMultipleSymbols.Select(c => c.ToFmpSymbolCompany()).ToList();
            _symbolCompany.AddRange(symbolsWithMultipleCompanyName.Select(c => c.ToFmpSymbolCompany()));
        }

        /// <summary>
        /// SymbolCompany
        /// </summary>
        public List<FmpSymbolCompany> SymbolCompany
        {
            get
            {
                return _symbolCompany;
            }
        }

        /// <summary>
        /// Distinct
        /// </summary>
        public StocksDistinct Distinct
        {
            get
            {
                return _stocksDistinct;
            }
        }

        /// <summary>
        /// RemoveDuplicateCompanies
        /// </summary>
        /// <param name="stockList"></param>
        /// <returns></returns>
        private List<Stock> CompaniesWithMultipleSymbols(List<Stock> stockList)
        {
            var companiesWithMultipleSymbols = (from stock in stockList
                                                group stock by stock.Name.ToUpper() into newGroup
                                                where newGroup.Count() > 1
                                                select newGroup.Key).ToList();

            return stockList.Where(s => companiesWithMultipleSymbols.Contains(s.Name)).ToList();
        }

        /// <summary>
        /// SymbolsWithMultipleCompanyName
        /// </summary>
        /// <param name="stockList"></param>
        /// <returns></returns>
        private List<Stock> SymbolsWithMultipleCompanyName(List<Stock> stockList)
        {
            var symbolsWithMultipleCompanyName = (from stock in stockList
                                                  group stock by stock.Symbol into newGroup
                                                  where newGroup.Count() > 1
                                                  select newGroup.Key).ToList();

            return stockList.Where(s => symbolsWithMultipleCompanyName.Contains(s.Symbol)).ToList();
        }
    }
}