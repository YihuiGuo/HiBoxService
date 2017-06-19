using Infrastructure.DataService;
using Infrastructure.Entities;
using Infrastructure.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InfrastructureConsoleTest
{
    class Program
    {
        static void PrintStat(HiBoxCabinet cabinet)
        {
            Console.Write("---------------------------------------");
            Console.WriteLine();
            Console.Write("Current Cabinet has {0} categories", cabinet.Categories.Count);
            foreach (var cat in cabinet.Categories)
            {
                Console.WriteLine("Category Name : {0} , Deposit: {1} , Rent: {2} , Available Count: {3} , lockers :{4}",

                    cat.DisplayName, cat.Deposit, cat.Rent, cat.AvailableItem, String.Join(",", cat.AvailableLocker.ToArray()));
            }
        }
        static void Main(string[] args)
        {
            var cabid = Guid.Parse("ea1d410c-37d7-e611-9c39-f8cab83fa799");
            var itemid = Guid.Parse("c621927c-39d7-e611-9c39-f8cab83fa799");
            ILogger logger = new MockFileLogger();
            IDataService svc = new MockDataService(logger);
            var cab = svc.GetCabinet(cabid);
            var user = svc.GetUserInfo("SomeUser");
            PrintStat(cab);

            var billId = svc.GetAvailableItemLocker(cabid, user.UserId, itemid, "XXXXX-X");
            var bill = svc.QueryBill(billId);
            var lockerid = bill.LockerId;

            PrintStat(cab);
            //open gate
            svc.RecordGatePropertyChange(cabid, lockerid, true);
            //take away item
            svc.RecordRfidPropertyChange(cabid, lockerid, string.Empty);

            //[optional] close gate
            svc.RecordGatePropertyChange(cabid, lockerid, false);




            Thread.Sleep(60000);
            var ret_lockerid = svc.GetAvailableEmptyLocker(cabid, user.UserId, itemid);
            //open gate
            svc.RecordGatePropertyChange(cabid, lockerid, true);
            //put back item
            svc.RecordRfidPropertyChange(cabid, lockerid, "return item A");
            //close gate
            svc.RecordGatePropertyChange(cabid, lockerid, false);

            Console.Read();
        }
    }
}
