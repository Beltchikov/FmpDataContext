﻿using FmpDataContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FmpDataContext.Queries
{
    public class DbCommands : DbCommandsBase
    {
        /// <summary>
        /// Compounder
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="dates"></param>
        /// <returns></returns>
        public static DbCommand Compounder(DbConnection connection, string sql, CompounderQueryParams parameters, List<string> dates)
        {
            var command = connection.CreateCommand();
            command.CommandText = sql;
            command.CommandType = CommandType.Text;

            AddDoubleParameter(command, "@RoeFrom", DbType.Double, parameters.RoeFrom);
            AddDoubleParameter(command, "@RoeTo", DbType.Double, parameters.RoeTo);
            AddDoubleParameter(command, "@ReinvestmentRateFrom", DbType.Double, parameters.ReinvestmentRateFrom);
            AddDoubleParameter(command, "@ReinvestmentRateTo", DbType.Double, parameters.ReinvestmentRateTo);

            AddStringListParameter(command, "@Dates", DbType.String, dates);
            AddStringListParameter(command, "@Exchanges", DbType.String, parameters.SelectedFmpExchanges);
            
            AddDoubleParameter(command, "@DebtEquityRatioFrom", DbType.Double, parameters.DebtEquityRatioFrom);
            AddDoubleParameter(command, "@DebtEquityRatioTo", DbType.Double, parameters.DebtEquityRatioTo);

            return command;
        }


        /// <summary>
        /// FindBySymbol
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="sql"></param>
        /// <param name="symbolList"></param>
        /// <param name="dates"></param>
        /// <returns></returns>
        public static DbCommand FindBySymbol(DbConnection connection, string sql, List<string> symbolList, List<string> dates)
        {
            var command = connection.CreateCommand();
            command.CommandText = sql;
            command.CommandType = CommandType.Text;

            AddStringListParameter(command, "@Symbols", DbType.String, symbolList);

            if(dates.Any())
            {
                AddStringListParameter(command, "@Dates", DbType.String, dates);
            }

            return command;
        }

        /// <summary>
        /// CommandFindByCompany
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <param name="v"></param>
        /// <param name="company"></param>
        /// <returns></returns>
        public static DbCommand FindByCompany(DbConnection connection, string sql, string company)
        {
            var command = connection.CreateCommand();
            command.CommandText = sql;
            command.CommandType = CommandType.Text;

            AddStringParameter(command, "@Symbol", DbType.String, company);
            return command;
        }
    }
}
