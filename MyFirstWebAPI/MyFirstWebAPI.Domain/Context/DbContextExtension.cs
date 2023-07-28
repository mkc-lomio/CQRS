using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Newtonsoft.Json;

namespace MyFirstWebAPI.Domain.Context
{
    public static class DbContextExtension
    {
        public static bool AllMigrationsApplied(this MyFirstWebAPIDbContext context)
        {
            var applied = context.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = context.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
        }

        public static void EnsureSeeded(this MyFirstWebAPIDbContext context)
        {
            if (!context.Countries.Any())
            {
                var data = JsonConvert.DeserializeObject<List<Country>>(File.ReadAllText("seed" + Path.DirectorySeparatorChar + "countries.json"));
                context.AddRange(data);
                context.SaveChanges();
            }
            if (!context.Risks.Any())
            {
                var data = JsonConvert.DeserializeObject<List<Risk>>(File.ReadAllText("seed" + Path.DirectorySeparatorChar + "risks.json"));
                context.AddRange(data);
                context.SaveChanges();
            }
            if (!context.RiskDetails.Any())
            {
                var data = JsonConvert.DeserializeObject<List<RiskDetail>>(File.ReadAllText("seed" + Path.DirectorySeparatorChar + "riskdetails.json"));
                context.AddRange(data);
                context.SaveChanges();
            }
        }
    }
}
