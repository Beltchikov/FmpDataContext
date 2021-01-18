using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FmpDataContext
{
    public class FmpHelper
    {
        /// <summary>
        /// BuildDatesList
        /// </summary>
        /// <param name="yearFrom"></param>
        /// <param name="yearTo"></param>
        /// <param name="dates"></param>
        /// <returns></returns>
        public static List<string> BuildDatesList(int yearFrom, int yearTo, List<string> dates)
        {
            List<string> resultList = new List<string>();

            for (int year = yearFrom; year <= yearTo; year++)
            {
                foreach (var date in dates)
                {
                    resultList.Add(year.ToString() + date[4..]);
                }
            }

            return resultList;
        }
    }
}
