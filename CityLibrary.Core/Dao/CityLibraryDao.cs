using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace CityLibrary.Core.Dao
{
    /// <summary>
    /// City Library data access object.
    /// </summary>
    public class CityLibraryDao : BaseDao
    {
        public List<User> GetUsers()
        {
            List<User> users = null;

            var context = new CityLibraryEntities();

            try
            {
                users = (
                    from u in context.Users
                    orderby u.LastName
                    select u
                ).ToList();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
            finally
            {
                context.Dispose();
            }

            return users;
        }

        public string CreateUser(User user)
        {
            string err = null;

            var context = new CityLibraryEntities();

            try
            {
                throw new Exception("Kurwaaaaaaaaaaaaaaaa");


                context.Users.Add(user);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                err = ex.Message;
            }

            return err;
        }
    }
}
