namespace MovieDatabase.Domain
{
    public abstract class ItemBase<TID>
    {
        public TID ID { get; set; }
    }
}
