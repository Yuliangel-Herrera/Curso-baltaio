using Fina.Api.Data;
using Fina.Core.Common;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Fina.Core.Requests.Transactions;
using Fina.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Fina.Api.Handlers
{
    public class TransactionHandler(AppDbContext context) : ITransactionHandler
    {
        public async Task<Responses<Transaction?>> CreateAsync(CreateTransactionRequest request)
        {
            if (request is { Type: Fina.Core.Enums.EtransactionType.Withdraw, Amount: > 0 })
                request.Amount *= -1;

            try
            {
                var transaction = new Transaction
                {
                    UserId = request.UserId,
                    CategoryId = request.CategoryId,
                    CreatedAT = DateTime.Now,
                    Amount = request.Amount,
                    PaidOrReceiveAt = request.PaidOrReceivedAt,
                    Title = request.Title,
                    Type = request.Type
                };
                await context.Transactions.AddAsync(transaction);
                await context.SaveChangesAsync();

                return new Responses<Transaction?>(transaction, code: 201, message: "Transação criada com sucesso!");
            }
            catch
            {
                return new Responses<Transaction?>(data: null, code: 200, message: "Não foi possível criar a transação");
            }
        }

        public async Task<Responses<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
        {
            try
            {
                var transaction = await context
                    .Transactions
                    .FirstOrDefaultAsync(c => c.Id == request.Id && c.UserId == request.UserId);

                if (transaction == null)
                    return new Responses<Transaction?>(data: null, code: 404, message: "Transação não encontrada");

                context.Transactions.Remove(transaction);
                await context.SaveChangesAsync();

                return new Responses<Transaction?>(transaction);
            }
            catch
            {
                return new Responses<Transaction?>(data: null, code: 500, message: "Não foi possível deletar a transação");
            }
        }

        public async Task<Responses<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request)
        {
            try
            {
                var transaction = await context
                    .Transactions
                    .FirstOrDefaultAsync(c => c.Id == request.Id && c.UserId == request.UserId);

                return transaction != null
                    ? new Responses<Transaction?>(data:null, code:404, message:"Transação não encontrada")
                    : new Responses<Transaction?>(transaction);
            }
            catch
            {
                return new Responses<Transaction?>(data: null, code: 500, message: "Não foi possível buscar a transação");
            }
        }

        public async Task<PagedResponse<List<Transaction>?>> GetByPeriodAsync(GetTransactionsByPeriodRequest request)
        {
            try
            {
                request.StartDate ??= DateTime.Now.GetFirstDay();
                request.EndDate ??= DateTime.Now.GetLastDay();
            }
            catch
            {
                return new PagedResponse<List<Transaction>?>(data:null, code:500, message:"Não foi possível determinar a data de início ou termino da transação");
            }

            try
            {
                var query = context
                    .Transactions
                    .AsNoTracking()
                    .Where(t =>
                           t.PaidOrReceiveAt >= request.StartDate &&
                           t.PaidOrReceiveAt <= request.EndDate &&
                           t.UserId == request.UserId)
                    .OrderBy(t => t.PaidOrReceiveAt);

                var transactions = await query
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();

                var Count = await query.CountAsync();

                return new PagedResponse<List<Transaction>?>(
                    transactions,
                    Count,
                    request.PageNumber,
                    request.PageSize);
            }
            catch
            {
                return new PagedResponse<List<Transaction>?>(data: null, code: 500, message: "Não foi possível buscar as transações");
            }
        }

        public async Task<Responses<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
        {
            if (request is { Type: Fina.Core.Enums.EtransactionType.Withdraw, Amount: > 0 })
                request.Amount *= -1;
            try
            {
                var transaction = await context
                    .Transactions
                    .FirstOrDefaultAsync(c => c.Id == request.Id && c.UserId == request.UserId);

                if (transaction == null)
                    return new Responses<Transaction?>(data: null, code: 404, message: "Transação não encontrada");

                transaction.CategoryId = request.CategoryId;
                transaction.Amount = request.Amount;
                transaction.Title = request.Title;
                transaction.Type = request.Type;
                transaction.PaidOrReceiveAt = request.PaidOrReceivedAt;

                context.Transactions.Update(transaction);
                await context.SaveChangesAsync();

                return new Responses<Transaction?>(transaction);
            }
            catch
            {
                return new Responses<Transaction?>(data: null, code: 500, message: "Não foi possível atualizar a transação");
            }
        }
    }
}
