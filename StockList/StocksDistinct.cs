using FmpDataContext.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FmpDataContext.StockList
{
    /// <summary>
    /// StocksDistinct
    /// </summary>
    public class StocksDistinct : StockListBase
    {
        protected StockListBase _docsMissing;
        protected StockListBase _docsMissingNoImportError;
        protected StockListBase _docsImported;

        /// <summary>
        /// StocksDistinct
        /// </summary>
        /// <param name="stockList"></param>
        /// <param name="years"></param>
        /// <param name="dates"></param>
        /// <param name="dataContext"></param>
        internal StocksDistinct(List<Stock> stockList, List<string> years, List<string> dates, DataContext dataContext) : base(stockList, years, dates, dataContext)
        {
            var stocksDocsMissing = DocsMissingAsList(years, dates, dataContext);
            _docsMissing = new StockListBase(stocksDocsMissing, years, dates, dataContext);

            var importErrorFmpSymbolList = (from importErrorFmpSymbol in dataContext.ImportErrorFmpSymbol
                                            select importErrorFmpSymbol.Symbol).ToList();
            var docsMissingNoImportError = stocksDocsMissing.Where(s => !importErrorFmpSymbolList.Contains(s.Symbol)).ToList();
            _docsMissingNoImportError = new StockListBase(docsMissingNoImportError, years, dates, dataContext);

            var stocksDocsImported = _stockList.Except(stocksDocsMissing, new CompanyNameComparer()).ToList();
            _docsImported = new StockListBase(stocksDocsImported, years, dates, dataContext);
        }

        /// <summary>
        /// DocsMissingAsList
        /// </summary>
        /// <param name="years"></param>
        /// <param name="dates"></param>
        /// <param name="dataContext"></param>
        /// <returns></returns>
        private List<Stock> DocsMissingAsList(List<string> years, List<string> dates, DataContext dataContext)
        {
            var stocksDocsMissing = new List<Stock>();

            foreach (var stock in _stockList)
            {
                var symbol = stock.Symbol;
                foreach (string year in years)
                {
                    if( ! IncomeStatementExists(symbol, year, dates, dataContext))
                    {
                        stocksDocsMissing.Add(stock);
                    }
                }
            }

            return stocksDocsMissing;
        }

        /// <summary>
        /// IncomeStatementExists
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="year"></param>
        /// <param name="dates"></param>
        /// <param name="dataContext"></param>
        /// <returns></returns>
        private bool IncomeStatementExists(string symbol, string year, List<string> dates, DataContext dataContext)
        {
            var datesOfYear = dates.Select(d => year + d[4..]).ToList();
            return dataContext.IncomeStatements.Any(i => i.Symbol == symbol && datesOfYear.Contains(i.Date));
        }

        /// <summary>
        /// DocsMissing
        /// </summary>
        public StockListBase DocsMissing
        {
            get
            {
                return _docsMissing;
            }
        }

        /// <summary>
        /// DocsMissingNoImportError
        /// </summary>
        public StockListBase DocsMissingNoImportError
        {
            get
            {
                return _docsMissingNoImportError;
            }
        }

        /// <summary>
        /// DocsImported
        /// </summary>
        public StockListBase DocsImported
        {
            get
            {
                return _docsImported;
            }
        }
    }
}