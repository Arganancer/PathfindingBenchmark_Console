using System;

namespace Pathfinding.DataStructures
{
	public class AVLNode<TKey, TValue>
	{
		public Boolean IsRoot => Parent is null;
		public Boolean HasParent => Parent != null;
		public Boolean HasLeftChild => Left != null;
		public Boolean HasRightChild => Right != null;
		public Boolean IsLeftChild => ReferenceEquals( Parent?.Left, this );
		public Boolean IsRightChild => ReferenceEquals( Parent?.Right, this );
		public AVLNode<TKey, TValue> Parent { get; set; }
		public AVLNode<TKey, TValue> Left { get; set; }
		public AVLNode<TKey, TValue> Right { get; set; }
		public TKey Key { get; set; }
		public TValue Value { get; set; }
		public Int32 Balance { get; set; }
		public Boolean IsValid => IsLeftChild || IsRightChild;
	}
}