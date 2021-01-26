-- Outer parameters
declare @yearFrom int;
declare @yearTo int;
declare @datesTemplate table (date nvarchar(450));
declare @dates table (date nvarchar(450));

declare @roeFrom float
declare @reinvestmentRateFrom float

set @yearFrom = 2019
set @yearTo = 2020
insert @datesTemplate(date) values('YYYY-03-31'),('YYYY-04-30'),('YYYY-06-30'),('YYYY-07-31'),('YYYY-08-31'),('YYYY-09-30'),('YYYY-12-31');   

set @roeFrom = 15
set @reinvestmentRateFrom = 50

-- Prepare query parameters
declare @currentYear int;
set @currentYear = @yearFrom

while(@currentYear <= @yearTo)
begin
	insert into @dates
	select replace(date, 'YYYY',@currentYear) from @datesTemplate
	set @currentYear = @currentYear +1;
end;
--select * from @dates

-- Query
select top 100 * from ViewCompounder
where 1=1
and Date in (select date from @dates)
and Roe >= @roeFrom
and ReinvestmentRate >= @reinvestmentRateFrom
order by Roe desc
