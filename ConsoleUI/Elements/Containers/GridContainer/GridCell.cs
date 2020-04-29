using System;
using ConsoleUI.Elements.BaseElements;

namespace ConsoleUI.Elements.Containers.GridContainer
{
	public class GridCell : ContainerElement
	{
		private readonly Grid m_ParentGrid;
		private UInt16 m_ColumnIndex;
		private UInt16 m_RowIndex;

		private UInt16 m_InnerRight;
		private UInt16 m_InnerBottom;
		private readonly GridCell m_LeftNeighbor;
		private GridCell m_TopNeighbor;

		public UInt16 ColumnIndex
		{
			get => m_ColumnIndex;
			set => SetProperty( ref m_ColumnIndex, value );
		}

		public UInt16 RowIndex
		{
			get => m_RowIndex;
			set => SetProperty( ref m_RowIndex, value );
		}

		public override UInt16 Right
		{
			get => base.Right;
			set
			{
				m_InnerRight = value;
				base.Right = (UInt16)( Left + m_InnerRight );
			}
		}

		public override UInt16 Bottom
		{
			get => base.Bottom;
			set
			{
				m_InnerBottom = value;
				base.Bottom = (UInt16)( Top + m_InnerBottom );
			}
		}

		public event EventHandler<CellUpdatedEventArgs> CellContentUpdated;

		public GridCell( Grid _parentGrid, UInt16 _columnIndex, UInt16 _rowIndex )
		{
			m_ParentGrid = _parentGrid;
			ColumnIndex = _columnIndex;
			RowIndex = _rowIndex;

			if ( ColumnIndex > 0 )
			{
				m_LeftNeighbor = m_ParentGrid.Cells[ColumnIndex - 1, RowIndex];
				m_LeftNeighbor.SubscribeToPropertyChanged( nameof( AbsoluteBottomRight ), OnLeftNeighborAbsoluteBottomRightChanged );
			}

			if ( RowIndex > 0 )
			{
				m_TopNeighbor = m_ParentGrid.Cells[ColumnIndex, RowIndex - 1];
				m_TopNeighbor.SubscribeToPropertyChanged( nameof( AbsoluteBottomRight ), OnTopNeighborAbsoluteBottomRightChanged );
			}

			SubscribeToPropertyChanged( nameof( ColumnIndex ), OnColumnChanged );
			SubscribeToPropertyChanged( nameof( RowIndex ), OnRowChanged );
		}

		public override void AddChild( Element _child )
		{
			base.AddChild( _child );
			_child.SubscribeToPropertyChanged( nameof( Width ), OnCellUpdated );
			_child.SubscribeToPropertyChanged( nameof( Height ), OnCellUpdated );
		}

		public virtual void UpdateTopNeighbor( GridCell _topNeighbor )
		{
			m_TopNeighbor = _topNeighbor;
		}

		protected override void RecalculateTop()
		{
			Top = (UInt16)( m_TopNeighbor?.BottomRight.Y + 1 ?? 0 );

			Bottom = m_InnerBottom;
		}

		protected override void RecalculateLeft()
		{
			Left = (UInt16)( m_LeftNeighbor?.BottomRight.X + 1 ?? 0 );

			Right = m_InnerRight;
		}

		protected virtual void OnCellUpdated()
		{
			CellContentUpdated?.Invoke( this, new CellUpdatedEventArgs( ColumnIndex, RowIndex ) );
		}

		private void OnLeftNeighborAbsoluteBottomRightChanged()
		{
			RecalculateLeft();
		}

		private void OnTopNeighborAbsoluteBottomRightChanged()
		{
			RecalculateTop();
		}

		private void OnRowChanged()
		{
			RecalculateTop();
			RecalculateLeft();
		}

		private void OnColumnChanged()
		{
			RecalculateTop();
			RecalculateLeft();
		}

		~GridCell()
		{
			UnsubscribeFromPropertyChanged( nameof( ColumnIndex ), OnColumnChanged );
			UnsubscribeFromPropertyChanged( nameof( RowIndex ), OnRowChanged );

			m_LeftNeighbor?.UnsubscribeFromPropertyChanged( nameof( AbsoluteBottomRight ), OnLeftNeighborAbsoluteBottomRightChanged );
			m_TopNeighbor?.UnsubscribeFromPropertyChanged( nameof( AbsoluteBottomRight ), OnTopNeighborAbsoluteBottomRightChanged );
		}
	}
}