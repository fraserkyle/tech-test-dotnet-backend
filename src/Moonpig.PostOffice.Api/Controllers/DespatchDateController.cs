using Moonpig.PostOffice.Api.Exceptions;

namespace Moonpig.PostOffice.Api.Controllers
{
    using Data;
    using Microsoft.AspNetCore.Mvc;
    using Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [Route("api/[controller]")]
    public class DespatchDateController : Controller
    {
        private readonly IDespatchDbContext _dbContext;

        public DespatchDateController(IDespatchDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet]
        public DespatchDate Get(List<int> productIds, DateTime orderDate)
        {
            var maxLeadTime = orderDate;

            foreach (var id in productIds)
            {
                var supplierId = _dbContext.Products.SingleOrDefault(x => x.ProductId == id)?.SupplierId;

                if (supplierId == null)
                {
                    throw new ProductNotFoundException(id);
                }

                var supplierLeadTime = _dbContext.Suppliers.SingleOrDefault(x => x.SupplierId == supplierId)?.LeadTime;

                if (supplierLeadTime == null)
                {
                    throw new SupplierNotFoundException(supplierId.Value);
                }

                var currentLeadTime = orderDate.AddDays(supplierLeadTime.Value);

                if (currentLeadTime > maxLeadTime)
                {
                    maxLeadTime = currentLeadTime;
                }
            }

            switch (maxLeadTime.DayOfWeek)
            {
                case DayOfWeek.Saturday:
                    return new DespatchDate { Date = maxLeadTime.AddDays(2) };
                case DayOfWeek.Sunday:
                    return new DespatchDate { Date = maxLeadTime.AddDays(1) };
                default:
                    return new DespatchDate { Date = maxLeadTime };
            }
        }
    }
}
