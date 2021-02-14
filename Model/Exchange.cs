using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FmpDataContext.Model
{
    /// <summary>
    /// Exchange
    /// </summary>
    public class Exchange
    {

        /// <summary>
        /// Exchange
        /// </summary>
        Exchange()
        {
            ExchangesFmp = new List<string>();
        }

        public string Name { get; private set; }
        public bool Selected { get; set; }
        public List<string> ExchangesFmp { get; private set; }

        /// <summary>
        /// Nyse
        /// </summary>
        public static Exchange Nyse
        {
            get
            {
                return new Exchange
                {
                    Name = "NYSE",
                    Selected = true,
                    ExchangesFmp = new List<string>
                    { "New York Stock Exchange","NYSE Arca","NYSE","NYSE American","NYSEArca"}
                };
            }
        }

        /// <summary>
        /// Nasdaq
        /// </summary>
        public static Exchange Nasdaq
        {
            get
            {
                return new Exchange
                {
                    Name = "NASDAQ",
                    Selected = true,
                    ExchangesFmp = new List<string>
                    { "Nasdaq","Nasdaq Global Select","NASDAQ Capital Market","NASDAQ Global Market","NasdaqGS", "NasdaqCM", "NasdaqGM"}
                };
            }
        }

        /// <summary>
        /// Lse
        /// </summary>
        public static Exchange Lse
        {
            get
            {
                return new Exchange
                {
                    Name = "LSE",
                    Selected = true,
                    ExchangesFmp = new List<string>
                    { "LSE"}
                };
            }
        }

        /// <summary>
        /// Hkse
        /// </summary>
        public static Exchange Hkse
        {
            get
            {
                return new Exchange
                {
                    Name = "HKSE",
                    Selected = false,
                    ExchangesFmp = new List<string>
                    { "HKSE"}
                };
            }
        }

        /// <summary>
        /// Asx
        /// </summary>
        public static Exchange Asx
        {
            get
            {
                return new Exchange
                {
                    Name = "ASX",
                    Selected = true,
                    ExchangesFmp = new List<string>
                    { "ASX"}
                };
            }
        }

        /// <summary>
        /// Nse
        /// </summary>
        public static Exchange Nse
        {
            get
            {
                return new Exchange
                {
                    Name = "NSE",
                    Selected = false,
                    ExchangesFmp = new List<string>
                    { "NSE"}
                };
            }
        }

        /// <summary>
        /// Canada
        /// </summary>
        public static Exchange Canada
        {
            get
            {
                return new Exchange
                {
                    Name = "Canada",
                    Selected = true,
                    ExchangesFmp = new List<string>
                    { "Toronto", "YHD"}
                };
            }
        }

        /// <summary>
        /// Europe
        /// </summary>
        public static Exchange Europe
        {
            get
            {
                return new Exchange
                {
                    Name = "Europe",
                    Selected = true,
                    ExchangesFmp = new List<string>
                    { "Paris", "XETRA", "BATS Exchange",
                    "MCX","Brussels","BATS",
                    "Amsterdam", "OSE", "SIX",
                    "NMS","Lisbon","NCM"}
                };
            }
        }

        /// <summary>
        /// FromString
        /// </summary>
        /// <param name="exchangeString"></param>
        /// <returns>Returns Exchange object or null if the object can not be created.</returns>
        public static Exchange FromString(string exchangeString)
        {
            switch (exchangeString)
            {
                case "Nyse":
                    return Nyse;
                case "Nasdaq":
                    return Nasdaq;
                case "Lse":
                    return Lse;
                case "Hkse":
                    return Hkse;
                case "Asx":
                    return Asx;
                case "Nse":
                    return Nse;
                case "Canada":
                    return Canada;
                case "Europe":
                    return Europe;
                default:
                    return null; ;
            }
        }

        /// <summary>
        /// FromString
        /// </summary>
        /// <param name="exchangesString"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static List<Exchange> FromString(string exchangesString, string separator)
        {
            List<Exchange> resultList = new List<Exchange>();

            var stringList = exchangesString.Split(separator).Select(d => d.Trim()).ToList();
            foreach (var exchangeString in stringList)
            {
                var exchange = Exchange.FromString(exchangeString);
                if(exchange != null)
                {
                    resultList.Add(exchange);
                }
            }

            return resultList;
        }
    }
}
