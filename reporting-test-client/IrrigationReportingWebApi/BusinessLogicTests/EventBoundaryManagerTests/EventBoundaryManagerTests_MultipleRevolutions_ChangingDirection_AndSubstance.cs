using System;
using System.Linq;
using Autofac.Extras.Moq;
using NUnit.Framework;
using Trimble.Ag.IrrigationReporting.BusinessLogic;

namespace BusinessLogicTests.EventBoundaryManagerTests
{
	/// <summary>
	/// in these tests we test the creation of multiple event boundaries that span multiple revolutions
	/// </summary>
	[TestFixture]
	public class EventBoundaryManagerTests_MultipleRevolutions_ChangingDirection_AndSubstance
	{
		[Test]
		public void GetEventBoundaries_Gets_Expected_Number_OfBoundaries()
		{
			var testData = MultiEventBoundaryTestData.GetDataWithMultipleRevolutions_ChangingDirection_AndSubstance();

			var expected = 5;

			var actual = manager.GetEventBoundaries(testData).Count();

			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void GetEventBoundaries_Sets_Expected_DegreesOfTravel_OnEach_Boundary()
		{
			var testData = MultiEventBoundaryTestData.GetDataWithMultipleRevolutions_ChangingDirection_AndSubstance();

			var boundaries = manager.GetEventBoundaries(testData).ToArray();

			Assert.AreEqual(360.0, boundaries[0].DegreesOfTravel);
			Assert.AreEqual(9.0, boundaries[1].DegreesOfTravel);
			Assert.AreEqual(360.0, boundaries[2].DegreesOfTravel);
			Assert.AreEqual(20.0, boundaries[3].DegreesOfTravel);
			Assert.AreEqual(190.0, boundaries[4].DegreesOfTravel);
		}

		[Test]
		public void GetEventBoundaries_Sets_Expected_StartBearing_OnEach_Boundary()
		{
			var testData = MultiEventBoundaryTestData.GetDataWithMultipleRevolutions_ChangingDirection_AndSubstance();

			var boundaries = manager.GetEventBoundaries(testData).ToArray();

			Assert.AreEqual(1, boundaries[0].StartBearing);
			Assert.AreEqual(1, boundaries[1].StartBearing);
			Assert.AreEqual(10, boundaries[2].StartBearing);
			Assert.AreEqual(10, boundaries[3].StartBearing);
			Assert.AreEqual(350, boundaries[4].StartBearing);
		}

		[Test]
		public void GetEventBoundaries_Sets_Expected_StopBearing_OnEach_Boundary()
		{
			var testData = MultiEventBoundaryTestData.GetDataWithMultipleRevolutions_ChangingDirection_AndSubstance();

			var boundaries = manager.GetEventBoundaries(testData).ToArray();

			Assert.AreEqual(1.0, boundaries[0].StopBearing);
			Assert.AreEqual(10, boundaries[1].StopBearing);
			Assert.AreEqual(10, boundaries[2].StopBearing);
			Assert.AreEqual(350, boundaries[3].StopBearing);
			Assert.AreEqual(180, boundaries[4].StopBearing);
		}

		[Test]
		public void GetEventBoundaries_Sets_Expected_StartJournalId_OnEach_Boundary()
		{
			var testData = MultiEventBoundaryTestData.GetDataWithMultipleRevolutions_ChangingDirection_AndSubstance();

			var boundaries = manager.GetEventBoundaries(testData).ToArray();

			Assert.AreEqual(2, boundaries[0].StartJournalId);
			Assert.AreEqual(6, boundaries[1].StartJournalId);
			Assert.AreEqual(7, boundaries[2].StartJournalId);
			Assert.AreEqual(10, boundaries[3].StartJournalId);
			Assert.AreEqual(12, boundaries[4].StartJournalId);
		}

		[Test]
		public void GetEventBoundaries_Sets_Expected_StopJournalId_OnEach_Boundary()
		{
			var testData = MultiEventBoundaryTestData.GetDataWithMultipleRevolutions_ChangingDirection_AndSubstance();

			var boundaries = manager.GetEventBoundaries(testData).ToArray();

			Assert.AreEqual(6, boundaries[0].StopJournalId);
			Assert.AreEqual(6, boundaries[1].StopJournalId);
			Assert.AreEqual(10, boundaries[2].StopJournalId);
			Assert.AreEqual(11, boundaries[3].StopJournalId);
			Assert.AreEqual(15, boundaries[4].StopJournalId);
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
