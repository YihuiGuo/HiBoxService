using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class HiBoxCategory
    {
        public Guid ItemType { get; set; }

        public string DisplayName { get; set; }

        public string Rent { get; set; }

        public decimal Deposit { get; set; }

        public int AvailableItem { get; set; }

        public int[] AvailableLocker { get; set; }

        public string Introduction { get; set; }

    }
}
