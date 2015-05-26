using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yogyakarta_Effective_Route.Helpers
{
    public static class Cumulative
    {
        public static List<double> CalcCumulative(List<double> probabilities)
        {
            int startingPoint = 0;
            List<double> cumulatives = new List<double>();
            cumulatives.Add(startingPoint);
            for (int i = 0; i < probabilities.Count(); i++)
            {
                double point = probabilities.ElementAt(i) + cumulatives.ElementAt(i);
                cumulatives.Add(point);
            }
            return cumulatives;
        }
    }
}
