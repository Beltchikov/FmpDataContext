using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FmpDataContext.Queries
{
    /// <summary>
    /// ResultSetList
    /// </summary>
    public class ResultSetListReinvestment
    {
        private List<ResultSetReinvestment> _resultSetList;

        ResultSetListReinvestment() { }

        /// <summary>
        /// ResultSetList
        /// </summary>
        /// <param name="resultSetList"></param>
        public ResultSetListReinvestment(List<ResultSetReinvestment> resultSetList)
        {
            _resultSetList = resultSetList;
        }

        /// <summary>
        /// CountTotal
        /// </summary>
        public int CountTotal { get; set; }

        /// <summary>
        /// ResultSets
        /// </summary>
        public List<ResultSetReinvestment> ResultSets
        {
            get
            {
                return _resultSetList;
            }
        }
    }
}
