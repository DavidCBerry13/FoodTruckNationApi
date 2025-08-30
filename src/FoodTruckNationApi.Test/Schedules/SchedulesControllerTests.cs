using AutoMapper;
using FoodTruckNation.Core.AppInterfaces;
using FoodTruckNation.Core.Domain;
using FoodTruckNationApi.Schedules;
using DavidBerry.Framework;
using DavidBerry.Framework.Functional;
using DavidBerry.Framework.TimeAndDate;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Threading.Tasks;

namespace FoodTruckNationApi.Test.Schedules
{
    public class SchedulesControllerTests
    {

        public SchedulesControllerTests()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<ScheduleModelAutomapperProfile>();
            });
            _mapper = new Mapper(config,
                t => SchedulesControllerTests.Resolve<Type, object>(t));
        }


        private readonly IMapper _mapper;


        [Fact]
        public void WhenNoDatesPassedIn_WeDefaultToTodayAndOneWeekFromToday()
        {
            // Arrange
            var now = DateTime.Now;
            var expectedStartDate = now.Date;
            var expectedEndDate = now.Date.AddDays(7);

            var mockLogger = GetMockLogger<SchedulesController>();

            var mockScheduleService = new Mock<IScheduleService>();
            mockScheduleService.Setup(r => r.GetSchedulesAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns(Task.FromResult(Result.Success<IEnumerable<Schedule>>(new List<Schedule>())));

            var dateTimeProvider = new UnitTestDateTimeProvider(now);

            SchedulesController controller = new SchedulesController(mockLogger.Object, _mapper,
                mockScheduleService.Object, dateTimeProvider);


            // Act
            var response = controller.Get(new GetSchedulesParameters());

            // Assert
            mockScheduleService.Verify(r => r.GetSchedulesAsync(expectedStartDate, expectedEndDate), Times.Once());
        }


        [Fact]
        public void WhenDatesPassedIn_PassedInDatesAreUsed()
        {
            // Arrange
            var expectedStartDate = new DateTime(2018, 4, 1);
            var expectedEndDate = new DateTime(2018, 4, 15);

            var mockLogger = GetMockLogger<SchedulesController>();
            var mockScheduleService = new Mock<IScheduleService>();
            mockScheduleService.Setup(r => r.GetSchedulesAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns(Task.FromResult(Result.Success<IEnumerable<Schedule>>(new List<Schedule>())));
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();

            SchedulesController controller = new SchedulesController(mockLogger.Object, _mapper,
                mockScheduleService.Object, mockDateTimeProvider.Object);

            // Act
            var response = controller.Get(new GetSchedulesParameters() { StartDate = expectedStartDate, EndDate = expectedEndDate });

            // Assert
            mockScheduleService.Verify(r => r.GetSchedulesAsync(expectedStartDate, expectedEndDate), Times.Once());
            mockDateTimeProvider.Verify(p => p.CurrentDateTime, Times.Never);
            mockDateTimeProvider.Verify(p => p.Today, Times.Never);

        }



        // Resolver method so AutoMapper will resolve the TestUrlResolver when it goes looking for the UrlResolver
        private static object Resolve<Type, Object>(Type t)
        {
            return new TestUrlResolver();
        }


        public Mock<ILogger<T>> GetMockLogger<T>()
        {
            var mockLogger = new Mock<ILogger<T>>();

            return mockLogger;
        }

    }
}
