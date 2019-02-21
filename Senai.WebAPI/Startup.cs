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
            services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1);
           
            // adiciona swagger para a documentação
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "SVIGUFO", Version = "v1" });
            });

            //adiciona autenticação
            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = "JWTBearer";
                options.DefaultChallengeScheme = "JWTBearer";
                }
            ).AddJwtBearer("JWTBearer",item => 
                item.TokenValidationParameters = new TokenValidationParameters() {
                    // Define quem está solicitando a chave
                    ValidateIssuer = true,
                    ValidIssuer = "Svigufo.WebApi",//Define o nome do solicitante
                    // Quem está recebendo a chave
                    ValidateAudience = true,
                    ValidAudience = "Usuario.Logado",
                    // Define tempo de expiração 
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(30),
                    //forma de criptografia
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("SVIGUFO-CHAVE-AUTENTICACAO"))
                }
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // utilizando swagger
                app.UseSwagger();

                //gera pagina swagger
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SVIGUFO");
                });
            }
            // usa o modelo mvc
            app.UseMvc();

            // usa autenticação por token
            app.UseAuthentication();
        }
    }
}
