using FluentAssertions;
using FoodTruckNationApi.FoodTrucks.Schedules;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FoodTruckNationApi.Test.FoodTrucks.Schedules
{
    public class FoodTruckScheduleParametersValidatorTest
    {




        [Fact]
        public void NoDatesInObjectShouldPass()
        {
            var parms = new FoodTruckScheduleParameters()
            {
                StartDate = null,
                EndDate = null
            };

            var validator = new FoodTruckScheduleParametersValidator();
            var result = validator.Validate(parms);

            result.IsValid.Should().BeTrue();
        }


        [Fact]
        public void SequencedDatesInObjectShouldPass()
        {
            var parms = new FoodTruckScheduleParameters()
            {
                StartDate = new DateTime(2017, 10, 9, 0, 0, 0),
                EndDate = new DateTime(2017, 10, 15, 23, 59, 59)
            };

            var validator = new FoodTruckScheduleParametersValidator();
            var result = validator.Validate(parms);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void HavingOnlyStartDateShouldFail()
        {
            var parms = new FoodTruckScheduleParameters()
            {
                StartDate = new DateTime(2017, 10, 9, 0, 0, 0),
                EndDate = null
            };

            var validator = new FoodTruckScheduleParametersValidator();
            var result = validator.Validate(parms);

            result.IsValid.Should().BeFalse();
        }


        [Fact]
        public void HavingOnlyEndDateShouldFail()
        {
            var parms = new FoodTruckScheduleParameters()
            {
                StartDate = null,
                EndDate = new DateTime(2017, 10, 15, 23, 59, 59)
            };

            var validator = new FoodTruckScheduleParametersValidator();
            var result = validator.Validate(parms);

            result.IsValid.Should().BeFalse();
        }


        [Fact]
        public void EndDateBeforeStartDateShouldFail()
        {
            var parms = new FoodTruckScheduleParameters()
            {
                StartDate = new DateTime(2017, 10, 15, 23, 59, 59),
                EndDate = new DateTime(2017, 10, 9, 0, 0, 0)
            };

            var validator = new FoodTruckScheduleParametersValidator();
            var result = validator.Validate(parms);

            result.IsValid.Should().BeFalse();
        }


    }
}
