using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieDatabase.Web.ViewModels
{
    public class MovieUpdateViewModel
    {
        public long ID { get; set; }
        public TimeSpan Length { get; set; }
        public EditMovieRequest MovieRequest { get; set; }
        public bool? LoginSuccess { get; set; }
    }
}
