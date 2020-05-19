using System.Collections.Generic;

namespace MovieDatabase.Web.ViewModels
{
    public class RegisterViewModel
    {
        public RegisterRequest UserModel { get; set; }
        public List<string> Messages { get; set; }
        public bool? LoginSuccess { get; set; }
        public bool? Success { get; set; }
    }
}
