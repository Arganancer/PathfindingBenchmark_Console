using System;

namespace Pathfinding.DataStructures
{
	[Serializable]
	public class AvlTreeV3<TKey, TValue> where TKey : IComparable<TKey> where TValue : IComparable<TValue>
	{
		protected AVLNode<TKey, TValue> m_Root;
		protected AVLNode<TKey, TValue> m_LowestValueNode;

		public virtual AVLNode<TKey, TValue> Insert( TKey _key, TValue _value, Func<TKey, TValue, TKey> _onKeyExists )
		{
			if ( m_Root == null )
			{
				m_Root = new AVLNode<TKey, TValue> { Key = _key, Value = _value };
				m_LowestValueNode = m_Root;
				return m_Root;
			}
			else
			{
				AVLNode<TKey, TValue> node = m_Root;

				while ( node != null )
				{
					if ( _key.CompareTo( node.Key ) < 0 )
					{
						if ( !node.HasLeftChild )
						{
							AVLNode<TKey, TValue> newNode = new AVLNode<TKey, TValue> { Key = _key, Value = _value, Parent = node };
							node.Left = newNode;
							if ( _key.CompareTo( m_LowestValueNode.Key ) < 0 )
							{
								m_LowestValueNode = newNode;
							}

							InsertBalance( node, 1 );

							return newNode;
						}

						node = node.Left;
					}
					else if ( _key.CompareTo( node.Key ) > 0 )
					{
						AVLNode<TKey, TValue> right = node.Right;

						if ( right == null )
						{
							AVLNode<TKey, TValue> newNode = new AVLNode<TKey, TValue> { Key = _key, Value = _value, Parent = node };
							node.Right = newNode;
							InsertBalance( node, -1 );

							return newNode;
						}

						node = right;
					}
					else
					{
						_key = _onKeyExists( _key, _value );
					}
				}
			}

			return default;
		}

		public virtual TValue Pop()
		{
			if ( m_Root == null )
			{
				return default;
			}

			AVLNode<TKey, TValue> lowestValueNode = m_LowestValueNode;
			AssignNextLowestValueNode();
			return Delete( lowestValueNode );
		}

		public virtual TValue Delete( TKey _key )
		{
			AVLNode<TKey, TValue> node = m_Root;

			while ( node != null )
			{
				if ( _key.CompareTo( node.Key ) < 0 )
				{
					node = node.Left;
				}
				else if ( _key.CompareTo( node.Key ) > 0 )
				{
					node = node.Right;
				}
				else
				{
					if ( m_LowestValueNode == node )
					{
						AssignNextLowestValueNode();
					}

					TValue deletedNode = Delete( node );

					return deletedNode;
				}
			}

			return default;
		}

		public virtual TValue Search( TKey _key )
		{
			AVLNode<TKey, TValue> node = m_Root;

			while ( node != null )
			{
				if ( _key.CompareTo( node.Key ) < 0 )
				{
					node = node.Left;
				}
				else if ( _key.CompareTo( node.Key ) > 0 )
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

		public virtual void Clear()
		{
			m_Root = null;
			m_LowestValueNode = null;
		}

		protected void AssignNextLowestValueNode()
		{
			if ( m_LowestValueNode.Right != null )
			{
				AVLNode<TKey, TValue> nextLowestNode = m_LowestValueNode.Right;
				while ( nextLowestNode?.Left != null )
				{
					nextLowestNode = nextLowestNode.Left;
				}

				m_LowestValueNode = nextLowestNode;
			}
			else
			{
				m_LowestValueNode = m_LowestValueNode.Parent;
			}
		}

		protected TValue Delete( AVLNode<TKey, TValue> _node )
		{
			AVLNode<TKey, TValue> left = _node.Left;
			AVLNode<TKey, TValue> right = _node.Right;
			TValue outputValue = _node.Value;

			if ( left == null )
			{
				if ( right == null )
				{
					if ( _node == m_Root )
					{
						m_Root = null;
					}
					else
					{
						AVLNode<TKey, TValue> parent = _node.Parent;

						if ( parent.Left == _node )
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
					Replace( _node, right );

					DeleteBalance( right, 0 );
				}
			}
			else if ( right == null )
			{
				Replace( _node, left );

				DeleteBalance( left, 0 );
			}
			else
			{
				AVLNode<TKey, TValue> successor = right;

				if ( successor.Left == null )
				{
					AVLNode<TKey, TValue> parent = _node.Parent;

					successor.Parent = parent;
					successor.Left = left;
					successor.Balance = _node.Balance;

					if ( left != null )
					{
						left.Parent = successor;
					}

					if ( _node == m_Root )
					{
						m_Root = successor;
					}
					else
					{
						if ( parent.Left == _node )
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

					AVLNode<TKey, TValue> parent = _node.Parent;
					AVLNode<TKey, TValue> successorParent = successor.Parent;
					AVLNode<TKey, TValue> successorRight = successor.Right;

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
					successor.Balance = _node.Balance;
					successor.Right = right;
					right.Parent = successor;

					if ( left != null )
					{
						left.Parent = successor;
					}

					if ( _node == m_Root )
					{
						m_Root = successor;
					}
					else
					{
						if ( parent.Left == _node )
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

		protected static void Replace( AVLNode<TKey, TValue> _target, AVLNode<TKey, TValue> _source )
		{
			if ( _target.IsLeftChild )
			{
				_target.Parent.Left = _source;
			}
			else
			{
				_target.Parent.Right = _source;
			}

			_source.Parent = _target.Parent;
		}

		protected void DeleteBalance( AVLNode<TKey, TValue> _node, Int32 _balance )
		{
			while ( _node != null )
			{
				_balance = _node.Balance += _balance;

				switch ( _balance )
				{
					case 2 when _node.Left.Balance >= 0:
					{
						_node = RotateRight( _node );

						if ( _node.Balance == -1 )
						{
							return;
						}

						break;
					}
					case 2:
						_node = RotateLeftRight( _node );
						break;
					case -2 when _node.Right.Balance <= 0:
					{
						_node = RotateLeft( _node );

						if ( _node.Balance == 1 )
						{
							return;
						}

						break;
					}
					case -2:
						_node = RotateRightLeft( _node );
						break;
					default:
					{
						if ( _balance != 0 )
						{
							return;
						}

						break;
					}
				}

				AVLNode<TKey, TValue> parent = _node.Parent;

				if ( parent != null )
				{
					_balance = parent.Left == _node ? -1 : 1;
				}

				_node = parent;
			}
		}

		protected void InsertBalance( AVLNode<TKey, TValue> _node, Int32 _balance )
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

				AVLNode<TKey, TValue> parent = _node.Parent;

				if ( parent != null )
				{
					_balance = parent.Left == _node ? 1 : -1;
				}

				_node = parent;
			}
		}

		protected AVLNode<TKey, TValue> RotateLeft( AVLNode<TKey, TValue> _node )
		{
			AVLNode<TKey, TValue> right = _node.Right;
			AVLNode<TKey, TValue> rightLeft = right.Left;
			AVLNode<TKey, TValue> parent = _node.Parent;

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

		protected AVLNode<TKey, TValue> RotateRight( AVLNode<TKey, TValue> _node )
		{
			AVLNode<TKey, TValue> left = _node.Left;
			AVLNode<TKey, TValue> leftRight = left.Right;
			AVLNode<TKey, TValue> parent = _node.Parent;

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

		protected AVLNode<TKey, TValue> RotateLeftRight( AVLNode<TKey, TValue> _node )
		{
			AVLNode<TKey, TValue> left = _node.Left;
			AVLNode<TKey, TValue> leftRight = left.Right;
			AVLNode<TKey, TValue> parent = _node.Parent;
			AVLNode<TKey, TValue> leftRightRight = leftRight.Right;
			AVLNode<TKey, TValue> leftRightLeft = leftRight.Left;

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

		protected AVLNode<TKey, TValue> RotateRightLeft( AVLNode<TKey, TValue> _node )
		{
			AVLNode<TKey, TValue> right = _node.Right;
			AVLNode<TKey, TValue> rightLeft = right.Left;
			AVLNode<TKey, TValue> parent = _node.Parent;
			AVLNode<TKey, TValue> rightLeftLeft = rightLeft.Left;
			AVLNode<TKey, TValue> rightLeftRight = rightLeft.Right;

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
	}
}