using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Trimble.Ag.IrrigationReporting.BusinessContracts;

namespace Trimble.Ag.IrrigationReporting.BusinessLogic
{
	public class EventBoundaryManager : IEventBoundaryManager
	{
		public IEnumerable<IrrigationEventBoundary> GetEventBoundaries(IEnumerable<IrrigationEvent> events)
		{

			var listOfBoundaries = new List<IrrigationEventBoundary>();
			var currentBoundary = (IrrigationEventBoundary)null;

			var sorted = events.OrderBy(it => it.CreatedDate);
			var listOfEvents = new LinkedList<IrrigationEvent>(sorted);

			for (var currentNode = listOfEvents.First; currentNode != null;)
			{
				var next = currentNode.Next;
				var previous = currentNode.Previous;


				if (EventIsAStartingBoundary(currentNode, currentBoundary))
				{
					currentBoundary = GetNewBoundary(listOfBoundaries, currentBoundary, currentNode, next);

					currentNode = next;
					continue;
				}
				else if (EventIsAStopBoundary(currentNode, currentBoundary))
				{
					var currentEvent = currentNode.Value;
					if (HasBearingDrift(currentNode, currentBoundary))
					{
						currentEvent.Bearing = currentBoundary.LastKnownBearing;
					}

					currentBoundary.StopBearing = currentEvent.Bearing;
					currentBoundary.StopJournalId = currentEvent.JournalId;

					UpdateRunningTotals(currentBoundary, currentNode);
					currentBoundary = SplitBoundaryForAStopEvent(listOfBoundaries, currentBoundary, currentNode, next, currentEvent);

					currentNode = next;
					continue;
				}
				else
				{
					if (! HasBearingDrift(currentNode, currentBoundary) && ! ElapsedTimeIsOutsideOfNormalRangeForCurrentBoundary(currentBoundary, currentNode))
					{
						UpdateRunningTotals(currentBoundary, currentNode);
						currentBoundary = SplitBoundaryForANormalEvent(listOfBoundaries, currentBoundary, currentNode, next);
					}
					else if (ElapsedTimeIsOutsideOfNormalRangeForCurrentBoundary(currentBoundary, currentNode))
					{
						currentBoundary = FinalizeCurrentBoundary(listOfBoundaries, currentBoundary, currentNode, next);
					}

					currentNode = next;
					continue;
				}
			}

			listOfBoundaries.RemoveAll(it => it.DegreesOfTravel == 0);

			return listOfBoundaries;
		}

		/// <summary>
		/// if the current boundary has been initialized and the current node is &gt; 60 minutes from the previous node then the event is outside of the normal range...
		/// </summary>
		/// <returns></returns>
		/// <remarks>use this value to determine if you should close the current boundary and start a new one...</remarks>
		private bool ElapsedTimeIsOutsideOfNormalRangeForCurrentBoundary(IrrigationEventBoundary currentBoundary, LinkedListNode<IrrigationEvent> currentNode)
		{
			return (currentBoundary != null && currentBoundary.StartJournalId > 0 &&  currentNode.Value != null && GetElapsedTimeBetweenEvents(currentNode).TotalMinutes > 60);
		}

		/// <summary>
		/// determines if we think the recorded bearing has drifted in the wrong direction from the last known bearing
		/// </summary>
		/// <remarks>
		/// we know what direction the pivot should be going.  Assuming the pivot is going forward, the bearings 
		/// should go from 1 =&gt; 360.  If the last known bearing is 90 and the current bearing is 89, the subtends
		/// between those bearings is 359, which indicates that the current bearing has drifted behind the last known bearing.
		/// The same is true when the pivot is moving in reverse.  This issue exists b/c there is an error of 5 meters in 
		/// gps readings and some pivots report status messages .
		/// </remarks>
		private bool HasBearingDrift(LinkedListNode<IrrigationEvent> currentNode, IrrigationEventBoundary currentBoundary)
		{
			var currentBearing = currentNode.Value.Bearing;

			var hasDrift = false;
			if (currentBoundary != null && currentBoundary.Direction == currentNode.Value.Direction)
			{
				var subtends = new Subtends(currentBoundary.LastKnownBearing, currentBearing, IsAntiClockwise(currentBoundary.Direction));
				hasDrift = subtends > 300;
			}

			return hasDrift;
		}

		private IrrigationEventBoundary SplitBoundaryForANormalEvent(IList<IrrigationEventBoundary> listOfBoundaries, IrrigationEventBoundary currentBoundary, LinkedListNode<IrrigationEvent> currentNode, LinkedListNode<IrrigationEvent> next)
		{
			if (currentBoundary != null)
			{
				var currentEvent = currentNode.Value;
				var runningTotal = currentBoundary.DegreesOfTravel;
				if (runningTotal > 360)
				{
					currentBoundary.StopBearing = currentBoundary.StartBearing;
					currentBoundary.StopJournalId = currentEvent.JournalId;
					currentBoundary.DegreesOfTravel = 360;

					currentEvent.Bearing = currentBoundary.StartBearing;
					currentBoundary = null;
					currentBoundary = GetNewBoundary(listOfBoundaries, currentBoundary, currentNode, next);
				}
			}

			return currentBoundary;
		}

		private IrrigationEventBoundary FinalizeCurrentBoundary(IList<IrrigationEventBoundary> listOfBoundaries, IrrigationEventBoundary currentBoundary, LinkedListNode<IrrigationEvent> currentNode, LinkedListNode<IrrigationEvent> next)
		{
			if (currentBoundary != null)
			{
				currentBoundary.StopBearing = currentBoundary.LastKnownBearing;
				currentBoundary.StopJournalId = currentBoundary.LastKnownJournalId;

				ValidateDegreesOfTravel(listOfBoundaries, currentBoundary);

				currentBoundary = null;
				currentBoundary = GetNewBoundary(listOfBoundaries, currentBoundary, currentNode, next);
			}

			return currentBoundary;
		}

		private IrrigationEventBoundary SplitBoundaryForAStopEvent(IList<IrrigationEventBoundary> listOfBoundaries, IrrigationEventBoundary currentBoundary, LinkedListNode<IrrigationEvent> currentNode, LinkedListNode<IrrigationEvent> next, IrrigationEvent currentEvent)
		{
			var runningTotal = currentBoundary.DegreesOfTravel;
			if (runningTotal > 360)
			{
				// close out the current boundary
				currentBoundary.StopBearing = currentBoundary.StartBearing;
				currentBoundary.StopJournalId = currentEvent.JournalId;
				currentBoundary.DegreesOfTravel = 360;

				// create a boundary for the remainder
				var currentStopBearing = currentEvent.Bearing;
				currentEvent.Bearing = currentBoundary.StartBearing;
				currentBoundary = null;
				currentBoundary = CreateNewEventBoundary(currentEvent, listOfBoundaries);
				currentBoundary.StopBearing = currentStopBearing;
				currentBoundary.LastKnownBearing = currentStopBearing;
				currentBoundary.LastKnownJournalId = currentEvent.JournalId;
				currentBoundary.StopJournalId = currentEvent.JournalId;


				var node = currentNode;
				var lastKnownDirection = currentNode.Value.Direction;
				while (lastKnownDirection.Equals(IrrigationEventsManager.DirectionNone, StringComparison.InvariantCultureIgnoreCase) ||
					lastKnownDirection.Equals(IrrigationEventsManager.DirectionUnknown, StringComparison.InvariantCultureIgnoreCase))
				{
					node = currentNode.Previous;
					lastKnownDirection = node.Value.Direction;
				}

				currentBoundary.DegreesOfTravel = new Subtends(currentBoundary.StartBearing, currentBoundary.StopBearing, IsAntiClockwise(lastKnownDirection));

				ValidateDegreesOfTravel(listOfBoundaries, currentBoundary);

				currentBoundary = null;
			}
			else
			{
				ValidateDegreesOfTravel(listOfBoundaries, currentBoundary);

				currentBoundary = null;
				currentBoundary = GetNewBoundary(listOfBoundaries, currentBoundary, currentNode, next);
			}

			return currentBoundary;
		}

		private static void ValidateDegreesOfTravel(IList<IrrigationEventBoundary> listOfBoundaries, IrrigationEventBoundary currentBoundary)
		{
			if (currentBoundary.StartBearing > 0 && currentBoundary.StopBearing > 0 && currentBoundary.DegreesOfTravel == 0)
			{
				listOfBoundaries.Remove(currentBoundary);
			}
		}

		private IrrigationEventBoundary GetNewBoundary(IList<IrrigationEventBoundary> listOfBoundaries, IrrigationEventBoundary currentBoundary, LinkedListNode<IrrigationEvent> currentNode, LinkedListNode<IrrigationEvent> next)
		{
			if (next != null && EventIsAStartingBoundary(currentNode, currentBoundary))
			{
				var startEvent = currentNode.Value;
				currentBoundary = CreateNewEventBoundary(startEvent, listOfBoundaries);
			}

			return currentBoundary;
		}

		private void UpdateRunningTotals(IrrigationEventBoundary currentBoundary, LinkedListNode<IrrigationEvent> currentNode)
		{
			if (currentBoundary != null)
			{
				UpdateDegreesOfTravel(currentNode, currentBoundary);
				UpdateElapsedTime(currentNode, currentBoundary);
			}
		}

		private static IrrigationEventBoundary CreateNewEventBoundary(IrrigationEvent startEvent, IList<IrrigationEventBoundary> listOfBoundaries)
		{
			var boundary = new IrrigationEventBoundary
			{
				StartBearing = startEvent.Bearing,
				PivotControllerId = startEvent.PivotControllerId,
				ScheduleId = startEvent.ScheduleId,
				DegreesOfTravel = 0,
				ElapsedTime = new TimeSpan(0),
				StartJournalId = startEvent.JournalId,
				Substance = startEvent.Substance,
				Velocity = startEvent.Velocity,
				Direction = startEvent.Direction,
				LastKnownBearing = startEvent.Bearing,
				LastKnownJournalId = startEvent.JournalId
			};

			listOfBoundaries.Add(boundary);

			return boundary;
		}

		private void UpdateDegreesOfTravel(LinkedListNode<IrrigationEvent> currentNode, IrrigationEventBoundary currentBoundary)
		{
			var previousNode = currentNode.Previous;
			var previousBearing = currentBoundary.LastKnownBearing;
			var currentBearing = currentNode.Value.Bearing;
			var currentValue = currentBoundary.DegreesOfTravel;

			decimal subtends = new Subtends(previousBearing, currentBearing, IsAntiClockwise(previousNode));
			
			if (subtends == 360.0m && currentNode.Value.CreatedDate.Subtract(currentNode.Previous.Value.CreatedDate).TotalMinutes < 60)
			{
				subtends = 0.0m;
			}

			currentBoundary.LastKnownBearing = currentNode.Value.Bearing;
			currentBoundary.LastKnownJournalId = currentNode.Value.JournalId;
			currentBoundary.DegreesOfTravel = currentValue + subtends;
		}

		private void UpdateElapsedTime(LinkedListNode<IrrigationEvent> currentNode, IrrigationEventBoundary currentBoundary)
		{
			var currentElapsedTime = currentBoundary.ElapsedTime;

			var elapsedTime = GetElapsedTimeBetweenEvents(currentNode);

			currentBoundary.ElapsedTime = currentElapsedTime.Add(elapsedTime);
		}

		private TimeSpan GetElapsedTimeBetweenEvents(LinkedListNode<IrrigationEvent> currentNode)
		{
			var previousDate = currentNode.Previous.Value.CreatedDate;
			var currentDate = currentNode.Value.CreatedDate;

			return currentDate.Subtract(previousDate);
		}

		private bool IsAntiClockwise(LinkedListNode<IrrigationEvent> currentNode)
		{
			return IsAntiClockwise(currentNode.Value.Direction);
		}

		private bool IsAntiClockwise(string direction)
		{
			return direction.Equals(IrrigationEventsManager.DirectionReverse, StringComparison.InvariantCultureIgnoreCase);
		}

		private bool EventIsAStartingBoundary(LinkedListNode<IrrigationEvent> node, IrrigationEventBoundary currentBoundary)
		{
			var current = node.Value;
			var isPumpOn = current.IsPumpOn;
			
			return (currentBoundary == null && isPumpOn && IsAKnownSubstance(current.Substance) && HasAKnownDirection(current.Direction));
		}

		private bool EventIsAStopBoundary(LinkedListNode<IrrigationEvent> node, IrrigationEventBoundary currentEventBoundary)
		{
			var eventIsAStopBoundary = false;
			if (currentEventBoundary == null)
			{
				return false;
			}

			var previous = node.Previous.Value;
			var current = node.Value;
			var next = node.Next;

			if (next == null
				|| VelocityChanged(previous, current) 
				|| PumpStatusChangedToFalse(previous, current) 
				|| SubstanceChanged(previous, current) 
				|| DirectionChanged(previous, current))
			{
				eventIsAStopBoundary = true;
			}

			return eventIsAStopBoundary;
		}
		private bool HasAKnownDirection(string direction)
		{
			return (direction.Equals(IrrigationEventsManager.DirectionForward, StringComparison.InvariantCultureIgnoreCase) ||
				direction.Equals(IrrigationEventsManager.DirectionReverse, StringComparison.InvariantCultureIgnoreCase));
		}

		private bool DirectionChanged(IrrigationEvent previous, IrrigationEvent current)
		{
			return (!current.Direction.Equals(previous.Direction, StringComparison.InvariantCultureIgnoreCase));
		}

		private bool VelocityChanged(IrrigationEvent previous, IrrigationEvent current)
		{
			return (current.Velocity != previous.Velocity);
		}

		private bool SubstanceChanged(IrrigationEvent previous, IrrigationEvent current)
		{
			return (! current.Substance.Equals(previous.Substance, StringComparison.InvariantCultureIgnoreCase));
		}

		private static bool PumpStatusChangedToFalse(IrrigationEvent previous, IrrigationEvent current)
		{
			return (current.IsPumpOn == false && previous.IsPumpOn == true);
		}

		private bool IsAKnownSubstance(string substance)
		{
			return substance.Equals(IrrigationEventsManager.EffluentSubstanceValue, StringComparison.InvariantCultureIgnoreCase) ||
				substance.Equals(IrrigationEventsManager.WaterSubstanceValue, StringComparison.InvariantCultureIgnoreCase) ||
				substance.Equals(IrrigationEventsManager.FertigationSubstanceValue, StringComparison.InvariantCultureIgnoreCase);
		}

		public EventBoundaryManager()
		{
		}
	}
}
