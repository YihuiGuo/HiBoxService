using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataService
{
    public interface IDataService
    {
        HiBoxUser GetUserInfo(string userId);

        HiBoxCabinet GetCabinet(Guid cabinetId);

        void RecordGatePropertyChange(string cabinetId, string lockerId, bool newProperty);

        void RecordRfidPropertyChange(string cabinetId, string lockerId, string newProperty);

    }
}
