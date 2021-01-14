using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FmpDataContext.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BalanceSheets",
                columns: table => new
                {
                    Date = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FillingDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AcceptedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Period = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CashAndCashEquivalents = table.Column<double>(type: "float", nullable: false),
                    ShortTermInvestments = table.Column<double>(type: "float", nullable: false),
                    CashAndShortTermInvestments = table.Column<double>(type: "float", nullable: false),
                    NetReceivables = table.Column<double>(type: "float", nullable: false),
                    Inventory = table.Column<double>(type: "float", nullable: false),
                    OtherCurrentAssets = table.Column<double>(type: "float", nullable: false),
                    TotalCurrentAssets = table.Column<double>(type: "float", nullable: false),
                    PropertyPlantEquipmentNet = table.Column<double>(type: "float", nullable: false),
                    Goodwill = table.Column<double>(type: "float", nullable: false),
                    IntangibleAssets = table.Column<double>(type: "float", nullable: false),
                    GoodwillAndIntangibleAssets = table.Column<double>(type: "float", nullable: false),
                    LongTermInvestments = table.Column<double>(type: "float", nullable: false),
                    TaxAssets = table.Column<double>(type: "float", nullable: false),
                    OtherNonCurrentAssets = table.Column<double>(type: "float", nullable: false),
                    TotalNonCurrentAssets = table.Column<double>(type: "float", nullable: false),
                    OtherAssets = table.Column<double>(type: "float", nullable: false),
                    TotalAssets = table.Column<double>(type: "float", nullable: false),
                    AccountPayables = table.Column<double>(type: "float", nullable: false),
                    ShortTermDebt = table.Column<double>(type: "float", nullable: false),
                    TaxPayables = table.Column<double>(type: "float", nullable: false),
                    DeferredRevenue = table.Column<double>(type: "float", nullable: false),
                    OtherCurrentLiabilities = table.Column<double>(type: "float", nullable: false),
                    TotalCurrentLiabilities = table.Column<double>(type: "float", nullable: false),
                    LongTermDebt = table.Column<double>(type: "float", nullable: false),
                    DeferredRevenueNonCurrent = table.Column<double>(type: "float", nullable: false),
                    DeferredTaxLiabilitiesNonCurrent = table.Column<double>(type: "float", nullable: false),
                    OtherNonCurrentLiabilities = table.Column<double>(type: "float", nullable: false),
                    TotalNonCurrentLiabilities = table.Column<double>(type: "float", nullable: false),
                    OtherLiabilities = table.Column<double>(type: "float", nullable: false),
                    TotalLiabilities = table.Column<double>(type: "float", nullable: false),
                    CommonStock = table.Column<double>(type: "float", nullable: false),
                    RetainedEarnings = table.Column<double>(type: "float", nullable: false),
                    AccumulatedOtherComprehensiveIncomeLoss = table.Column<double>(type: "float", nullable: false),
                    OthertotalStockholdersEquity = table.Column<double>(type: "float", nullable: false),
                    TotalStockholdersEquity = table.Column<double>(type: "float", nullable: false),
                    TotalLiabilitiesAndStockholdersEquity = table.Column<double>(type: "float", nullable: false),
                    TotalInvestments = table.Column<double>(type: "float", nullable: false),
                    TotalDebt = table.Column<double>(type: "float", nullable: false),
                    NetDebt = table.Column<double>(type: "float", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinalLink = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BalanceSheets", x => new { x.Symbol, x.Date });
                });

            migrationBuilder.CreateTable(
                name: "Batches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataTransferId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartSymbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndSymbol = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Batches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CashFlowStatements",
                columns: table => new
                {
                    Date = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FillingDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AcceptedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Period = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NetIncome = table.Column<double>(type: "float", nullable: false),
                    DepreciationAndAmortization = table.Column<double>(type: "float", nullable: false),
                    DeferredIncomeTax = table.Column<double>(type: "float", nullable: false),
                    StockBasedCompensation = table.Column<double>(type: "float", nullable: false),
                    ChangeInWorkingCapital = table.Column<double>(type: "float", nullable: false),
                    AccountsReceivables = table.Column<double>(type: "float", nullable: false),
                    Inventory = table.Column<double>(type: "float", nullable: false),
                    AccountsPayables = table.Column<double>(type: "float", nullable: false),
                    OtherWorkingCapital = table.Column<double>(type: "float", nullable: false),
                    OtherNonCashItems = table.Column<double>(type: "float", nullable: false),
                    NetCashProvidedByOperatingActivities = table.Column<double>(type: "float", nullable: false),
                    InvestmentsInPropertyPlantAndEquipment = table.Column<double>(type: "float", nullable: false),
                    AcquisitionsNet = table.Column<double>(type: "float", nullable: false),
                    PurchasesOfInvestments = table.Column<double>(type: "float", nullable: false),
                    SalesMaturitiesOfInvestments = table.Column<double>(type: "float", nullable: false),
                    OtherInvestingActivites = table.Column<double>(type: "float", nullable: false),
                    NetCashUsedForInvestingActivites = table.Column<double>(type: "float", nullable: false),
                    DebtRepayment = table.Column<double>(type: "float", nullable: false),
                    CommonStockIssued = table.Column<double>(type: "float", nullable: false),
                    CommonStockRepurchased = table.Column<double>(type: "float", nullable: false),
                    DividendsPaid = table.Column<double>(type: "float", nullable: false),
                    OtherFinancingActivites = table.Column<double>(type: "float", nullable: false),
                    NetCashUsedProvidedByFinancingActivities = table.Column<double>(type: "float", nullable: false),
                    EffectOfForexChangesOnCash = table.Column<double>(type: "float", nullable: false),
                    NetChangeInCash = table.Column<double>(type: "float", nullable: false),
                    CashAtEndOfPeriod = table.Column<double>(type: "float", nullable: false),
                    CashAtBeginningOfPeriod = table.Column<double>(type: "float", nullable: false),
                    OperatingCashFlow = table.Column<double>(type: "float", nullable: false),
                    CapitalExpenditure = table.Column<double>(type: "float", nullable: false),
                    FreeCashFlow = table.Column<double>(type: "float", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinalLink = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashFlowStatements", x => new { x.Symbol, x.Date });
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Company = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Exchange = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Company);
                });

            migrationBuilder.CreateTable(
                name: "DataTransfer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataTransfer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IncomeStatements",
                columns: table => new
                {
                    Date = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FillingDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AcceptedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Period = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Revenue = table.Column<double>(type: "float", nullable: false),
                    CostOfRevenue = table.Column<double>(type: "float", nullable: false),
                    GrossProfit = table.Column<double>(type: "float", nullable: false),
                    GrossProfitRatio = table.Column<double>(type: "float", nullable: false),
                    ResearchAndDevelopmentExpenses = table.Column<double>(type: "float", nullable: false),
                    GeneralAndAdministrativeExpenses = table.Column<double>(type: "float", nullable: false),
                    SellingAndMarketingExpenses = table.Column<double>(type: "float", nullable: false),
                    OtherExpenses = table.Column<double>(type: "float", nullable: false),
                    OperatingExpenses = table.Column<double>(type: "float", nullable: false),
                    CostAndExpenses = table.Column<double>(type: "float", nullable: false),
                    InterestExpense = table.Column<double>(type: "float", nullable: false),
                    DepreciationAndAmortization = table.Column<double>(type: "float", nullable: false),
                    Ebitda = table.Column<double>(type: "float", nullable: false),
                    Ebitdaratio = table.Column<double>(type: "float", nullable: false),
                    OperatingIncome = table.Column<double>(type: "float", nullable: false),
                    OperatingIncomeRatio = table.Column<double>(type: "float", nullable: false),
                    TotalOtherIncomeExpensesNet = table.Column<double>(type: "float", nullable: false),
                    IncomeBeforeTax = table.Column<double>(type: "float", nullable: false),
                    IncomeBeforeTaxRatio = table.Column<double>(type: "float", nullable: false),
                    IncomeTaxExpense = table.Column<double>(type: "float", nullable: false),
                    NetIncome = table.Column<double>(type: "float", nullable: false),
                    NetIncomeRatio = table.Column<double>(type: "float", nullable: false),
                    Eps = table.Column<double>(type: "float", nullable: false),
                    Epsdiluted = table.Column<double>(type: "float", nullable: false),
                    WeightedAverageShsOut = table.Column<double>(type: "float", nullable: false),
                    WeightedAverageShsOutDil = table.Column<double>(type: "float", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinalLink = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeStatements", x => new { x.Symbol, x.Date });
                });

            migrationBuilder.CreateTable(
                name: "NotResolved",
                columns: table => new
                {
                    Company = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotResolved", x => x.Company);
                });

            migrationBuilder.CreateTable(
                name: "NotUnique",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotUnique", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    Symbol = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Exchange = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.Symbol);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_Name",
                table: "Stocks",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BalanceSheets");

            migrationBuilder.DropTable(
                name: "Batches");

            migrationBuilder.DropTable(
                name: "CashFlowStatements");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "DataTransfer");

            migrationBuilder.DropTable(
                name: "IncomeStatements");

            migrationBuilder.DropTable(
                name: "NotResolved");

            migrationBuilder.DropTable(
                name: "NotUnique");

            migrationBuilder.DropTable(
                name: "Stocks");
        }
    }
}
