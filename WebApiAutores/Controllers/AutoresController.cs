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

        // This method returns a list of all authors
        [HttpGet]
        public async Task<ActionResult<List<Autor>>> Get()
        {
            return await context.Autores.ToListAsync();
        }

        // This method returns an author by ID
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

        // This method returns an author by name 
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

        // This method insert an author in the database
        [HttpPost]
        public async Task<ActionResult> Post(Autor autor) 
        {
            context.Add(autor);
            await context.SaveChangesAsync();
            return Ok("Creation succefully");
        }

        // This method update data about an author
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

        // This method delete an author by ID 
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
