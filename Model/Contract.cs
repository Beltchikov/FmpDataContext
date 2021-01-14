using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FmpDataContext.Model
{
    public class Contract
    {
        [Key]
        public string Company { get; set; }
        public string Symbol { get; set; }
        public string Exchange { get; set; }
        public string Currency { get; set; }
        public string SecType { get; set; }
    }
}
