using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using VegankoService;
using Microsoft.EntityFrameworkCore.InMemory;
using VegankoService.Data;
using Xunit;
using Microsoft.Extensions.Logging;

namespace IntegrationTests
{
    public class CustomWebApplicationFactory<TStartup>
     : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove the app's ApplicationDbContext registration.
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<VegankoContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                DbContextOptionsBuilder dbBuilder = new DbContextOptionsBuilder();
                dbBuilder.UseInMemoryDatabase("InMemoryDbForTesting");


                // Add ApplicationDbContext using an in-memory database for testing.
                //services.Add(new VegankoContext(dbBuilder.Options));

                // Build the service provider.
                var sp = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database
                // context (ApplicationDbContext).
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<VegankoContext>();
                    var logger = scopedServices
                        .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    // Ensure the database is created.
                    db.Database.EnsureCreated();

                    try
                    {
                        // Seed the database with test data.
                        Utilities.InitializeDbForTests(db);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                            "database with test messages. Error: {Message}", ex.Message);
                    }
                }
            });
        }
    }
}