using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using FirstCodeDb.Service;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FirstCodeDb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddFluentValidation(f=> {
                    f.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                    f.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                    f.ImplicitlyValidateChildProperties = true;
                });

            services.AddDbContext<FCDbContext>(options =>
               options.UseSqlServer(Configuration["ConnectionStrings:FCDbContext"]));

            services.AddTransient<IDateTimeService, DateTimeService>();
            services.AddScoped<IFCDBContext, FCDbContext>();
            //services.AddMvc()
            //   .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            //    .AddJsonOptions(options =>
            //    {
            //        options.SerializerSettings.Formatting = Formatting.Indented;
            //    })
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env, FCDbContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //db.Database.Migrate();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
