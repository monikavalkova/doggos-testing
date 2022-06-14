using System;
using System.Linq;
using Anima.WebAPI.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Anima.WebAPI.Data

{
    public class SeedDatabase
    {
        public void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private void SeedData(AppDbContext context)
        {
            if (!context.AnimalsForAdoption.Any())
            {
                AddAnimalsForAdoption(context);
            }
            context.SaveChanges();
        }

        private void AddAnimalsForAdoption(AppDbContext context)
        {
            Console.WriteLine("### Adding animals for adoption to the empty in-memory database");

            context.AnimalsForAdoption.AddRange(
              new AFA
              {
                  Id = "cass-id",
                  Name = "Cassi",
                  Species = Species.CAT,
                  Gender = Gender.FEMALE
              },
              new AFA
              {
                  Id = "roki-id",
                  Name = "Roki",
                  City = "Varna",
                  Country = "Bulgaria",
                  Species = Species.DOG,
                  Gender = Gender.FEMALE
              },
              new AFA
              {
                  Id = "bar-id",
                  Name = "Barack",
                  Species = Species.CAT,
                  Gender = Gender.MALE
              },
              new AFA
              {
                  Id = "don-id",
                  Name = "Donald",
                  Species = Species.DUCK,
                  Gender = Gender.MALE
              },
              new AFA
              {
                  Id = "arno-id",
                  Name = "Arnold",
                  Species = Species.DOG,
                  Gender = Gender.MALE
              },
              new AFA
              {
                  Id = "bug-id",
                  Name = "Nancy",
                  Species = Species.BUG
              }
            );
        }
    }
}