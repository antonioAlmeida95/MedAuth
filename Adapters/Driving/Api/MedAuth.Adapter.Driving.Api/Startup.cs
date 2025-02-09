using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using MedAuth.Adapter.Driven.Database;
using MedAuth.Core.Application;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace MedAuth.Adapter.Driving.Api;

public class Startup
{
        /// <summary>
    /// Configurações da aplicação.
    /// </summary>
    public IConfiguration Configuration { get; }
    /// <summary>
    /// Variável de ambiente.
    /// </summary>
    public IWebHostEnvironment CurrentEnvironment { get; }
    
    /// <summary>
    /// COnstrutor da Classe.
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="env"></param>
    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
        Configuration = configuration;
        CurrentEnvironment = env;
    }
    
    /// <summary>
    /// Método de definições e configurações dos serviços.
    /// </summary>
    /// <param name="services"></param>
    public void ConfigureServices(IServiceCollection services)
    {
        //Injeção de Dependencias Adapters
        services.AddMedAuthApiModule();
        services.AddMedAuthDatabaseModule(Configuration);
        
        //Injeção de Dependecias Core
        services.AddMedAuthApplicationModule(Configuration);
        
        services.AddHttpClient();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Documentação API",
                Version = "v1",
                Description = "API desafio 05 pos tech da avaliação técnica.",
                Contact = new OpenApiContact
                {
                    Name = "Antonio Lucas de Almeida",
                    Url = new Uri("https://github.com/LucasStark95")
                }
            });
            
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
        });

        services.AddControllers()
            .AddJsonOptions(o => o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
        
        var secretKey = Configuration.GetSection("Credentials:Secret:SecretKey").Value ?? string.Empty;
        services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
    }
    
    /// <summary>
    /// Método de definições e configurações do APP.
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseCors("SiteCorsPolicy");
        }
            
        app.UseHttpsRedirection();
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.RoutePrefix = string.Empty;
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Documentação Api");
        });

        app.UseAuthentication();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}