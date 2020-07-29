using System;

namespace Moonpig.PostOffice.Api.Exceptions
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(int id) : base($"No product was found relating to product id {id}.")
        {
        }
    }
}
