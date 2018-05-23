using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Trimble.Ag.IrrigationReporting.BusinessContracts;
using DC = Trimble.Ag.IrrigationReporting.DataContracts;

namespace Trimble.Ag.IrrigationReporting.BusinessLogic
{
	public class IrrigationEventsManager : IIrrigationEventsManager
	{
		public IEnumerable<IrrigationEvent> GetEvents(IrrigationEventRequest requestData)
		{
			var request = GetRequestData(requestData);
			var events = eventData.GetEvents(request);
			var result = new Collection<IrrigationEvent>();

			var startTime = DateTime.MinValue;
			var elapsedTime = 0.0;
			
			DateTime previousDate = DateTime.MinValue;

			foreach (var irrigationEvent in events)
			{

				if (previousDate != DateTime.MinValue)
				{
					elapsedTime = Math.Round(irrigationEvent.CreatedDate.Subtract(previousDate).TotalSeconds, 2);
				}

				result.Add(GetNewEvent(irrigationEvent, elapsedTime));
				previousDate = irrigationEvent.CreatedDate;
			}

			return result;
		}

		public int CountOfEventsWithZeroBearing(IrrigationEventRequest requestData)
		{
			var request = GetRequestData(requestData);
			return eventData.CountOfEventsWithZeroBearing(request);
		}

		public double CalculateZeroBearingEventPercentage(int totalEvents, int zeroBearingEvents)
		{
			var result = ((double)zeroBearingEvents / (double)totalEvents) * 100.00;
			return result;
		}

		private DC.IrrigationEventRequest GetRequestData(IrrigationEventRequest request)
		{
			return new DC.IrrigationEventRequest
			{
				StartAt = GetStartTime(request),
				StartBearing = request.StartBearing,
				StartDate = request.StartDate,
				StopAt = GetStopTime(request),
				StopBearing = request.StopBearing,
				StopDate = request.StopDate,
				PivotId = request.PivotId
			};
		}

		private static IrrigationEvent GetNewEvent(DC.IrrigationEvent irrigationEvent, double timeBetweenEvents)
		{
			return new IrrigationEvent
			{
				Bearing = irrigationEvent.Bearing,
				CreatedDate = irrigationEvent.CreatedDate,
				Direction = GetDirection(irrigationEvent.RotationId ?? 0),
				IsPumpOn = irrigationEvent.Pump,
				JournalId = irrigationEvent.JournalId,
				PivotControllerId = irrigationEvent.PivotControllerId,
				ScheduleId = irrigationEvent.ScheduleId ?? 0,
				Substance = irrigationEvent.Substance,
				Velocity = irrigationEvent.Velocity ?? 0,
				DisplaySubstance = GetDisplaySubstance(irrigationEvent.Substance),
				TimeBetweenEvents = timeBetweenEvents
			};
		}

		private static string GetDisplaySubstance(string substance)
		{
			var result = UnknownDisplayValue;
			if (substance == null)
			{
				return result;
			}

			switch (substance.ToLowerInvariant())
			{
				case NoneSubstanceValue:
					result = NoneDisplayValue;
					break;
				case WaterPendingSubstanceValue:
					result = WaterPendingDisplayValue;
					break;
				case WaterSubstanceValue:
					result = WaterDisplayValue;
					break;
				case WaterOffStubstanceValue:
					result = WaterOffDisplayValue;
					break;
				case EffluentPendingSubstanceValue:
					result = EffluentPendingDisplayValue;
					break;
				case EffluentSubstanceValue:
					result = EffluentDisplayValue;
					break;
				case EffluentOffSubstanceValue:
					result = EffluentOffDisplayValue;
					break;
				case FertigationPendingSubstanceValue:
					result = FertigationPendingDisplayValue;
					break;
				case FertigationSubstanceValue:
					result = FertigationDisplayValue;
					break;
				case FertigationOffSubstanceValue:
					result = FertigationOffDisplayValue;
					break;
				case UnknownSubstanceValue:
				default:
					result = UnknownDisplayValue;
					break;
			}

			return result;
		}

		private static string GetDirection(int rotationId)
		{
			var direction = DirectionUnknown;

			switch (rotationId)
			{
				case 0:
					direction = DirectionNone;
					break;
				case 1:
					direction = DirectionForward;
					break;
				case 2:
					direction = DirectionReverse;
					break;
				case 3:
				default:
					return direction;
			}

			return direction;
		}

		private static DC.TimeData GetStartTime(IrrigationEventRequest request)
		{
			return new DC.TimeData
			{
				Hours = request.StartAt.Hours,
				Minutes = request.StartAt.Minutes,
				Seconds = request.StartAt.Seconds
			};
		}
		private DC.TimeData GetStopTime(IrrigationEventRequest request)
		{
			return new DC.TimeData
			{
				Hours = request.StopAt.Hours,
				Minutes = request.StopAt.Minutes,
				Seconds = request.StopAt.Seconds
			};
		}

		public IrrigationEventsManager(DC.IIrrigationEventData eventData)
		{
			this.eventData = eventData;
		}

		private readonly DC.IIrrigationEventData eventData;

		public const string UnknownDisplayValue = "Unknown Material";
		public const string NoneDisplayValue = "Set to Dry";
		public const string WaterPendingDisplayValue = "Water Pending";
		public const string WaterDisplayValue = "Using Water";
		public const string WaterOffDisplayValue = "Set to Water";
		public const string EffluentPendingDisplayValue = "Effluent Pending";
		public const string EffluentDisplayValue = "Using Effluent";
		public const string EffluentOffDisplayValue = "Set to Effluent";
		public const string FertigationPendingDisplayValue = "Fertigation Pending";
		public const string FertigationDisplayValue = "Using Fertigation";
		public const string FertigationOffDisplayValue = "Set to Fertigation";

		public const string NoneSubstanceValue = "none";
		public const string WaterPendingSubstanceValue = "pending_water";
		public const string WaterSubstanceValue = "water";
		public const string WaterOffStubstanceValue = "water_off";
		public const string EffluentPendingSubstanceValue = "pending_effluent";
		public const string EffluentSubstanceValue = "effluent";
		public const string EffluentOffSubstanceValue = "effluent_off";
		public const string FertigationPendingSubstanceValue = "pending_fertigation";
		public const string FertigationSubstanceValue = "fertigation";
		public const string FertigationOffSubstanceValue = "fertigation_off";
		public const string UnknownSubstanceValue = "unknown";

		public const string DirectionUnknown = "Unknown";
		public const string DirectionNone = "None";
		public const string DirectionForward = "Forward";
		public const string DirectionReverse = "Reverse";
	}
}
