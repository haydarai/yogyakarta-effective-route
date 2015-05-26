using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yogyakarta_Effective_Route.Helpers
{
    public class Mutation
    {
        public static List<int> SwapMutation(List<int> genes)
        {
            //generating crossover point
            Random rnd = new Random();

            if (rnd.NextDouble() < 0.02)
            {
                int index = rnd.Next(1, (genes.Count() - 1) / 2);
                int index2 = rnd.Next((genes.Count() - 1) / 2, genes.Count());

                //swapping 2 points
                int temp = genes[index];
                genes[index] = genes[index2];
                genes[index2] = temp;
            }
            return genes;
        }
    }
}
