using Fina.Api.Common.Api;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Fina.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Fina.Api.Endpoints.Categories
{
    public class UpdateCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPut("/", HandleAsync)
               .WithName("Categories: Update")
               .WithSummary("Atualiza uma categoria")
               .WithDescription("Atualiza uma categoria")
               .WithOrder(2)
               .Produces<Responses<Category?>>();

        private static async Task<IResult> HandleAsync(
            [FromServices] ICategoryHandler handler,
            [FromBody] UpdateCategoryRequest request,
            long id)
        {
            request.UserId = ApiConfiguration.UserId;
            request.Id = id;

            var result = await handler.UpdateAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
