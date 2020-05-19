using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieDatabase.Web.ViewModels
{
    public class PersonListViewModel
    {
        public PaginatedDataOfSimplePerson listPerson { get; set; }
        public bool? LoginSuccess { get; set; }
    }
}
