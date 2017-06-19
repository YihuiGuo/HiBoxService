using Infrastructure.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class HiBoxBill
    {
        public Guid BillId { get; set; }

        public BillType BillType { get; set; }

        public string UserId { get; set; }

        public Guid ItemType { get; set; }

        public string DisplayName { get; set; }

        public string Rent { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public Guid CabinetId { get; set; }

        public int LockerId { get; set; }

        public int Cost { get; set; }
    }
}
