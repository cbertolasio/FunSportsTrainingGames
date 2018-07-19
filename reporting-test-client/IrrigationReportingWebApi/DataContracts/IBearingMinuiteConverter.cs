namespace Trimble.Ag.IrrigationReporting.DataContracts
{
	public interface IBearingMinuiteConverter
	{
		decimal ToDegrees(int? bearingMinutes);
		int ToBearingMinutes(double bearing);
	}
}
