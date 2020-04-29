using System;

namespace ConsoleUI.Elements.Containers.GridContainer
{
	public class CellUpdatedEventArgs : EventArgs
	{
		public UInt16 RowIndex { get; }

		public UInt16 ColumnIndex { get; }

		public CellUpdatedEventArgs( UInt16 _columnIndex, UInt16 _rowIndex )
		{
			ColumnIndex = _columnIndex;
			RowIndex = _rowIndex;
		}
	}
}