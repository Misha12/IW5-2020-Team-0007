using System.Collections.Generic;

namespace MovieDatabase.API.Models
{
    public class MovieInput
    {
        /// <summary>
        /// Název filmu.
        /// </summary>
        public string OriginalName { get; set; }

        /// <summary>
        /// Identifikátor žánru.
        /// </summary>
        public int Genre { get; set; }

        /// <summary>
        /// Délka filmu.
        /// </summary>
        public long Length { get; set; }

        /// <summary>
        /// Země původu filmu.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Popis filmu.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// URL odkaz na titulní obrázek.
        /// </summary>
        public string TitleImageUrl { get; set; }

        /// <summary>
        /// Překlady názvů filmů.
        /// </summary>
        public List<MovieNameInput> Names { get; set; }

        public bool IsValid()
        {
            // Vlastni validace, protože používáme 1 model i pro editaci, kde jsou pole výše nepovinná.
            return !string.IsNullOrEmpty(OriginalName) && Genre > 0 && Length > -1 && !string.IsNullOrEmpty(Country);
        }
    }
}
