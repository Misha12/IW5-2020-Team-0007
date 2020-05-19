using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieDatabase.Web.ViewModels
{
    public class UserPassViewModel
    {
        public PasswordChangeRequest PasswordModel { get; set; }
        public bool? LoginSuccess { get; set; }
    }
}
