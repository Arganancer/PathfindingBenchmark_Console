using System;

namespace Program.Log
{
	public class NavTestLogItem
	{
		public Double ExecutionTime { get; }

		public Int32 NbOfSteps { get; }

		public Int64 BytesUsed { get; }

		public NavTestLogItem( Double _executionTime, Int32 _nbOfSteps, Int64 _bytesUsed )
		{
			ExecutionTime = _executionTime;
			NbOfSteps = _nbOfSteps;
			BytesUsed = _bytesUsed;
		}
	}
}