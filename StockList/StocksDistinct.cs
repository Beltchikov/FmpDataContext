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
            var docsMissingNoImportError = stocksDocsMissing.Where(s => ! importErrorFmpSymbolList.Contains(s.Symbol)).ToList();
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

            foreach (string year in years)
            {
                var datesOfYear = dates.Select(d => year + d[4..]);
                //var stocksDocsMissingYear = (from stock in _stockList
                //                             join income in dataContext.IncomeStatements
                //                             on new { a = stock.Symbol, b = date } equals new { a = income.Symbol, b = income.Date } into stockIncomeRecords
                //                             from stockIncome in stockIncomeRecords.DefaultIfEmpty()
                //                             join balance in dataContext.BalanceSheets
                //                             on new { a = stock.Symbol, b = date } equals new { a = balance.Symbol, b = balance.Date } into stockIncomeBalanceRecords
                //                             from stockIncomeBalance in stockIncomeBalanceRecords.DefaultIfEmpty()
                //                             join cash in dataContext.CashFlowStatements
                //                             on new { a = stock.Symbol, b = date } equals new { a = cash.Symbol, b = cash.Date } into stockIncomeBalanceCashRecords
                //                             from stockIncomeBalanceCash in stockIncomeBalanceCashRecords.DefaultIfEmpty()
                //                             where ((stockIncome == null) || (stockIncomeBalance == null) || (stockIncomeBalanceCash == null))
                //                             && !string.IsNullOrWhiteSpace(stock.Name)
                //                             select stock).ToList();

                var stocksDocsMissingYear = (from stock in _stockList
                                             where ! dataContext.IncomeStatements.Any(i => i.Symbol == stock.Symbol && datesOfYear.Contains(i.Date))
                                             select stock).ToList();

                stocksDocsMissing = stocksDocsMissing.Union(stocksDocsMissingYear).ToList();
            }

            return stocksDocsMissing;
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