namespace MovieDatabase.Domain.DTO
{
    public class RateDetail : Rate
    {
        public Movie Movie { get; set; }

        public RateDetail(Entity.Rate rate) : base(rate)
        {
            if(rate.Movie != null)
                Movie = new Movie(rate.Movie);
        }

        public RateDetail() { }
    }
}
