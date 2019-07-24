using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ArtistManagement.WebApi.Bootstrap
{
    public static class RegisterSwaggerService
    {
        public static IServiceCollection AddMySwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApiVersioning(options => options.ReportApiVersions = true);

            services
                .AddMvcCore()
                .AddApiExplorer()
                .AddVersionedApiExplorer(options =>
                    {
                        options.AssumeDefaultVersionWhenUnspecified = true;
                        options.GroupNameFormat = "'v'VVV";
                    });

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            services.AddSwaggerGen(options =>
            {
                // This call remove version from parameter, without it we will have version as parameter 
                // for all endpoints in swagger UI
                options.OperationFilter<RemoveVersionFromParameter>();

                // This make replacement of v{version:apiVersion} to real version of corresponding swagger doc.
                options.DocumentFilter<ReplaceVersionWithExactValueInPath>();

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

                // Enabling data annotations
                options.EnableAnnotations();

                // The corresponding Schemas will list enum names rather than integer values.
                options.DescribeAllEnumsAsStrings();
                options.DescribeStringEnumsInCamelCase();

                // Custom operation filters
                options.OperationFilter<DefaultResponseOperationFilter>();

                // Set fluent validation
                options.AddFluentValidationRules();
            });

            return services;
        }
    }

    public class DefaultResponseOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (!context.ApiDescription.HttpMethod.Equals(HttpMethod.Get))
            {
                operation.Responses["400"] = new Response
                {
                    Description = "Bad Request",
                    Schema = context.SchemaRegistry.Definitions.ContainsKey("ValidationErrorResponse") ? context.SchemaRegistry.Definitions["ValidationErrorResponse"] : null
                };
            }

            operation.Responses["500"] = new Response { Description = "Internal Server Error" };
        }
    }

    public class RemoveVersionFromParameter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            var versionParameter = operation.Parameters.SingleOrDefault(p => p.Name == "version");

            if (versionParameter != null)
                operation.Parameters.Remove(versionParameter);
        }
    }

    public class ReplaceVersionWithExactValueInPath : IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Paths = swaggerDoc.Paths
                .ToDictionary(
                    path => path.Key.Replace("v{version}", $"v{swaggerDoc.Info.Version}"),
                    path => path.Value
                );
        }
    }
}