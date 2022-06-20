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
                  Gender = Gender.FEMALE,
                  ImageUrl = "https://imengine.prod.srp.navigacloud.com/?uuid=f7c67417-8ceb-5254-8e74-44b9946beb85&type=primary&q=72&width=720"
              },
              new AFA
              {
                  Id = "roki-id",
                  Name = "Roki",
                  City = "Varna",
                  Country = "Bulgaria",
                  Species = Species.DOG,
                  Gender = Gender.FEMALE,
                  ImageUrl = "https://dl5zpyw5k3jeb.cloudfront.net/photos/pets/46769726/1/?bust=1575604120&width=720"
              },
              new AFA
              {
                  Id = "bar-id",
                  Name = "Barack",
                  Species = Species.CAT,
                  Gender = Gender.MALE,
                  ImageUrl = "https://images.squarespace-cdn.com/content/v1/54628e00e4b0b862de638015/1594589044654-MW22PA47X00AI2NXPMLW/image-asset.jpeg"
              },
              new AFA
              {
                  Id = "don-id",
                  Name = "Donald",
                  Species = Species.DUCK,
                  Gender = Gender.MALE,
                  ImageUrl = "https://p0.pikist.com/photos/768/102/animal-baby-bird-born-child-cute-duck-duckling-ducklings.jpg"
                  
              },
              new AFA
              {
                  Id = "arno-id",
                  Name = "Arnold",
                  Species = Species.DOG,
                  Gender = Gender.MALE,
                  ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/3/31/Bo_official_portrait_%28cropped_2%29.jpg"
              },
              new AFA
              {
                  Id = "bug-id",
                  Name = "Nancy",
                  Species = Species.BUG,
                  ImageUrl = "https://www.boredpanda.com/blog/wp-content/uploads/2021/10/cute-bug-6-616d3a2f784a2__700.jpg"
              }
            );
        }
    }
}