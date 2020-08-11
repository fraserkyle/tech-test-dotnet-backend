using System;
using System.Collections.Generic;
using Moonpig.PostOffice.Api.Model;
using Moonpig.PostOffice.Data;

namespace Moonpig.PostOffice.Api
{
    public interface IDespatchCalculator
    {
        DespatchDate Calculate(IEnumerable<Supplier> suppliers, DateTime orderDate, IEnumerable<BlockedDate> blockedDates);
    }
}