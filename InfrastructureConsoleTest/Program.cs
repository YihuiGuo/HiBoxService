using Infrastructure.DataService;
using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                Console.WriteLine("Category Name : {0} , Deposit: {1} , Rent: {2} , Available Count: {3} ",

                    cat.DisplayName, cat.Deposit, cat.Rent, cat.AvailableItem);
            }
        }
        static void Main(string[] args)
        {
            IDataService svc = new MockDataService();
            var cab = svc.GetCabinet(Guid.Parse("ea1d410c-37d7-e611-9c39-f8cab83fa799"));

            PrintStat(cab);
            Console.Read();
        }
    }
}
    