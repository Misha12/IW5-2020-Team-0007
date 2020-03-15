﻿using System.Collections.Generic;

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

        /// <summary>
        /// Seznam identifikátorů osob, co ve filmu hrají.
        /// </summary>
        public List<long> Actors { get; set; }

        /// <summary>
        /// Seznam identifikátorů osob, co film režírují.
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