select top 100 b.TotalStockholdersEquity * 100/nullif(i.NetIncome, 0) Roe from IncomeStatements i
inner join BalanceSheets b on i.Symbol = b.Symbol and i.Date = b.Date
inner join CashFlowStatements c on i.Symbol = c.Symbol and i.Date = c.Date