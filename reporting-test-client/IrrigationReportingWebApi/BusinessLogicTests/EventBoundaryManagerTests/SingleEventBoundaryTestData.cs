using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Trimble.Ag.IrrigationReporting.BusinessContracts;
using Trimble.Ag.IrrigationReporting.BusinessLogic;

namespace BusinessLogicTests.EventBoundaryManagerTests
{
	public static class SingleEventBoundaryTestData
	{
		public static IEnumerable<IrrigationEvent> GetForwardOnlyData()
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

				// event #7 is the stop boundary, and in this case no other boundaries should be created
				new IrrigationEvent{ JournalId = 7, Substance = IrrigationEventsManager.NoneSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 70, IsPumpOn = false, Velocity = 0, CreatedDate = new DateTime(2018, 6, 1, 13, 0, 0) },
				new IrrigationEvent{ JournalId = 8, Substance = IrrigationEventsManager.NoneSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 80, IsPumpOn = false, Velocity = 0, CreatedDate = new DateTime(2018, 6, 1, 13, 10, 0) },
				new IrrigationEvent{ JournalId = 9, Substance = IrrigationEventsManager.NoneSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 90, IsPumpOn = false, Velocity = 0, CreatedDate = new DateTime(2018, 6, 1, 13, 20, 0) },
				new IrrigationEvent{ JournalId = 10, Substance = IrrigationEventsManager.NoneSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 100, IsPumpOn = false, Velocity = 0, CreatedDate = new DateTime(2018, 6, 1, 13, 30, 0) },
			};
		}

		/// <summary>
		/// returns test data that models a single irrigation event where the pivot is traveling in the reverse direction
		/// </summary>
		/// <returns></returns>
		public static IEnumerable<IrrigationEvent> GetReverseOnlyData()
		{
			return new Collection<IrrigationEvent>
			{
				// arbitrary event...
				new IrrigationEvent{ JournalId = 1, Substance = IrrigationEventsManager.NoneSubstanceValue, Direction = IrrigationEventsManager.DirectionNone, Bearing = 60, IsPumpOn = false, Velocity = 0, CreatedDate = new DateTime(2018, 6, 1, 12, 0, 0) },

				// this is the start boundary
				new IrrigationEvent{ JournalId = 2, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 40, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 10, 0) },

				// arbitrary events
				new IrrigationEvent{ JournalId = 3, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 20, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 20, 0) },
				new IrrigationEvent{ JournalId = 4, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 350, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 30, 0) },
				new IrrigationEvent{ JournalId = 5, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 330, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 40, 0) },
				new IrrigationEvent{ JournalId = 6, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 310, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 50, 0) },

				// event #7 is the stop boundary, and in this case no other boundaries should be created
				new IrrigationEvent{ JournalId = 7, Substance = IrrigationEventsManager.NoneSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 290, IsPumpOn = false, Velocity = 0, CreatedDate = new DateTime(2018, 6, 1, 13, 0, 0) },
				new IrrigationEvent{ JournalId = 8, Substance = IrrigationEventsManager.NoneSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 270, IsPumpOn = false, Velocity = 0, CreatedDate = new DateTime(2018, 6, 1, 13, 10, 0) },
				new IrrigationEvent{ JournalId = 9, Substance = IrrigationEventsManager.NoneSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 250, IsPumpOn = false, Velocity = 0, CreatedDate = new DateTime(2018, 6, 1, 13, 20, 0) },
				new IrrigationEvent{ JournalId = 10, Substance = IrrigationEventsManager.NoneSubstanceValue, Direction = IrrigationEventsManager.DirectionNone, Bearing = 230, IsPumpOn = false, Velocity = 0, CreatedDate = new DateTime(2018, 6, 1, 13, 30, 0) },
			};
		}

		public static IEnumerable<IrrigationEvent> GetData_ChangingFrom_Reverse_To_Forward()
		{
			return new Collection<IrrigationEvent>
			{
				// arbitrary event...
				new IrrigationEvent{ JournalId = 1, Substance = IrrigationEventsManager.NoneSubstanceValue, Direction = IrrigationEventsManager.DirectionNone, Bearing = 60, IsPumpOn = false, Velocity = 0, CreatedDate = new DateTime(2018, 6, 1, 12, 0, 0) },

				// this is the start boundary
				new IrrigationEvent{ JournalId = 2, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 40, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 10, 0) },

				// arbitrary events
				new IrrigationEvent{ JournalId = 3, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 20, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 20, 0) },
				new IrrigationEvent{ JournalId = 4, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 350, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 30, 0) },
				new IrrigationEvent{ JournalId = 5, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 330, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 40, 0) },
				new IrrigationEvent{ JournalId = 6, Substance = IrrigationEventsManager.WaterSubstanceValue, Direction = IrrigationEventsManager.DirectionReverse, Bearing = 310, IsPumpOn = true, Velocity = 50, CreatedDate = new DateTime(2018, 6, 1, 12, 50, 0) },

				// event #7 is the stop boundary, and in this case no other boundaries should be created
				new IrrigationEvent{ JournalId = 7, Substance = IrrigationEventsManager.NoneSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 290, IsPumpOn = false, Velocity = 80, CreatedDate = new DateTime(2018, 6, 1, 13, 0, 0) },
				new IrrigationEvent{ JournalId = 8, Substance = IrrigationEventsManager.NoneSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 295, IsPumpOn = false, Velocity = 80, CreatedDate = new DateTime(2018, 6, 1, 13, 10, 0) },
				new IrrigationEvent{ JournalId = 9, Substance = IrrigationEventsManager.NoneSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 300, IsPumpOn = false, Velocity = 80, CreatedDate = new DateTime(2018, 6, 1, 13, 20, 0) },
				new IrrigationEvent{ JournalId = 10, Substance = IrrigationEventsManager.NoneSubstanceValue, Direction = IrrigationEventsManager.DirectionForward, Bearing = 310, IsPumpOn = false, Velocity = 80, CreatedDate = new DateTime(2018, 6, 1, 13, 30, 0) },
			};
		}
	}

}
