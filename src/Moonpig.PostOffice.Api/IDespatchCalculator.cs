using System;
using System.Collections.Generic;
using Moonpig.PostOffice.Api.Model;

namespace Moonpig.PostOffice.Api
{
    public interface IDespatchCalculator
    {
        DespatchDate Calculate(IEnumerable<int> supplierLeadTimes, DateTime orderDate);
    }
}