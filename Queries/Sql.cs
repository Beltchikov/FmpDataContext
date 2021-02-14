﻿using FmpDataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FmpDataContext.Queries
{
    public class Sql
    {
        /// <summary>
        /// Compounder
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="dates"></param>
        /// <returns></returns>
        public static string Compounder(CompounderQueryParams parameters, List<string> dates)
        {
            string sqlBase = $@"select v.Symbol, v.Date, v.Equity, v.Debt, v.NetIncome, v.Roe, v.ReinvestmentRate, v.DebtEquityRatio
                from ViewCompounder v 
                inner join Stocks s
                on v.Symbol = s.Symbol
                where 1 = 1
                and v.Date in (@Dates) ";

            string datesAsParam = CreateCommaSeparatedParams("@Dates", dates.Count);
            string sql = sqlBase.Replace("@Dates", datesAsParam);

            if (parameters.RoeFrom.HasValue)
            {
                sql += " and v.Roe >= @RoeFrom ";
            }
            if (parameters.RoeTo.HasValue)
            {
                sql += " and Roe <= @RoeTo ";
            }
            if (parameters.ReinvestmentRateFrom.HasValue)
            {
                sql += " and v.ReinvestmentRate >= @ReinvestmentRateFrom ";
            }
            if (parameters.ReinvestmentRateTo.HasValue)
            {
                sql += " and ReinvestmentRate <= @ReinvestmentRateTo ";
            }
            if (parameters.DebtEquityRatioFrom.HasValue)
            {
                sql += " and DebtEquityRatio >= @DebtEquityRatioFrom ";
            }
            if (parameters.DebtEquityRatioTo.HasValue)
            {
                sql += " and DebtEquityRatio <= @DebtEquityRatioTo ";
            }
            if (parameters.Exchanges.Any())
            {
                sql += " and s.Exchange in(@Exchanges) ";
            }
            
            string exchangesAsParam = CreateCommaSeparatedParams("@Exchanges", parameters.SelectedFmpExchanges.Count);
            sql = sql.Replace("@Exchanges", exchangesAsParam);

            var ascDesc = parameters.Descending ? " DESC " : " ASC ";
            sql += $" order by {parameters.OrderBy} {ascDesc} ";

            return sql;
        }

        /// <summary>
        /// FindBySymbol
        /// </summary>
        /// <param name="symbolList"></param>
        /// <param name="dates"></param>
        /// <returns></returns>
        public static string FindBySymbol(List<string> symbolList, List<string> dates)
        {
            string sqlBase = $@"select Symbol, Date, Equity, Debt, NetIncome, Roe, ReinvestmentRate, DebtEquityRatio
                from ViewCompounder 
                where 1 = 1
                and Symbol in (@Symbols)
                and Date in (@Dates)";

            string symbolsAsParam = CreateCommaSeparatedParams("@Symbols", symbolList.Count);
            var sql = sqlBase.Replace("@Symbols", symbolsAsParam);

            if (dates.Any())
            {
                string datesAsParam = CreateCommaSeparatedParams("@Dates", dates.Count);
                sql = sql.Replace("@Dates", datesAsParam);
            }

            return sql;
        }

        /// <summary>
        /// SqlFindByCompany
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public static string FindByCompany(string company)
        {
            string sqlBase = $@"select v.Symbol, s.Name
                from ViewCompounder v
                inner join Stocks s on s.Symbol = v.Symbol
                where 1 = 1
                and s.Name like '%@Company%';";

            string sql = sqlBase.Replace("@Company", company);
            return sql;
        }

        /// <summary>
        /// CreateCommaSeparatedParams
        /// </summary>
        /// <param name="paramBase"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        private static string CreateCommaSeparatedParams(string paramBase, int count)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                sb.Append(paramBase + i.ToString());
                if (i < count - 1)
                {
                    sb.Append(",");
                }
            }

            return sb.ToString();
        }
    }
}
