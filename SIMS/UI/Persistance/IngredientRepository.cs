using SIMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS.UI.Persistance
{
    public class IngredientRepository : Repository<Ingredient>
    {
        public override IEnumerable<Entity> Search (string term = "", string sort = "")
        {
            List<Entity> result = new List<Entity>();

            foreach(Entity entity in ApplicationContext.Instance.Ingredients)
            {
                if(((Ingredient)entity).Name.ToLower().Contains(term.ToLower()) ||
                    ((Ingredient)entity).Description.ToLower().Contains(term.ToLower()) )
                {
                    result.Add(entity);
                }
            }

            if (sort == "Name ASC")
            {
                result = result.OrderBy(o => ((Ingredient)o).Name).ToList();
            }

            if (sort == "Name DSC")
            {
                result = result.OrderByDescending(o => ((Ingredient)o).Name).ToList();
            }

            if (sort == "By freqency")
            {
                result = result.OrderByDescending(o => GetFrequency((Ingredient)o)).ToList();
            }


            return result;
        }


        public double GetFrequency(Ingredient ingredient) 
        {
            double freq = 0;

            foreach (Entity medicine in ApplicationContext.Instance.Medicines) 
            {
                Medicine med = medicine as Medicine;

                if (med.Ingredients.ContainsKey(ingredient)) 
                {
                    freq += med.Ingredients[ingredient];
                }
            }



            return freq;
        }
    }
}
