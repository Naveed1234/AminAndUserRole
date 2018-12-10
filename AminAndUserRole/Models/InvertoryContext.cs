using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AminAndUserRole.Models
{
    public class InvertoryContext:DbContext
    {
        public InvertoryContext() : base("name=constr") { }

        public DbSet<User> Users { get; set; }
    }
}