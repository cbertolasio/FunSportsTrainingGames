using System;

namespace Trimble.Ag.IrrigationReporting.BusinessContracts
{
	public struct Subtends : IEquatable<decimal>
	{
		public const decimal DegreesOfTravel = 360.0m;

		public Subtends(Decimal startAngle, Decimal stopAngle, bool antiClockwise = false)
		{
			StartAngle = startAngle;
			StopAngle = stopAngle;
			AntiClockwise = antiClockwise;
		}

		public decimal StartAngle { get; set; }
		public decimal StopAngle { get; set; }
		public bool AntiClockwise { get; set; }

		private Decimal GetSubtends()
		{
			if (AntiClockwise)
			{
				var angle = (((StopAngle - StartAngle) % DegreesOfTravel) + DegreesOfTravel) % DegreesOfTravel;
				return DegreesOfTravel - angle;
			}
			else
			{
				return (((StopAngle - StartAngle) % DegreesOfTravel) + DegreesOfTravel) % DegreesOfTravel;
			}

		}

		public bool Equals(decimal other)
		{
			return other == GetSubtends();
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;

			if (!(obj is decimal))
			{
				return false;
			}
			else
			{
				return Equals((decimal)obj);
			}
		}

		public override string ToString()
		{
			return GetSubtends().ToString();
		}

		public string ToString(string format)
		{
			return GetSubtends().ToString(format);
		}

		public override int GetHashCode()
		{
			return GetSubtends().GetHashCode();
		}

		public static implicit operator decimal(Subtends subtends)
		{
			return subtends.GetSubtends();
		}
	}
}
