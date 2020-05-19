using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieDatabase.Web.ViewModels
{
    public class GenresViewModel
    {
        public List<Genre> Genres { get; set; }
        public bool? LoginSuccess { get; set; }
    }
}
