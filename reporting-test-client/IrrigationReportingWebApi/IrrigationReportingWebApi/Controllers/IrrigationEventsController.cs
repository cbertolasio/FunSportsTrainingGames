using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using BC = Trimble.Ag.IrrigationReporting.BusinessContracts;
using Trimble.Ag.IrrigationReporting.IrrigationReportingWebApi.Models;
using System.Collections.ObjectModel;
using Microsoft.Extensions.Configuration;

namespace Trimble.Ag.IrrigationReporting.WebApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class IrrigationEventsController : Controller
	{
		[HttpPost("{id}")]
		[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IrrigationEventsResponse))]
		public IActionResult Post(int id, [FromBody] IrrigationEventRequest request)
		{
			BC.IrrigationEventRequest requestData = GetRequestData(request, id);

			var events = eventManager.GetEvents(requestData);

			var eventsOut = new Collection<IrrigationEvent>();
			foreach (var irrigationEvent in events)
			{
				eventsOut.Add(GetNewEvent(irrigationEvent));
			}

			var totalEvents = eventsOut.Count;
			var totalZeroBearings = eventManager.CountOfEventsWithZeroBearing(requestData);
			var percentageOfZeroBearings = eventManager.CalculateZeroBearingEventPercentage(totalEvents, totalZeroBearings);

			var response = new IrrigationEventsResponse
			{
				Events = eventsOut,
				TotalEvents = totalEvents,
				TotalEventsWithUnknownBearings = totalZeroBearings,
				PercentageOfZeroBearings = percentageOfZeroBearings
			};

			var eventSummary = eventManager.GetEventSummary(events);
			var responseSummary = new Collection<IrrigationEventSummary>();
			foreach (var item in eventSummary)
			{
				responseSummary.Add(GetResponseSummary(item));
			}
			response.Summary = responseSummary;

			var ouput = StatusCode((int)HttpStatusCode.OK, response);
			return ouput;
		}

		private IrrigationEventSummary GetResponseSummary(BC.IrrigationEventSummary item)
		{
			return new IrrigationEventSummary
			{
				Count = item.Count,
				Direction = item.Direction,
				DisplaySubstance = item.DisplaySubstance,
				IsPumpOn = item.IsPumpOn
			};
		}

		private BC.IrrigationEventRequest GetRequestData(IrrigationEventRequest request, int pivotId)
		{
			return new BC.IrrigationEventRequest
			{
				StartAt = GetStartTime(request),
				StartBearing = request.StartBearing,
				StartDate = request.StartDate,
				StopAt = GetStopTime(request),
				StopBearing = request.StopBearing,
				StopDate = request.StopDate,
				PivotId = pivotId
			};
		}

		private static IrrigationEvent GetNewEvent(BC.IrrigationEvent irrigationEvent)
		{
			return new IrrigationEvent {
				Bearing = irrigationEvent.Bearing,
				CreatedDate = irrigationEvent.CreatedDate,
				Direction = irrigationEvent.Direction,
				IsPumpOn = irrigationEvent.IsPumpOn,
				JournalId = irrigationEvent.JournalId,
				PivotControllerId = irrigationEvent.PivotControllerId,
				ScheduleId = irrigationEvent.ScheduleId,
				Substance = irrigationEvent.Substance,
				DisplaySubstance = irrigationEvent.DisplaySubstance,
				Velocity = irrigationEvent.Velocity,
				TimeBetweenEvents = irrigationEvent.TimeBetweenEvents
			};
		}

		private static BC.TimeData GetStartTime(IrrigationEventRequest request)
		{
			return new BC.TimeData
			{
				Hours = request.StartAt.Hour,
				Minutes = request.StartAt.Minute,
				Seconds = request.StartAt.Second
			};
		}
		private BC.TimeData GetStopTime(IrrigationEventRequest request)
		{
			return new BC.TimeData
			{
				Hours = request.StopAt.Hour,
				Minutes = request.StopAt.Minute,
				Seconds = request.StopAt.Second
			};
		}

		public IrrigationEventsController(BC.IIrrigationEventsManager eventManager, IConfiguration config)
		{
			this.eventManager = eventManager;
		}

		private readonly BC.IIrrigationEventsManager eventManager;
	}
}