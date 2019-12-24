using AutoMapper;
using EatCode.Api.Mappings;
using EatCode.Api.Neo4J;
using EatCode.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Settings;

namespace EatCode.Api
{

    //"5d9a4349750d6d40a4dbd782"
    //{a47fbae9-442b-4199-aa88-3e83021baa0c
    //}

    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.Configure<MatrixSettings>(options => Configuration.GetSection("MatrixSettings").Bind(options));

            services.AddTransient<IRecipeService, RecipeService>();
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<IMatrixService, MatrixCRUD>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
