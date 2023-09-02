using Sewa_Domain.Common;
using Sewa_Domain.Common.Enums;

namespace Sewa_Domain.Entities
{
    public class Ticket:EntityBase
    {
        public string Token { get; set; }
        public Guid ServiceTypeId { get; set; }
        public ServiceType ServiceType { get; set; }
        public Guid OfficeId { get; set; }
        public Office Office { get; set; }
        public string? Description { get; set; }
        public ResoloutionStatusEnum Status { get; set; }
        public DateTime? ServiceStartDateTime { get; set; }
        public DateTime? ServiceEndDateTime { get; set; }
    }
}
