using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebAPI.Configuration;

public class ApiTokenHeaderFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (!context.MethodInfo.DeclaringType!.Name.Equals("AuthController"))
        {
            operation.Parameters.Add(new()
            {
                Name = "ApiToken",
                In = ParameterLocation.Header,
                Required = false
            });
        }
    }
}
