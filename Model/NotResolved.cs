using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FmpDataContext.Model
{
    public class NotResolved
    {
        [Key]
        public string Company { get; set; }
    }
}
