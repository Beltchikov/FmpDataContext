﻿using FmpDataContext.Model;
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
        protected List<string> _years;
        protected List<string> _dates;
        protected DataContext _dataContext;


        /// <summary>
        /// StockListBase
        /// </summary>
        /// <param name="stocksRecieved"></param>
        /// <param name="years"></param>
        /// <param name="dates"></param>
        /// <param name="dataContext"></param>
        public StockListBase(List<Stock> stocksRecieved, List<string> years, List<string> dates, DataContext dataContext)
        {
            _stockList = stocksRecieved;
            _years = years;
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
        /// Years
        /// </summary>
        public List<string> Years
        {
            get
            {
                return _years;
            }
        }

        /// <summary>
        /// Dates
        /// </summary>
        public List<string> Dates
        {
            get
            {
                return _dates;
            }
        }

        /// <summary>
        /// DataContext
        /// </summary>
        public DataContext DataContext
        {
            get
            {
                return _dataContext;
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
        /// SymbolsTop100AsText
        /// </summary>
        public string SymbolsTop100AsText
        {
            get
            {
                if(!Symbols.Any())
                {
                    return string.Empty;
                }
                               
                return Symbols.OrderBy(s=>s).Take(100).Aggregate((r, n) => r + Environment.NewLine + n);
            }
        }

        /// <summary>
        /// SymbolsTop1000AsText
        /// </summary>
        public string SymbolsTop1000AsText
        {
            get
            {
                if (!Symbols.Any())
                {
                    return string.Empty;
                }

                return Symbols.OrderBy(s => s).Take(1000).Aggregate((r, n) => r + Environment.NewLine + n);
            }
        }

        /// <summary>
        /// ToList
        /// </summary>
        /// <returns></returns>
        public List<Stock> ToList()
        {
            return _stockList;
        }
    }
}
