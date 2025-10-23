using Blog.Data;
using Blog.Extensions;
using Blog.Models;
using Blog.ViewModels;
using Blog.ViewModels.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Blog.Controllers
{
    [ApiController]
    public class CategoryController : ControllerBase
    {
        //recuperar as categorias
        //colocamos v1 como tratamento de versões 
        [HttpGet("v1/categories")]
        public async Task<IActionResult> GetAsync([FromServices]IMemoryCache cache, [FromServices]BlogDataContext context)
        {
            try
            {
                var categories = cache.GetOrCreate("CategoriesCache", entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
                    return GetCategories(context);
                });
                return Ok(new ResultViewModel<List<Category>>(categories));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<Category>>("05XE04 - Falha interna no servidor")); //padronização ViewModel
            }
        }

        private List<Category> GetCategories(BlogDataContext context)
        {
            return context.Categories.ToList();
        }

        //recupera uma categoria
        [HttpGet("v1/categories/{id:int}")] 
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id, [FromServices] BlogDataContext context)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

                if (category == null)
                    return NotFound(new ResultViewModel<Category>("Conteúdo Não encontrado"));

                return Ok(new ResultViewModel<Category>(category));
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResultViewModel<Category>("05XE05 - Falha interna no servidor"));
            }
        }

        //cria uma categoria
        [HttpPost("v1/categories")]
        public async Task<IActionResult> PostAsync([FromBody] EditorCategoryViewModel model, [FromServices] BlogDataContext context)
        {
            if(!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Category> (ModelState.GetErrors()));
            try
            {
                var category = new Category
                {
                    Id = 0,
                    Name = model.Name,
                    Slug = model.Slug,
                };
                await context.Categories.AddAsync(category); //criando um item
                await context.SaveChangesAsync();

                return Created($"v1/categories/{category.Id}", new ResultViewModel<Category>(category));
            }
            catch (DbUpdateException ex) 
            {
                //05XE9: podemos colocar um codigo para achar mais facilmente o erro, caso acontecer
                return StatusCode(500, new ResultViewModel<Category>("05XE9 - Não foi possível incluir a categoria"));
            }
            catch
            { 
                return StatusCode(500, new ResultViewModel<Category>("05XE10 - Falha interna no servidor"));
            }
        }

        //atualiza uma categoria
        [HttpPut("v1/categories/{id:int}")]
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] EditorCategoryViewModel model, [FromServices] BlogDataContext context)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

                if (category == null)
                    return NotFound(new ResultViewModel<Category>("Conteúdo não encontrado"));

                category.Name = model.Name;
                category.Slug = model.Slug;

                context.Categories.Update(category);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Category>(category));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Category>("05XE8 - Não foi possível alterar a categoria"));
            }
            catch 
            {
                return StatusCode(500, new ResultViewModel<Category>("05XE11 - Falha interna no servidor"));
            }
        }

        //deleta uma categoria
        [HttpDelete("v1/categories/{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id, [FromServices] BlogDataContext context)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

                if (category == null)
                    return NotFound(new ResultViewModel<Category>("Conteúdo não encontrado"));

                context.Categories.Remove(category);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Category>(category));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Category>("05XE7 - Não foi possível deletar a categoria"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Category>("05XE12 - Falha interna no servidor"));
            }
        }
    }
}
