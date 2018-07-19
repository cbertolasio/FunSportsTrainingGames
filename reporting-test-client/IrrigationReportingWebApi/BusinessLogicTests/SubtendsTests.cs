using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Trimble.Ag.IrrigationReporting.BusinessContracts;

namespace BusinessLogicTests
{
	[TestFixture]
	public class SubtendsTests
	{
		[Test]
		public void Test_Equals()
		{
			var expected = 60.0m;
			var actual = new Subtends(0.0m, 60.0m);

			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void Test_EqualsOperator()
		{
			var expected = 60.0m;
			var actual = new Subtends(0.0m, 60.0m);

			Assert.IsTrue(expected == actual);
		}

		[Test]
		public void Subtends_NeverExceeds_360()
		{
			var expected = 5m;
			var actual = new Subtends(0, 365);
			Assert.AreEqual(expected, actual);

			actual = new Subtends(0, 725);
			Assert.AreEqual(expected, actual);
		}

		[TestCase(350.0, 10.0, 20.0)]
		[TestCase(270.0, 10.0, 100.0)]
		[TestCase(10.0, 9.0, 359.0)]
		public void Subtends_Handles_Bearings_That_Cross_360_Clockwise(decimal startAngle, decimal stopAngle, decimal expected)
		{
			var actual = new Subtends(startAngle, stopAngle);

			Assert.AreEqual(expected, actual);
		}

		[TestCase(10.0, 350.0, true, 20.0)]
		[TestCase(90.0, 270.0, true, 180.0)]
		[TestCase(350.0, 355.0, true, 355.0)]
		public void Subtends_Handles_Bearings_That_When_Directionality_Is_AntiClockwise(decimal startAngle, decimal stopAngle, bool antiClocwise, decimal expected)
		{
			var actual = new Subtends(startAngle, stopAngle, antiClocwise);

			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void LessThan_Operator_IsSupported()
		{
			var sample = 60m;
			var actual = new Subtends(0, 60.1m);

			Assert.IsTrue(sample < actual);
		}

		[Test]
		public void GreaterThan_Operator_IsSupported()
		{
			var sample = 60.1m;
			var actual = new Subtends(0, 60.0m);

			Assert.IsTrue(sample > actual);
		}

		[Test]
		public void Decimal_Equals()
		{
			var sample = new Subtends(0, 60);
			var actual = 60m;

			Assert.IsTrue(sample.Equals(actual));
		}

		[Test]
		public void ObjectEquals_Returns_True()
		{
			var sample = new Subtends(0, 60);
			Object actual = 60m;

			Assert.IsTrue(sample.Equals(actual));
		}

		[Test]
		public void ObjectEquals_Returns_False_When_Null()
		{
			Object actual = null;
			var sample = new Subtends(0, 60);
			Assert.IsTrue(sample.Equals(actual) == false);

			bool actual2 = true;
			Assert.IsTrue(sample.Equals(actual2) == false);
		}

		[Test]
		public void ToString_Returns_ExpectedResult()
		{
			var expected = "60.0";
			var actual = new Subtends(0, 60).ToString();

			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void HashCode_Returns_Expected_Value()
		{
			var expected = new Subtends(0, 60).GetHashCode();
			var actual = new Subtends(0, 60).GetHashCode();

			Assert.AreEqual(expected, actual);
		}
	}
}
