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
            Console.WriteLine("### Adding animals for adoption to the empty database");

            context.AnimalsForAdoption.AddRange(
              new AFA
              {
                  Id = "existing-id",
                  Name = "Cassandra",
                  Species = Species.CAT
              },
              new AFA
              {
                  Id = "2f81a686-7531-11e8-86e5-f0d5bf731f68",
                  Name = "George",
                  Species = Species.DOG
                  
              },
              new AFA
              {
                  Id = "f9ce325d-ed8c-4fad-899b-fc997ed199ad",
                  Name = "Barack",
                  Species = Species.CAT
              },
              new AFA
              {
                  Id = "b769d25a-86dc-4ec6-a022-dfa4112354f9",
                  Name = "Donald",
                  Species = Species.DUCK
              },
              new AFA
              {
                  Id = "822dcf18-54eb-4394-8884-1c73addf25c7",
                  Name = "Arnold",
                  Species = Species.DOG
              }
            );
        }
    }
}