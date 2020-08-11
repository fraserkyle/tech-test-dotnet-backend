using Moonpig.PostOffice.Api.Model;
using Shouldly;
using System;
using System.Collections.Generic;
using Moonpig.PostOffice.Api.Exceptions;
using Xunit;

namespace Moonpig.PostOffice.Tests
{
    public class WhenSupplierBlockedDatesCauseLaterDespatchThanOtherSupplierBlockedDates : DespatchDateControllerTestBase
    {
         private DespatchDate _result;
        private readonly Exception _exception;

        public WhenSupplierBlockedDatesCauseLaterDespatchThanOtherSupplierBlockedDates(DespatchDateFixture fixture) : base(fixture)
        {
            _exception = Record.Exception(() =>
                _result = Fixture.Controller.Get(new List<int>() { 1, 2 }, Fixture.SupplierOneBlockedDateStart));
        }

        [Fact]
        public void ShouldNotThrowException()
        {
            _exception.ShouldBeNull();
        }

        [Fact]
        public void ShouldSkipBlockedDates()
        {
            _result.Date.ShouldBe(new DateTime(2018, 6, 25));
        }
    }

    public class WhenSupplierBlockedDatesCauseLaterDespatchThanLargeLeadtimeSupplier : DespatchDateControllerTestBase
    {
         private DespatchDate _result;
        private readonly Exception _exception;

        public WhenSupplierBlockedDatesCauseLaterDespatchThanLargeLeadtimeSupplier(DespatchDateFixture fixture) : base(fixture)
        {
            _exception = Record.Exception(() =>
                _result = Fixture.Controller.Get(new List<int>() { 1, 9 }, Fixture.SupplierOneBlockedDateStart));
        }

        [Fact]
        public void ShouldNotThrowException()
        {
            _exception.ShouldBeNull();
        }

        [Fact]
        public void ShouldSkipBlockedDates()
        {
            _result.Date.ShouldBe(new DateTime(2018, 6, 22));
        }
    }

    public class WhenSupplierHasSingleBlockedDate : DespatchDateControllerTestBase
    {
         private DespatchDate _result;
        private readonly Exception _exception;

        public WhenSupplierHasSingleBlockedDate(DespatchDateFixture fixture) : base(fixture)
        {
            _exception = Record.Exception(() =>
                _result = Fixture.Controller.Get(new List<int>() { 1 }, Fixture.SupplierOneBlockedDateStart));
        }

        [Fact]
        public void ShouldNotThrowException()
        {
            _exception.ShouldBeNull();
        }

        [Fact]
        public void ShouldSkipBlockedDates()
        {
            _result.Date.ShouldBe(new DateTime(2018, 6, 22));
        }
    }

    public class WhenProductIsNotFound : DespatchDateControllerTestBase
    {
        private DespatchDate _result;
        private readonly Exception _exception;

        public WhenProductIsNotFound(DespatchDateFixture fixture) : base(fixture)
        {
            _exception = Record.Exception(() =>
                _result = Fixture.Controller.Get(new List<int>() {Fixture.UnknownProductId}, Fixture.DefaultOrderDate));
        }

        [Fact]
        public void ShouldReturnNull()
        {
            _result.ShouldBeNull();
        }

        [Fact]
        public void ShouldThrowProductNotFoundException()
        {
            _exception.ShouldBeOfType<AggregateException>();
            _exception.InnerException.ShouldBeOfType<ProductNotFoundException>();
        }
    }

    public class WhenSupplierIsNotFound : DespatchDateControllerTestBase
    {
        private DespatchDate _result;
        private readonly Exception _exception;

        public WhenSupplierIsNotFound(DespatchDateFixture fixture) : base(fixture)
        {
            _exception = Record.Exception(() =>
                _result = Fixture.Controller.Get(new List<int>() {Fixture.UnknownSupplierProductId}, Fixture.DefaultOrderDate));
        }

        [Fact]
        public void ShouldReturnNull()
        {
            _result.ShouldBeNull();
        }

        [Fact]
        public void ShouldThrowProductNotFoundException()
        {
            _exception.ShouldBeOfType<AggregateException>();
            _exception.InnerException.ShouldBeOfType<SupplierNotFoundException>();
        }
    }

    public class WhenOneProductWithLeadTimeOfOneDay : DespatchDateControllerTestBase
    {
        private readonly DespatchDate _result;

        public WhenOneProductWithLeadTimeOfOneDay(DespatchDateFixture fixture) : base(fixture)
        {
            _result = Fixture.Controller.Get(new List<int>() {1}, Fixture.DefaultOrderDate);
        }

        [Fact]
        public void ShouldReturnTomorrow()
        {
            _result.Date.Date.ShouldBe(Fixture.DefaultOrderDate.Date.AddDays(1));
        }
    }

    public class WhenOneProductWithLeadTimeOfTwoDay : DespatchDateControllerTestBase
    {
        private readonly DespatchDate _result;

        public WhenOneProductWithLeadTimeOfTwoDay(DespatchDateFixture fixture) : base(fixture)
        {
            _result = Fixture.Controller.Get(new List<int>() { 2 }, Fixture.DefaultOrderDate);
        }

        [Fact]
        public void ShouldReturnTodayPlusTwoDays()
        {
            _result.Date.Date.ShouldBe(Fixture.DefaultOrderDate.Date.AddDays(2));
        }
    }

    public class WhenOneProductWithLeadTimeOfThreeDay : DespatchDateControllerTestBase
    {
        private readonly DespatchDate _result;

        public WhenOneProductWithLeadTimeOfThreeDay(DespatchDateFixture fixture) : base(fixture)
        {
            _result = Fixture.Controller.Get(new List<int>() { 3 }, Fixture.DefaultOrderDate);
        }

        [Fact]
        public void ShouldReturnTodayPlusThreeDays()
        {
            _result.Date.Date.ShouldBe(Fixture.DefaultOrderDate.Date.AddDays(3));
        }
    }

    public class WhenOneProductAndOrderDayIsSaturday : DespatchDateControllerTestBase
    {
        private readonly DespatchDate _result;

        public WhenOneProductAndOrderDayIsSaturday(DespatchDateFixture fixture) : base(fixture)
        {
            _result = Fixture.Controller.Get(new List<int>() { 1 }, Fixture.SaturdayOrderDate);
        }

        [Fact]
        public void ShouldAddExtraTwoDays()
        {
            _result.Date.ShouldBe(Fixture.SaturdayOrderDate.Date.AddDays(3));
        }
    }

    public class WhenOneProductAndOrderDayIsSunday : DespatchDateControllerTestBase
    {
        private readonly DespatchDate _result;

        public WhenOneProductAndOrderDayIsSunday(DespatchDateFixture fixture) : base(fixture)
        {
            _result = Fixture.Controller.Get(new List<int>() { 3 }, Fixture.SundayOrderDate);
        }

        [Fact]
        public void ShouldAddExtraDay()
        {
            _result.Date.ShouldBe(Fixture.SundayOrderDate.Date.AddDays(4));
        }
    }

    public abstract class DespatchDateControllerTestBase : IClassFixture<DespatchDateFixture>
    {
        protected DespatchDateControllerTestBase(DespatchDateFixture fixture)
        {
            Fixture = fixture;
        }

        protected DespatchDateFixture Fixture { get; private set; }
    }
}
