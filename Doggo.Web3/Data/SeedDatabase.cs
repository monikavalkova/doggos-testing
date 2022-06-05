using System;
using System.Linq;
using Doggo.API.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Doggo.Web3.Data

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
                  Name = "Cassandra",
                  Species = Species.CAT,
                  Gender = Gender.FEMALE
              },
              new AFA
              {
                  Id = "hecu-id",
                  Name = "Hecuba",
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
              }
            );
        }
    }
}