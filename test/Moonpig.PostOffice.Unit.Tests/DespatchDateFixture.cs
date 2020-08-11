using System;
using Moonpig.PostOffice.Api;
using Moonpig.PostOffice.Api.Controllers;
using Moonpig.PostOffice.Data;

namespace Moonpig.PostOffice.Tests
{
    public class DespatchDateFixture
    {
        public DespatchDateFixture()
        {
            Controller = new DespatchDateController(new DespatchDbContext(), new DespatchCalculator());
        }

        public DespatchDateController Controller { get; set; }

        public DateTime DefaultOrderDate => new DateTime(2020, 7, 27);

        public DateTime SaturdayOrderDate => new DateTime(2018, 1, 20);

        public DateTime SupplierOneBlockedDateStart => new DateTime(2018, 06, 11);

        public DateTime SundayOrderDate => new DateTime(2018, 1, 21);

        public int UnknownSupplierProductId => 9999;

        public int UnknownProductId => 99999;
    }
}
