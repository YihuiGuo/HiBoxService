﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class HiBoxUser
    {
        public string UserId { get; set; }

        public List<string> UnsettledBills { get; set; }
    }
}
