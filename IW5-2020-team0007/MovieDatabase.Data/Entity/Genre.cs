using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.Data.Entity
{
    public class Genre : EntityBase<int>
    {
        [StringLength(255)]
        public string Name { get; set; }

        public ISet<GenreMap> Movies { get; set; }

        public Genre()
        {
            Movies = new HashSet<GenreMap>();
        }
    }
}
