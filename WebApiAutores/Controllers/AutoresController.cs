using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.Entities;

namespace WebApiAutores.Controllers
{
    [ApiController]
    [Route("api/autores")]
    public class AutoresController : ControllerBase
    {
        public AutoresController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public ApplicationDbContext context { get; }

        [HttpGet]
        public async Task<ActionResult<List<Autor>>> Get()
        {
            return await context.Autores.ToListAsync();
        }

        [HttpGet("id")]
        public async Task<ActionResult<Autor>> GetById(int id)
        {
            var authorExist = await context.Autores.FirstOrDefaultAsync(x => x.Id == id);
            if (authorExist == null)
            {
                return NotFound($"The author´s ID {id} is not exist!");
            }
            return Ok(authorExist);
        }

        [HttpGet("Name")]
        public async Task<ActionResult<Autor>> GetByName(string nameAuthor)
        {
            var authorName = await context.Autores.FirstOrDefaultAsync(x => x.Name.Contains(nameAuthor));
            if (authorName == null)
            {
                return NotFound();
            }
            return Ok (authorName);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Autor autor) 
        {
            context.Add(autor);
            await context.SaveChangesAsync();
            return Ok("Creation succefully");
        }

        [HttpPut("{Id:int}")]
        public async Task<ActionResult> Put(Autor autor, int Id)
        {
            var exist = await context.Autores.AnyAsync(x => x.Id == Id);
            if (!exist)
            {
                return NotFound();
            }
            if (autor.Id != Id)
            {
                return BadRequest("The author ID is not match about ID URL");
            }
            context.Update(autor);
            await context.SaveChangesAsync();
            return Ok();

	    }

        [HttpDelete("{Id:int}")]
        public async Task<ActionResult> Delete(int Id)
        {
            var exist = await context.Autores.AnyAsync(x=> x.Id == Id);
            if (!exist)
            {
                return NotFound();
            }
            context.Remove(new Autor() { Id = Id });
            await context.SaveChangesAsync();
            return Ok("The Author has been erased !!");
        }

	}
    
}
