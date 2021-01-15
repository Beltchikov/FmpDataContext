using FmpDataContext.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FmpDataContext.StockList
{
    /// <summary>
    /// StockListBase
    /// </summary>
    public class StockListBase
    {
        protected List<Stock> _stockList;
        protected List<string> _dates;
        protected DataContext _dataContext;

        /// <summary>
        /// StockListBase
        /// </summary>
        /// <param name="stocksRecieved"></param>
        public StockListBase(List<Stock> stocksRecieved, List<string> dates, DataContext dataContext)
        {
            _stockList = stocksRecieved;
            _dates = dates;
            _dataContext = dataContext;
        }

        /// <summary>
        /// AsJson
        /// </summary>
        public string AsJson
        {
            get
            {
                return JsonSerializer.Serialize(_stockList);
            }
        }

        /// <summary>
        /// Symbols
        /// </summary>
        public List<string> Symbols
        {
            get
            {
                return _stockList.Select(s => s.Symbol).OrderBy(u => u).ToList();
            }
        }

        /// <summary>
        /// SymbolsAsText
        /// </summary>
        public string SymbolsAsText
        {
            get
            {
                return Symbols.Aggregate((r, n) => r + Environment.NewLine + n);
            }
        }
    }
}
