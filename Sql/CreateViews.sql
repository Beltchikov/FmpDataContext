USE FmpData
GO

IF object_id(N'dbo.ViewCompounder', 'V') IS NOT NULL
	DROP VIEW dbo.ViewCompounder
GO

CREATE VIEW dbo.ViewCompounder AS

select 
i.Symbol,
i.Date,
i.NetIncome * 100/nullif(b.TotalStockholdersEquity, 0) Roe,
c.CapitalExpenditure *-100 /nullif(i.NetIncome, 0) ReinvestmentRate,
b.TotalStockholdersEquity Equity,
b.TotalLiabilities Debt,
i.NetIncome,
b.TotalLiabilities/nullif(b.TotalStockholdersEquity,0) DebtEquityRatio
from IncomeStatements i
inner join BalanceSheets b on i.Symbol = b.Symbol and i.Date = b.Date
inner join CashFlowStatements c on i.Symbol = c.Symbol and i.Date = c.Date

-- select top 100 * from ViewCompounder
-- select count(*) from ViewCompounder