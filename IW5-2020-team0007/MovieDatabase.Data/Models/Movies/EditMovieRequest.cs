using MovieDatabase.Data.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.Data.Models.Movies
{
    public class EditMovieRequest
    {
        public string OriginalName { get; set; }

        [GenreIDsExists(AllowNulls = true, ErrorMessage = "Neplatný seznam žánrů. Některý ze žánrů neexistuje.")]
        public List<int> GenreIds { get; set; }

        [Range(0, long.MaxValue, ErrorMessage = "Neplatná délka filmu. Záporné hodnoty nejsou povoleny.")]
        public long? Length { get; set; }

        [StringLength(50, ErrorMessage = "Maximální délka země původu je 50 znaků.")]
        public string Country { get; set; }

        public string Description { get; set; }

        [Url(ErrorMessage = "Neplatný formát adresy profilového obrázku.")]
        public string TitleImageUrl { get; set; }

        public int? CreatedYear { get; set; }

        public List<MovieNameRequest> MovieNames { get; set; }

        [PersonsExists(AllowNulls = true, ErrorMessage = "Neexistující osoba v seznamu herců.")]
        public List<long> Actors { get; set; }

        [PersonsExists(AllowNulls = true, ErrorMessage = "Neexistující osoba v seznamu režisérů.")]
        public List<long> Directors { get; set; }
    }
}
