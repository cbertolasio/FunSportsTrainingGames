using Autofac.Extras.Moq;
using NUnit.Framework;
using Trimble.Ag.IrrigationReporting.DataAccess;

namespace DataAccessUnitTests
{
	[TestFixture]
	public class PumpStatusConverterTests
	{
		[TestCase(true, "true")]
		[TestCase(false, "false")]
		[TestCase(false, "")]
		[TestCase(false, null)]
		public void Test(bool expected, string pumpValue)
		{
			var actual = converter.GetPumpStatus(pumpValue);
			Assert.AreEqual(expected, actual);
		}

		[SetUp]
		public void Setup()
		{
			var mockery = AutoMock.GetLoose();

			converter = mockery.Create<PumpStatusConverter>();
		}

		private PumpStatusConverter converter;
	}
}
