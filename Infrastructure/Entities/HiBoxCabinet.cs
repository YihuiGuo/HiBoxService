using Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class HiBoxCabinet
    {
        public Guid CabinetId { get; set; }

        public List<HiBoxCategory> Categories { get; set; }

        public string Location { get; set; }

        public List<HiBoxLocker> Lockers { get; set; }


    }
}
