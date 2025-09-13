using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Fina.Core.Responses;
using System.Net.Http.Json;

namespace Fina.Web.Handlers
{
    public class CategoryHandler(IHttpClientFactory httpClientFactory) : ICategoryHandler
    {
        private readonly HttpClient _client = httpClientFactory.CreateClient(WebConfiguration.HttpClientName);
        public async Task<Responses<Category?>> CreateAsync(CreateCategoryRequest request)
        {
            var result = await _client.PostAsJsonAsync($"v1/categories", request);
            return await result.Content.ReadFromJsonAsync<Responses<Category?>>()
                ?? new Responses<Category?>(null, 400, "Falha ao criar categoria ");
        }

        public async Task<Responses<Category?>> DeleteAsync(DeleteCategoryRequest request)
        {
            var result = await _client.DeleteAsync($"v1/categories/{request.Id}/");
            return await result.Content.ReadFromJsonAsync<Responses<Category?>>()
                ?? new Responses<Category?>(null, 400, "Falha ao excluir a categoria ");
        }

        public async Task<PagedResponse<List<Category>?>> GetAllAsync(GetAllCategoryRequest request)
            => await _client.GetFromJsonAsync<PagedResponse<List<Category>?>>($"v1/categories")
                ?? new PagedResponse<List<Category>?>(null, 400, "Falha ao obter categorias ");

        public async Task<Responses<Category?>> GetByIdAsync(GetCategoryByIdRequest request)
            => await _client.GetFromJsonAsync<Responses<Category?>>($"v1/categories/{request.Id}")
                ?? new Responses<Category?>(null, 400, "Falha ao obter categoria ");

        public async Task<Responses<Category?>> UpdateAsync(UpdateCategoryRequest request)
        {
            var result = await _client.PutAsJsonAsync($"v1/categories/{request.Id}", request);
            return await result.Content.ReadFromJsonAsync<Responses<Category?>>()
                ?? new Responses<Category?>(null, 400, "Falha ao atualizar categoria ");
        }
    }
}
