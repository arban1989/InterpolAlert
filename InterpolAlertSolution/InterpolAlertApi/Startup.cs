using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterpolAlertApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;


namespace InterpolAlertApi
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
            services.AddMvc().AddJsonOptions(o =>
            {
                o.JsonSerializerOptions.PropertyNamingPolicy = null;
                o.JsonSerializerOptions.DictionaryKeyPolicy = null;
            });

            var connectionString = Configuration["connectionStrings:InterpolDbConnectionString"];
            services.AddDbContext<DbContextInterpol>(c => c.UseSqlServer(connectionString));

            services.AddScoped<IAutoreRepository, AutoreRepository>();
            services.AddScoped<IEventoRepository, EventoRepository>();
            services.AddScoped<IEsitoRepository, EsitoRepository>();
            services.AddScoped<IMandanteRepository, MandanteRepository>();
            services.AddScoped<IFazioneRepository, FazioneRepository>();
            services.AddScoped<ITipoVittimaRepository, TipoVittimaRepository>();
            services.AddScoped<ITipoEventoRepository, TipoEventoRepository>();
            services.AddScoped<IGravitaRepository, GravitaRepository>();
            services.AddScoped<ILocalitaRepository, LocalitaRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
