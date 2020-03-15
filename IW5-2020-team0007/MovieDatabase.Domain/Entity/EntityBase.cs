using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.Domain.Entity
{
    public class EntityBase<TID>
    {
        [Key]
        public TID ID { get; set; }
    }
}
