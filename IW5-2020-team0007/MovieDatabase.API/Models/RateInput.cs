namespace MovieDatabase.API.Models
{
    public class RateInput
    {
        public string Text { get; set; }
        public int Score { get; set; }

        public bool IsValid() => !string.IsNullOrEmpty(Text);

        public int GetScore()
        {
            if (Score < 0) return 0;
            if (Score > 100) return 100;

            return Score;
        }
    }
}
