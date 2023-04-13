using System.Collections.Generic;


namespace WebApiAutores.Entities
{
    public class Autor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Libro> Libros { get; set; }    
    }
}
