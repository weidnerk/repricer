using dsmodels;
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
        private static string log_file = "repricer.txt";

        static void Main(string[] args)
        {
            string msg = null;
            Task.Run(async () =>
            {
                msg = await repricer();
            }).Wait();
            Console.WriteLine(msg);
            // Console.ReadKey();
        }

        static async Task<string> repricer()
        {
            int priceDiffer = 0;
            string subject = "Repricer";
            string body = null;
            string log = null;
            int i = 0;
            int count = db.PostedListings.Count();
            foreach(PostedListing listing in db.PostedListings.ToList())
            {
                Console.WriteLine((++i) + "/" + count.ToString());
                try { 
                    var r = await sclib.Scrape.GetDetail(listing.SourceUrl);
                    //Console.WriteLine(listing.Title);
                    //Console.WriteLine(r.price);
                    if (r.price != listing.SupplierPrice)
                    {
                        ++priceDiffer;
                        if (listing.SupplierPrice < r.price)
                        {
                            log = "Prices differ -- NEED TO ADJUST - supplier price increased.";
                            body += log + "\n";
                            dsutil.DSUtil.WriteFile(log_file, log);
                        }
                        else
                        {
                            log = "Prices differ";
                            body += log + "\n";
                            dsutil.DSUtil.WriteFile(log_file, log);
                        }

                        log = string.Format("{0} {1}", listing.ListedItemID, listing.Title);
                        body += log + "\n";
                        dsutil.DSUtil.WriteFile(log_file, log);

                        log = string.Format("CategoryID: {0}", listing.CategoryID);
                        body += log + "\n";
                        dsutil.DSUtil.WriteFile(log_file, log);

                        log = string.Format("last price: {0}", r.price);
                        body += log + "\n";
                        dsutil.DSUtil.WriteFile(log_file, log);

                        log = string.Format("recorded supplier price: {0}", listing.SupplierPrice);
                        body += log + "\n";
                        dsutil.DSUtil.WriteFile(log_file, log);

                        log = string.Format("{0}", listing.SourceUrl);
                        body += log + "\n";
                        dsutil.DSUtil.WriteFile(log_file, log);

                        log = string.Empty;
                        body += log + "\n\n";
                        dsutil.DSUtil.WriteFile(log_file, log);
                    }
                }
                catch (Exception exc)
                {
                    dsutil.DSUtil.WriteFile(log_file, string.Format("{0} {1}", listing.ListedItemID, listing.Title));
                    dsutil.DSUtil.WriteFile(log_file, string.Format("{0}", exc.Message));
                }
            }
            if (!string.IsNullOrEmpty(body))
                DSUtil.SendMailDev("ventures2019@gmail.com", subject, body);

            string msg = string.Format("found {0} different prices.", priceDiffer);
            return msg;
        }
    }
}
