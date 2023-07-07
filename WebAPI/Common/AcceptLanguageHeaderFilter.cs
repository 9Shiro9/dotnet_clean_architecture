﻿using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebAPI.Common
{
    public class AcceptLanguageHeaderFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters.Add(new OpenApiParameter
            {
                In = ParameterLocation.Header,
                Name = "accept-language",
                Description = "Pass accept-language: example - en-US or my-MM",
                Schema = new OpenApiSchema
                {
                    Type = "String"
                }
            });
        }
    }
}
