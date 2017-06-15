using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Entities;

namespace Infrastructure.DataService
{
    public class MockDataService : IDataService
    {
        static List<HiBoxCabinet> Cabinets;
        /// <summary>
        /// In this mock service , we have a cabinet
        /// </summary>
        /// <param name="cabinetId"></param>
        /// <returns></returns>
        /// 
        public MockDataService()
        {
            Cabinets = new List<HiBoxCabinet>();
            //mock data for cabinet with id :
            //"ea1d410c-37d7-e611-9c39-f8cab83fa799"
            var lockers1 = new List<HiBoxLocker>();
            int j = 0;
            for (; j < 5; j++)
            {
                lockers1.Add(new HiBoxLocker()
                {
                    LockerId = j,
                    GateOpened = false,
                    ItemRfid = "something" + j.ToString()
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
            }
            var cab1 = new HiBoxCabinet()
            {
                CabinetId = Guid.Parse("ea1d410c-37d7-e611-9c39-f8cab83fa799"),
                Categories = new List<HiBoxCategory>()
                    {
                    new HiBoxCategory()
                    {
                        ItemType=Guid.Parse("c621927c-39d7-e611-9c39-f8cab83fa799"),
                        Deposit=50,
                        DisplayName="something",
                        AvailableItem = 5,
                        AvailableLocker = new int[] { 0,1,2,3,4},
                        Introduction="mock-up item One",
                        Rent="2/h"
                    },
                    new HiBoxCategory()
                    {
                        ItemType = Guid.Parse("bdbcf299-7eda-e611-9c3b-f8cab83fa799"),
                        Deposit=60,
                        DisplayName="something",
                        AvailableItem = 5,
                        AvailableLocker = new int[] { 5,6,7,8,9},
                        Introduction="mock-up item Two",
                        Rent="4/h"
                    }
                    },
                Location = "MSBJW",
                Lockers = lockers1
            };


            Cabinets.Add(cab1);
        }
        public HiBoxCabinet GetCabinet(Guid cabinetId)
        {
            return Cabinets.First(p => p.CabinetId == cabinetId);
        }

        public HiBoxUser GetUserInfo(string userId)
        {
            return new HiBoxUser()
            {
                UserId = "mockuserfoo",
                UnsettledBills = new string[] { "bill1", "bill2" }
            };
        }

        public void RecordGatePropertyChange(string cabinetId, string lockerId, bool newProperty)
        {
            
        }

        public void RecordRfidPropertyChange(string cabinetId, string lockerId, string newProperty)
        {
            throw new NotImplementedException();
        }
    }
}
