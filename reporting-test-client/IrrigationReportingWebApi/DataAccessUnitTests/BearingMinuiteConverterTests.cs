using Autofac.Extras.Moq;
using NUnit.Framework;
using Trimble.Ag.IrrigationReporting.DataAccess;

namespace DataAccessUnitTests
{
	[TestFixture]
	public class BearingMinuiteConverterTests
	{
		[TestCase(0, 0)]
		[TestCase(60, 1)]
		[TestCase(21600, 360)]
		public void ToBearingMinutes_Returns_Expected(int expected, double bearing)
		{
			var actual = converter.ToBearingMinutes(bearing);
			Assert.AreEqual(expected, actual);
		}

		[TestCase(0, null)]
		[TestCase(0, 0)]
		[TestCase(1, 60)]
		[TestCase(360, 21600)]
		public void ToDegrees_Returns_Expected(int expected, int? bearingMinutes)
		{
			var actual = converter.ToDegrees(bearingMinutes);
			Assert.AreEqual(expected, actual);
		}

		[SetUp]
		public void Setup()
		{
			var mockery = AutoMock.GetLoose();

			converter = mockery.Create<BearingMinuiteConverter>();
		}

		private BearingMinuiteConverter converter;
	}
}
