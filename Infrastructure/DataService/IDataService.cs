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

        Guid GetAvailableItemLocker(Guid cabinetId, string userId, Guid itemType, string TradeId);

        int GetAvailableEmptyLocker(Guid cabinetId, string userId, Guid itemType);

        void RecordGatePropertyChange(Guid cabinetId, int lockerId, bool newProperty);

        void RecordRfidPropertyChange(Guid cabinetId, int lockerId, string newProperty);

        HiBoxBill QueryBill(Guid billId);

    }
}
