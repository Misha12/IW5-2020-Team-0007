using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.API.Models
{
    public class MovieInput
    {
        /// <summary>
        /// Název filmu.
        /// </summary>
        [Required]
        public string OriginalName { get; set; }

        /// <summary>
        /// Identifikátor žánru.
        /// </summary>
        [Required]
        public int Genre { get; set; }

        /// <summary>
        /// Délka filmu.
        /// </summary>
        [Required]
        public long Length { get; set; }

        /// <summary>
        /// Země původu filmu.
        /// </summary>
        [Required]
        public string Country { get; set; }

        /// <summary>
        /// Popis filmu.
        /// </summary>
        [Required]
        public string Description { get; set; }
    }
}
