using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Sql;
using System.Data.Entity;
using WebApplication1.Models;

namespace WebApplication1.Context
{
    public class PersonManagmentContext : DbContext
    {
        public DbSet<Person> Persons { get;set;}
        public PersonManagmentContext() : base("PersonManagmentContextDB"){}
    }
}