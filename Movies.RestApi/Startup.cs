using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Movies.RestApi.Data;
using Movies.RestApi.Data.Repositories;

namespace Movies.RestApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            LoggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(b => { b.AddConsole(); });
            Logger = LoggerFactory.CreateLogger(GetType());
        }

        public IConfiguration Configuration { get; }
        protected ILoggerFactory LoggerFactory { get; }
        protected ILogger Logger { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Logger.LogInformation("Adding DbContext...");
            services.AddDbContext<MoviesContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Movies.RestApi", Version = "v1" });
            });

            Logger.LogInformation("Adding Mediatr...");
            services.AddMediatR(typeof(Startup));

            Logger.LogInformation("Adding all validators...");
            services.AddValidatorsFromAssembly(typeof(Startup).Assembly);

            services.AddScoped<IMovieRepository, MovieRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Movies.RestApi v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
