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
	public class EventBoundaryManagerTests_SubstanceChange_FromWater_ToFertigation
	{
		/// <summary>
		/// in this test we are concerned with the second irrigation event boundary
		/// </summary>
		[Test]
		public void GetEventBoundaries_Finds_Correct_StartBoundary_When_TestData_HasPrevious_Data()
		{
			var testData = MultiEventBoundaryTestData.GetDataWithSubstanceChange().ToArray();
			var actualBoundaries = manager.GetEventBoundaries(testData).ToArray();

			var expectedJournalId = testData[7].JournalId;
			var expectedBearing = testData[7].Bearing;

			Assert.AreEqual(2, actualBoundaries.Count(), "Precon:");

			Assert.AreEqual(expectedJournalId, actualBoundaries[1].StartJournalId);
			Assert.AreEqual(expectedBearing, actualBoundaries[1].StartBearing);
		}

		[Test]
		public void GetEventBoundaries_Finds_Correct_StopBoundary()
		{
			var testData = MultiEventBoundaryTestData.GetDataWithSubstanceChange().ToList();

			var actualBoundaries = manager.GetEventBoundaries(testData).ToArray();

			var expectedJournalId = testData[11].JournalId;
			var expectedBearing = testData[11].Bearing;

			Assert.IsTrue(actualBoundaries.Count() == 2, "Precon");

			Assert.AreEqual(expectedJournalId, actualBoundaries[1].StopJournalId);
			Assert.AreEqual(expectedBearing, actualBoundaries[1].StopBearing);
		}

		[Test]
		public void GetEventBoundaries_Sets_Expected_DegreesOfTravel_For_The_LastBoundary()
		{
			var testData = MultiEventBoundaryTestData.GetDataWithSubstanceChange();

			// there should be 130 degrees of travel between record 8 and record 12
			var expected = Convert.ToDouble(new Subtends(Convert.ToDecimal(GetStartEvent(testData).Bearing), Convert.ToDecimal(GetStopEvent(testData).Bearing)));

			var boundaries = manager.GetEventBoundaries(testData);

			var boundary = boundaries.Last();

			var actual = boundary.DegreesOfTravel;

			Assert.AreEqual(30.0, expected, "PRECON");

			Assert.AreEqual(expected, actual, "ACTUAL: Boundary.StartJournalId - " + boundary.StartJournalId + ", Boundary.StopJournalId - " + boundary.StopJournalId);
		}

		[Test]
		public void GetEventBoundaries_Sets_Expected_ElapsedTime()
		{
			var testData = MultiEventBoundaryTestData.GetDataWithSubstanceChange();

			var expected = GetStopEvent(testData).CreatedDate.Subtract(GetStartEvent(testData).CreatedDate).TotalMinutes;

			var boundaries = manager.GetEventBoundaries(testData);

			var boundary = boundaries.Last();
			var actual = boundary.ElapsedTime.TotalMinutes;

			Assert.IsTrue(expected > 0, "PRECON: Expected is not > 0: " + expected);

			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void GetEventBoundaries_Gets_Expected_Number_OfBoundaries()
		{
			var testData = MultiEventBoundaryTestData.GetDataWithSubstanceChange();

			var expected = 2;

			var actual = manager.GetEventBoundaries(testData).Count();

			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void GetEventBoundaries_Sets_Expected_Velocity()
		{
			var testData = MultiEventBoundaryTestData.GetDataWithSubstanceChange();

			var expected = GetStartEvent(testData).Velocity;

			var actual = manager.GetEventBoundaries(testData).Last().Velocity;

			Assert.IsTrue(expected > 0, "PRECON: expected is not > 0");

			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void GetEVentBoundaries_Sets_Substance_To_Fertigation()
		{
			var testData = MultiEventBoundaryTestData.GetDataWithSubstanceChange();

			var expected = IrrigationEventsManager.FertigationSubstanceValue;

			var actual = manager.GetEventBoundaries(testData).Last().Substance;

			Assert.AreEqual(expected, actual);
		}

		[SetUp]
		public void Setup()
		{
			var mockery = AutoMock.GetLoose();

			manager = mockery.Create<EventBoundaryManager>();
		}

		private IrrigationEvent GetStartEvent(IEnumerable<IrrigationEvent> testData)
		{
			return testData.ElementAt(7);
		}

		private IrrigationEvent GetStopEvent(IEnumerable<IrrigationEvent> testData)
		{
			return testData.ElementAt(11);
		}

		private EventBoundaryManager manager;
	}
}
