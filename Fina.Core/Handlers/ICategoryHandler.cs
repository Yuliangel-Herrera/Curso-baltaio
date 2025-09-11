using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Fina.Core.Responses;

namespace Fina.Core.Handlers
{
    public interface ICategoryHandler
    {
        Task<Responses<Category?>> CreateAsync(CreateCategoryRequest request);
        Task<Responses<Category?>> UpdateAsync(UpdateCategoryRequest request);
        Task<Responses<Category?>> DeleteAsync(DeleteCategoryRequest request);
        Task<Responses<Category?>> GetByIdAsync(GetCategoryByIdRequest request);
        Task<PagedResponse<List<Category>>> GetAllAsync(GetAllCategoryRequest request);
    }
}
