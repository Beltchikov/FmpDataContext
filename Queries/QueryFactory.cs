using FmpDataContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace FmpDataContext.Queries
{
    /// <summary>
    /// QueryFactory
    /// </summary>
    public class QueryFactory : IQueryFactory
    {
        private readonly object lockObject = new object();
        string _connectionString;

        private CompounderQuery _compounderQuery;
        private WithBestRoeQuery _withBestRoeQuery;
        private RoeHistoryQuery _roeHistoryQuery;
        private RevenueHistoryQuery _revenueHistoryQuery;
        private ReinvestmentHistoryQuery _reinvestmentHistoryQuery;
        private OperatingIncomeHistoryQuery _operatingIncomeHistoryQuery;
        private EpsHistoryQuery _epsHistoryQuery;
        private IncrementalRoeQuery _incrementalRoeQuery;
        private CashConversionQuery _cashConversionQuery;
        private CompanyNameQuery _companyNameQuery;
        private CountByYearsQuery _countByYearsQuery;
        private SymbolByCompanyQuery _symbolByCompanyQuery;

        private QueryFactory() { }

        public QueryFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// CompounderQuery
        /// </summary>
        public CompounderQuery CompounderQuery
        {
            get
            {
                lock (lockObject)
                {
                    if (_compounderQuery == null)
                    {
                        _compounderQuery = new CompounderQuery(DataContext.Instance(_connectionString));
                    }
                    return _compounderQuery;
                }
            }
        }

        /// <summary>
        /// WithBestRoeQuery
        /// </summary>
        public WithBestRoeQuery WithBestRoeQuery
        {
            get
            {
                lock (lockObject)
                {
                    if (_withBestRoeQuery == null)
                    {
                        _withBestRoeQuery = new WithBestRoeQuery(DataContext.Instance(_connectionString));
                    }
                    return _withBestRoeQuery;
                }
            }
        }

        /// <summary>
        /// RoeHistoryQuery
        /// </summary>
        public RoeHistoryQuery RoeHistoryQuery
        {
            get
            {
                lock (lockObject)
                {
                    if (_roeHistoryQuery == null)
                    {
                        _roeHistoryQuery = new RoeHistoryQuery(DataContext.Instance(_connectionString));
                    }
                    return _roeHistoryQuery;
                }
            }
        }

        /// <summary>
        /// RevenueHistoryQuery
        /// </summary>
        public RevenueHistoryQuery RevenueHistoryQuery
        {
            get
            {
                lock (lockObject)
                {
                    if (_revenueHistoryQuery == null)
                    {
                        _revenueHistoryQuery = new RevenueHistoryQuery(DataContext.Instance(_connectionString));
                    }
                    return _revenueHistoryQuery;
                }
            }
        }

        /// <summary>
        /// ReinvestmentHistoryQuery
        /// </summary>
        public ReinvestmentHistoryQuery ReinvestmentHistoryQuery
        {
            get
            {
                lock (lockObject)
                {
                    if (_reinvestmentHistoryQuery == null)
                    {
                        _reinvestmentHistoryQuery = new ReinvestmentHistoryQuery(DataContext.Instance(_connectionString));
                    }
                    return _reinvestmentHistoryQuery;
                }
            }
        }

        /// <summary>
        /// OperatingIncomeHistoryQuery
        /// </summary>
        public OperatingIncomeHistoryQuery OperatingIncomeHistoryQuery
        {
            get
            {
                lock (lockObject)
                {
                    if (_operatingIncomeHistoryQuery == null)
                    {
                        _operatingIncomeHistoryQuery = new OperatingIncomeHistoryQuery(DataContext.Instance(_connectionString));
                    }
                    return _operatingIncomeHistoryQuery;
                }
            }
        }

        /// <summary>
        /// EpsHistoryQuery
        /// </summary>
        public EpsHistoryQuery EpsHistoryQuery
        {
            get
            {
                lock (lockObject)
                {
                    if (_epsHistoryQuery == null)
                    {
                        _epsHistoryQuery = new EpsHistoryQuery(DataContext.Instance(_connectionString));
                    }
                    return _epsHistoryQuery;
                }
            }
        }

        /// <summary>
        /// IncrementalRoeQuery
        /// </summary>
        public IncrementalRoeQuery IncrementalRoeQuery
        {
            get
            {
                lock (lockObject)
                {
                    if (_incrementalRoeQuery == null)
                    {
                        _incrementalRoeQuery = new IncrementalRoeQuery(DataContext.Instance(_connectionString));
                    }
                    return _incrementalRoeQuery;
                }
            }
        }

        /// <summary>
        /// CompanyNameQuery
        /// </summary>
        public CompanyNameQuery CompanyNameQuery
        {
            get
            {
                lock (lockObject)
                {
                    if (_companyNameQuery == null)
                    {
                        _companyNameQuery = new CompanyNameQuery(DataContext.Instance(_connectionString));
                    }
                    return _companyNameQuery;
                }
            }
        }

        /// <summary>
        /// CashConversionQuery
        /// </summary>
        public CashConversionQuery CashConversionQuery
        {
            get
            {
                lock (lockObject)
                {
                    if (_cashConversionQuery == null)
                    {
                        _cashConversionQuery = new CashConversionQuery(DataContext.Instance(_connectionString));
                    }
                    return _cashConversionQuery;
                }
            }
        }

        /// <summary>
        /// CountByYearsQuery
        /// </summary>
        public CountByYearsQuery CountByYearsQuery
        {
            get
            {
                lock (lockObject)
                {
                    if (_countByYearsQuery == null)
                    {
                        _countByYearsQuery = new CountByYearsQuery(DataContext.Instance(_connectionString));
                    }
                    return _countByYearsQuery;
                }
            }
        }

        /// <summary>
        /// SymbolByCompanyQuery
        /// </summary>
        public SymbolByCompanyQuery SymbolByCompanyQuery
        {
            get
            {
                lock (lockObject)
                {
                    if (_symbolByCompanyQuery == null)
                    {
                        _symbolByCompanyQuery = new SymbolByCompanyQuery(DataContext.Instance(_connectionString));
                    }
                    return _symbolByCompanyQuery;
                }
            }
        }

    }
}
