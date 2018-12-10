using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AminAndUserRole.Models
{
    public class UserData
    {
        InvertoryContext db = new InvertoryContext();
        public User GetUser(string UserName, string Password)
        {
            using (db)
            {
                return (from c in db.Users.Include("Role") where c.UserName.Equals(UserName) && c.Password.Equals(Password) select c).FirstOrDefault();
            }
        }
    }
}