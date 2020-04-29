using System;
using System.Collections;
using System.Collections.Generic;

namespace Pathfinding.DataStructures
{
	[Serializable]
	public class AvlTree<TKey, TValue> : IEnumerable<TValue> where TKey : IComparable<TKey> where TValue : IComparable<TValue>
	{
		private AvlNode m_Root;

		public void Insert( TKey _key, TValue _value )
		{
			if ( m_Root == null )
			{
				m_Root = new AvlNode { Key = _key, Value = _value };
			}
			else
			{
				AvlNode node = m_Root;

				while ( node != null )
				{
					Int32 compare = _key.CompareTo( node.Key );

					if ( compare <= 0 )
					{
						AvlNode left = node.Left;

						if ( left == null )
						{
							node.Left = new AvlNode { Key = _key, Value = _value, Parent = node };
							InsertBalance( node, 1 );
							return;
						}

						node = left;
					}
					else if ( compare > 0 )
					{
						AvlNode right = node.Right;

						if ( right == null )
						{
							node.Right = new AvlNode { Key = _key, Value = _value, Parent = node };
							InsertBalance( node, -1 );
							return;
						}

						node = right;
					}
				}
			}
		}

		public TValue Pop()
		{
			if ( m_Root == null )
			{
				return default;
			}

			AvlNode currentNode = m_Root;

			while ( currentNode.Left != null )
			{
				currentNode = currentNode.Left;
			}

			return Delete( currentNode.Key, currentNode.Value );
		}

		public TValue Delete( TKey _key )
		{
			AvlNode node = m_Root;

			while ( node != null )
			{
				Int32 compare = _key.CompareTo( node.Key );

				if ( compare < 0 )
				{
					node = node.Left;
				}
				else if ( compare > 0 )
				{
					node = node.Right;
				}
				else
				{
					AvlNode left = node.Left;
					AvlNode right = node.Right;
					TValue outputValue = node.Value;

					if ( left == null )
					{
						if ( right == null )
						{
							if ( node == m_Root )
							{
								m_Root = null;
							}
							else
							{
								AvlNode parent = node.Parent;

								if ( parent.Left == node )
								{
									parent.Left = null;

									DeleteBalance( parent, -1 );
								}
								else
								{
									parent.Right = null;

									DeleteBalance( parent, 1 );
								}
							}
						}
						else
						{
							Replace( node, right );

							DeleteBalance( node, 0 );
						}
					}
					else if ( right == null )
					{
						Replace( node, left );

						DeleteBalance( node, 0 );
					}
					else
					{
						AvlNode successor = right;

						if ( successor.Left == null )
						{
							AvlNode parent = node.Parent;

							successor.Parent = parent;
							successor.Left = left;
							successor.Balance = node.Balance;

							if ( left != null )
							{
								left.Parent = successor;
							}

							if ( node == m_Root )
							{
								m_Root = successor;
							}
							else
							{
								if ( parent.Left == node )
								{
									parent.Left = successor;
								}
								else
								{
									parent.Right = successor;
								}
							}

							DeleteBalance( successor, 1 );
						}
						else
						{
							while ( successor.Left != null )
							{
								successor = successor.Left;
							}

							AvlNode parent = node.Parent;
							AvlNode successorParent = successor.Parent;
							AvlNode successorRight = successor.Right;

							if ( successorParent.Left == successor )
							{
								successorParent.Left = successorRight;
							}
							else
							{
								successorParent.Right = successorRight;
							}

							if ( successorRight != null )
							{
								successorRight.Parent = successorParent;
							}

							successor.Parent = parent;
							successor.Left = left;
							successor.Balance = node.Balance;
							successor.Right = right;
							right.Parent = successor;

							if ( left != null )
							{
								left.Parent = successor;
							}

							if ( node == m_Root )
							{
								m_Root = successor;
							}
							else
							{
								if ( parent.Left == node )
								{
									parent.Left = successor;
								}
								else
								{
									parent.Right = successor;
								}
							}

							DeleteBalance( successorParent, -1 );
						}
					}

					return outputValue;
				}
			}

			return default;
		}

		public TValue Delete( TKey _key, TValue _value )
		{
			AvlNode node = m_Root;

			while ( node != null )
			{
				Int32 compare = _key.CompareTo( node.Key );

				if ( compare == 0 && _value.Equals( node.Value ) )
				{
					AvlNode left = node.Left;
					AvlNode right = node.Right;
					TValue outputValue = node.Value;

					if ( left == null )
					{
						if ( right == null )
						{
							if ( node == m_Root )
							{
								m_Root = null;
							}
							else
							{
								AvlNode parent = node.Parent;

								if ( parent.Left == node )
								{
									parent.Left = null;

									DeleteBalance( parent, -1 );
								}
								else
								{
									parent.Right = null;

									DeleteBalance( parent, 1 );
								}
							}
						}
						else
						{
							Replace( node, right );

							DeleteBalance( node, 0 );
						}
					}
					else if ( right == null )
					{
						Replace( node, left );

						DeleteBalance( node, 0 );
					}
					else
					{
						AvlNode successor = right;

						if ( successor.Left == null )
						{
							AvlNode parent = node.Parent;

							successor.Parent = parent;
							successor.Left = left;
							successor.Balance = node.Balance;

							if ( left != null )
							{
								left.Parent = successor;
							}

							if ( node == m_Root )
							{
								m_Root = successor;
							}
							else
							{
								if ( parent.Left == node )
								{
									parent.Left = successor;
								}
								else
								{
									parent.Right = successor;
								}
							}

							DeleteBalance( successor, 1 );
						}
						else
						{
							while ( successor.Left != null )
							{
								successor = successor.Left;
							}

							AvlNode parent = node.Parent;
							AvlNode successorParent = successor.Parent;
							AvlNode successorRight = successor.Right;

							if ( successorParent.Left == successor )
							{
								successorParent.Left = successorRight;
							}
							else
							{
								successorParent.Right = successorRight;
							}

							if ( successorRight != null )
							{
								successorRight.Parent = successorParent;
							}

							successor.Parent = parent;
							successor.Left = left;
							successor.Balance = node.Balance;
							successor.Right = right;
							right.Parent = successor;

							if ( left != null )
							{
								left.Parent = successor;
							}

							if ( node == m_Root )
							{
								m_Root = successor;
							}
							else
							{
								if ( parent.Left == node )
								{
									parent.Left = successor;
								}
								else
								{
									parent.Right = successor;
								}
							}

							DeleteBalance( successorParent, -1 );
						}
					}

					return outputValue;
				}

				if ( compare <= 0 )
				{
					node = node.Left;
				}
				else
				{
					node = node.Right;
				}
			}

			return default;
		}

		public TValue Search( TKey _key )
		{
			AvlNode node = m_Root;

			while ( node != null )
			{
				Int32 compare = _key.CompareTo( node.Key );

				if ( compare < 0 )
				{
					node = node.Left;
				}
				else if ( compare > 0 )
				{
					node = node.Right;
				}
				else
				{
					return node.Value;
				}
			}

			return default;
		}

		public TValue Search( TKey _key, TValue _value )
		{
			AvlNode node = m_Root;

			while ( node != null )
			{
				Int32 compare = _key.CompareTo( node.Key );

				if ( compare == 0 && _value.CompareTo( node.Value ) == 0 )
				{
					return node.Value;
				}

				if ( compare <= 0 )
				{
					node = node.Left;
				}
				else if ( compare > 0 )
				{
					node = node.Right;
				}
			}

			return default;
		}

		public void Clear()
		{
			m_Root = null;
		}

		public IEnumerator<TValue> GetEnumerator()
		{
			return new AvlNodeEnumerator( m_Root );
		}

		private static AvlNode Replace( AvlNode _target, AvlNode _source )
		{
			AvlNode left = _source.Left;
			AvlNode right = _source.Right;

			_target.Balance = _source.Balance;
			_target.Key = _source.Key;
			_target.Value = _source.Value;
			_target.Left = left;
			_target.Right = right;

			if ( left != null )
			{
				left.Parent = _target;
			}

			if ( right != null )
			{
				right.Parent = _target;
			}

			return _target;
		}

		private void DeleteBalance( AvlNode _node, Int32 _balance )
		{
			while ( _node != null )
			{
				_balance = _node.Balance += _balance;

				if ( _balance == 2 )
				{
					if ( _node.Left.Balance >= 0 )
					{
						_node = RotateRight( _node );

						if ( _node.Balance == -1 )
						{
							return;
						}
					}
					else
					{
						_node = RotateLeftRight( _node );
					}
				}
				else if ( _balance == -2 )
				{
					if ( _node.Right.Balance <= 0 )
					{
						_node = RotateLeft( _node );

						if ( _node.Balance == 1 )
						{
							return;
						}
					}
					else
					{
						_node = RotateRightLeft( _node );
					}
				}
				else if ( _balance != 0 )
				{
					return;
				}

				AvlNode parent = _node.Parent;

				if ( parent != null )
				{
					_balance = parent.Left == _node ? -1 : 1;
				}

				_node = parent;
			}
		}

		private void InsertBalance( AvlNode _node, Int32 _balance )
		{
			while ( _node != null )
			{
				_balance = _node.Balance += _balance;

				if ( _balance == 0 )
				{
					return;
				}

				if ( _balance == 2 )
				{
					if ( _node.Left.Balance == 1 )
					{
						RotateRight( _node );
					}
					else
					{
						RotateLeftRight( _node );
					}

					return;
				}

				if ( _balance == -2 )
				{
					if ( _node.Right.Balance == -1 )
					{
						RotateLeft( _node );
					}
					else
					{
						RotateRightLeft( _node );
					}

					return;
				}

				AvlNode parent = _node.Parent;

				if ( parent != null )
				{
					_balance = parent.Left == _node ? 1 : -1;
				}

				_node = parent;
			}
		}

		private AvlNode RotateLeft( AvlNode _node )
		{
			AvlNode right = _node.Right;
			AvlNode rightLeft = right.Left;
			AvlNode parent = _node.Parent;

			right.Parent = parent;
			right.Left = _node;
			_node.Right = rightLeft;
			_node.Parent = right;

			if ( rightLeft != null )
			{
				rightLeft.Parent = _node;
			}

			if ( _node == m_Root )
			{
				m_Root = right;
			}
			else if ( parent.Right == _node )
			{
				parent.Right = right;
			}
			else
			{
				parent.Left = right;
			}

			right.Balance++;
			_node.Balance = -right.Balance;

			return right;
		}

		private AvlNode RotateRight( AvlNode _node )
		{
			AvlNode left = _node.Left;
			AvlNode leftRight = left.Right;
			AvlNode parent = _node.Parent;

			left.Parent = parent;
			left.Right = _node;
			_node.Left = leftRight;
			_node.Parent = left;

			if ( leftRight != null )
			{
				leftRight.Parent = _node;
			}

			if ( _node == m_Root )
			{
				m_Root = left;
			}
			else if ( parent.Left == _node )
			{
				parent.Left = left;
			}
			else
			{
				parent.Right = left;
			}

			left.Balance--;
			_node.Balance = -left.Balance;

			return left;
		}

		private AvlNode RotateLeftRight( AvlNode _node )
		{
			AvlNode left = _node.Left;
			AvlNode leftRight = left.Right;
			AvlNode parent = _node.Parent;
			AvlNode leftRightRight = leftRight.Right;
			AvlNode leftRightLeft = leftRight.Left;

			leftRight.Parent = parent;
			_node.Left = leftRightRight;
			left.Right = leftRightLeft;
			leftRight.Left = left;
			leftRight.Right = _node;
			left.Parent = leftRight;
			_node.Parent = leftRight;

			if ( leftRightRight != null )
			{
				leftRightRight.Parent = _node;
			}

			if ( leftRightLeft != null )
			{
				leftRightLeft.Parent = left;
			}

			if ( _node == m_Root )
			{
				m_Root = leftRight;
			}
			else if ( parent.Left == _node )
			{
				parent.Left = leftRight;
			}
			else
			{
				parent.Right = leftRight;
			}

			if ( leftRight.Balance == -1 )
			{
				_node.Balance = 0;
				left.Balance = 1;
			}
			else if ( leftRight.Balance == 0 )
			{
				_node.Balance = 0;
				left.Balance = 0;
			}
			else
			{
				_node.Balance = -1;
				left.Balance = 0;
			}

			leftRight.Balance = 0;

			return leftRight;
		}

		private AvlNode RotateRightLeft( AvlNode _node )
		{
			AvlNode right = _node.Right;
			AvlNode rightLeft = right.Left;
			AvlNode parent = _node.Parent;
			AvlNode rightLeftLeft = rightLeft.Left;
			AvlNode rightLeftRight = rightLeft.Right;

			rightLeft.Parent = parent;
			_node.Right = rightLeftLeft;
			right.Left = rightLeftRight;
			rightLeft.Right = right;
			rightLeft.Left = _node;
			right.Parent = rightLeft;
			_node.Parent = rightLeft;

			if ( rightLeftLeft != null )
			{
				rightLeftLeft.Parent = _node;
			}

			if ( rightLeftRight != null )
			{
				rightLeftRight.Parent = right;
			}

			if ( _node == m_Root )
			{
				m_Root = rightLeft;
			}
			else if ( parent.Right == _node )
			{
				parent.Right = rightLeft;
			}
			else
			{
				parent.Left = rightLeft;
			}

			if ( rightLeft.Balance == 1 )
			{
				_node.Balance = 0;
				right.Balance = -1;
			}
			else if ( rightLeft.Balance == 0 )
			{
				_node.Balance = 0;
				right.Balance = 0;
			}
			else
			{
				_node.Balance = 1;
				right.Balance = 0;
			}

			rightLeft.Balance = 0;

			return rightLeft;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
		
		[Serializable]
		private sealed class AvlNode
		{
			public AvlNode Parent { get; set; }
			public AvlNode Left { get; set; }
			public AvlNode Right { get; set; }
			public TKey Key { get; set; }
			public TValue Value { get; set; }
			public Int32 Balance { get; set; }
		}
		
		[Serializable]
		private sealed class AvlNodeEnumerator : IEnumerator<TValue>
		{
			private readonly AvlNode m_Root;
			private Action m_Action;
			private AvlNode m_Current;
			private AvlNode m_Right;

			public TValue Current => m_Current.Value;

			public AvlNodeEnumerator( AvlNode _root )
			{
				m_Right = m_Root = _root;
				m_Action = m_Root == null ? Action.End : Action.Right;
			}

			public Boolean MoveNext()
			{
				switch ( m_Action )
				{
					case Action.Right:
						m_Current = m_Right;

						while ( m_Current.Left != null )
						{
							m_Current = m_Current.Left;
						}

						m_Right = m_Current.Right;
						m_Action = m_Right != null ? Action.Right : Action.Parent;

						return true;

					case Action.Parent:
						while ( m_Current.Parent != null )
						{
							AvlNode previous = m_Current;

							m_Current = m_Current.Parent;

							if ( m_Current.Left == previous )
							{
								m_Right = m_Current.Right;
								m_Action = m_Right != null ? Action.Right : Action.Parent;

								return true;
							}
						}

						m_Action = Action.End;

						return false;

					default:
						return false;
				}
			}

			public void Reset()
			{
				m_Right = m_Root;
				m_Action = m_Root == null ? Action.End : Action.Right;
			}

			public void Dispose()
			{
			}

			Object IEnumerator.Current => Current;

			private enum Action
			{
				Parent,
				Right,
				End
			}
		}
	}
}