using System;
using System.Collections.Generic;
using System.Text;

namespace FmpDataContext.Queries
{
    public class HistoryQueryParams
    {
        public int YearFrom { get; set; }
        public List<string> Dates { get; set; }
        public int HistoryDepth { get; set; }
    }
}
