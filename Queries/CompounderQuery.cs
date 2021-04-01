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
        public ResultSetListReinvestment Run(CompounderQueryParams parameters)
        {
            ResultSetListReinvestment resultSetList = null;
            QueryFactory queryFactory = new QueryFactory(DataContext.ConnectionString);

            var p = parameters;

            var dates = FmpHelper.BuildDatesList(parameters.YearFrom, parameters.YearTo, parameters.Dates);
            var command = DbCommands.Compounder(DataContext.Database.GetDbConnection(), Sql.Compounder(parameters, dates), parameters, dates);

            var queryAsEnumerable = QueryAsEnumerable(command, ResultSetFunctions.Compounder);
            if (queryAsEnumerable.Count() > parameters.MaxResultCount)
            {
                return new ResultSetListReinvestment(new List<ResultSetReinvestment>()) { CountTotal = queryAsEnumerable.Count() };
            }

            var queryAsEnumerableList = queryAsEnumerable.ToList();

            queryAsEnumerableList = AddHistoryData(queryAsEnumerableList, parameters, queryFactory.RoeHistoryQuery, a => a.RoeHistory);
            queryAsEnumerableList = AddHistoryData(queryAsEnumerableList, parameters, queryFactory.RevenueHistoryQuery, a => a.RevenueHistory);
            queryAsEnumerableList = AddHistoryData(queryAsEnumerableList, parameters, queryFactory.EpsHistoryQuery, a => a.EpsHistory);

            queryAsEnumerableList = AdjustToGrowthKoef(queryAsEnumerableList, parameters.RoeGrowthKoef, r => r.RoeHistory);
            queryAsEnumerableList = AdjustToGrowthKoef(queryAsEnumerableList, parameters.RevenueGrowthKoef, r => r.RevenueHistory);
            queryAsEnumerableList = AdjustToGrowthKoef(queryAsEnumerableList, parameters.EpsGrowthKoef, r => r.EpsHistory);

            List<ResultSetReinvestment> listOfResultSets = p.PageSize == 0 
                ? queryAsEnumerableList.ToList()
                : queryAsEnumerableList.Skip(p.CurrentPage * p.PageSize).Take(p.PageSize).ToList();
            resultSetList = new ResultSetListReinvestment(listOfResultSets);
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
        public ResultSetListReinvestment FindBySymbol(CompounderQueryParams parameters, List<string> symbolList)
        {
            ResultSetListReinvestment resultSetList = null;
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

            List<ResultSetReinvestment> listOfResultSets = queryAsEnumerable.ToList();
            resultSetList = new ResultSetListReinvestment(listOfResultSets);
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
        private ResultSetListReinvestment AddHistoryData(ResultSetListReinvestment inputResultSetList, HistoryQueryParams parameters,
            HistoryQuery query, Func<ResultSetReinvestment, List<double>> funcAttributeToSet)
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
        private List<ResultSetReinvestment> AddHistoryData(List<ResultSetReinvestment> inputResultSetList, HistoryQueryParams parameters,
            HistoryQuery query, Func<ResultSetReinvestment, List<double>> funcAttributeToSet)
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
        private List<ResultSetReinvestment> AdjustToGrowthKoef(List<ResultSetReinvestment> inputResultSetList, int? growthKoef, Func<ResultSetReinvestment, List<double>> funcResultSetParam)
        {
            if (!growthKoef.HasValue)
            {
                return inputResultSetList;
            }

            List<ResultSetReinvestment> resultSetList = new List<ResultSetReinvestment>();

            foreach (ResultSetReinvestment resultSet in inputResultSetList)
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
        private ResultSetListReinvestment AddCompanyName(ResultSetListReinvestment inputResultSetList)
        {
            QueryFactory queryFactory = new QueryFactory(DataContext.ConnectionString);
            ResultSetListReinvestment resultSetList = queryFactory.CompanyNameQuery.Run(inputResultSetList);
            return resultSetList;
        }

        /// <summary>
        /// AddCompanyName
        /// </summary>
        /// <param name="inputResultSetList"></param>
        /// <returns></returns>
        private List<ResultSetReinvestment> AddCompanyName(List<ResultSetReinvestment> inputResultSetList)
        {
            QueryFactory queryFactory = new QueryFactory(DataContext.ConnectionString);
            List<ResultSetReinvestment> resultSetList = queryFactory.CompanyNameQuery.Run(inputResultSetList);
            return resultSetList;
        }

        /// <summary>
        /// AddDebtEquityIncome
        /// </summary>
        /// <param name="inputResultSetList"></param>
        /// <returns></returns>
        private ResultSetListReinvestment AddDebtEquityIncome(ResultSetListReinvestment inputResultSetList)
        {
            for (int i = 0; i < inputResultSetList.ResultSets.Count(); i++)
            {
                ResultSetReinvestment resultSet = inputResultSetList.ResultSets[i];
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
        private List<ResultSetReinvestment> AddDebtEquityIncome(List<ResultSetReinvestment> inputResultSetList)
        {
            for (int i = 0; i < inputResultSetList.Count(); i++)
            {
                ResultSetReinvestment resultSet = inputResultSetList[i];
                resultSet.DebtEquityIncome.Add(resultSet.Debt.Value);
                resultSet.DebtEquityIncome.Add(resultSet.Equity.Value);
                resultSet.DebtEquityIncome.Add(resultSet.NetIncome.Value);
            }
            return inputResultSetList;
        }
    }
}
