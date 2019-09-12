using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace TestDrillDown
{
    public class DevAV
    {
        static string[] regions = new string[] { "Klocna", "Šaban", "Šabili", "Mitar Mirić", "Čika Spasoje" };
        static Dictionary<string, List<string>> categorizedProducts;

        public static Dictionary<string, List<string>> CategorizedProducts
        {
            get
            {
                if (categorizedProducts == null)
                {
                    categorizedProducts = new Dictionary<string, List<string>>();
                    categorizedProducts["Dizlo"] = new List<string>() { "Dizlo" };
                    categorizedProducts["Benga"] = new List<string>() { "Benga" };
                    categorizedProducts["Puno je sati"] = new List<string>() { "Desktop", "Laptop", "Tablet", "Printer" };
                    categorizedProducts["Bip bop bup"] = new List<string>() { "Television", "Home Audio", "Headphone", "DVD Player" };
                    categorizedProducts["Patrik Struart"] = new List<string>() { "GPS Unit", "Radar", "Car Alarm", "Car Accessories" };
                    categorizedProducts["Gajo je stvarnooo naj"] = new List<string>() { "Battery", "Charger", "Converter", "Tester", "AC/DC Adapter" };
                }
                return categorizedProducts;
            }
        }

        public static List<DevAVDataItem> GetTotalSales()
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            DateTime now = DateTime.Now;
            DateTime endDate = new DateTime(now.Year, now.Month, 1);
            List<DevAVDataItem> items = new List<DevAVDataItem>();
            foreach (string region in regions)
            {
                double companyFactor = rnd.NextDouble() * 0.6 + 1;
                foreach (string productCategory in CategorizedProducts.Keys)
                {
                    double categoryFactor = rnd.NextDouble() * 0.6 + 1;
                    foreach (string productName in CategorizedProducts[productCategory])
                    {
                        int maxSales = rnd.Next(60, 140);
                        for (int i = 0; i < 1000; i++)
                        {
                            if (i % 100 == 0)
                                maxSales = Math.Max(40, rnd.Next(maxSales - 20, maxSales + 20));
                            DateTime date = endDate.AddDays(-i - 1);
                            decimal sales = Convert.ToDecimal(rnd.Next(20, maxSales) * companyFactor * categoryFactor);
                            items.Add(DevAVDataItem.CreateByNameProductCategoryRegionSales(date, productName, productCategory, region, sales));
                        }
                    }
                }
            }
            return items;
        }

        public static List<DevAVDataItem> GetTotalSales(List<ExpensesEntity> data)
        {
            List<DevAVDataItem> items = new List<DevAVDataItem>();

            int lvl = 0;

            if (data.FirstOrDefault(x => x.Lvl == 1) != null) lvl = 1;

            regions = data.Where(x => x.Lvl == lvl).Select(x => x.OuName).Distinct().ToArray();
            var frodCat = data.Where(x => x.Lvl == lvl).Select(x => x.ExpenseName).Distinct().ToArray();

            foreach (string region in regions)
            {
                regions = data.Where(x => x.Lvl == 1).Select(x => x.ExpenseName).Distinct().ToArray();
                foreach (string productCategory in frodCat)
                {
                    var sale = data.Where(x => x.OuName.Equals(region) && x.ExpenseName.Equals(productCategory)).Select(x => x.ValueTotal).FirstOrDefault();
                    var itemsFilter = data.Where(x => x.OuName.Equals(region) && x.ExpenseName.Equals(productCategory)).ToList();
                    if (itemsFilter.Count < 6)
                    {
                        for(int i = 0; i<(6 - itemsFilter.Count); i++)
                        {
                            DateTime value = new DateTime(itemsFilter.FirstOrDefault().Year, itemsFilter.FirstOrDefault().Month, 15);
                            items.Add(DevAVDataItem.CreateByNameProductCategoryRegionSales(value.AddMonths(-(i + 1)), productCategory, productCategory, region, 0));
                        }
                    }
                    foreach (var item in itemsFilter)
                    {
                        DateTime value = new DateTime(item.Year, item.Month, 15);
                        items.Add(DevAVDataItem.CreateByNameProductCategoryRegionSales(value, productCategory, productCategory, region, item.ValueTotal));                       
                    }
                }                   
            }        
    
            return items;
        }

        public class DevAVDataItem
        {
            public static DevAVDataItem CreateByNameProductCategoryRegionSales(DateTime date, string productName, string productCategory, string region, decimal sales)
            {
                DevAVDataItem item = new DevAVDataItem();
                item.SaleDate = date;
                item.ProductName = productName;
                item.ProductCategory = productCategory;
                item.Region = region;
                item.Sales = sales;
                return item;
            }

            public int Year { get; private set; }
            public string Region { get; private set; }
            public decimal Sales { get; private set; }
            public decimal Cost { get; private set; }
            public string ProductCategory { get; private set; }
            public string ProductName { get; private set; }
            public string Company { get; private set; }
            public DateTime SaleDate { get; private set; }
            public decimal Charges { get; private set; }
            public decimal Penalties { get; private set; }

            DevAVDataItem() { }
        }
    }
}