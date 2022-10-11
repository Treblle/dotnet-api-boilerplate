namespace DotNet_API_Boilerplate.Presentation.Extensions;
using Asp.Versioning;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Treblle.Net.Core;
using DotNet_API_Boilerplate.Core;
using DotNet_API_Boilerplate.Core.Common.Configuration;
using DotNet_API_Boilerplate.Presentation.Swagger.OperationFilters;

[ExcludeFromCodeCoverage]
public static class ProgramExtensions
{
    public static WebApplicationBuilder ConfigureBuilder(this WebApplicationBuilder builder)
    {
        #region Logging

        builder.Host.UseSerilog((hostContext, loggerConfiguration) =>
        {
            var assembly = Assembly.GetEntryAssembly();

            loggerConfiguration.ReadFrom.Configuration(hostContext.Configuration)
                    .Enrich.WithProperty("Assembly Version", assembly?.GetCustomAttribute<AssemblyFileVersionAttribute>()?.Version)
                    .Enrich.WithProperty("Assembly Informational Version", assembly?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion);
        });

        #endregion Logging

        #region Serialisation

        builder.Services.Configure<JsonOptions>(opt =>
        {
            opt.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            opt.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            opt.SerializerOptions.PropertyNameCaseInsensitive = true;
            opt.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            opt.SerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
        });

        #endregion Serialisation

        #region Versioning

        builder.Services.AddApiVersioning(opt =>
        {
            opt.DefaultApiVersion = new ApiVersion(1, 0);
            opt.ReportApiVersions = true;
            opt.AssumeDefaultVersionWhenUnspecified = true;
            opt.ApiVersionReader = new HeaderApiVersionReader("api-version");
        });

        #endregion Versioning

        #region Swagger

        var ti = CultureInfo.CurrentCulture.TextInfo;

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1",
               new OpenApiInfo
               {
                   Version = "v1",
                   Title = $".NET API Boilerplate - {ti.ToTitleCase(builder.Environment.EnvironmentName)} ",
                   Description = "A template for a .NET 6.0 API using the best REST API practices.",
                   Contact = new OpenApiContact
                   {
                       Name = ".NET API Boilerplate",
                       Url = new Uri("https://github.com/Treblle/dotnet-api-boilerplate")
                   },
                   License = new OpenApiLicense()
                   {
                       Name = ".NET API Boilerplate - License - MIT",
                       Url = new Uri("https://opensource.org/licenses/MIT")
                   },
                   TermsOfService = new Uri("https://github.com/Treblle/dotnet-api-boilerplate")
               });

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

            options.EnableAnnotations();
            options.DocInclusionPredicate((name, api) => true);
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the bearer scheme."
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

            options.OperationFilter<ApiVersionOperationFilter>();
        });

        #endregion Swagger

        #region Configuration

        var jwtConfiguration = builder.Configuration.GetSection(JwtConfiguration.Section).Get<JwtConfiguration>();
        builder.Services.AddSingleton(jwtConfiguration);

        #endregion Configuration

        #region Project Dependencies

        builder.Services.AddInfrastructure();
        builder.Services.AddApplication();
        builder.Services.AddHttpContextAccessor();

        #endregion Project Dependencies

        #region Authentication

        builder.Services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(opt => opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtConfiguration.Issuer,
            ValidAudience = jwtConfiguration.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.Key))
        }
        );
        builder.Services.AddAuthorization();

        #endregion Authentication

        return builder;
    }

    public static WebApplication ConfigureApplication(this WebApplication app)
    {
        #region Treblle

        app.UseTreblle();

        #endregion Treblle

        #region Exceptions

        app.UseGlobalExceptionHandler();

        #endregion Exceptions

        #region Logging

        app.UseSerilogRequestLogging();

        #endregion Logging

        #region Swagger

        var ti = CultureInfo.CurrentCulture.TextInfo;

        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", $".NET API Boilerplate - {ti.ToTitleCase(app.Environment.EnvironmentName)} - V1"));

        #endregion Swagger

        #region Security

        app.UseHsts();

        #endregion Security

        #region API Configuration

        app.UseHttpsRedirection();

        #endregion API Configuration

        #region Authentication

        app.UseAuthentication();
        app.UseAuthorization();

        #endregion Authentication

        #region Services

        app.Services.ConfigureInfrastructure();

        #endregion

        return app;
    }
}
