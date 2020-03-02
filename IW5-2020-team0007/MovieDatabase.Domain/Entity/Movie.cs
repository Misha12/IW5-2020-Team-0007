using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieDatabase.Domain.Entity
{
    public class Movie : EntityBase<long>
    {
        [StringLength(255)]
        public string OriginalName { get; set; }
        
        public int GenreID { get; set; }
        public string TitleImage { get; set; }
        
        [StringLength(20)]
        public string Country { get; set; }
        public long Length { get; set; }
        public string Description { get; set; }

        public ISet<MovieName> Names { get; set; }
        public ISet<Rate> Rates { get; set; }
        
        [ForeignKey("GenreID")]
        public Genre Genre { get; set; }

        public ISet<MoviePerson> Actors { get; set; }
        public ISet<MoviePerson> Directors { get; set; }

        public Movie()
        {
            Names = new HashSet<MovieName>();
            Rates = new HashSet<Rate>();
            Actors = new HashSet<MoviePerson>();
            Directors = new HashSet<MoviePerson>();
        }
    }
}
