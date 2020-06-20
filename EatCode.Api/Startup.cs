using AutoMapper;
using EatCode.Api.Mappings;
using EatCode.Api.Neo4J;
using EatCode.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Settings;

namespace EatCode.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cooking API", Version = "v1" });
            });


            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.Configure<MatrixSettings>(options => Configuration.GetSection("MatrixSettings").Bind(options));

            services.Configure<FilesMongoDbSettings>(options => Configuration.GetSection("MongoDbSettings").Bind(options));
            services.Configure<RecipesMongoDbSettings>(options => Configuration.GetSection("MongoDbSettings").Bind(options));
            services.Configure<ScoreboardMongoDbSettings>(options => Configuration.GetSection("MongoDbSettings").Bind(options));

            services.AddTransient<IRecipeService, RecipeService>();
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<IMatrixService, MatrixCRUD>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cooking API");
            });


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