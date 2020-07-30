using System;
using System.Collections.Generic;
using System.Linq;
using Moonpig.PostOffice.Api.Model;

namespace Moonpig.PostOffice.Api
{
    public class DespatchCalculator : IDespatchCalculator
    {
        public DespatchDate Calculate(IEnumerable<int> supplierLeadTimes, DateTime orderDate)
        {
            var maxLeadTime = supplierLeadTimes.Max();

            orderDate = GetNextPostOfficeWorkingDate(orderDate);

            return new DespatchDate()
            {
                Date = AddLeadTimeSkippingWeekends(orderDate, maxLeadTime)
            };
        }

        private static DateTime GetNextPostOfficeWorkingDate(DateTime orderDate)
        {
            if (orderDate.DayOfWeek == DayOfWeek.Saturday)
            {
                orderDate = orderDate.AddDays(2);
            }
            else if (orderDate.DayOfWeek == DayOfWeek.Sunday)
            {
                orderDate = orderDate.AddDays(1);
            }

            return orderDate;
        }

        private DateTime AddLeadTimeSkippingWeekends(DateTime orderDate, int leadTime)
        {
            var result = orderDate;

            while (leadTime > 0)
            {
                result = result.AddDays(1);

                if (result.DayOfWeek == DayOfWeek.Saturday || result.DayOfWeek == DayOfWeek.Sunday)
                {
                    continue;
                }

                leadTime--;
            }

            return result;
        }
    }
}
