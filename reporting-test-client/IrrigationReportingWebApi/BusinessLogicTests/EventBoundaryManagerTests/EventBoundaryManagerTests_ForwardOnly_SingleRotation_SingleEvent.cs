using System;
using System.Collections.Generic;
using System.Linq;
using Autofac.Extras.Moq;
using NUnit.Framework;
using Trimble.Ag.IrrigationReporting.BusinessContracts;
using Trimble.Ag.IrrigationReporting.BusinessLogic;

namespace BusinessLogicTests.EventBoundaryManagerTests
{

	[TestFixture]
	public class EventBoundaryManagerTests_ForwardOnly_SingleRotation_SingleEvent
	{
		[Test]
		public void GetEventBoundaries_Finds_Correct_StartBoundary_When_TestData_HasPrevious_Data()
		{
			var testData = SingleEventBoundaryTestData.GetForwardOnlyData().ToArray();
			var actualBoundaries = manager.GetEventBoundaries(testData).ToArray();

			var expectedJournalId = testData[1].JournalId;
			var expectedBearing = testData[1].Bearing;

			Assert.IsTrue(actualBoundaries.Count() == 1, "Precon");

			Assert.AreEqual(expectedJournalId, actualBoundaries[0].StartJournalId);
			Assert.AreEqual(expectedBearing, actualBoundaries[0].StartBearing);
		}

		[Test]
		public void GetEventBoundaryes_Finds_Correct_StartBoundary_When_TestData_HasNoPrevious_Data()
		{
			var testData = SingleEventBoundaryTestData.GetForwardOnlyData().ToList();
			testData.RemoveAt(0);
			
			var actualBoundaries = manager.GetEventBoundaries(testData).ToArray();

			var expectedJournalId = testData[0].JournalId;
			var expectedBearing = testData[0].Bearing;

			Assert.IsTrue(actualBoundaries.Count() == 1, "Precon");

			Assert.AreEqual(expectedJournalId, actualBoundaries[0].StartJournalId);
			Assert.AreEqual(expectedBearing, actualBoundaries[0].StartBearing);
		}

		[Test]
		public void GetEventBoundaries_Finds_Correct_StopBoundary()
		{
			var testData = SingleEventBoundaryTestData.GetForwardOnlyData().ToList();
			
			var actualBoundaries = manager.GetEventBoundaries(testData).ToArray();

			var expectedJournalId = testData[6].JournalId;
			var expectedBearing = testData[6].Bearing;

			Assert.IsTrue(actualBoundaries.Count() == 1, "Precon");

			Assert.AreEqual(expectedJournalId, actualBoundaries[0].StopJournalId);
			Assert.AreEqual(expectedBearing, actualBoundaries[0].StopBearing);
		}

		[Test]
		public void GetEventBoundaries_Sets_Expected_DegreesOfTravel()
		{
			
			var testData = SingleEventBoundaryTestData.GetForwardOnlyData();

			// there should be 60 degrees of travel between record 2 and record 7
			var expected = Math.Abs(GetStopEvent(testData).Bearing - GetStartEvent(testData).Bearing);

			var boundaries = manager.GetEventBoundaries(testData);

			var boundary = boundaries.First();

			var actual = boundary.DegreesOfTravel;

			Assert.IsTrue(expected == 60, "PRECON");

			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void GetEventBoundaries_Sets_Expected_ElapsedTime()
		{
			var testData = SingleEventBoundaryTestData.GetForwardOnlyData();

			var expected = GetStopEvent(testData).CreatedDate.Subtract(GetStartEvent(testData).CreatedDate).TotalMinutes;

			var boundaries = manager.GetEventBoundaries(testData);

			var boundary = boundaries.First();
			var actual = boundary.ElapsedTime.TotalMinutes;

			Assert.IsTrue(expected > 0 , "PRECON");

			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void GetEventBoundaries_Gets_Expected_Number_OfBoundaries()
		{
			var testData = SingleEventBoundaryTestData.GetForwardOnlyData();

			var expected = 1;

			var actual = manager.GetEventBoundaries(testData).Count();

			Assert.AreEqual(expected, actual);
		}

		private IrrigationEvent GetStartEvent(IEnumerable<IrrigationEvent> testData)
		{
			return testData.ElementAt(1);
		}

		private IrrigationEvent GetStopEvent(IEnumerable<IrrigationEvent> testData)
		{
			return testData.ElementAt(6);
		}

		[SetUp]
		public void Setup()
		{
			var mockery = AutoMock.GetLoose();

			manager = mockery.Create<EventBoundaryManager>();
		}

		private EventBoundaryManager manager;
	}
}