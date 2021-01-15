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
        protected StockListBase _docsImported;

        /// <summary>
        /// StocksDistinct
        /// </summary>
        /// <param name="stockList"></param>
        /// <param name="dates"></param>
        /// <param name="dataContext"></param>
        internal StocksDistinct(List<Stock> stockList, List<string> dates, DataContext dataContext) : base(stockList, dates, dataContext)
        {
            var stocksDocsMissing = new List<Stock>();

            foreach(string date in dates)
            {
                var stocksDocsMissingDate = (from stock in _stockList
                                             join income in dataContext.IncomeStatements
                                             on new { a = stock.Symbol, b = date } equals new { a = income.Symbol, b = income.Date } into stockIncomeRecords
                                             from stockIncome in stockIncomeRecords.DefaultIfEmpty()
                                             join balance in dataContext.BalanceSheets
                                             on new { a = stock.Symbol, b = date } equals new { a = balance.Symbol, b = balance.Date } into stockIncomeBalanceRecords
                                             from stockIncomeBalance in stockIncomeBalanceRecords.DefaultIfEmpty()
                                             join cash in dataContext.CashFlowStatements
                                             on new { a = stock.Symbol, b = date } equals new { a = cash.Symbol, b = cash.Date } into stockIncomeBalanceCashRecords
                                             from stockIncomeBalanceCash in stockIncomeBalanceCashRecords.DefaultIfEmpty()
                                             where ((stockIncome == null) || (stockIncomeBalance == null) || (stockIncomeBalanceCash == null))
                                             && !string.IsNullOrWhiteSpace(stock.Name)
                                             select stock).ToList();
                stocksDocsMissing.AddRange(stocksDocsMissingDate);
            }

            _docsMissing = new StockListBase(stocksDocsMissing, dates, dataContext);
            var stocksDocsImported = _stockList.Except(stocksDocsMissing, new CompanyNameComparer()).ToList();
            _docsImported = new StockListBase(stocksDocsImported, dates, dataContext);
        }

        /// <summary>
        /// StockListBase
        /// </summary>
        public StockListBase DocsMissing
        {
            get
            {
                return _docsMissing;
            }
        }

        /// <summary>
        /// StockListBase
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