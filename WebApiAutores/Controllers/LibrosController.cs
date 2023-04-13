using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.Entities;
using WebApiAutores.Migrations;

namespace WebApiAutores.Controllers
{
    [ApiController]
    [Route("api/libros")]
    public class LibrosController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public LibrosController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<Libro>> Get(int Id)
        {
            return await context.Libros.FirstOrDefaultAsync(x => x.Id == Id);

        }

        [HttpPost]
        public async Task<ActionResult> Post(Libro libros)
        {
            var existAutor = await context.Autores.AnyAsync(x => x.Id == libros.AutorId);
            if (!existAutor)
            {
                return BadRequest($"The identifier of author {libros.AutorId} is not exist: ");
            }

            context.Add(libros);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Libro libros)
        {
            var existLibro = await context.Libros.AnyAsync(x => x.Id == libros.Id);
            if (!existLibro)
            {
                return NotFound($"ID {libros.Id} not exist");
            }

            context.Update(libros);
            await context.SaveChangesAsync();
            return Ok("Changes succefully!"); 
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existLibro = await context.Libros.AnyAsync(x => x.Id == id);
            if (!existLibro)
            {
                return BadRequest($"The Id {id} is not exist");
            }
            context.Remove(new Libro { Id = id });
            await context.SaveChangesAsync();
            return Ok("Delete process succefully!");

        }
        
    }
}
