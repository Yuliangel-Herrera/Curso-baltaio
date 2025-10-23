using Microsoft.AspNetCore.Mvc;
using MinhaApi.Data;
using MinhaApi.Models;

namespace MinhaApi.Controller
{
    [ApiController]
    public class HomeController : ControllerBase //ControllerBase: possue métodos adicionais que a classe Controller
    {
        [HttpGet("/")]
        public IActionResult Get([FromServices] AppDbContext context) //Instancia do DbContext por injeção de dependencia
            => Ok(context.Todos.ToList());

        [HttpGet("/{id:int}")]
        public IActionResult GetById([FromRoute] int id, [FromServices] AppDbContext context) //Instancia do DbContext por injeção de dependencia
        {
            var todos = context.Todos.FirstOrDefault(x => x.Id == id);
            if(todos == null)
                return NotFound();

            return Ok(todos);
        }

        [HttpPost("/")]
        public IActionResult Post([FromBody] TodoModel todo, [FromServices] AppDbContext context)
        {
            context.Todos.Add(todo);
            context.SaveChanges();

            return Created($"/{todo.Id}", todo); //pede a URL do objeto antes
        }

        [HttpPut("/{id:int}")]
        public IActionResult Put([FromRoute] int id, [FromBody] TodoModel todo, [FromServices] AppDbContext context)
        {
            var model = context.Todos.FirstOrDefault(x => x.Id == id);
            if (model == null)
                return NotFound();

            model.Title = todo.Title;
            model.Done = todo.Done;

            context.Todos.Update(model);
            context.SaveChanges();
            return Ok(model);
        }
        [HttpDelete("/{id:int}")]
        public IActionResult Delete([FromRoute] int id, [FromServices] AppDbContext context)
        {
            var model = context.Todos.FirstOrDefault(x => x.Id == id);
            if(model == null)
                return NotFound();

            context.Todos.Remove(model);
            context.SaveChanges();
            return Ok(model);
        }
    }
}
