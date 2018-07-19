using Autofac.Extras.Moq;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Trimble.Ag.IrrigationReporting.BusinessContracts;
using Trimble.Ag.IrrigationReporting.BusinessLogic;
using DC = Trimble.Ag.IrrigationReporting.DataContracts;

namespace BusinessLogicTests
{

	[TestFixture]
    public class IrrigationEventsManagerTests
    {
		[Test]
		public void GetChangePpoints_Returns_ExpectedCount()
		{
			var testBoundaries = TestEventBoundaries();
			var expected = testBoundaries.Count();

			changePointManager.Setup(it => it.GetEventBoundaries(It.IsAny<IEnumerable<IrrigationEvent>>())).Returns(() => testBoundaries);
			
			var actual = manager.GetEventBoundaries(TestEvents()).Count();

			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void GetChangePoints_DelegatesTo_ChangePointManager()
		{
			manager.GetEventBoundaries(TestEvents());

			changePointManager.Verify(it => it.GetEventBoundaries(It.IsAny<IEnumerable<IrrigationEvent>>()));
		}


		[Test]
		public void GetEventSummary_Returns_Expected_Result()
		{
			var events = TestEvents();
			
			var expected = 2;  // hard coded grouping

			var actual = manager.GetEventSummary(events).Count();
			
			Assert.AreEqual(expected, actual);
		}

		public static IEnumerable<IrrigationEvent> TestEvents() {
			return new Collection<IrrigationEvent>
			{
				new IrrigationEvent { Direction = "forward", IsPumpOn = true, DisplaySubstance = "Water" },
				new IrrigationEvent { Direction = "forward", IsPumpOn = true, DisplaySubstance = "Water" },
				new IrrigationEvent { Direction = "reverse", IsPumpOn = true, DisplaySubstance = "Effluent" },
				new IrrigationEvent { Direction = "reverse", IsPumpOn = true, DisplaySubstance = "Effluent" },
			};
		}

		public static IEnumerable<IrrigationEventBoundary> TestEventBoundaries()
		{
			return new Collection<IrrigationEventBoundary> { new IrrigationEventBoundary { DegreesOfTravel = 45, StartBearing = 10, StopBearing = 55 } };
		}

		[Test]
		public void GetEvents_Returns_Expected_Result()
        {
			var expected = TestResult.Count();
			var result = manager.GetEvents(TestRequest);
			var actual = result.Count();
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void GetCountOfEventsWithZeroBearing_Returns_Expected_Result()
		{
			var expected = 42;
			dataSource.Setup(it => it.CountOfEventsWithZeroBearing(It.IsAny<DC.IrrigationEventRequest>())).Returns(() => expected);
			var actual = manager.CountOfEventsWithZeroBearing(TestRequest);
			Assert.AreEqual(expected, actual);
		}


		[TestCase(100, 0, 0.00)]
		[TestCase(100, 1, 1.00)]
		[TestCase(100, 10, 10.00)]
		[TestCase(100,100, 100.00)]
		public void CalculateZeroBearingEventPercentage_Returns_Expected_Percentage(int totalEvents, int zeroBearingEvents, double expected)
		{
			var actual = manager.CalculateZeroBearingEventPercentage(totalEvents, zeroBearingEvents);
			Assert.AreEqual(expected, actual);
		}

		[TestCase(IrrigationEventsManager.EffluentOffSubstanceValue, IrrigationEventsManager.EffluentOffDisplayValue)]
		[TestCase(IrrigationEventsManager.EffluentSubstanceValue, IrrigationEventsManager.EffluentDisplayValue)]
		[TestCase(IrrigationEventsManager.EffluentOffSubstanceValue, IrrigationEventsManager.EffluentOffDisplayValue)]
		[TestCase(IrrigationEventsManager.EffluentPendingSubstanceValue, IrrigationEventsManager.EffluentPendingDisplayValue)]
		[TestCase(IrrigationEventsManager.EffluentSubstanceValue, IrrigationEventsManager.EffluentDisplayValue)]
		[TestCase(IrrigationEventsManager.FertigationSubstanceValue, IrrigationEventsManager.FertigationDisplayValue)]
		[TestCase(IrrigationEventsManager.FertigationOffSubstanceValue, IrrigationEventsManager.FertigationOffDisplayValue)]
		[TestCase(IrrigationEventsManager.FertigationPendingSubstanceValue, IrrigationEventsManager.FertigationPendingDisplayValue)]
		[TestCase(IrrigationEventsManager.NoneSubstanceValue, IrrigationEventsManager.NoneDisplayValue)]
		[TestCase(IrrigationEventsManager.UnknownSubstanceValue, IrrigationEventsManager.UnknownDisplayValue)]
		[TestCase(IrrigationEventsManager.WaterSubstanceValue, IrrigationEventsManager.WaterDisplayValue)]
		[TestCase(IrrigationEventsManager.WaterOffStubstanceValue, IrrigationEventsManager.WaterOffDisplayValue)]
		[TestCase(IrrigationEventsManager.WaterPendingSubstanceValue, IrrigationEventsManager.WaterPendingDisplayValue)]
		public void GetEvents_Sets_Expected_DisplaySubstance(string substance, string expected)
		{
			TestResult.First().Substance = substance;
			var actual = manager.GetEvents(TestRequest).First().DisplaySubstance;
			Assert.AreEqual(expected, actual);
		}

		[TestCase(IrrigationEventsManager.DirectionForward, 1)]
		[TestCase(IrrigationEventsManager.DirectionReverse, 2)]
		[TestCase(IrrigationEventsManager.DirectionUnknown, 3)]
		[TestCase(IrrigationEventsManager.DirectionNone, 0)]
		public void GetEvents_Sets_Expected_Direction(string expected, int rotationId)
		{
			TestResult.First().RotationId = rotationId;
			var actual = manager.GetEvents(TestRequest).First().Direction;
			Assert.AreEqual(expected, actual);
		}

		[SetUp]
		public void Setup()
		{
			var mockery = AutoMock.GetLoose();

			dataSource = mockery.Mock<DC.IIrrigationEventData>();
			changePointManager = mockery.Mock<IEventBoundaryManager>();
			manager = mockery.Create<IrrigationEventsManager>();

			TestRequest = new IrrigationEventRequest
			{
				StartAt = new TimeData { Hours = 13, Minutes = 30, Seconds = 42 },
				StartBearing = 1,
				StartDate = DateTime.UtcNow.AddDays(-10),
				StopAt = new TimeData { Hours = 18, Minutes = 24, Seconds = 42 },
				StopBearing = 360,
				StopDate = DateTime.UtcNow
			};

			TestResult = new Collection<DC.IrrigationEvent>
			{
				new DC.IrrigationEvent { },
				new DC.IrrigationEvent { },
				new DC.IrrigationEvent { }
			};

			dataSource.Setup(it => it.GetEvents(It.IsAny<DC.IrrigationEventRequest>())).Returns(() => TestResult);
		}

		private IrrigationEventsManager manager;
		private IrrigationEventRequest TestRequest;
		private IEnumerable<DC.IrrigationEvent> TestResult;
		private Mock<DC.IIrrigationEventData> dataSource;
		private Mock<IEventBoundaryManager> changePointManager;
	}
}
