using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieDatabase.Data.Entity
{
    [Table("Movies")]
    public class Movie : EntityBase<long>
    {
        [StringLength(255)]
        public string OriginalName { get; set; }

        public string TitleImageUrl { get; set; }

        [StringLength(50)]
        public string Country { get; set; }

        public long Length { get; set; }

        public string Description { get; set; }

        public ISet<MoviePerson> Persons { get; set; }
        public ISet<Rating> Ratings { get; set; }
        public ISet<MovieName> Names { get; set; }
        public ISet<GenreMap> Genres { get; set; }


        public Movie()
        {
            Persons = new HashSet<MoviePerson>();
            Ratings = new HashSet<Rating>();
            Names = new HashSet<MovieName>();
            Genres = new HashSet<GenreMap>();
        }
    }
}
