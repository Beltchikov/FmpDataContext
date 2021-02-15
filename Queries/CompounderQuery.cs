using FmpDataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.Common;

namespace FmpDataContext.Queries
{
    /// <summary>
    /// CompounderQuery
    /// </summary>
    public class CompounderQuery : QueryBase
    {
        public CompounderQuery(DataContext dataContext) : base(dataContext) { }

        /// <summary>
        /// Run
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public ResultSetList Run(CompounderQueryParams parameters)
        {
            ResultSetList resultSetList = null;
            QueryFactory queryFactory = new QueryFactory(DataContext.ConnectionString);

            var p = parameters;

            var dates = FmpHelper.BuildDatesList(parameters.YearFrom, parameters.YearTo, parameters.Dates);
            var command = DbCommands.Compounder(DataContext.Database.GetDbConnection(), Sql.Compounder(parameters, dates), parameters, dates);

            var queryAsEnumerable = QueryAsEnumerable(command, ResultSetFunctions.Compounder);
            if (queryAsEnumerable.Count() > parameters.MaxResultCount)
            {
                return new ResultSetList(new List<ResultSet>()) { CountTotal = queryAsEnumerable.Count() };
            }

            var queryAsEnumerableList = queryAsEnumerable.ToList();

            queryAsEnumerableList = AddHistoryData(queryAsEnumerableList, parameters, queryFactory.RoeHistoryQuery, a => a.RoeHistory);
            queryAsEnumerableList = AddHistoryData(queryAsEnumerableList, parameters, queryFactory.RevenueHistoryQuery, a => a.RevenueHistory);
            queryAsEnumerableList = AddHistoryData(queryAsEnumerableList, parameters, queryFactory.EpsHistoryQuery, a => a.EpsHistory);

            queryAsEnumerableList = AdjustToGrowthKoef(queryAsEnumerableList, parameters.RoeGrowthKoef, r => r.RoeHistory);
            queryAsEnumerableList = AdjustToGrowthKoef(queryAsEnumerableList, parameters.RevenueGrowthKoef, r => r.RevenueHistory);
            queryAsEnumerableList = AdjustToGrowthKoef(queryAsEnumerableList, parameters.EpsGrowthKoef, r => r.EpsHistory);

            List<ResultSet> listOfResultSets = queryAsEnumerableList.Skip(p.CurrentPage * p.PageSize).Take(p.PageSize).ToList();
            resultSetList = new ResultSetList(listOfResultSets);
            resultSetList.CountTotal = queryAsEnumerableList.Count();

            resultSetList = AddHistoryData(resultSetList, parameters, queryFactory.ReinvestmentHistoryQuery, a => a.ReinvestmentHistory);
            resultSetList = AddHistoryData(resultSetList, parameters, queryFactory.IncrementalRoeQuery, a => a.IncrementalRoe);
            resultSetList = AddHistoryData(resultSetList, parameters, queryFactory.OperatingIncomeHistoryQuery, a => a.OperatingIncome);
            resultSetList = AddHistoryData(resultSetList, parameters, queryFactory.CashConversionQuery, a => a.CashConversionHistory);
            resultSetList = AddCompanyName(resultSetList);
            resultSetList = AddDebtEquityIncome(resultSetList);

            return resultSetList;
        }

        /// <summary>
        /// Count
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int Count(CompounderQueryParams parameters)
        {
            var dates = FmpHelper.BuildDatesList(parameters.YearFrom, parameters.YearTo, parameters.Dates);
            var command = DbCommands.Compounder(DataContext.Database.GetDbConnection(), Sql.Compounder(parameters, dates), parameters, dates);
            return QueryAsEnumerable(command, ResultSetFunctions.Compounder).Count();
        }

        /// <summary>
        /// Run
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="symbolList"></param>
        /// <returns></returns>
        public ResultSetList FindBySymbol(CompounderQueryParams parameters, List<string> symbolList)
        {
            ResultSetList resultSetList = null;
            QueryFactory queryFactory = new QueryFactory(DataContext.ConnectionString);

            var dates = FmpHelper.BuildDatesList(parameters.YearFrom, parameters.YearTo, parameters.Dates);
            var command = DbCommands.FindBySymbol(DataContext.Database.GetDbConnection(), Sql.FindBySymbol(symbolList, dates), symbolList, dates);
            var queryAsEnumerable = QueryAsEnumerable(command, ResultSetFunctions.FindBySymbol).ToList();

            queryAsEnumerable = AddHistoryData(queryAsEnumerable, parameters, queryFactory.RoeHistoryQuery, a => a.RoeHistory);
            queryAsEnumerable = AddHistoryData(queryAsEnumerable, parameters, queryFactory.RevenueHistoryQuery, a => a.RevenueHistory);
            queryAsEnumerable = AddHistoryData(queryAsEnumerable, parameters, queryFactory.EpsHistoryQuery, a => a.EpsHistory);

            queryAsEnumerable = AdjustToGrowthKoef(queryAsEnumerable, parameters.RoeGrowthKoef, r => r.RoeHistory);
            queryAsEnumerable = AdjustToGrowthKoef(queryAsEnumerable, parameters.RevenueGrowthKoef, r => r.RevenueHistory);
            queryAsEnumerable = AdjustToGrowthKoef(queryAsEnumerable, parameters.EpsGrowthKoef, r => r.EpsHistory);

            List<ResultSet> listOfResultSets = queryAsEnumerable.ToList();
            resultSetList = new ResultSetList(listOfResultSets);
            resultSetList.CountTotal = queryAsEnumerable.Count();

            resultSetList = AddHistoryData(resultSetList, parameters, queryFactory.ReinvestmentHistoryQuery, a => a.ReinvestmentHistory);
            resultSetList = AddHistoryData(resultSetList, parameters, queryFactory.IncrementalRoeQuery, a => a.IncrementalRoe);
            resultSetList = AddHistoryData(resultSetList, parameters, queryFactory.OperatingIncomeHistoryQuery, a => a.OperatingIncome);
            resultSetList = AddHistoryData(resultSetList, parameters, queryFactory.CashConversionQuery, a => a.CashConversionHistory);
            resultSetList = AddCompanyName(resultSetList);
            resultSetList = AddDebtEquityIncome(resultSetList);

            return resultSetList;

        }

        /// <summary>
        /// AddHistoryData
        /// </summary>
        /// <param name="inputResultSetList"></param>
        /// <param name="parameters"></param>
        /// <param name="query"></param>
        /// <param name="funcAttributeToSet"></param>
        /// <returns></returns>
        private ResultSetList AddHistoryData(ResultSetList inputResultSetList, HistoryQueryParams parameters,
            HistoryQuery query, Func<ResultSet, List<double>> funcAttributeToSet)
        {
            for (int i = 0; i < inputResultSetList.ResultSets.Count(); i++)
            {
                foreach (string dateParam in parameters.Dates)
                {
                    string date = parameters.YearFrom.ToString() + dateParam[4..];

                    var queryResults = query.Run(inputResultSetList.ResultSets[i].Symbol, date, parameters.HistoryDepth);
                    if (!queryResults.Any())
                    {
                        continue;
                    }

                    queryResults.Reverse();
                    for (int ii = 0; ii < queryResults.Count(); ii++)
                    {
                        inputResultSetList.ResultSets.Select(funcAttributeToSet).ToList()[i].Add(queryResults[ii]);
                    }
                    break;
                }
            }

            return inputResultSetList;
        }

        /// <summary>
        /// AddHistoryData
        /// </summary>
        /// <param name="inputResultSetList"></param>
        /// <param name="parameters"></param>
        /// <param name="query"></param>
        /// <param name="funcAttributeToSet"></param>
        /// <returns></returns>
        private List<ResultSet> AddHistoryData(List<ResultSet> inputResultSetList, HistoryQueryParams parameters,
            HistoryQuery query, Func<ResultSet, List<double>> funcAttributeToSet)
        {
            for (int i = 0; i < inputResultSetList.Count(); i++)
            {
                foreach (string dateParam in parameters.Dates)
                {
                    string date = parameters.YearFrom.ToString() + dateParam[4..];

                    var queryResults = query.Run(inputResultSetList[i].Symbol, date, parameters.HistoryDepth);
                    if (!queryResults.Any())
                    {
                        continue;
                    }

                    queryResults.Reverse();
                    for (int ii = 0; ii < queryResults.Count(); ii++)
                    {
                        inputResultSetList.Select(funcAttributeToSet).ToList()[i].Add(queryResults[ii]);
                    }
                    break;
                }
            }

            return inputResultSetList;
        }


        /// <summary>
        /// AdjustToRoeGrowthKoef
        /// </summary>
        /// <param name="inputResultSetList"></param>
        /// <param name="growthKoef"></param>
        /// <param name="funcResultSetParam"></param>
        /// <returns></returns>
        private List<ResultSet> AdjustToGrowthKoef(List<ResultSet> inputResultSetList, int? growthKoef, Func<ResultSet, List<double>> funcResultSetParam)
        {
            if (!growthKoef.HasValue)
            {
                return inputResultSetList;
            }

            List<ResultSet> resultSetList = new List<ResultSet>();

            foreach (ResultSet resultSet in inputResultSetList)
            {
                if (funcResultSetParam(resultSet).Grows() >= growthKoef.Value)
                {
                    resultSetList.Add(resultSet);
                }
            }

            return resultSetList;
        }

        /// <summary>
        /// AddCompanyName
        /// </summary>
        /// <param name="inputResultSetList"></param>
        /// <returns></returns>
        private ResultSetList AddCompanyName(ResultSetList inputResultSetList)
        {
            QueryFactory queryFactory = new QueryFactory(DataContext.ConnectionString);
            ResultSetList resultSetList = queryFactory.CompanyNameQuery.Run(inputResultSetList);
            return resultSetList;
        }

        /// <summary>
        /// AddCompanyName
        /// </summary>
        /// <param name="inputResultSetList"></param>
        /// <returns></returns>
        private List<ResultSet> AddCompanyName(List<ResultSet> inputResultSetList)
        {
            QueryFactory queryFactory = new QueryFactory(DataContext.ConnectionString);
            List<ResultSet> resultSetList = queryFactory.CompanyNameQuery.Run(inputResultSetList);
            return resultSetList;
        }

        /// <summary>
        /// AddDebtEquityIncome
        /// </summary>
        /// <param name="inputResultSetList"></param>
        /// <returns></returns>
        private ResultSetList AddDebtEquityIncome(ResultSetList inputResultSetList)
        {
            for (int i = 0; i < inputResultSetList.ResultSets.Count(); i++)
            {
                ResultSet resultSet = inputResultSetList.ResultSets[i];
                resultSet.DebtEquityIncome.Add(resultSet.Debt.Value);
                resultSet.DebtEquityIncome.Add(resultSet.Equity.Value);
                resultSet.DebtEquityIncome.Add(resultSet.NetIncome.Value);
            }
            return inputResultSetList;
        }

        /// <summary>
        /// AddDebtEquityIncome
        /// </summary>
        /// <param name="inputResultSetList"></param>
        /// <returns></returns>
        private List<ResultSet> AddDebtEquityIncome(List<ResultSet> inputResultSetList)
        {
            for (int i = 0; i < inputResultSetList.Count(); i++)
            {
                ResultSet resultSet = inputResultSetList[i];
                resultSet.DebtEquityIncome.Add(resultSet.Debt.Value);
                resultSet.DebtEquityIncome.Add(resultSet.Equity.Value);
                resultSet.DebtEquityIncome.Add(resultSet.NetIncome.Value);
            }
            return inputResultSetList;
        }
    }
}
