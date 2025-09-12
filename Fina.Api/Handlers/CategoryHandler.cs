using Fina.Api.Data;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Fina.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Fina.Api.Handlers
{
    public class CategoryHandler(AppDbContext context) : ICategoryHandler
    {
        public async Task<Responses<Category?>> CreateAsync(CreateCategoryRequest request)
        {
            await Task.Delay(5000); //simulando um delay de 5 segundos
            var category = new Category //criando uma categoria e atribuindo os valores do request para armazenar no BD
            {
                Title = request.Title,
                Description = request.Description,
                UserId = request.UserId
            };
            try
            {
                await context.Categories.AddAsync(category); //adicionando a categoria no contexto
                await context.SaveChangesAsync(); //salvando as mudanças no contexto (banco de dados)

                return new Responses<Category?>(category, code: 201, message: "Categoria criada com sucesso!"); //response
            }
            catch //(Exception e) não recomendado
            {
                // Serilog, OpenTelemetry
                // Console.WriteLine(e.Message);
                // throw;
                return new Responses<Category?>(data:null, code: 500, message: "Erro ao criar categoria");
            }
        }

        public async Task<Responses<Category?>> DeleteAsync(DeleteCategoryRequest request)
        {
            try
            {
                var category = await context //AppDbContext
                    .Categories//DbSet<Category>
                    .FirstOrDefaultAsync(c => c.Id == request.Id && c.UserId == request.UserId); //buscando a categoria pelo Id e UserId

                if (category is null) //se a categoria não for encontrada
                    return new Responses<Category?>(data: null, code: 404, message: "Categoria não encontrada"); //response

                context.Categories.Remove(category); 
                await context.SaveChangesAsync(); //salvando as mudanças no contexto (banco de dados)

                return new Responses<Category?>(category, message: "Categoria excluida com sucesso!"); //response
            }
            catch
            {
                return new Responses<Category?>(data: null, code: 500, message: "Erro ao atualizar categoria");
            }
        }

        public async Task<PagedResponse<List<Category>?>> GetAllAsync(GetAllCategoryRequest request)
        {
            try
            {
                // count e Select
                var query = context
                    .Categories
                    .AsNoTracking()
                    .Where(c => c.UserId == request.UserId)
                    .OrderBy(c => c.Title); //ordenando por título

                var categories = await query
                    .Skip((request.PageNumber - 1) * request.PageSize) //paginação
                    .Take(request.PageSize)
                    .ToListAsync(); //executando a query e retornando a lista de categorias

                var count = await query.CountAsync(); //contando o total de categorias  

                return new PagedResponse<List<Category>?>(
                    categories,
                    count,
                    request.PageNumber,
                    request.PageSize
                );
            }
            catch
            {
                return new PagedResponse<List<Category>?>(data: null, code: 500, message: "Não foi possivel recuperar as categorias");
            }
        }

        public async Task<Responses<Category?>> GetByIdAsync(GetCategoryByIdRequest request)
        {
            try
            {
                var category = await context
                    .Categories
                    .AsNoTracking() //melhora a performance, pois não precisa rastrear a entidade
                    .FirstOrDefaultAsync(c => c.Id == request.Id && c.UserId == request.UserId);

                return category is null
                    ? new Responses<Category?>(data: null, code: 404, message: "Categoria não encontrada")
                    : new Responses<Category?>(category);
            }
            catch
            {
                return new Responses<Category?>(data: null, code: 500, message: "Não foi possivel recuperar a categoria");
            }
        }

        public async Task<Responses<Category?>> UpdateAsync(UpdateCategoryRequest request)
        {
            try
            {
                var category = await context //AppDbContext
                    .Categories//DbSet<Category>
                    .FirstOrDefaultAsync(c => c.Id == request.Id && c.UserId == request.UserId); //buscando a categoria pelo Id e UserId

                if (category is null) //se a categoria não for encontrada
                     return new Responses<Category?>(data:null, code: 404, message: "Categoria não encontrada"); //response

                category.Title = request.Title; //atualizando os valores da categoria
                category.Description = request.Description;

                context.Categories.Update(category); //atualizando a categoria no contexto
                await context.SaveChangesAsync(); //salvando as mudanças no contexto (banco de dados)

                return new Responses<Category?>(category, message: "Categoria atualizada com sucesso!"); //response
            }
            catch
            {
                return new Responses<Category?>(data: null, code: 500, message: "Erro ao atualizar categoria");
            }
        }
    }
}
