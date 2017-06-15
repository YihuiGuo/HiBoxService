using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class HiBoxLocker
    {
        public int LockerId { get; set; }

        public bool GateOpened { get; set; }

        public string ItemRfid { get; set; }

    }
}
