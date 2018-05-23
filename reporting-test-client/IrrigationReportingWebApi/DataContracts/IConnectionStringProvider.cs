namespace Trimble.Ag.IrrigationReporting.DataContracts
{
	public interface IConnectionStringProvider
	{
		string GetConnectionString();
		string ConnectionName { get; }
	}
}
