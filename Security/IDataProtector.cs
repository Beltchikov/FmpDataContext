using FmpDataContext.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FmpDataContext.Security
{
    public interface IDataProtector
    {
        ResultSetListReinvestment Protect(ResultSetListReinvestment data);
    }
}
