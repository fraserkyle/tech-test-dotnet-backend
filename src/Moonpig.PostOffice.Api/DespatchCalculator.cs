using System;
using System.Collections.Generic;
using System.Linq;
using Moonpig.PostOffice.Api.Model;
using Moonpig.PostOffice.Data;

namespace Moonpig.PostOffice.Api
{
    public class DespatchCalculator : IDespatchCalculator
    {
        public DespatchDate Calculate(IEnumerable<Supplier> suppliers, DateTime orderDate, IEnumerable<BlockedDate> blockedDates)
        {
            orderDate = GetNextPostOfficeWorkingDate(orderDate);

            return new DespatchDate()
            {
                Date = suppliers.Max(x => AddLeadTimeSkippingWeekends(x, orderDate, blockedDates.Where(y => y.SupplierId == x.SupplierId).Select(y => y.Date).ToArray()))
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

        private static DateTime AddLeadTimeSkippingWeekends(Supplier supplier, DateTime orderDate, DateTime[] blockedDates)
        {
            var result = orderDate;

            var leadTime = supplier.LeadTime;

            while (leadTime > 0)
            {
                result = result.AddDays(1);

                if (result.DayOfWeek == DayOfWeek.Saturday || result.DayOfWeek == DayOfWeek.Sunday || blockedDates.Contains(result))
                {
                    continue;
                }

                leadTime--;
            }

            return result;
        }
    }
}
