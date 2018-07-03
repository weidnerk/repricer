using dsutil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repricer
{
    class Program
    {
        static DataModelsDB db = new DataModelsDB();
        static void Main(string[] args)
        {
            Task.Run(async () =>
            {
                await repricer();
            }).Wait();
            Console.ReadKey();
        }

        static async Task repricer()
        {
            foreach(PostedListing listing in db.Listings )
            {
                var r = await sclib.Scrape.GetDetail(listing.SourceUrl);
                Console.WriteLine(listing.Title);
                Console.WriteLine(r.price);
                if (r.price != listing.SupplierPrice)
                {
                    await DSUtil.SendMailDev("kevinw@midfinance.com");
                    Console.WriteLine("prices different");
                }
                else
                    Console.WriteLine("prices match");

                Console.WriteLine("");
            }
        }
    }
}
