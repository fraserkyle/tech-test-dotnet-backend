using System;

namespace Moonpig.PostOffice.Api.Exceptions
{
    public class SupplierNotFoundException : Exception
    {
        public SupplierNotFoundException(int id) : base($"No supplier was found relating to supplier id {id}")
        {
        }
    }
}
