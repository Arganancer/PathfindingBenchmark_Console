using System;
using ConsoleUI.Elements.BaseElements;

namespace ConsoleUI.Elements.Containers.ListContainer
{
	public sealed class ListItem : ContainerElement
	{
		public ListItem()
		{
			RightAnchor = 1.0f;
			UseHorizontalAnchors = true;
		}

		public Guid ItemID { get; }

		public ListItem( Guid _itemID )
		{
			ItemID = _itemID;
			LeftAnchor = 0.0f;
			RightAnchor = 1.0f;
			UseHorizontalAnchors = true;
		}
	}
}