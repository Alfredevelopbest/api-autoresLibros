namespace WebApiAutores.Entities
{
    public class Libro
    {
        public int Id { get; set; }
        public String Titulo { get; set; }
        public int AutorId { get; set; }
        public Autor Autor { get; set; }
        
    }
}
