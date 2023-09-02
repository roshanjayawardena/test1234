using Sewa_Domain.Common;

namespace Sewa_Domain.Entities
{
    public class Office : EntityBase
    {
        public string Address { get; set; }
        public string Region { get; set; }
        public string State { get; set; }
    }
}
