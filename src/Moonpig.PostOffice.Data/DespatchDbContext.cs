namespace Moonpig.PostOffice.Data
{
    using System.Linq;
    using System.Collections.Generic;
    using System;

    public class DespatchDbContext : IDespatchDbContext
    {
        public IQueryable<Supplier> Suppliers =>
            new List<Supplier>
            {
                new Supplier
                {
                    SupplierId = 1,
                    Name = "Acme Corporation",
                    LeadTime = 1
                },
                new Supplier
                {
                    SupplierId = 2,
                    Name = "Sunnyside Flowers",
                    LeadTime = 2
                },
                new Supplier
                {
                    SupplierId = 3,
                    Name = "Drinks Warehouse",
                    LeadTime = 1
                },
                new Supplier
                {
                    SupplierId = 4,
                    Name = "TailSpin Toys",
                    LeadTime = 3
                },
                new Supplier
                {
                    SupplierId = 5,
                    Name = "Disney",
                    LeadTime = 6
                }
                ,
                new Supplier
                {
                    SupplierId = 6,
                    Name = "Tacky T-Shirts",
                    LeadTime = 13
                }
            }.AsQueryable();

        public IQueryable<Product> Products =>
            new List<Product>
            {
                new Product { ProductId = 1, Name = "Greetings Card", SupplierId = 1 },
                new Product { ProductId = 2, Name = "Flowers", SupplierId = 2 },
                new Product { ProductId = 3, Name = "Soft Toy", SupplierId = 4 },
                new Product { ProductId = 4, Name = "Chocolate", SupplierId = 1 },
                new Product { ProductId = 5, Name = "Canvas", SupplierId = 1 },
                new Product { ProductId = 6, Name = "Plant", SupplierId = 2 },
                new Product { ProductId = 7, Name = "Alcohol", SupplierId = 1 },
                new Product { ProductId = 8, Name = "Box Set", SupplierId = 7 },
                new Product { ProductId = 9, Name = "Frozen Doll", SupplierId = 5 },
                new Product { ProductId = 10, Name = "Personalised Tacky T-Shirt", SupplierId = 6 },
                new Product { ProductId = 9999, Name = "False Product", SupplierId = 9999 }
            }.AsQueryable();

        public IQueryable<BlockedDate> BlockedDates
       {
           get
           {
               return new List<BlockedDate>
                          {
                              new BlockedDate { SupplierId = 1, Date = new DateTime(2018,03,13) },
                              new BlockedDate { SupplierId = 1, Date = new DateTime(2018,03,23) },
                              new BlockedDate { SupplierId = 1, Date = new DateTime(2018,03,30) },
                              new BlockedDate { SupplierId = 1, Date = new DateTime(2018,04,02) },
                              new BlockedDate { SupplierId = 1, Date = new DateTime(2018,05,14) },
                              new BlockedDate { SupplierId = 1, Date = new DateTime(2018,05,15) },
                              new BlockedDate { SupplierId = 1, Date = new DateTime(2018,05,16) },
                              new BlockedDate { SupplierId = 1, Date = new DateTime(2018,06,11) },
                              new BlockedDate { SupplierId = 1, Date = new DateTime(2018,06,12) },
                              new BlockedDate { SupplierId = 1, Date = new DateTime(2018,06,13) },
                              new BlockedDate { SupplierId = 1, Date = new DateTime(2018,06,14) },
                              new BlockedDate { SupplierId = 1, Date = new DateTime(2018,06,15) },
                              new BlockedDate { SupplierId = 1, Date = new DateTime(2018,06,18) },
                              new BlockedDate { SupplierId = 1, Date = new DateTime(2018,06,19) },
                              new BlockedDate { SupplierId = 1, Date = new DateTime(2018,06,20) },
                              new BlockedDate { SupplierId = 1, Date = new DateTime(2018,06,21) },
                              new BlockedDate { SupplierId = 2, Date = new DateTime(2018,03,20) },
                              new BlockedDate { SupplierId = 2, Date = new DateTime(2018,04,16) },
                              new BlockedDate { SupplierId = 2, Date = new DateTime(2018,04,17) },
                              new BlockedDate { SupplierId = 2, Date = new DateTime(2018,06,11) },
                              new BlockedDate { SupplierId = 2, Date = new DateTime(2018,06,12) },
                              new BlockedDate { SupplierId = 2, Date = new DateTime(2018,06,13) },
                              new BlockedDate { SupplierId = 2, Date = new DateTime(2018,06,14) },
                              new BlockedDate { SupplierId = 2, Date = new DateTime(2018,06,15) },
                              new BlockedDate { SupplierId = 2, Date = new DateTime(2018,06,18) },
                              new BlockedDate { SupplierId = 2, Date = new DateTime(2018,06,19) },
                              new BlockedDate { SupplierId = 2, Date = new DateTime(2018,06,20) },
                              new BlockedDate { SupplierId = 2, Date = new DateTime(2018,06,21) }
                          }.AsQueryable();
           }
       }
    }
}
