using FmpDataContext.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace FmpDataContext
{
    /// <summary>
    /// DataContext
    /// </summary>
    public class DataContext : DbContext
    {
        private static DataContext _dataContext;
        private static readonly object lockObject = new object();
        private string _connectionString;

        public DataContext()
        {
        }

        public DataContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public static DataContext Instance(string connectionString)
        {
            lock (lockObject)
            {
                if (_dataContext == null)
                {
                    _dataContext = new DataContext(connectionString);
                }
                return _dataContext;
            }
        }

        /// <summary>
        /// OnConfiguring
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (string.IsNullOrWhiteSpace(_connectionString))
            {
                _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FmpData;Integrated Security=True;";
            }
            optionsBuilder.UseSqlServer(_connectionString);

            base.OnConfiguring(optionsBuilder);
        }

        /// <summary>
        /// OnModelCreating
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IncomeStatement>().HasKey(p => new { p.Symbol, p.Date });
            modelBuilder.Entity<BalanceSheet>().HasKey(p => new { p.Symbol, p.Date });
            modelBuilder.Entity<CashFlowStatement>().HasKey(p => new { p.Symbol, p.Date });
        }

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<DataTransfer> DataTransfer { get; set; }
        public DbSet<Batch> Batches { get; set; }
        public DbSet<IncomeStatement> IncomeStatements { get; set; }
        public DbSet<BalanceSheet> BalanceSheets { get; set; }
        public DbSet<CashFlowStatement> CashFlowStatements { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<NotResolved> NotResolved { get; set; }
        public DbSet<NotUnique> NotUnique { get; set; }
        public DbSet<FmpSymbolCompany> FmpSymbolCompany { get; set; }
        public DbSet<ImportErrorFmpSymbol> ImportErrorFmpSymbol { get; set; }
        public string ConnectionString
        {
            get
            {
                return _connectionString;
            }
        }
    }
}
