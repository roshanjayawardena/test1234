using Sewa_Domain.Common;
using Sewa_Domain.Common.Enums;

namespace Sewa_Domain.Entities
{
    public class BusinessUser : EntityBase
    {
        public string Name { get; set; }
        public Guid OfficeId { get; set; }
        public Office Office { get; set; }
        public BusinessUserStatusEnum Status { get; set; }  
    }
}
