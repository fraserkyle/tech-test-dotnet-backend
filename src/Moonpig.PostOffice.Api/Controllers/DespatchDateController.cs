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
        private readonly IDespatchCalculator _despatchCalculator;

        public DespatchDateController(IDespatchDbContext dbContext, IDespatchCalculator despatchCalculator)
        {
            _dbContext = dbContext;
            _despatchCalculator = despatchCalculator;
        }


        [HttpGet]
        public DespatchDate Get(List<int> productIds, DateTime orderDate)
        {
            return _despatchCalculator.Calculate(GetSupplierLeadTimes(productIds.ToArray()), orderDate);
        }

        private IEnumerable<int> GetSupplierLeadTimes(IEnumerable<int> productIds)
        {
            var distinctProductIds = productIds.Distinct().ToArray();
            var exceptions = new List<Exception>();
            var products = _dbContext.Products.Where(x => distinctProductIds.Contains(x.ProductId)).Distinct().ToArray();

            if (products.Length != distinctProductIds.Length)
            {
                exceptions.AddRange(distinctProductIds
                    .Where(productId => products.All(product => product.ProductId != productId))
                    .Select(productId => new ProductNotFoundException(productId)));
            }

            var distinctSupplierIds = products.Select(x => x.SupplierId).Distinct().ToArray();

            var suppliers = _dbContext.Suppliers.Where(supplier => distinctSupplierIds.Contains(supplier.SupplierId))
                .ToArray();

            if (suppliers.Length != distinctSupplierIds.Length)
            {
                exceptions.AddRange(distinctSupplierIds
                    .Where(supplierId => suppliers.All(supplier => supplier.SupplierId != supplierId))
                    .Select(supplierId => new SupplierNotFoundException(supplierId)));
            }

            if (exceptions.Any())
            {
                throw new AggregateException(exceptions);
            }

            return suppliers.Select(x => x.LeadTime);
        }
    }
}
