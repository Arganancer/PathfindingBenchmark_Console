using System;
using System.Linq;
using ConsoleUI.Elements.BaseElements;

namespace ConsoleUI.Elements.Containers.GridContainer
{
	public class Grid : ContainerElement
	{
		private UInt16 m_Columns;
		private UInt16 m_Rows;

		public Grid()
		{
			AutoHeight = true;
			AutoWidth = true;
			m_Columns = 0;
			m_Rows = 0;
			Cells = new GridCell[0, 0];
		}

		public void UpdateCellContent<TElement>( UInt16 _column, UInt16 _row, Action<TElement> _update ) where TElement : Element
		{
			TElement child = Cells[_column, _row].Children.OfType<TElement>().FirstOrDefault();
			if ( child is null )
			{
				throw new Exception( $"Child of type [{typeof( TElement ).Name}] does not exist in GridCell[{_column},{_row}]" );
			}

			_update( child );
		}

		public void AddChild( Element _child, UInt16 _columnIndex, UInt16 _rowIndex )
		{
			UpdateGrid( _columnIndex + 1, _rowIndex + 1 );
			GridCell cell = Cells[_columnIndex, _rowIndex];
			cell.Right = UpdateColumn( _columnIndex, _child.Width );
			cell.Bottom = UpdateRow( _rowIndex, _child.Height );

			cell.AddChild( _child );
			Cells[_columnIndex, _rowIndex] = cell;
			base.AddChild( cell );
		}

		public void OnCellContentContentUpdated( Object _sender, CellUpdatedEventArgs _e )
		{
			UpdateColumn( _e.ColumnIndex );
			UpdateRow( _e.RowIndex );
		}

		private void UpdateGrid( Int32 _columns, Int32 _rows )
		{
			if ( _columns <= Cells.GetLength( 0 ) && _rows <= Cells.GetLength( 1 ) )
			{
				return;
			}

			m_Columns = (UInt16)Math.Max( _columns, Cells.GetLength( 0 ) );
			m_Rows = (UInt16)Math.Max( _rows, Cells.GetLength( 1 ) );

			GridCell[,] oldGridCells = Cells;
			Cells = new GridCell[m_Columns, m_Rows];
			for ( UInt16 row = 0; row < m_Rows; row++ )
			{
				for ( UInt16 column = 0; column < m_Columns; column++ )
				{
					if ( column >= oldGridCells.GetLength( 0 ) || row >= oldGridCells.GetLength( 1 ) )
					{
						GridCell cell = new GridCell( this, column, row );
						cell.CellContentUpdated += OnCellContentContentUpdated;
						Cells[column, row] = cell;
					}
					else
					{
						Cells[column, row] = oldGridCells[column, row];
					}
				}
			}
		}

		private UInt16 GetLargestChildWidthInColumn( UInt16 _column, UInt16 _newWidth = 0 )
		{
			UInt16 largestWidth = _newWidth;
			for ( Int32 y = 0; y < m_Rows; y++ )
			{
				UInt16 childWidth = Cells[_column, y]?.Children.FirstOrDefault()?.Width ?? 0;
				if ( largestWidth < childWidth )
				{
					largestWidth = childWidth;
				}
			}

			return largestWidth;
		}

		private UInt16 GetTallestChildHeightInRow( UInt16 _row, UInt16 _newHeight = 0 )
		{
			UInt16 tallestHeight = _newHeight;
			for ( Int32 x = 0; x < m_Columns; x++ )
			{
				UInt16 childHeight = Cells[x, _row]?.Children.FirstOrDefault()?.Height ?? 0;
				if ( tallestHeight < childHeight )
				{
					tallestHeight = childHeight;
				}
			}

			return tallestHeight;
		}

		private UInt16 UpdateColumn( UInt16 _column, UInt16 _newWidth = 0 )
		{
			UInt16 width = GetLargestChildWidthInColumn( _column, _newWidth );
			for ( Int32 y = 0; y < m_Rows; y++ )
			{
				GridCell cell = Cells[_column, y];
				if ( cell != null )
				{
					cell.Right = width;
				}
			}

			return width;
		}

		private UInt16 UpdateRow( UInt16 _row, UInt16 _newHeight = 0 )
		{
			UInt16 height = GetTallestChildHeightInRow( _row, _newHeight );
			for ( Int32 x = 0; x < m_Columns; x++ )
			{
				GridCell cell = Cells[x, _row];
				if ( cell != null )
				{
					cell.Bottom = height;
				}
			}

			return height;
		}

		protected internal GridCell[,] Cells { get; private set; }
	}
}