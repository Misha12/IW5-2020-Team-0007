using System.Collections.Generic;

namespace MovieDatabase.API.Models
{
    public class MovieInput
    {
        /// <summary>
        /// Movie name.
        /// </summary>
        public string OriginalName { get; set; }

        /// <summary>
        /// Genre ID.
        /// </summary>
        public int Genre { get; set; }

        /// <summary>
        /// Movie lengh.
        /// </summary>
        public long Length { get; set; }

        /// <summary>
        /// Country of origins.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Description of movie.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// URL link for title image.
        /// </summary>
        public string TitleImageUrl { get; set; }

        /// <summary>
        /// Translation for movie names .
        /// </summary>
        public List<MovieNameInput> Names { get; set; }

        /// <summary>
        /// Collection of actros IDs in movie.
        /// </summary>
        public List<long> Actors { get; set; }

        /// <summary>
        /// Collection of directors IDs in movie.
        /// </summary>
        public List<long> Directors { get; set; }

        public bool IsValid(out string errorMessage)
        {
            errorMessage = null;

            if(string.IsNullOrEmpty(OriginalName))
            {
                errorMessage = "Original name of movie can't be empty.";
                return false;
            }

            if(Genre <= 0)
            {
                errorMessage = "Genre was not specified.";
                return false;
            }

            if(Length < 0)
            {
                errorMessage = "Movie lenght was not specified.";
                return false;
            }

            if(string.IsNullOrEmpty(Country))
            {
                errorMessage = "Country of origins was not specified.";
                return false;
            }

            return true;
            // Vlastni validace, protože používáme 1 model i pro editaci, kde jsou pole výše nepovinná.
        }
    }
}
