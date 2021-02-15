namespace FmpDataContext.Queries
{
    public interface IQueryFactory
    {
        CashConversionQuery CashConversionQuery { get; }
        CompanyNameQuery CompanyNameQuery { get; }
        CompounderQuery CompounderQuery { get; }
        CountByYearsQuery CountByYearsQuery { get; }
        EpsHistoryQuery EpsHistoryQuery { get; }
        IncrementalRoeQuery IncrementalRoeQuery { get; }
        OperatingIncomeHistoryQuery OperatingIncomeHistoryQuery { get; }
        ReinvestmentHistoryQuery ReinvestmentHistoryQuery { get; }
        RevenueHistoryQuery RevenueHistoryQuery { get; }
        RoeHistoryQuery RoeHistoryQuery { get; }
        SymbolByCompanyQuery SymbolByCompanyQuery { get; }
        WithBestRoeQuery WithBestRoeQuery { get; }
    }
}