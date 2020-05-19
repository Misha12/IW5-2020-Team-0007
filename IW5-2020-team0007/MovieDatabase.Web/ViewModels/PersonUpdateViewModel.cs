using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieDatabase.Web.ViewModels
{
    public class PersonUpdateViewModel
    {
        public long ID { get; set; }
        public EditPersonRequest EditPerson { get; set; }
        public bool? LoginSuccess { get; set; }
    }
}
