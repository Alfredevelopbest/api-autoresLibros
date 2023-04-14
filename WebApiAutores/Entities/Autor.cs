using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApiAutores.Entities
{
    public class Autor
    {
        public int Id { get; set; }
        [Required (ErrorMessage ="¡Field {0} is requiered!")]
        [StringLength(maximumLength:40,ErrorMessage ="Maximum characters accepted are {1}")]
        
        public string Name { get; set; }
        public List<Libro> Libros { get; set; }    
    }
}
