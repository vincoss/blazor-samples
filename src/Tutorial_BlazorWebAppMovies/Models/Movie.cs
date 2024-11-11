using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Tutorial_BlazorWebAppMovies.Models
{
    public class Movie
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public DateOnly ReleaseDate { get; set; }

        public string? Genre { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
    }
}
