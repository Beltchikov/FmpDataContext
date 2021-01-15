using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FmpDataContext.SymbolAndDocs
{
    public class SymbolAndDocsComparer : IEqualityComparer<SymbolAndDocs>
    {
        public bool Equals(SymbolAndDocs x, SymbolAndDocs y)
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

        public int GetHashCode([DisallowNull] SymbolAndDocs obj)
        {
            int hash = Encoding.Unicode.GetBytes(obj.Symbol.ToUpper()).Aggregate((r, n) => (byte)(r + n));
            return hash;
        }
    }
}
