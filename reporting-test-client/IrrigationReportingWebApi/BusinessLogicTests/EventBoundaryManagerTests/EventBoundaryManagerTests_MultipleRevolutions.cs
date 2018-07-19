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
	public class EventBoundaryManagerTests_MultipleRevolutions_WithContinuous_EventData
	{
		[Test]
		public void GetEventBoundaries_Gets_Expected_Number_OfBoundaries()
		{
			var testData = MultiEventBoundaryTestData.GetDataWithMultipleRevolutions();

			var expected = 3;

			var actual = manager.GetEventBoundaries(testData).ToList();

			Assert.AreEqual(expected, actual.Count());
		}

		[Test]
		public void GetEventBoundaries_Sets_Expected_DegreesOfTravel_OnEach_Boundary()
		{
			var testData = MultiEventBoundaryTestData.GetDataWithMultipleRevolutions();

			var boundaries = manager.GetEventBoundaries(testData).ToArray();

			Assert.AreEqual(360.0m, boundaries[0].DegreesOfTravel);
			Assert.AreEqual(360.0m, boundaries[1].DegreesOfTravel);
			Assert.AreEqual(109.0m, boundaries[2].DegreesOfTravel);
		}

		[Test]
		public void GetEventBoundaries_Sets_Expected_StartBearing_OnEach_Boundary()
		{
			var testData = MultiEventBoundaryTestData.GetDataWithMultipleRevolutions();

			var boundaries = manager.GetEventBoundaries(testData).ToArray();

			Assert.AreEqual(1.0, boundaries[0].StartBearing);
			Assert.AreEqual(1.0, boundaries[1].StartBearing);
			Assert.AreEqual(1.0, boundaries[2].StartBearing);
		}

		[Test]
		public void GetEventBoundaries_Sets_Expected_StopBearing_OnEach_Boundary()
		{
			var testData = MultiEventBoundaryTestData.GetDataWithMultipleRevolutions();

			var boundaries = manager.GetEventBoundaries(testData).ToArray();

			Assert.AreEqual(1.0, boundaries[0].StopBearing);
			Assert.AreEqual(1.0, boundaries[1].StopBearing);
			Assert.AreEqual(110.0, boundaries[2].StopBearing);
		}

		[Test]
		public void GetEventBoundaries_Sets_Expected_StartJournalId_OnEach_Boundary()
		{
			var testData = MultiEventBoundaryTestData.GetDataWithMultipleRevolutions();

			var boundaries = manager.GetEventBoundaries(testData).ToArray();

			Assert.AreEqual(2, boundaries[0].StartJournalId);
			Assert.AreEqual(6, boundaries[1].StartJournalId);
			Assert.AreEqual(10, boundaries[2].StartJournalId);
		}

		[Test]
		public void GetEventBoundaries_Sets_Expected_StopJournalId_OnEach_Boundary()
		{
			var testData = MultiEventBoundaryTestData.GetDataWithMultipleRevolutions();

			var boundaries = manager.GetEventBoundaries(testData).ToArray();

			Assert.AreEqual(6, boundaries[0].StopJournalId);
			Assert.AreEqual(10, boundaries[1].StopJournalId);
			Assert.AreEqual(12, boundaries[2].StopJournalId);
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
