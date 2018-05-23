namespace Trimble.Ag.IrrigationReporting.DataContracts
{
	public interface IPumpStatusConverter
	{
		bool GetPumpStatus(string pumpValue);
	}
}
