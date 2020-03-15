namespace MovieDatabase.Domain.DTO
{
    public class Rate : ItemBase<long>
    {
        public long MovieID { get; set; }
        public string Text { get; set; }
        public int Score { get; set; }
    }
}
