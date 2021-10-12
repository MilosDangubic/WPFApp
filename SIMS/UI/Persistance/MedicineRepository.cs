using SIMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS.UI.Persistance
{
    public class MedicineRepository : Repository<Medicine>
    {

        public override IEnumerable<Entity> GetAll()
        {
            List<Entity> result = new List<Entity>();

            foreach (Entity entity in ApplicationContext.Instance.Medicines)
            {
                if (((Medicine)entity).Deleted)
                {
                    continue;
                }

                if (ApplicationContext.Instance.User != null && ApplicationContext.Instance.User.Usertype == "Pharmacists" && ((Medicine)entity).Accepted) 
                {
                    result.Add(entity);
                }
                else if (ApplicationContext.Instance.User != null && ApplicationContext.Instance.User.Usertype == "Patient")
                {
                    result.Add(entity);
                }
                else if (ApplicationContext.Instance.User != null && ApplicationContext.Instance.User.Usertype == "Doctor")
                {
                    result.Add(entity);
                }

            }

            return result;
        }

        public override IEnumerable<Entity> Search (string term = "", string sort = "")
        {
            List<Entity> result = new List<Entity>();

            foreach (Entity entity in ApplicationContext.Instance.Medicines)
            {
                if(((Medicine)entity).Name.ToLower().Contains(term.ToLower()) ||
                    ((Medicine)entity).Manufacturer.ToLower().Contains(term.ToLower()) ||
                    ((Medicine)entity).Price.ToLower().Contains(term.ToLower()) ||
                    ((Medicine)entity).Quantity.ToLower().Contains(term.ToLower()) ||
                    ((Medicine)entity).Ingredient.ToLower().Contains(term.ToLower()))
                {
                    result.Add(entity);
                }


                foreach (KeyValuePair<Ingredient, double> ing in ((Medicine)entity).Ingredients) 
                {
                    if (ing.Key.Name.ToLower().Contains(term.ToLower()) && !IsExist(result, entity.ID)) 
                    {
                        result.Add(entity);
                        break;
                    }
                }

            }

            if (sort == "Name ASC")
            {
                result = result.OrderBy(o => ((Medicine)o).Name).ToList();
            }

            if (sort == "Name DSC")
            {
                result = result.OrderByDescending(o => ((Medicine)o).Name).ToList();
            }

            if (sort == "Price ASC")
            {
                result = result.OrderBy(o => ((Medicine)o).Price).ToList();
            }

            if (sort == "Price DSC")
            {
                result = result.OrderByDescending(o => ((Medicine)o).Price).ToList();
            }

            if (sort == "Quantity ASC")
            {
                result = result.OrderBy(o => double.Parse(((Medicine)o).Quantity)).ToList();
            }

            if (sort == "Quantity DSC")
            {
                result = result.OrderByDescending(o => double.Parse(((Medicine)o).Quantity)).ToList();
            }

            return result;

        }


        private bool IsExist(IEnumerable<Entity> entities, string id) 
        {
            foreach (Entity entity in entities) 
            {
                if (entity.ID == id) 
                {
                    return true;
                }
            }

            return false;
        }
    }
}
