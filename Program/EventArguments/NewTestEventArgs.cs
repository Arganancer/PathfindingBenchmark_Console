using System;
using World.Geometry;

namespace Program.EventArguments
{
	public class NewTestEventArgs : EventArgs
	{
		public Vector3i StartPos;
		public Vector3i EndPos;
		public Int32 TestNumber;

		public NewTestEventArgs( Vector3i _startPos, Vector3i _endPos, Int32 _testNumber )
		{
			StartPos = _startPos;
			EndPos = _endPos;
			TestNumber = _testNumber;
		}
	}
}