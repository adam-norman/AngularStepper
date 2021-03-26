using App.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.DataAccess.Data.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly IServiceScopeFactory _scopeFactory;


        public DbInitializer(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }
        void IDbInitializer.Initialize()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
                {
                    context.Database.Migrate();
                }
            }
        }

        void IDbInitializer.SeedData()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
                {

                    //add admin user
                    if (!context.Steps.Any())
                    {
                        var steps = new List<Step>{
                            new Step {  Title="Step", Description="Description", Items=new List<Item>{
                            new Item{  Title="Some title", Description="some description", StepId=1}
                            } },
                           };
                        context.Steps.AddRange(steps);
                    }

                    context.SaveChanges();
                }
            }
        }
    }
}
