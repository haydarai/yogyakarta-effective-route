using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yogyakarta_Effective_Route.Models;

namespace Yogyakarta_Effective_Route.Helpers
{
    public static class Selection
    {
        public static List<Population> RWSSelection(Population population)
        {
            Population selectedparents = new Population();
            selectedparents.genes = new List<Gene>();
            Population notselectedparents = new Population();
            notselectedparents.genes = new List<Gene>();
            List<double> fitness = new List<double>();
            for (int i = 0; i < population.genes.Count; i++)
            {
                fitness.Add(population.genes[i].fitness);
            }
            List<double> cumulatives = Cumulative.CalcCumulative((Probability.CalcProbability(fitness)));
            Random rnd = new Random();
            for (int i = 0; i < population.genes.Count; i++)
            {
                int selectedindex = pickIndexOfGene(rnd.NextDouble(), cumulatives);
                if (!selectedparents.genes.Contains(population.genes[selectedindex]))
                {
                    selectedparents.genes.Add(population.genes[i]);
                }
                else
                {
                    notselectedparents.genes.Add(population.genes[i]);
                }
            }
            List<Population> result = new List<Population>();
            result.Add(selectedparents);
            result.Add(notselectedparents);
            return result;
        }

        private static int pickIndexOfGene(double value, List<double> cumulatives)
        {
            for (int i = 0; i < cumulatives.Count(); i++)
            {
                if (value > cumulatives[i] && value < cumulatives[i + 1])
                {
                    return i;
                }
            }
            return 0;
        }
    }
}
