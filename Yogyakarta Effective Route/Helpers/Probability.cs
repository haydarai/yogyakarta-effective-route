using System.Collections.Generic;
using System.Linq;

namespace Yogyakarta_Effective_Route.Helpers
{
    public static class Probability
    {
        public static List<double> CalcProbability(List<double> fitness)
        {
            List<double> probabilities = new List<double>();
            for (int i = 0; i < fitness.Count(); i++)
            {
                double probability = 1 / (fitness.ElementAt(i) / fitness.Sum());
                probabilities.Add(probability);
            }
            return probabilities;
        }
    }
}
