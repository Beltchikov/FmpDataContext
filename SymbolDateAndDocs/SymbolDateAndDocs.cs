using FmpDataContext.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FmpDataContext.SymbolDateAndDocs
{
    public class SymbolDateAndDocs
    {
        public string Symbol { get; set; }
        public IncomeStatement IncomeStatement { get; set; }
        public BalanceSheet BalanceSheet { get; set; }
        public CashFlowStatement CashFlowStatement { get; set; }
        public bool Completed
        {
            get
            {
                return IncomeStatement != null && BalanceSheet != null && CashFlowStatement != null;
            }
        }

        public bool Persisted { get; set; }
       
        public bool PersistenceFailed { get; set; }
    }
}
