namespace MovieDatabase.API.Models
{
    public class ErrorModel
    {
        public string Text { get; }

        public ErrorModel(string text)
        {
            Text = text;
        }

        public ErrorModel() { }
    }
}
