using FmpDataContext.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FmpDataContext.Security
{
    public class DataProtector : IDataProtector
    {
        public ResultSetList Protect(ResultSetList data)
        {
            const string MASK = "XXXX";
            
            for(int i = 0; i < data.ResultSets.Count; i++)
            {
                var resultSet = data.ResultSets[i];
                if(resultSet.Symbol.Contains("."))
                {
                    resultSet.Symbol = MASK;
                    resultSet.Name = MASK;
                }
            }

            return data;
        }
    }
}
