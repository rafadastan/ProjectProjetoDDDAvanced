using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Projeto01.Presentation.Api.Configurations;

namespace Projeto01.Presentation.Api
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
            services.AddControllers();

            //configura��o do swagger
            SwaggerConfiguration.AddSwagger(services);

            //configura��o do EntityFramework
            EntityFrameworkConfiguration.AddEntityFramework(services, Configuration);

            //configura��o para autentica��o por JWT
            JwtConfiguration.AddJwt(services, Configuration);

            //configura��o para inje��o de depend�ncia
            DependencyInjectionConfiguration.AddDependencyInjection(services);

            //configura��o de CORS
            CorsConfiguration.AddCors(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            //configura��o de CORS
            CorsConfiguration.UseCors(app);

            //configura��o do JWT
            JwtConfiguration.UseJwt(app);

            //configura��o do swagger
            SwaggerConfiguration.UseSwagger(app);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
