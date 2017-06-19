using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Helpers
{
    public static class LockerHelper
    {
        public static bool IsLockerAvailable(HiBoxLocker locker)
        {
            return locker.GateOpened == false && !string.IsNullOrEmpty(locker.ItemRfid);
        }

        public static bool ShouldStartCharging(HiBoxLocker locker)
        {
            return locker.GateOpened == true && string.IsNullOrEmpty(locker.ItemRfid);
        }

        public static bool ShouldStopCharging(HiBoxLocker locker)
        {
            return locker.GateOpened == false && !string.IsNullOrEmpty(locker.ItemRfid);
        }

        public static int ArrangeLocker(HiBoxCabinet cabinet, Guid itemType)
        {
            var category = cabinet.Categories.First(c => c.ItemType == itemType);
            var lockerId = category.AvailableLocker.ToList().FirstOrDefault();
            category.AvailableLocker.Remove(lockerId);
            category.AvailableItem--;
            return lockerId;
        }
    }
}
