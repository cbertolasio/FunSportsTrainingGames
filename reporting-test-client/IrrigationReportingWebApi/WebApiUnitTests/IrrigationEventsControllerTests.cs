using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using Autofac.Extras.Moq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Trimble.Ag.IrrigationReporting.IrrigationReportingWebApi.Models;
using Trimble.Ag.IrrigationReporting.WebApi.Controllers;
using BC = Trimble.Ag.IrrigationReporting.BusinessContracts;

namespace WebApiUnitTests
{
	[TestFixture]
	public class IrrigationEventsControllerTests
	{
		[Test]
		public void Post_Returns_Summary()
		{
			mockEvents.Setup(it => it.GetEventSummary(It.IsAny<IEnumerable<BC.IrrigationEvent>>())).Returns(TestSummary);

			var result = (IrrigationEventsResponse)((ObjectResult)controller.Post(TestId, TestRequest)).Value;

			var actual = result.Summary;

			Assert.IsNotNull(actual);
		}

		[Test]
		public void Post_Returns_Count()
		{
			var expected = 2;

			var actual = (IrrigationEventsResponse)((ObjectResult)controller.Post(TestId, TestRequest)).Value;

			Assert.AreEqual(expected, actual.TotalEvents);
		}
		[Test]
		public void Post_Returns_ExpectedValue()
		{
			var expectedCount = 2;
			var actual = (IrrigationEventsResponse)((ObjectResult)controller.Post(TestId, TestRequest)).Value;
			Assert.AreEqual(expectedCount, actual.Events.Count());
		}

		[Test]
		public void Post_Returns_ExpectedStatus()
		{
			var actual = (ObjectResult)controller.Post(TestId, TestRequest);
			Assert.AreEqual((int)HttpStatusCode.OK, actual.StatusCode);
		}

		[Test]
		public void Post_Gets_Events_From_EventManager()
		{
			controller.Post(TestId, TestRequest);
			mockEvents.Verify(it => it.GetEvents(It.IsAny<BC.IrrigationEventRequest>()));
		}

		[Test]
		public void Post_Gets_CountOfEvents_With_ZeroBearingEvents()
		{
			var expectedCount = 42;
			mockEvents.Setup(it => it.CountOfEventsWithZeroBearing(It.IsAny<BC.IrrigationEventRequest>())).Returns(expectedCount);

			var actual = (IrrigationEventsResponse)((ObjectResult)controller.Post(TestId, TestRequest)).Value;

			Assert.AreEqual(expectedCount, actual.TotalEventsWithUnknownBearings);
		}

		[Test]
		public void Post_Returns_Boundaries()
		{
			var testBoundaries = new List<BC.IrrigationEventBoundary> { new BC.IrrigationEventBoundary { }, new BC.IrrigationEventBoundary { }, new BC.IrrigationEventBoundary { } };
			mockBoundaries.Setup(it => it.GetEventBoundaries(It.IsAny<IEnumerable<BC.IrrigationEvent>>())).Returns(testBoundaries);

			var expected = new List<IrrigationEventBoundary> { new IrrigationEventBoundary { }, new IrrigationEventBoundary { }, new IrrigationEventBoundary { } };

			var actual = (IrrigationEventsResponse)((ObjectResult)controller.Post(TestId, TestRequest)).Value;

			Assert.AreEqual(expected.Count(), actual.Boundaries.Count());
		}

		[Test]
		public void Post_Gets_Expected_ZeroBearingPercentage()
		{
			var expected = TestPercentage;
			var actual = GetActual();
			Assert.AreEqual(expected, actual.PercentageOfZeroBearings);
		}

		private IrrigationEventsResponse GetActual()
		{
			return (IrrigationEventsResponse)((ObjectResult)controller.Post(TestId, TestRequest)).Value;
		}

		[Test]
		public void Post_Get_Expected_DisplaySubstance()
		{
			var expected = "foo";
			var response = GetActual();
			var actual = response.Events.First().DisplaySubstance;
			Assert.AreEqual(expected, actual);
		}

		[SetUp]
		public void Init()
		{
			var mockery = AutoMock.GetLoose();
			mockEvents = mockery.Mock<BC.IIrrigationEventsManager>();
			mockBoundaries = mockery.Mock<BC.IEventBoundaryManager>();

			controller = mockery.Create<IrrigationEventsController>();

			TestRequest = new IrrigationEventRequest
			{
				StartAt = new TimeData { Hour = 13, Minute = 30, Second = 42 },
				StartBearing = 1,
				StartDate = DateTime.UtcNow.AddDays(-10),
				StopAt = new TimeData { Hour = 18, Minute = 24, Second = 42 },
				StopBearing = 360,
				StopDate = DateTime.UtcNow
			};

			TestSummary = new Collection<BC.IrrigationEventSummary>
			{
				new BC.IrrigationEventSummary { Count = 100, Direction = "forward", DisplaySubstance = "Water", IsPumpOn = true },
				new BC.IrrigationEventSummary { Count = 200, Direction = "reverse", DisplaySubstance = "Effluent", IsPumpOn = true }
			};

			mockEvents.Setup(it => it.GetEvents(It.IsAny<BC.IrrigationEventRequest>())).Returns(() => new List<BC.IrrigationEvent>
			{
				new BC.IrrigationEvent { JournalId = 1, Bearing = 360, Direction = "Reverse", DisplaySubstance = "foo" },
				new BC.IrrigationEvent { JournalId = 2, Bearing = 359.5m, Direction = "Reverse" }
			});

			mockEvents.Setup(it => it.CountOfEventsWithZeroBearing(It.IsAny<BC.IrrigationEventRequest>())).Returns(TestEventsWithZeroBearing);
			mockEvents.Setup(it => it.CalculateZeroBearingEventPercentage(It.IsAny<int>(), It.IsAny<int>())).Returns(TestPercentage);
		}

		private IrrigationEventsController controller;
		private IrrigationEventRequest TestRequest;
		private int TestId = 123;
		private Mock<BC.IIrrigationEventsManager> mockEvents;
		private int TestEventsWithZeroBearing = 10;
		private double TestPercentage = 1.00;
		private IEnumerable<BC.IrrigationEventSummary> TestSummary;
		private Mock<BC.IEventBoundaryManager> mockBoundaries;
	}
}
