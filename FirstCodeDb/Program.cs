using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FirstCodeDb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build()
                .Seeded()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                 .ConfigureWebHostDefaults(webBuilder =>
                 {
                     webBuilder.UseStartup<Startup>();
                 });
        }
    }

    public static class SeedData
    {
        public static IHost Seeded(this IHost Builder)
        {
            try
            {
                using (var scope = Builder.Services.GetService<IServiceScopeFactory>().CreateScope())
                {
                    // alternatively resolve UserManager instead and pass that if only think you want to seed are the users     
                    using (var context = scope.ServiceProvider.GetRequiredService<FCDbContext>())
                    {
                        SeedData.SeedAsync(context).GetAwaiter().GetResult();
                    }
                }
            }
            catch (Exception e)
            {
                var a = e;
            }
            return Builder;
        }

        public static async Task SeedAsync(FCDbContext context)
        {
            //var context = serviceProvider.GetRequiredService<YourDbContext>();
            context.Database.EnsureCreated();

            var taxonomySeed = new List<Taxonomy>
                    {
                        new Taxonomy
                        {
                            Id = 1,
                            Key = "DC1",
                            Value = "abcx"
                        },
                        new Taxonomy
                        {
                            Id = 2,
                            Key = "DC2",
                            Value = "abcy"
                        },
                        new Taxonomy
                        {
                            Id = 3,
                            Key = "DC3",
                            Value = "abcz"
                        },
                    };
            var masterData = await context.Taxonomy.ToListAsync();
            var removeList = masterData.Where(w => !taxonomySeed.Contains(w)).ToList();
            if (removeList.Any())
            {
                context.RemoveRange(removeList);
            }
            var addList = taxonomySeed.Where(w => !masterData.Contains(w)).ToList();
            await context.AddRangeAsync(addList);

            await context.SaveChangesAsync();
            await SeedMaster(context);
        }


        private static async Task SeedMaster(FCDbContext context)
        {
            var masterSeed = new List<Master>
                    {
                        new Master
                        {
                            Id = 1,
                            Key = "MS1",
                            Value = "wweee1",
                            TaxonomyId = 1
                        },
                        new Master
                        {
                            Id = 2,
                            Key = "MS2",
                            Value = "qqrrr2",
                            TaxonomyId = 2,
                        },
                        new Master
                        {
                            Id = 3,
                            Key = "MS3",
                            Value = "zzcc3",
                            TaxonomyId = 3,
                        },
                    };
            var masterData = await context.Master.ToListAsync();
            var removeList = masterData.Where(w => !masterSeed.Contains(w)).ToList();
            if (removeList.Any())
            {
                context.RemoveRange(removeList);
            }
            var addList = masterSeed.Where(w => !masterData.Contains(w)).ToList();
            await context.AddRangeAsync(addList);

            await context.SaveChangesAsync();
        }
    }
}
