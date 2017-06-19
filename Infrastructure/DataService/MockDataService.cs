using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Entities;
using Infrastructure.Helpers;
using Infrastructure.Log;

namespace Infrastructure.DataService
{
    public class MockDataService : IDataService
    {
        static List<HiBoxCabinet> Cabinets;
        static List<HiBoxBill> Bills;
        ILogger Logger;
        /// <summary>
        /// In this mock service , we have a cabinet
        /// </summary>
        /// <param name="cabinetId"></param>
        /// <returns></returns>
        /// 
        public MockDataService(ILogger logger)
        {
            Logger = logger;
            Cabinets = new List<HiBoxCabinet>();
            Bills = new List<HiBoxBill>();
            //mock data for cabinet with id :
            //"ea1d410c-37d7-e611-9c39-f8cab83fa799"
            var lockers1 = new List<HiBoxLocker>();
            var lockers2 = new List<HiBoxLocker>();
            int j = 0;
            for (; j < 5; j++)
            {
                lockers1.Add(new HiBoxLocker()
                {
                    LockerId = j,
                    GateOpened = false,
                    ItemRfid = "something" + j.ToString()
                });
                lockers2.Add(new HiBoxLocker()
                {
                    LockerId = j,
                    GateOpened = false,
                    ItemRfid = "locker2 something" + j.ToString()
                });
            }
            for (; j < 10; j++)
            {
                lockers1.Add(new HiBoxLocker()
                {
                    LockerId = j,
                    GateOpened = false,
                    ItemRfid = "something else" + j.ToString()
                });
                lockers2.Add(new HiBoxLocker()
                {
                    LockerId = j,
                    GateOpened = false,
                    ItemRfid = "locker2 something else" + j.ToString()
                });
            }
            var cab2 = new HiBoxCabinet()
            {
                CabinetId = Guid.Parse("eb1d410c-37d7-e611-9c39-f8cab83fa799"),
                Categories = new List<HiBoxCategory>()
                    {
                    new HiBoxCategory()
                    {
                        ItemType=Guid.Parse("c721927c-39d7-e611-9c39-f8cab83fa799"),
                        Deposit=50,
                        DisplayName="C",
                        AvailableItem = 5,
                        AvailableLocker = new List<int>() { 0,1,2,3,4},
                        Introduction="mock-up item One",
                        Rent="2/h"
                    },
                    new HiBoxCategory()
                    {
                        ItemType = Guid.Parse("bebcf299-7eda-e611-9c3b-f8cab83fa799"),
                        Deposit=60,
                        DisplayName="D",
                        AvailableItem = 5,
                        AvailableLocker = new List<int>() { 5,6,7,8,9},
                        Introduction="mock-up item Two",
                        Rent="4/h"
                    }
                    },
                Location = "MSBJW",
                Lockers = lockers1
            };
            var cab1 = new HiBoxCabinet()
            {
                CabinetId = Guid.Parse("ea1d410c-37d7-e611-9c39-f8cab83fa799"),
                Categories = new List<HiBoxCategory>()
                    {
                    new HiBoxCategory()
                    {
                        ItemType=Guid.Parse("c621927c-39d7-e611-9c39-f8cab83fa799"),
                        Deposit=50,
                        DisplayName="A",
                        AvailableItem = 5,
                        AvailableLocker = new List<int>() { 0,1,2,3,4},
                        Introduction="mock-up item One",
                        Rent="2/h"
                    },
                    new HiBoxCategory()
                    {
                        ItemType = Guid.Parse("bdbcf299-7eda-e611-9c3b-f8cab83fa799"),
                        Deposit=60,
                        DisplayName="B",
                        AvailableItem = 5,
                        AvailableLocker = new List<int>() { 5,6,7,8,9},
                        Introduction="mock-up item Two",
                        Rent="4/h"
                    }
                    },
                Location = "MSBJW",
                Lockers = lockers1
            };


            Cabinets.Add(cab1);
            Cabinets.Add(cab2);
        }
        public HiBoxCabinet GetCabinet(Guid cabinetId)
        {
            return Cabinets.First(p => p.CabinetId == cabinetId);
        }

        public HiBoxUser GetUserInfo(string userId)
        {
            return new HiBoxUser()
            {
                UserId = userId,
                UnsettledBills = new List<string>()
            };
        }

        public void RecordGatePropertyChange(Guid cabinetId, int lockerId, bool newProperty)
        {
            var cabinet = Cabinets.First(c => c.CabinetId == cabinetId);
            var locker = cabinet.Lockers.First(l => l.LockerId == lockerId);
            var oldStatus = locker.GateOpened == true ? "Opened" : "Closed";
            locker.GateOpened = newProperty;
            var newStatus = newProperty == true ? "Opened" : "Closed";
            Logger.LogMessage($"Update Gate Status: Cabinet:{cabinet.CabinetId}, Locker:{locker.LockerId}, Old Status:{oldStatus}, New Status:{newStatus}");
            if (LockerHelper.ShouldStopCharging(locker))
            {
                var bill = Bills.First(b => b.CabinetId == cabinetId && b.LockerId == lockerId);
                bill.EndTime = DateTime.Now;
                bill.Cost = 100;
                Logger.LogMessage($"Stop Timing: Bill Id: {bill.BillId},Start Time:{bill.StartTime}, End Time:{bill.EndTime}");
            }
        }

        public void RecordRfidPropertyChange(Guid cabinetId, int lockerId, string newProperty)
        {
            var cabinet = Cabinets.First(c => c.CabinetId == cabinetId);
            var locker = cabinet.Lockers.First(l => l.LockerId == lockerId);
            var oldStatus = locker.ItemRfid;
            locker.ItemRfid = newProperty;
            Logger.LogMessage($"Update Rfid Status: Cabinet:{cabinet.CabinetId}, Locker:{locker.LockerId}, Old Status:{oldStatus}, New Status:{newProperty}");
            if (LockerHelper.ShouldStartCharging(locker))
            {
                var bill = Bills.First(b => b.CabinetId == cabinetId && b.LockerId == lockerId);
                bill.StartTime = DateTime.Now;
                Logger.LogMessage($"Start Timing: Bill Id: {bill.BillId},Start Time:{bill.StartTime}");

            }

        }

        public int GetAvailableEmptyLocker(Guid cabinetId, string userId, Guid itemType)
        {
            var targetCabinet = Cabinets.FirstOrDefault(c => c.CabinetId == cabinetId);
            var targetCategory = targetCabinet?.Categories.FirstOrDefault(cat => cat.ItemType == itemType);
            if (targetCategory == null)
            {
                Logger.LogWarning($"No Available Category When Return, Cabinet Id :{targetCabinet.CabinetId}, Expect Item Type:{itemType}");
                return -1;
            }
            var lockerId = targetCabinet.Lockers.FirstOrDefault(l => string.IsNullOrEmpty(l.ItemRfid)).LockerId;
            targetCategory.AvailableLocker.Add(lockerId);
            targetCategory.AvailableItem++;
            return lockerId;
        }

        public Guid GetAvailableItemLocker(Guid cabinetId, string userId, Guid itemType, string TradeId)
        {
            var targetCabinet = Cabinets.First(c => c.CabinetId == cabinetId);
            var targetCategory = targetCabinet.Categories.First(cat => cat.ItemType == itemType);
            if (targetCategory == null)
            {
                Logger.LogWarning($"No Available Category When Borrow, Cabinet Id :{targetCabinet.CabinetId}, Expect Item Type:{itemType}");
                return new Guid();
            }
            var expectedAmount = targetCategory.Deposit;

            var paidAmount = BankHelper.CheckStatusAndAmount(TradeId);

            if (expectedAmount == paidAmount)
            {
                var lockerid = targetCabinet.Lockers.First(l => LockerHelper.IsLockerAvailable(l)).LockerId;
                targetCategory.AvailableLocker.Remove(lockerid);
                targetCategory.AvailableItem--;

                var id = Guid.NewGuid();
                Logger.LogMessage($"Assigned Locker:Cabinet Id :{cabinetId}, Locker Id:{lockerid}, Generated Bill:{id}");
                Bills.Add(new HiBoxBill()
                {
                    BillId = id,
                    BillType = Constants.BillType.Deposit,
                    CabinetId = cabinetId,
                    DisplayName = targetCategory.DisplayName,
                    UserId = userId,
                    ItemType = itemType,
                    LockerId = lockerid,
                    Rent = targetCategory.Rent
                });

                Console.WriteLine("Opening Gate" + lockerid);
                return id;
            }
            else return new Guid();

        }

        public HiBoxBill QueryBill(Guid billId)
        {
            var bill = Bills.FirstOrDefault(b => b.BillId == billId);
            return bill;
        }
    }
}
