using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yogyakarta_Effective_Route.Models;

namespace Yogyakarta_Effective_Route.Helpers
{
    public class Crossover
    {
        List<int> offspring;
        List<int> offspring2;

        public void Order(List<int> parent1, List<int> parent2)
        {
            //generating crossover point 
            int index = 0;
            int index2 = parent1.Count() / 2;

            offspring = new List<int>();
            offspring2 = new List<int>();
            for (int i = 0; i < parent1.Count; i++)
            {
                offspring.Add(0);
                offspring2.Add(0);
            }
            //assigning offspring value
            for (int i = index; i <= index2; i++)
            {
                offspring[i] = parent1.ElementAt(i);
                offspring2[i] = parent2.ElementAt(i);
            }
            //generating offspring
            offspring = generateOffSpring(index, index2, parent2, offspring);
            offspring2 = generateOffSpring(index, index2, parent1, offspring2);
        }

        private List<int> generateOffSpring(int index, int index2, List<int> parent1, List<int> offspring)
        {
            int navigator = index2 + 1;
            int offspringnavigator = navigator;
            do
            {
                if (!offspring.Contains(parent1.ElementAt(navigator)))
                {
                    offspring[offspringnavigator] = parent1.ElementAt(navigator);
                    offspringnavigator++;
                    if (offspringnavigator >= parent1.Count())
                    {
                        offspringnavigator = 0;
                    }
                }
                navigator++;
                if (navigator >= parent1.Count())
                {
                    navigator = 0;
                }
            } while (navigator != index2 + 1);
            return offspring;
        }

        public List<int> getOffspring()
        {
            return offspring;
        }
        public List<int> getOffspring2()
        {
            return offspring2;
        }
    }
}
