using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FmpDataContext.Model
{
    public class ImportErrorFmpSymbol
    {
        [Key]
        public string Symbol { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
