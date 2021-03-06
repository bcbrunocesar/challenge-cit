﻿using Challenge.Domain.Handlers;
using Challenge.Domain.Repository;
using Challenge.Domain.Services;
using Challenge.Infra.Repository;
using Challenge.Infra.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Challenge.Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(options => { options.SerializerSettings.DateFormatString = "dd/MM/yyyy"; }); 
            services.AddResponseCompression();
            services.AddTransient<ICidadeService, CidadeService>();
            services.AddTransient<ICoberturaRepository, CoberturaRepository>();
            services.AddTransient<CotacaoHandler, CotacaoHandler>();

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Info{Title = "Challenge C&T", Version = "v1"});
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseResponseCompression();
            app.UseSwagger();
            app.UseSwaggerUI(x => { x.SwaggerEndpoint("/swagger/v1/swagger.json", "Challenge C&T - V1"); });
        }
    }
}
