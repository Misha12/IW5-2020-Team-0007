using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieDatabase.Web.ViewModels
{
    public class PersonNewViewModel
    {
        public CreatePersonRequest PersonModel { get; set; }
        public bool? LoginSuccess { get; set; }
    }
}
