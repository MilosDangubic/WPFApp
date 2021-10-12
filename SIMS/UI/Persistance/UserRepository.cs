using SIMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS.UI.Persistance
{
    public class UserRepository : Repository<User>
    {
        public User GetUserWithUsernameAndPassword(string email, string passwor) 
        {
            foreach (Entity entity in ApplicationContext.Instance.Users)
            {
                if (((User)entity).Email == email && ((User)entity).Password == passwor)
                {
                    return entity as User;
                }
            }

            return null;
        }

        public User GetUserWithJMBG(string jmbg)
        {
            foreach (Entity entity in ApplicationContext.Instance.Users)
            {
                if (((User)entity).JMBG == jmbg)

                {
                    return entity as User;
                }
            }

            return null;
        }

        public User GetUserWithEmail(string email)
        {
            foreach (Entity entity in ApplicationContext.Instance.Users)
            {
                if (((User)entity).Email == email)

                {
                    return entity as User;
                }
            }

            return null;
        }


        public override IEnumerable<Entity> Search(string term="", string sort = "")
        {
            List<Entity> result = new List<Entity>();

            foreach (Entity entity in ApplicationContext.Instance.Users)
            {
                if (((User)entity).Firstname.ToLower().Contains(term.ToLower()) ||
                    ((User)entity).Lastname.ToLower().Contains(term.ToLower()) ||
                    ((User)entity).JMBG.ToLower().Contains(term.ToLower()) ||
                    ((User)entity).Email.ToLower().Contains(term.ToLower()) ||
                    ((User)entity).Cellphone.ToLower().Contains(term.ToLower()) ||
                    ((User)entity).Usertype.ToLower().Contains(term.ToLower()))
                {
                    result.Add(entity);
                }
            }

            if (sort == "Name ASC") 
            {
                result = result.OrderBy(o => ((User)o).Firstname).ToList();
            }

            if (sort == "Name DSC")
            {
                result = result.OrderByDescending(o => ((User)o).Firstname).ToList();
            }

            if (sort == "Last name ASC")
            {
                result = result.OrderBy(o => ((User)o).Lastname).ToList();
            }

            if (sort == "Last name DSC")
            {
                result = result.OrderByDescending(o => ((User)o).Lastname).ToList();
            }

            if (sort == "User type ASC")
            {
                result = result.OrderBy(o => ((User)o).Usertype).ToList();
            }

            if (sort == "User type DSC")
            {
                result = result.OrderByDescending(o => ((User)o).Usertype).ToList();
            }

            return result;

        }


    }
}
