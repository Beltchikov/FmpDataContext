using FmpDataContext.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FmpDataContext.Queries
{
    /// <summary>
    /// CompounderQueryParams
    /// </summary>
    public class CompounderQueryParams : CompounderCountQueryParams
    {
        public string OrderBy { get; set; }
        public bool Descending { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int MaxResultCount { get; set; }
        
    }
}
