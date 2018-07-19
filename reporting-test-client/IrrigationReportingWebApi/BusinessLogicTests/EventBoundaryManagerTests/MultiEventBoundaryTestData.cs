using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Trimble.Ag.IrrigationReporting.BusinessContracts;
using Trimble.Ag.IrrigationReporting.BusinessLogic;

namespace BusinessLogicTests.EventBoundaryManagerTests
{
	public static class MultiEventBoundaryTestData
	{
		public static IEnumerable<IrrigationEvent> GetForwardToReverseData()
		{
			return new Collection<IrrigationEvent>
			{
				// arbitrary event...
				new IrrigationEvent{ JournalId = 1, Substance = IrrigationEventsManager.NoneSubstanceValue, Direction = IrrigationEventsManager.DirectionNone, Bearing = 0.0m, IsPumpOn = false, Velocity = 0, CreatedDate = new DateTime(2018, 6, 1, 12, 0, 0) },

				// this is the start boundary
				new IrrigationEvent{ JournalId = 2, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 10.0m, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 10, 0) },

				// arbitrary events
				new IrrigationEvent{ JournalId = 3, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 20, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 20, 0) },
				new IrrigationEvent{ JournalId = 4, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 30, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 30, 0) },
				new IrrigationEvent{ JournalId = 5, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 40, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 40, 0) },
				new IrrigationEvent{ JournalId = 6, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 60, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 50, 0) },

				// inserting a bogus bearing for testing purposes - pretending the pivot stopped before it went in reverse...
				new IrrigationEvent{ JournalId = 7, Substance = IrrigationEventsManager.NoneSubstanceValue, Direction = IrrigationEventsManager.DirectionNone, Bearing = 70, IsPumpOn = false, Velocity = 0, CreatedDate = new DateTime(2018, 6, 1, 12, 55, 0) },

				// event #7 is the stop boundary, and in this case it should also be the start event for the 2nd boundary
				new IrrigationEvent{ JournalId = 8, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 70, IsPumpOn = true, Velocity = 80, CreatedDate = new DateTime(2018, 6, 1, 13, 0, 0) },
				new IrrigationEvent{ JournalId = 9, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 30, IsPumpOn = true, Velocity = 80, CreatedDate = new DateTime(2018, 6, 1, 13, 10, 0) },
				new IrrigationEvent{ JournalId = 10, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 350, IsPumpOn = true, Velocity = 80, CreatedDate = new DateTime(2018, 6, 1, 13, 20, 0) },

				// this should be the last irrigation event before the stop event
				new IrrigationEvent{ JournalId = 11, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 310, IsPumpOn = true, Velocity = 80, CreatedDate = new DateTime(2018, 6, 1, 13, 30, 0) },
				
				// more status records that should not cause boundary events...
				new IrrigationEvent{ JournalId = 12, Substance = IrrigationEventsManager.NoneSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 300, IsPumpOn = false, Velocity = 0, CreatedDate = new DateTime(2018, 6, 1, 13, 40, 0) }
			};
		}

		public static IEnumerable<IrrigationEvent> GetReverseToForwardData()
		{
			return new Collection<IrrigationEvent>
			{
				// arbitrary event...
				new IrrigationEvent{ JournalId = 1, Substance = IrrigationEventsManager.NoneSubstanceValue, Direction = IrrigationEventsManager.DirectionNone, Bearing = 0.0m, IsPumpOn = false, Velocity = 0, CreatedDate = new DateTime(2018, 6, 1, 12, 0, 0) },

				// this is the start boundary
				new IrrigationEvent{ JournalId = 2, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 10.0m, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 10, 0) },

				// arbitrary events
				new IrrigationEvent{ JournalId = 3, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 350, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 20, 0) },
				new IrrigationEvent{ JournalId = 4, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 330, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 30, 0) },
				new IrrigationEvent{ JournalId = 5, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 310, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 40, 0) },
				new IrrigationEvent{ JournalId = 6, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 290, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 50, 0) },

				// inserting a bogus bearing for testing purposes - pretending the pivot stopped before it went forward...
				new IrrigationEvent{ JournalId = 7, Substance = IrrigationEventsManager.NoneSubstanceValue, Direction = IrrigationEventsManager.DirectionNone, Bearing = 290, IsPumpOn = false, Velocity = 0, CreatedDate = new DateTime(2018, 6, 1, 12, 55, 0) },

				// event #8 is the start boundary
				new IrrigationEvent{ JournalId = 8, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 310, IsPumpOn = true, Velocity = 80, CreatedDate = new DateTime(2018, 6, 1, 13, 0, 0) },
				new IrrigationEvent{ JournalId = 9, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 330, IsPumpOn = true, Velocity = 80, CreatedDate = new DateTime(2018, 6, 1, 13, 10, 0) },
				new IrrigationEvent{ JournalId = 10, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 360, IsPumpOn = true, Velocity = 80, CreatedDate = new DateTime(2018, 6, 1, 13, 20, 0) },

				// this should be the last irrigation event before the stop event
				new IrrigationEvent{ JournalId = 11, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 20, IsPumpOn = true, Velocity = 80, CreatedDate = new DateTime(2018, 6, 1, 13, 30, 0) },
				
				// more status records that should not cause boundary events...
				new IrrigationEvent{ JournalId = 12, Substance = IrrigationEventsManager.NoneSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 40, IsPumpOn = false, Velocity = 0, CreatedDate = new DateTime(2018, 6, 1, 13, 40, 0) }
			};
		}

		public static IEnumerable<IrrigationEvent> GetDataWithIncreasingSpeedChange()
		{
			return new Collection<IrrigationEvent>
			{
				// arbitrary event...
				new IrrigationEvent{ JournalId = 1, Substance = IrrigationEventsManager.NoneSubstanceValue, Direction = IrrigationEventsManager.DirectionNone, Bearing = 0.0m, IsPumpOn = false, Velocity = 0, CreatedDate = new DateTime(2018, 6, 1, 12, 0, 0) },

				// this is the start boundary
				new IrrigationEvent{ JournalId = 2, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 10.0m, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 10, 0) },

				// arbitrary events
				new IrrigationEvent{ JournalId = 3, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 20, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 20, 0) },
				new IrrigationEvent{ JournalId = 4, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 30, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 30, 0) },
				new IrrigationEvent{ JournalId = 5, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 40, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 40, 0) },
				new IrrigationEvent{ JournalId = 6, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 50, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 50, 0) },
				new IrrigationEvent{ JournalId = 7, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 60, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 55, 0) },

				// event #8 is the stop boundary and start boundary b/c of speed change
				new IrrigationEvent{ JournalId = 8, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 70, IsPumpOn = true, Velocity = 80, CreatedDate = new DateTime(2018, 6, 1, 13, 0, 0) },
				new IrrigationEvent{ JournalId = 9, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 80, IsPumpOn = true, Velocity = 80, CreatedDate = new DateTime(2018, 6, 1, 13, 10, 0) },
				new IrrigationEvent{ JournalId = 10, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 90, IsPumpOn = true, Velocity = 80, CreatedDate = new DateTime(2018, 6, 1, 13, 20, 0) },

				// this should be the last irrigation event before the stop event
				new IrrigationEvent{ JournalId = 11, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 100, IsPumpOn = true, Velocity = 80, CreatedDate = new DateTime(2018, 6, 1, 13, 30, 0) },
				
				// more status records that should not cause boundary events...
				new IrrigationEvent{ JournalId = 12, Substance = IrrigationEventsManager.NoneSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 110, IsPumpOn = true, Velocity = 80, CreatedDate = new DateTime(2018, 6, 1, 13, 40, 0) }
			};
		}

		public static IEnumerable<IrrigationEvent> GetDataWithDecreasingSpeedChange()
		{
			return new Collection<IrrigationEvent>
			{
				// arbitrary event...
				new IrrigationEvent{ JournalId = 1, Substance = IrrigationEventsManager.NoneSubstanceValue, Direction = IrrigationEventsManager.DirectionNone, Bearing = 0.0m, IsPumpOn = false, Velocity = 0, CreatedDate = new DateTime(2018, 6, 1, 12, 0, 0) },

				// this is the start boundary
				new IrrigationEvent{ JournalId = 2, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 10.0m, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 10, 0) },

				// arbitrary events
				new IrrigationEvent{ JournalId = 3, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 20, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 20, 0) },
				new IrrigationEvent{ JournalId = 4, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 30, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 30, 0) },
				new IrrigationEvent{ JournalId = 5, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 40, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 40, 0) },
				new IrrigationEvent{ JournalId = 6, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 50, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 50, 0) },
				new IrrigationEvent{ JournalId = 7, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 60, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 55, 0) },

				// event #8 is the stop boundary and start boundary b/c of speed change
				new IrrigationEvent{ JournalId = 8, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 70, IsPumpOn = true, Velocity = 20, CreatedDate = new DateTime(2018, 6, 1, 13, 0, 0) },
				new IrrigationEvent{ JournalId = 9, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 80, IsPumpOn = true, Velocity = 20, CreatedDate = new DateTime(2018, 6, 1, 13, 10, 0) },
				new IrrigationEvent{ JournalId = 10, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 90, IsPumpOn = true, Velocity = 20, CreatedDate = new DateTime(2018, 6, 1, 13, 20, 0) },

				// this should be the last irrigation event before the stop event
				new IrrigationEvent{ JournalId = 11, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 100, IsPumpOn = true, Velocity = 20, CreatedDate = new DateTime(2018, 6, 1, 13, 30, 0) },
				
				// more status records that should not cause boundary events...
				new IrrigationEvent{ JournalId = 12, Substance = IrrigationEventsManager.NoneSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 110, IsPumpOn = false, Velocity = 0, CreatedDate = new DateTime(2018, 6, 1, 13, 40, 0) }
			};
		}

		public static IEnumerable<IrrigationEvent> GetDataWithSubstanceChange()
		{
			return new Collection<IrrigationEvent>
			{
				// arbitrary event...
				new IrrigationEvent{ JournalId = 1, Substance = IrrigationEventsManager.NoneSubstanceValue, Direction = IrrigationEventsManager.DirectionNone, Bearing = 0.0m, IsPumpOn = false, Velocity = 0, CreatedDate = new DateTime(2018, 6, 1, 12, 0, 0) },

				// this is the start boundary
				new IrrigationEvent{ JournalId = 2, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 10.0m, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 10, 0) },

				// arbitrary events
				new IrrigationEvent{ JournalId = 3, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 20, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 20, 0) },
				new IrrigationEvent{ JournalId = 4, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 30, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 30, 0) },
				new IrrigationEvent{ JournalId = 5, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 40, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 40, 0) },
				new IrrigationEvent{ JournalId = 6, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 60, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 50, 0) },

				// inserting a bogus bearing for testing purposes - pretending the pivot stopped before it went in reverse...
				new IrrigationEvent{ JournalId = 7, Substance = IrrigationEventsManager.NoneSubstanceValue, Direction = IrrigationEventsManager.DirectionNone, Bearing = 70, IsPumpOn = false, Velocity = 0, CreatedDate = new DateTime(2018, 6, 1, 12, 55, 0) },

				// event #7 is the stop boundary, and in this case it should also be the start event for the 2nd boundary
				new IrrigationEvent{ JournalId = 8, Substance = IrrigationEventsManager.FertigationSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 70, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 13, 0, 0) },
				new IrrigationEvent{ JournalId = 9, Substance = IrrigationEventsManager.FertigationSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 80, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 13, 10, 0) },
				new IrrigationEvent{ JournalId = 10, Substance = IrrigationEventsManager.FertigationSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 90, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 13, 20, 0) },

				// this should be the last irrigation event before the stop event
				new IrrigationEvent{ JournalId = 11, Substance = IrrigationEventsManager.FertigationSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 100, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 13, 30, 0) },
				
				// more status records that should not cause boundary events...
				new IrrigationEvent{ JournalId = 12, Substance = IrrigationEventsManager.NoneSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 100, IsPumpOn = false, Velocity = 0, CreatedDate = new DateTime(2018, 6, 1, 13, 40, 0) }
			};
		}

		public static IEnumerable<IrrigationEvent> GetDataWithSubstanceChangedFromWaterToEffluent()
		{
			return new Collection<IrrigationEvent>
			{
				// arbitrary event...
				new IrrigationEvent{ JournalId = 1, Substance = IrrigationEventsManager.NoneSubstanceValue, Direction = IrrigationEventsManager.DirectionNone, Bearing = 0.0m, IsPumpOn = false, Velocity = 0, CreatedDate = new DateTime(2018, 6, 1, 12, 0, 0) },

				// this is the start boundary
				new IrrigationEvent{ JournalId = 2, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 10.0m, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 10, 0) },

				// arbitrary events
				new IrrigationEvent{ JournalId = 3, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 20, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 20, 0) },
				new IrrigationEvent{ JournalId = 4, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 30, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 30, 0) },
				new IrrigationEvent{ JournalId = 5, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 40, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 40, 0) },
				new IrrigationEvent{ JournalId = 6, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 60, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 50, 0) },

				// inserting a bogus bearing for testing purposes - pretending the pivot stopped before it went in reverse...
				new IrrigationEvent{ JournalId = 7, Substance = IrrigationEventsManager.NoneSubstanceValue, Direction = IrrigationEventsManager.DirectionNone, Bearing = 70, IsPumpOn = false, Velocity = 0, CreatedDate = new DateTime(2018, 6, 1, 12, 55, 0) },

				// event #7 is the stop boundary, and in this case it should also be the start event for the 2nd boundary
				new IrrigationEvent{ JournalId = 8, Substance = IrrigationEventsManager.EffluentSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 70, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 13, 0, 0) },
				new IrrigationEvent{ JournalId = 9, Substance = IrrigationEventsManager.EffluentSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 80, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 13, 10, 0) },
				new IrrigationEvent{ JournalId = 10, Substance = IrrigationEventsManager.EffluentSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 90, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 13, 20, 0) },

				// this should be the last irrigation event before the stop event
				new IrrigationEvent{ JournalId = 11, Substance = IrrigationEventsManager.EffluentSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 100, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 13, 30, 0) },
				
				// more status records that should not cause boundary events...
				new IrrigationEvent{ JournalId = 12, Substance = IrrigationEventsManager.NoneSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 100, IsPumpOn = false, Velocity = 0, CreatedDate = new DateTime(2018, 6, 1, 13, 40, 0) }
			};
		}

		public static IEnumerable<IrrigationEvent> GetDataWithMultipleRevolutions()
		{
			return new Collection<IrrigationEvent>
			{
				// arbitrary event...
				new IrrigationEvent{ JournalId = 1, Substance = IrrigationEventsManager.NoneSubstanceValue, Direction = IrrigationEventsManager.DirectionNone, Bearing = 0.0m, IsPumpOn = false, Velocity = 0, CreatedDate = new DateTime(2018, 6, 1, 12, 0, 0) },

				// this is the start boundary
				new IrrigationEvent{ JournalId = 2, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 1, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 10, 0) },

				// arbitrary events
				new IrrigationEvent{ JournalId = 3, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 90, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 20, 0) },
				new IrrigationEvent{ JournalId = 4, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 180, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 30, 0) },
				new IrrigationEvent{ JournalId = 5, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 270, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 40, 0) },

				// this should be the first event of the 2nd revolution
				new IrrigationEvent{ JournalId = 6, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 10, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 50, 0) },
				new IrrigationEvent{ JournalId = 7, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 90, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 55, 0) },
				new IrrigationEvent{ JournalId = 8, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 180, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 13, 0, 0) },
				new IrrigationEvent{ JournalId = 9, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 270, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 13, 10, 0) },
				new IrrigationEvent{ JournalId = 10, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 10, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 13, 20, 0) },

				// this should be the last irrigation event before the stop event
				new IrrigationEvent{ JournalId = 11, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 90, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 13, 30, 0) },
				
				// more status records that should not cause boundary events...
				new IrrigationEvent{ JournalId = 12, Substance = IrrigationEventsManager.NoneSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 110, IsPumpOn = false, Velocity = 0, CreatedDate = new DateTime(2018, 6, 1, 13, 40, 0) }
			};
		}

		public static IEnumerable<IrrigationEvent> GetDataWithMultipleRevolutions_And_BearingDrift()
		{
			return new Collection<IrrigationEvent>
			{
				// arbitrary event...
				new IrrigationEvent{ JournalId = 1, Substance = IrrigationEventsManager.NoneSubstanceValue, Direction = IrrigationEventsManager.DirectionNone, Bearing = 0.0m, IsPumpOn = false, Velocity = 0, CreatedDate = new DateTime(2018, 6, 1, 12, 0, 0) },

				// this is the start boundary
				new IrrigationEvent{ JournalId = 2, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 1, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 10, 0) },

				// arbitrary events
				new IrrigationEvent{ JournalId = 3, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 90, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 20, 0) },
				new IrrigationEvent{ JournalId = 4, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 89m, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 30, 0) },
				new IrrigationEvent{ JournalId = 5, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 91, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 40, 0) },

				// this should be the first event of the 2nd revolution
				new IrrigationEvent{ JournalId = 6, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 10, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 50, 0) },
				new IrrigationEvent{ JournalId = 7, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 90, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 55, 0) },
				new IrrigationEvent{ JournalId = 8, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 180, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 13, 0, 0) },
				new IrrigationEvent{ JournalId = 9, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 270, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 13, 10, 0) },
				new IrrigationEvent{ JournalId = 10, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 10, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 13, 20, 0) },

				// this should be the last irrigation event before the stop event
				new IrrigationEvent{ JournalId = 11, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 90, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 13, 30, 0) },
				
				// this is a stop event b/c pump and substance change, but the bearing has drift, so we will need to handle it.
				new IrrigationEvent{ JournalId = 12, Substance = IrrigationEventsManager.NoneSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 89, IsPumpOn = false, Velocity = 0, CreatedDate = new DateTime(2018, 6, 1, 13, 40, 0) }
			};
		}

		public static IEnumerable<IrrigationEvent> GetDataWithMultipleRevolutions_ChangingDirection_AndSubstance()
		{
			return new Collection<IrrigationEvent>
			{ 
			// arbitrary event...
			new IrrigationEvent { JournalId = 1, Substance = IrrigationEventsManager.NoneSubstanceValue, Direction = IrrigationEventsManager.DirectionNone, Bearing = 0.0m, IsPumpOn = false, Velocity = 0, CreatedDate = new DateTime(2018, 6, 1, 12, 0, 0) },

				// this is the start boundary
				new IrrigationEvent { JournalId = 2, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 1, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 10, 0) },

				// arbitrary events
				new IrrigationEvent { JournalId = 3, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 90, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 20, 0) },
				new IrrigationEvent { JournalId = 4, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 180, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 30, 0) },
				new IrrigationEvent { JournalId = 5, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 270, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 40, 0) },

				// this here we stop the pivot so we can hookup fertigation
				new IrrigationEvent { JournalId = 6, Substance = IrrigationEventsManager.NoneSubstanceValue, Direction = IrrigationEventsManager.DirectionNone, Bearing = 10, IsPumpOn = false, Velocity = 0, CreatedDate = new DateTime(2018, 6, 1, 12, 50, 0) },

				// here we start the pivot with fertigation running reverse
				new IrrigationEvent { JournalId = 7, Substance = IrrigationEventsManager.FertigationSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 10, IsPumpOn = true, Velocity = 80, CreatedDate = new DateTime(2018, 6, 1, 12, 55, 0) },
				new IrrigationEvent { JournalId = 8, Substance = IrrigationEventsManager.FertigationSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 270, IsPumpOn = true, Velocity = 80, CreatedDate = new DateTime(2018, 6, 1, 13, 0, 0) },
				new IrrigationEvent { JournalId = 9, Substance = IrrigationEventsManager.FertigationSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 45, IsPumpOn = true, Velocity = 80, CreatedDate = new DateTime(2018, 6, 1, 13, 10, 0) },
				// this event should cause degrees traveled to be > 360 so a new boundary should get created
				new IrrigationEvent { JournalId = 10, Substance = IrrigationEventsManager.FertigationSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 350, IsPumpOn = true, Velocity = 80, CreatedDate = new DateTime(2018, 6, 1, 13, 20, 0) },

				// here we stop the pivot so we can go forward
				new IrrigationEvent { JournalId = 11, Substance = IrrigationEventsManager.NoneSubstanceValue, Direction = IrrigationEventsManager.DirectionNone, Bearing = 350, IsPumpOn = false, Velocity = 0, CreatedDate = new DateTime(2018, 6, 1, 13, 30, 0) },
				
				// just for fun we will run the pivot forward for some time with effluent
				new IrrigationEvent { JournalId = 12, Substance = IrrigationEventsManager.EffluentSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 350, IsPumpOn = true, Velocity = 70, CreatedDate = new DateTime(2018, 6, 1, 13, 40, 0) },
				new IrrigationEvent { JournalId = 13, Substance = IrrigationEventsManager.EffluentSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 20, IsPumpOn = true, Velocity = 70, CreatedDate = new DateTime(2018, 6, 1, 13, 50, 0) },
				new IrrigationEvent { JournalId = 14, Substance = IrrigationEventsManager.EffluentSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 40, IsPumpOn = true, Velocity = 70, CreatedDate = new DateTime(2018, 6, 1, 14, 10, 0) },
				new IrrigationEvent { JournalId = 15, Substance = IrrigationEventsManager.EffluentSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 180, IsPumpOn = true, Velocity = 70, CreatedDate = new DateTime(2018, 6, 1, 15, 20, 0) }
			};
		}

		public static IEnumerable<IrrigationEvent> GetDataWithMultipleRevolutions_ChangingDirection_AndSubstance_AndBearingDrift()
		{
			return new Collection<IrrigationEvent>
			{  // arbitrary event...
				new IrrigationEvent { JournalId = 1, Substance = IrrigationEventsManager.NoneSubstanceValue, Direction = IrrigationEventsManager.DirectionNone, Bearing = 0.0m, IsPumpOn = false, Velocity = 0, CreatedDate = new DateTime(2018, 6, 1, 12, 0, 0) },

				// this is the start boundary
				new IrrigationEvent { JournalId = 2, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 1, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 10, 0) },

				// arbitrary events
				new IrrigationEvent { JournalId = 3, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 90, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 20, 0) },
				new IrrigationEvent { JournalId = 4, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 89, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 30, 0) },
				new IrrigationEvent { JournalId = 5, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 91, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 40, 0) },

				// this here we stop the pivot so we can hookup fertigation
				new IrrigationEvent { JournalId = 6, Substance = IrrigationEventsManager.NoneSubstanceValue, Direction = IrrigationEventsManager.DirectionNone, Bearing = 10, IsPumpOn = false, Velocity = 0, CreatedDate = new DateTime(2018, 6, 1, 12, 50, 0) },

				// here we start the pivot with fertigation running reverse
				new IrrigationEvent { JournalId = 7, Substance = IrrigationEventsManager.FertigationSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 10, IsPumpOn = true, Velocity = 80, CreatedDate = new DateTime(2018, 6, 1, 12, 55, 0) },
				new IrrigationEvent { JournalId = 8, Substance = IrrigationEventsManager.FertigationSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 11, IsPumpOn = true, Velocity = 80, CreatedDate = new DateTime(2018, 6, 1, 13, 0, 0) },
				new IrrigationEvent { JournalId = 9, Substance = IrrigationEventsManager.FertigationSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 350, IsPumpOn = true, Velocity = 80, CreatedDate = new DateTime(2018, 6, 1, 13, 10, 0) },
				// this event should cause degrees traveled to be > 360 so a new boundary should get created
				new IrrigationEvent { JournalId = 10, Substance = IrrigationEventsManager.FertigationSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 330, IsPumpOn = true, Velocity = 80, CreatedDate = new DateTime(2018, 6, 1, 13, 20, 0) },

				// here we stop the pivot so we can go forward
				new IrrigationEvent { JournalId = 11, Substance = IrrigationEventsManager.NoneSubstanceValue, Direction = IrrigationEventsManager.DirectionNone, Bearing = 350, IsPumpOn = false, Velocity = 0, CreatedDate = new DateTime(2018, 6, 1, 13, 30, 0) },
				
				// just for fun we will run the pivot forward for some time with effluent
				new IrrigationEvent { JournalId = 12, Substance = IrrigationEventsManager.EffluentSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 350, IsPumpOn = true, Velocity = 70, CreatedDate = new DateTime(2018, 6, 1, 13, 40, 0) },
				new IrrigationEvent { JournalId = 13, Substance = IrrigationEventsManager.EffluentSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 20, IsPumpOn = true, Velocity = 70, CreatedDate = new DateTime(2018, 6, 1, 13, 50, 0) },
				new IrrigationEvent { JournalId = 14, Substance = IrrigationEventsManager.EffluentSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 40, IsPumpOn = true, Velocity = 70, CreatedDate = new DateTime(2018, 6, 1, 14, 10, 0) },
				new IrrigationEvent { JournalId = 15, Substance = IrrigationEventsManager.EffluentSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 180, IsPumpOn = true, Velocity = 70, CreatedDate = new DateTime(2018, 6, 1, 15, 20, 0) }
			};
		}

		public static IEnumerable<IrrigationEvent> GetData_Modeling_Reverse_ThenA_BearingDriftSequence_FollowedByA_VeryLongDelay()
		{
			return new Collection<IrrigationEvent>
			{ 
				// arbitrary event...
				new IrrigationEvent { JournalId = 1, Substance = IrrigationEventsManager.NoneSubstanceValue, Direction = IrrigationEventsManager.DirectionNone, Bearing = 0.0m, IsPumpOn = false, Velocity = 0, CreatedDate = new DateTime(2018, 6, 1, 12, 0, 0) },

				new IrrigationEvent { JournalId = 2, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 117.9m, IsPumpOn = true, Velocity = 7, CreatedDate = new DateTime(2018, 6, 1, 12, 10, 0) },

				new IrrigationEvent { JournalId = 3, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 117.783333m, IsPumpOn = true, Velocity = 7, CreatedDate = new DateTime(2018, 6, 1, 12, 20, 0) },
				new IrrigationEvent { JournalId = 4, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 117.766667m, IsPumpOn = true, Velocity = 7, CreatedDate = new DateTime(2018, 6, 1, 12, 30, 0) },
				new IrrigationEvent { JournalId = 5, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 117.8m, IsPumpOn = true, Velocity = 6, CreatedDate = new DateTime(2018, 6, 1, 12, 40, 0) },

				new IrrigationEvent { JournalId = 6, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 117.75m, IsPumpOn = true, Velocity = 6, CreatedDate = new DateTime(2018, 6, 1, 12, 50, 0) },


				new IrrigationEvent { JournalId = 7, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 117.683333m, IsPumpOn = true, Velocity = 6, CreatedDate = new DateTime(2018, 6, 1, 12, 55, 0) },
				new IrrigationEvent { JournalId = 8, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 117.7m, IsPumpOn = true, Velocity = 6, CreatedDate = new DateTime(2018, 6, 1, 13, 0, 0) },
				new IrrigationEvent { JournalId = 9, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 117.733333m, IsPumpOn = true, Velocity = 7, CreatedDate = new DateTime(2018, 6, 1, 13, 10, 0) },

				new IrrigationEvent { JournalId = 10, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 118.05m, IsPumpOn = true, Velocity = 7, CreatedDate = new DateTime(2018, 6, 1, 13, 20, 0) },

				new IrrigationEvent { JournalId = 11, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 118.033333m, IsPumpOn = true, Velocity = 7, CreatedDate = new DateTime(2018, 6, 1, 13, 30, 0) },

				new IrrigationEvent { JournalId = 12, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 118m, IsPumpOn = true, Velocity = 6, CreatedDate = new DateTime(2018, 6, 1, 13, 40, 0) },

				// this data picks up 1 day later... and is outside of the max bearing drift angle... b/c 118 to 160 going in reverse is 318
				new IrrigationEvent { JournalId = 13, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 160.533333m, IsPumpOn = true, Velocity = 6, CreatedDate = new DateTime(2018, 6, 2, 13, 38, 0) },
				new IrrigationEvent { JournalId = 14, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 160.583333m, IsPumpOn = true, Velocity = 6, CreatedDate = new DateTime(2018, 6, 2, 14, 37, 0) },
				new IrrigationEvent { JournalId = 15, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 160.416667m, IsPumpOn = true, Velocity = 6, CreatedDate = new DateTime(2018, 6, 2, 14, 50, 0) },
				new IrrigationEvent { JournalId = 16, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 160.383333m, IsPumpOn = true, Velocity = 6, CreatedDate = new DateTime(2018, 6, 2, 15, 0, 0) },
			};
		}

		public static IEnumerable<IrrigationEvent> GetData_Modeling_Reverse_ThenA_BearingDriftSequence_FollowedByA_VeryLongDelayInAStopEvent()
		{
			return new Collection<IrrigationEvent>
			{ 
				// arbitrary event...
				new IrrigationEvent { JournalId = 1, Substance = IrrigationEventsManager.NoneSubstanceValue, Direction = IrrigationEventsManager.DirectionNone, Bearing = 0.0m, IsPumpOn = false, Velocity = 0, CreatedDate = new DateTime(2018, 6, 1, 12, 0, 0) },

				new IrrigationEvent { JournalId = 2, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 117.9m, IsPumpOn = true, Velocity = 7, CreatedDate = new DateTime(2018, 6, 1, 12, 10, 0) },

				new IrrigationEvent { JournalId = 3, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 117.783333m, IsPumpOn = true, Velocity = 7, CreatedDate = new DateTime(2018, 6, 1, 12, 20, 0) },
				new IrrigationEvent { JournalId = 4, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 117.766667m, IsPumpOn = true, Velocity = 7, CreatedDate = new DateTime(2018, 6, 1, 12, 30, 0) },
				new IrrigationEvent { JournalId = 5, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 117.8m, IsPumpOn = true, Velocity = 6, CreatedDate = new DateTime(2018, 6, 1, 12, 40, 0) },

				new IrrigationEvent { JournalId = 6, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 117.75m, IsPumpOn = true, Velocity = 6, CreatedDate = new DateTime(2018, 6, 1, 12, 50, 0) },


				new IrrigationEvent { JournalId = 7, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 117.683333m, IsPumpOn = true, Velocity = 6, CreatedDate = new DateTime(2018, 6, 1, 12, 55, 0) },
				new IrrigationEvent { JournalId = 8, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 117.7m, IsPumpOn = true, Velocity = 6, CreatedDate = new DateTime(2018, 6, 1, 13, 0, 0) },
				new IrrigationEvent { JournalId = 9, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 117.733333m, IsPumpOn = true, Velocity = 7, CreatedDate = new DateTime(2018, 6, 1, 13, 10, 0) },

				new IrrigationEvent { JournalId = 10, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 118.05m, IsPumpOn = true, Velocity = 7, CreatedDate = new DateTime(2018, 6, 1, 13, 20, 0) },

				new IrrigationEvent { JournalId = 11, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 118.033333m, IsPumpOn = true, Velocity = 7, CreatedDate = new DateTime(2018, 6, 1, 13, 30, 0) },

				new IrrigationEvent { JournalId = 12, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 118m, IsPumpOn = true, Velocity = 6, CreatedDate = new DateTime(2018, 6, 1, 13, 40, 0) },

				// this data picks up 1 day later... and is outside of the max bearing drift angle... b/c 118 to 160 going in reverse is 318
				new IrrigationEvent { JournalId = 13, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 160.533333m, IsPumpOn = true, Velocity = 7, CreatedDate = new DateTime(2018, 6, 2, 13, 38, 0) },
				new IrrigationEvent { JournalId = 14, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 160.583333m, IsPumpOn = true, Velocity = 7, CreatedDate = new DateTime(2018, 6, 2, 14, 37, 0) },
				new IrrigationEvent { JournalId = 15, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 160.416667m, IsPumpOn = true, Velocity = 7, CreatedDate = new DateTime(2018, 6, 2, 14, 50, 0) },
				new IrrigationEvent { JournalId = 16, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 160.383333m, IsPumpOn = true, Velocity = 7, CreatedDate = new DateTime(2018, 6, 2, 15, 0, 0) },
			};
		}
	}
}
