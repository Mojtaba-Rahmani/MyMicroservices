

namespace Ordering.Domain.Commen
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public string CreateB { get; set; }

        public DateTime CreateDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }

    }
}
