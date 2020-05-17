using System;

namespace MovieDatabase.Common.Extensions
{
    public static class DateTimeExtensions
    {
        /// <see cref="https://github.com/Misha12/GrillBot/blob/master/Grillbot/Database/Entity/Birthday.cs"/>
        public static int ComputeAge(this DateTime dateTime)
        {
            var today = DateTime.Today;
            var age = today.Year - dateTime.Year;
            if (dateTime.Date > today.AddYears(-age)) age--;

            return age;
        }
    }
}
