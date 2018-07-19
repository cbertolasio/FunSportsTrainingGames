using System;
using System.Linq;
using Autofac.Extras.Moq;
using NUnit.Framework;
using Trimble.Ag.IrrigationReporting.BusinessLogic;

namespace BusinessLogicTests.EventBoundaryManagerTests
{
	[TestFixture]
	public class EventBoundaryManagerTests_Skip_EventsWith_BearingDrift
	{
		[Test]
		public void GetEventBoundaries_Returns_Expected_DegreesOfTravel()
		{
			var testData = MultiEventBoundaryTestData.GetDataWithMultipleRevolutions_And_BearingDrift();
			var actualBoundaries = manager.GetEventBoundaries(testData).ToList();
			var expected = 360.0m;
			var actual = actualBoundaries[0].DegreesOfTravel;

			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void GetEventBoundaries_Returns_Expected_DegreesOfTravel_When_StopBearing_HasBearingDrift()
		{
			var testData = MultiEventBoundaryTestData.GetDataWithMultipleRevolutions_And_BearingDrift();
			var actualBoundaries = manager.GetEventBoundaries(testData).ToList();
			var expected = 89.0m;
			var actual = actualBoundaries[2].DegreesOfTravel;

			Assert.AreEqual(3, actualBoundaries.Count(), "PRECON");

			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void GetEventBoundaries_Returns_Expected_Boundaries_When_BearingDriftExists_AndThereIsA_LargeGapInData()
		{
			var testData = MultiEventBoundaryTestData.GetData_Modeling_Reverse_ThenA_BearingDriftSequence_FollowedByA_VeryLongDelay();
			var expected = 3;
			var actualBoundaries = manager.GetEventBoundaries(testData).ToList();
			var actual = actualBoundaries.Count();
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void GetEventBoundarues_Returns_Expected_Values_ForThe_LastBoundary_InANormalEvent()
		{
			var testData = MultiEventBoundaryTestData.GetData_Modeling_Reverse_ThenA_BearingDriftSequence_FollowedByA_VeryLongDelay();
			var expectedStartBearing = 160.533333m;
			var expectedStartJournalId = 13;
			var expectedStopBearing = 160.383333m;
			var expectedStopJournalId = 16;

			var actualBoundaries = manager.GetEventBoundaries(testData).ToList();
			var actual = actualBoundaries[2];

			Assert.AreEqual(expectedStartJournalId, actual.StartJournalId);
			Assert.AreEqual(expectedStartBearing, actual.StartBearing);

			Assert.AreEqual(expectedStopJournalId, actual.StopJournalId);
			Assert.AreEqual(expectedStopBearing, actual.StopBearing);
		}

		[Test]
		public void GetEventBoundaries_Returns_Expected_Values_ForThe_LastBoundary_InAStopEvent()
		{
			/// item #13 has a long dealay, bearing drift, and it is also a stop event b/c of the speed change
			var testData = MultiEventBoundaryTestData.GetData_Modeling_Reverse_ThenA_BearingDriftSequence_FollowedByA_VeryLongDelay();
			var expectedStartBearing = 160.533333m;
			var expectedStartJournalId = 13;
			var expectedStopBearing = 160.383333m;
			var expectedStopJournalId = 16;

			var actualBoundaries = manager.GetEventBoundaries(testData).ToList();
			var actual = actualBoundaries[2];

			Assert.AreEqual(expectedStartJournalId, actual.StartJournalId);
			Assert.AreEqual(expectedStartBearing, actual.StartBearing);

			Assert.AreEqual(expectedStopJournalId, actual.StopJournalId);
			Assert.AreEqual(expectedStopBearing, actual.StopBearing);
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
