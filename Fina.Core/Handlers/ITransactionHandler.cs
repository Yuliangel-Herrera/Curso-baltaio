using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Fina.Core.Requests.Transactions;
using Fina.Core.Responses;

namespace Fina.Core.Handlers
{
    internal interface ITransactionHandler
    {
        Task<Responses<Transaction?>> CreateAsync(CreateTransactionRequest request);
        Task<Responses<Transaction?>> UpdateAsync(UpdateTransactionRequest request);
        Task<Responses<Transaction?>> DeleteAsync(DeleteTransactionRequest request);
        Task<Responses<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request);
        Task<PagedResponse<List<Transaction>>> GetByPeriodAsync(GetAllCategoryRequest request);
    }
}
