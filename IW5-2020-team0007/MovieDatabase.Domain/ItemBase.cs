namespace MovieDatabase.Domain
{
    public abstract class ItemBase<TID>
    {
        public TID ID { get; set; }

        protected ItemBase(TID id)
        {
            ID = id;
        }

        protected ItemBase() { }
    }
}
