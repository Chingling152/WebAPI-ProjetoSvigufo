using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using System;

namespace Senai.WebAPI
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //adiciona o Modelo MVC na ultima versão (2.1)
            services.AddMvc().AddJsonOptions(opt =>{
                opt.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            }).SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1);
           
            // adiciona swagger para a documentação
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "SVIGUFO", Version = "v1" });
            });

            //adiciona autenticação
            //Implementa autenticação
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            }
            ).AddJwtBearer("JwtBearer", options => {
                //Define as opções 
                options.TokenValidationParameters = new TokenValidationParameters {
                    //Quem esta solicitando
                    ValidateIssuer = true,
                    //Quem esta validadando
                    ValidateAudience = true,
                    //Definindo o tempo de expiração
                    ValidateLifetime = true,
                    //Forma de criptografia
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("svigufo-chave-autenticacao")),
                    //Tempo de expiração do Token
                    ClockSkew = TimeSpan.FromMinutes(30),
                    //Nome da Issuer, de onde esta vindo
                    ValidIssuer = "SviGufo.WebApi",
                    //Nome da Audience, de onde esta vindo
                    ValidAudience = "SviGufo.WebApi"
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SVIGUFO");
            });

            //Habilita a autenticação
            app.UseAuthentication();

            //Habilita o MVC
            app.UseMvc();
        }
    }
}
