using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Trimble.Ag.IrrigationReporting.DataContracts;
using DC = Trimble.Ag.IrrigationReporting.DataContracts;
using DM = Trimble.Ag.IrrigationReporting.DataModels.Models;

namespace Trimble.Ag.IrrigationReporting.DataAccess
{

	//TODO: turn this into a proper repository
	public class IrrigationEventsData : DC.IIrrigationEventData
	{
		public IEnumerable<DC.IrrigationEvent> GetEvents(DC.IrrigationEventRequest requestData)
		{
			var pivotId = requestData.PivotId;
			var startBearing = requestData.StartBearing;
			var stopBearing = requestData.StopBearing;
			var stopDate = requestData.StopDate;
			var startDate = requestData.StartDate;
			var stopTime = requestData.StopAt;
			var startTime = requestData.StartAt;

			var startTimespan = GetTimespan(startTime);
			startDate.Add(startTimespan);

			var stopTimespan = GetTimespan(stopTime);
			stopDate.Add(stopTimespan);

			var startBearingMinutes = startBearing * 60;
			var stopBearingMinutes = stopBearing * 60;

			var builder = new StringBuilder();
			builder.Append("select ie.* from irrigation.irrigation_events as ie ");
			builder.Append($"where ie.createdon >= '{startDate}' and ie.createdon <= '{stopDate}' ");
			builder.Append($"and ie.bearing >= {startBearingMinutes} and ie.bearing <= {stopBearingMinutes} ");
			builder.Append("and ie.pivotcontrollerid in (select distinct h.controllerid ");
			builder.Append("from irrigation.control_and_bearing_controller_history as h ");
			builder.Append($"where h.pivotid = {pivotId});");

			var cmd = repositoryBase.Context.CreateCommand();
			cmd.CommandText = builder.ToString();
			var databaseResult = repositoryBase.GetEnumerable(cmd);

			var events = new Collection<DC.IrrigationEvent>();

			foreach (var item in databaseResult)
			{
				events.Add(new IrrigationEvent
				{
					Bearing = GetBearingInDegrees(item.Bearing),
					CreatedDate = item.CreatedOn,
					JournalId = item.JournalId,
					PivotControllerId = item.PivotControllerId,
					Pump = GetPumpStatus(item.Pump),
					RotationId = item.RotationId,
					ScheduleId = item.ScheduleId,
					Substance = item.Substance,
					Velocity = item.Velocity,
				});
			}

			return events;
		}

		private static TimeSpan GetTimespan(TimeData startTime)
		{
			return new TimeSpan(startTime.Hours, startTime.Minutes, startTime.Seconds);
		}

		public int CountOfEventsWithZeroBearing(DC.IrrigationEventRequest requestData)
		{
			var pivotId = requestData.PivotId;
			var startBearing = requestData.StartBearing;
			var stopBearing = requestData.StopBearing;
			var stopDate = requestData.StopDate;
			var startDate = requestData.StartDate;
			var stopTime = requestData.StopAt;
			var startTime = requestData.StartAt;

			var startTimespan = GetTimespan(startTime);
			startDate.Add(startTimespan);

			var stopTimespan = GetTimespan(stopTime);
			stopDate.Add(stopTimespan);

			var startBearingMinutes = startBearing * 60;
			var stopBearingMinutes = stopBearing * 60;

			var builder = new StringBuilder();
			builder.Append("select count(journalid) from irrigation.irrigation_events as ie ");
			builder.Append($"where ie.createdon >= '{startDate}' and ie.createdon <= '{stopDate}' ");
			builder.Append($"and (ie.bearing is null or ie.bearing = 0) ");
			builder.Append("and ie.pivotcontrollerid in (select distinct h.controllerid ");
			builder.Append("from irrigation.control_and_bearing_controller_history as h ");
			builder.Append($"where h.pivotid = {pivotId});");

			var cmd = repositoryBase.Context.CreateCommand();
			cmd.CommandText = builder.ToString();

			return repositoryBase.ExecuteScalar(cmd);
		}

		private double GetBearingInDegrees(int? bearing)
		{
			return bearingMinuiteConverter.ToDegrees(bearing);
		}

		private bool GetPumpStatus(string pumpValue)
		{
			return pumpStatusConverter.GetPumpStatus(pumpValue);
		}

		public IrrigationEventsData(IRepositoryBase<DM.IrrigationEvent> repositoryBase, IPumpStatusConverter pumpStatusConverter, IBearingMinuiteConverter bearingMinuiteConverter)
		{
			this.repositoryBase = repositoryBase;
			this.pumpStatusConverter = pumpStatusConverter;
			this.bearingMinuiteConverter = bearingMinuiteConverter;
		}

		private readonly IRepositoryBase<DM.IrrigationEvent> repositoryBase;
		private readonly IPumpStatusConverter pumpStatusConverter;
		private readonly IBearingMinuiteConverter bearingMinuiteConverter;
	}
}
