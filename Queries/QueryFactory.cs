﻿using FmpDataContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace FmpDataContext.Queries
{
    /// <summary>
    /// QueryFactory
    /// </summary>
    public class QueryFactory
    {
        private static readonly object lockObject = new object();

        private static CompounderQuery _compounderQuery;
        private static WithBestRoeQuery _withBestRoeQuery;
        private static RoeHistoryQuery _roeHistoryQuery;
        private static RevenueHistoryQuery _revenueHistoryQuery;
        private static ReinvestmentHistoryQuery _reinvestmentHistoryQuery;
        private static OperatingIncomeHistoryQuery _operatingIncomeHistoryQuery;
        private static EpsHistoryQuery _epsHistoryQuery;
        private static IncrementalRoeQuery _incrementalRoeQuery;
        private static CashConversionQuery _cashConversionQuery;
        private static CompanyNameQuery _companyNameQuery;
        private static CountByYearsQuery _countByYearsQuery;
        private static SymbolByCompanyQuery _symbolByCompanyQuery;
        

        /// <summary>
        /// CompounderQuery
        /// </summary>
        public static CompounderQuery CompounderQuery
        {
            get
            {
                lock (lockObject)
                {
                    if (_compounderQuery == null)
                    {
                        _compounderQuery = new CompounderQuery(DataContext.Instance(Configuration.Instance["ConnectionString"]));
                    }
                    return _compounderQuery;
                }
            }
        }

        /// <summary>
        /// WithBestRoeQuery
        /// </summary>
        public static WithBestRoeQuery WithBestRoeQuery
        {
            get
            {
                lock (lockObject)
                {
                    if (_withBestRoeQuery == null)
                    {
                        _withBestRoeQuery = new WithBestRoeQuery(DataContext.Instance(Configuration.Instance["ConnectionString"]));
                    }
                    return _withBestRoeQuery;
                }
            }
        }

        /// <summary>
        /// RoeHistoryQuery
        /// </summary>
        public static RoeHistoryQuery RoeHistoryQuery
        {
            get
            {
                lock (lockObject)
                {
                    if (_roeHistoryQuery == null)
                    {
                        _roeHistoryQuery = new RoeHistoryQuery(DataContext.Instance(Configuration.Instance["ConnectionString"]));
                    }
                    return _roeHistoryQuery;
                }
            }
        }

        /// <summary>
        /// RevenueHistoryQuery
        /// </summary>
        public static RevenueHistoryQuery RevenueHistoryQuery
        {
            get
            {
                lock (lockObject)
                {
                    if (_revenueHistoryQuery == null)
                    {
                        _revenueHistoryQuery = new RevenueHistoryQuery(DataContext.Instance(Configuration.Instance["ConnectionString"]));
                    }
                    return _revenueHistoryQuery;
                }
            }
        }

        /// <summary>
        /// ReinvestmentHistoryQuery
        /// </summary>
        public static ReinvestmentHistoryQuery ReinvestmentHistoryQuery
        {
            get
            {
                lock (lockObject)
                {
                    if (_reinvestmentHistoryQuery == null)
                    {
                        _reinvestmentHistoryQuery = new ReinvestmentHistoryQuery(DataContext.Instance(Configuration.Instance["ConnectionString"]));
                    }
                    return _reinvestmentHistoryQuery;
                }
            }
        }

        /// <summary>
        /// OperatingIncomeHistoryQuery
        /// </summary>
        public static OperatingIncomeHistoryQuery OperatingIncomeHistoryQuery
        {
            get
            {
                lock (lockObject)
                {
                    if (_operatingIncomeHistoryQuery == null)
                    {
                        _operatingIncomeHistoryQuery = new OperatingIncomeHistoryQuery(DataContext.Instance(Configuration.Instance["ConnectionString"]));
                    }
                    return _operatingIncomeHistoryQuery;
                }
            }
        }

        /// <summary>
        /// EpsHistoryQuery
        /// </summary>
        public static EpsHistoryQuery EpsHistoryQuery
        {
            get
            {
                lock (lockObject)
                {
                    if (_epsHistoryQuery == null)
                    {
                        _epsHistoryQuery = new EpsHistoryQuery(DataContext.Instance(Configuration.Instance["ConnectionString"]));
                    }
                    return _epsHistoryQuery;
                }
            }
        }

        /// <summary>
        /// IncrementalRoeQuery
        /// </summary>
        public static IncrementalRoeQuery IncrementalRoeQuery
        {
            get
            {
                lock (lockObject)
                {
                    if (_incrementalRoeQuery == null)
                    {
                        _incrementalRoeQuery = new IncrementalRoeQuery(DataContext.Instance(Configuration.Instance["ConnectionString"]));
                    }
                    return _incrementalRoeQuery;
                }
            }
        }

        /// <summary>
        /// CompanyNameQuery
        /// </summary>
        public static CompanyNameQuery CompanyNameQuery
        {
            get
            {
                lock (lockObject)
                {
                    if (_companyNameQuery == null)
                    {
                        _companyNameQuery = new CompanyNameQuery(DataContext.Instance(Configuration.Instance["ConnectionString"]));
                    }
                    return _companyNameQuery;
                }
            }
        }

        /// <summary>
        /// CashConversionQuery
        /// </summary>
        public static CashConversionQuery CashConversionQuery
        {
            get
            {
                lock (lockObject)
                {
                    if (_cashConversionQuery == null)
                    {
                        _cashConversionQuery = new CashConversionQuery(DataContext.Instance(Configuration.Instance["ConnectionString"]));
                    }
                    return _cashConversionQuery;
                }
            }
        }

        /// <summary>
        /// CountByYearsQuery
        /// </summary>
        public static CountByYearsQuery CountByYearsQuery
        {
            get
            {
                lock (lockObject)
                {
                    if (_countByYearsQuery == null)
                    {
                        _countByYearsQuery = new CountByYearsQuery(DataContext.Instance(Configuration.Instance["ConnectionString"]));
                    }
                    return _countByYearsQuery;
                }
            }
        }

        /// <summary>
        /// SymbolByCompanyQuery
        /// </summary>
        public static SymbolByCompanyQuery SymbolByCompanyQuery
        {
            get
            {
                lock (lockObject)
                {
                    if (_symbolByCompanyQuery == null)
                    {
                        _symbolByCompanyQuery = new SymbolByCompanyQuery(DataContext.Instance(Configuration.Instance["ConnectionString"]));
                    }
                    return _symbolByCompanyQuery;
                }
            }
        }

    }
}
