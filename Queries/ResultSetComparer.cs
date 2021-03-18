using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace FmpDataContext.Queries
{
    /// <summary>
    /// ResultSetComparer
    /// </summary>
    public class ResultSetComparer : IEqualityComparer<ResultSetReinvestment>
    {
        public bool Equals(ResultSetReinvestment x, ResultSetReinvestment y)
        {
            if (x == null && y == null)
            {
                return true;
            }

            if (x == null || y == null)
            {
                return false;
            }

            return x.Symbol.ToUpper() == y.Symbol.ToUpper();
        }

        public int GetHashCode([DisallowNull] ResultSetReinvestment obj)
        {
            int hash = Encoding.Unicode.GetBytes(obj.Symbol.ToUpper()).Aggregate((r, n) => (byte)(r + n));
            return hash;
        }
    }
}