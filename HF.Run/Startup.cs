using Hangfire;
using Hangfire.Mongo;
using HF.Run.Jobs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HF.Run
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHangfire(config =>
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");

                var migrationOptions = new MongoStorageOptions
                {
                    MigrationOptions = new MongoMigrationOptions
                    {
                        Strategy = MongoMigrationStrategy.Migrate,
                        BackupStrategy = MongoBackupStrategy.Collections
                    }
                };
                config.UseMongoStorage(connectionString, migrationOptions);
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var options = new BackgroundJobServerOptions();
            options.Queues = new[] { "default", "notDefault" };
            options.WorkerCount = 2;

            app.UseHangfireServer(options);
            app.UseHangfireDashboard();

            RecurringJob.AddOrUpdate<StoreScoreJob>(StoreScoreJob.JobId, j => j.Run(null, null), Cron.Daily);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection().UseMvc();
        }
    }
}