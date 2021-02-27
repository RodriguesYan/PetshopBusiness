using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetshopBusinessAPI.Filters
{
    public class TagDescriptionsDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Tags = new[] {
            new OpenApiTag { Name = "ClientUser", Description = "Cria e autentica usuarios" }
            //new OpenApiTag { Name = "Listas", Description = "Consulta as listas de leitura." }
        };
        }
    }
}
