using Account.Apis.Errors;
using Account.Apis.Extentions;
using Account.Core.Models.Account;
using Account.Core.Models.Identity;
using Account.Reposatory.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Account.Apis
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region configure service

            builder.Services.AddControllers();
            builder.Services.Configure<MailSettings>(builder.Configuration.GetSection(nameof(MailSettings)));

            builder.Services.AddIdentityServices(builder.Configuration);

            builder.Services.AddSwaggerService();
            builder.Services.AddAplictionService();
            builder.Services.AddMemoryCache();


            #endregion
            var app = builder.Build();

            #region Update automatically
            // Create a service scope to resolve services
            using var scope = app.Services.CreateScope();
            var Services = scope.ServiceProvider;

            // Obtain logger factory to create loggers
            var loggerfactory = Services.GetRequiredService<ILoggerFactory>();
            try
            {
                // Get the database context for Identity
                var identityDbContext = Services.GetRequiredService<AppIdentityDbContext>();

                // Apply database migration asynchronously
                await identityDbContext.Database.MigrateAsync();

                // Get the UserManager service to manage users
                var usermanager = Services.GetRequiredService<UserManager<AppUser>>();

                // Seed initial user data for the Identity context
                //await AppIdentityDbContextSeed.SeedUserAsync(usermanager);
            }
            catch (Exception ex)
            {
                // If an exception occurs during migration or seeding, log the error
                var logger = loggerfactory.CreateLogger<Program>();
                logger.LogError(ex, "An Error Occurred During Applying The Migrations");
            }
            #endregion


            #region configure middlewares
            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseMiddleware<ExeptionMiddleWares>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddlewares();
            }
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            #endregion
            app.Run();
        }
    }
}
