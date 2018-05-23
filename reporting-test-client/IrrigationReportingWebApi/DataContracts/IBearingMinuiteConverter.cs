namespace Trimble.Ag.IrrigationReporting.DataContracts
{
	public interface IBearingMinuiteConverter
	{
		double ToDegrees(int? bearingMinutes);
		int ToBearingMinutes(double bearing);
	}
}
