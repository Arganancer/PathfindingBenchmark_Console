using System;

namespace Pathfinding.DataStructures
{
	public class AVLTreeV2<TKey, TValue> where TKey : IComparable<TKey> where TValue : IComparable<TValue>
	{
		private AVLNode<TKey, TValue> m_Root;
		private AVLNode<TKey, TValue> m_LowestValueNode;

		public void Insert( TKey _key, TValue _value, Func<TKey, TValue, TKey> _onKeyExists )
		{
			if ( m_Root is null )
			{
				m_Root = new AVLNode<TKey, TValue> { Key = _key, Value = _value };
				m_LowestValueNode = m_Root;
				return;
			}

			AVLNode<TKey, TValue> currentNode = m_Root;

			while ( currentNode != null )
			{
				switch ( currentNode.Key.CompareTo( _key ) )
				{
					case -1:
					{
						if ( !currentNode.HasLeftChild )
						{
							AVLNode<TKey, TValue> newNode = new AVLNode<TKey, TValue> { Key = _key, Value = _value, Parent = currentNode };
							currentNode.Left = newNode;
							InsertBalance( newNode );
							if ( _key.CompareTo( m_LowestValueNode.Key ) == -1 )
							{
								m_LowestValueNode = newNode;
							}

							return;
						}

						currentNode = currentNode.Left;
						break;
					}
					case 1:
					{
						if ( !currentNode.HasRightChild )
						{
							currentNode.Right = new AVLNode<TKey, TValue> { Key = _key, Value = _value, Parent = currentNode };
							InsertBalance( currentNode.Right );
							return;
						}

						currentNode = currentNode.Right;
						break;
					}
					default:
					{
						_key = _onKeyExists( _key, _value );
						break;
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

			AVLNode<TKey, TValue> currentNode = m_Root;

			while ( currentNode.Left != null )
			{
				currentNode = currentNode.Left;
			}

			return Delete( currentNode.Key );
		}

		public TValue Delete( TKey _key )
		{
			AVLNode<TKey, TValue> currentNode = m_Root;

			while ( currentNode != null )
			{
				switch ( _key.CompareTo( currentNode.Key ) )
				{
					case 1:
					{
						currentNode = currentNode.Left;
						break;
					}
					case -1:
					{
						currentNode = currentNode.Right;
						break;
					}
					default:
					{
						Delete( currentNode );
						return currentNode.Value;
					}
				}
			}

			return default;
		}

		public TValue Search( TKey _key )
		{
			var node = m_Root;

			while ( node != null )
			{
				switch ( _key.CompareTo( node.Key ) )
				{
					case 1:
					{
						node = node.Left;
						break;
					}
					case -1:
					{
						node = node.Right;
						break;
					}
					default:
					{
						return node.Value;
					}
				}
			}

			return default;
		}

		public void Update( TKey _key, TKey _newKey, Action<TValue> _updateValue = null )
		{

		}

		public void Clear()
		{
			m_Root = null;
		}

		public Boolean Any() => m_Root != null;

		private void Replace( AVLNode<TKey, TValue> _originalNode, AVLNode<TKey, TValue> _newNode )
		{
			_newNode.Parent = _originalNode.Parent;
		}

		private void Delete( AVLNode<TKey, TValue> _node )
		{
			if ( !_node.HasLeftChild )
			{
				if ( !_node.HasRightChild )
				{
					if ( _node.IsRoot )
					{
						m_Root = null;
					}
					else
					{
						AVLNode<TKey, TValue> parent = _node.Parent;
						if ( _node.IsLeftChild )
						{
							parent.Left = null;
							parent.Balance -= 1;
						}
						else
						{							
							parent.Right = null;
							parent.Balance += 1;
						}

						DeleteBalance( _node.Parent );
					}
				}
				else
				{
					AVLNode<TKey, TValue> right = _node.Right;
					Replace( _node, right );
					DeleteBalance( right );
				}
			}
			else if ( !_node.HasRightChild )
			{
				AVLNode<TKey, TValue> left = _node.Left;
				Replace( _node, left );
				DeleteBalance( left );
			}
			else
			{
				AVLNode<TKey, TValue> successor = _node.Right;

				if ( !successor.HasLeftChild )
				{
					successor.Parent = _node.Parent;
					successor.Left = _node.Left;
					successor.Balance = _node.Balance;

					if ( _node.HasLeftChild )
					{
						_node.Left.Parent = successor;
					}
				}
				else
				{
					while ( successor.HasLeftChild )
					{
						successor = successor.Left;
					}

					if ( successor.IsLeftChild )
					{
						successor.Parent.Left = successor.Right;
					}
					else
					{
						successor.Parent.Right = successor.Right;
					}

					if ( successor.HasRightChild )
					{
						successor.Right.Parent = successor.Parent;
					}

					successor.Parent = _node.Parent;
					successor.Left = _node.Left;
					successor.Balance = _node.Balance;
					successor.Right = _node.Right;
					successor.Right.Parent = successor;

					if ( _node.HasLeftChild )
					{
						_node.Left.Parent = successor;
					}
				}

				if ( _node.IsRoot )
				{
					m_Root = successor;
				}
				else
				{
					if ( _node.IsLeftChild )
					{
						_node.Parent.Left = successor;
					}
					else
					{
						_node.Parent.Right = successor;
					}
				}

				DeleteBalance( successor );
			}
		}

		private void DeleteBalance( AVLNode<TKey, TValue> _node )
		{
			while ( true )
			{
				if ( _node.Balance > 1 || _node.Balance < -1 )
				{
					Rebalance( _node );
					return;
				}

				if ( _node.HasParent )
				{
					if ( _node.IsLeftChild )
					{
						_node.Parent.Balance -= 1;
					}
					else
					{
						_node.Parent.Balance += 1;
					}

					if ( _node.Parent.Balance != 0 )
					{
						_node = _node.Parent;
						continue;
					}
				}

				break;
			}
		}

		private void InsertBalance( AVLNode<TKey, TValue> _node )
		{
			while ( true )
			{
				if ( _node.Balance > 1 || _node.Balance < -1 )
				{
					Rebalance( _node );
					return;
				}

				if ( _node.HasParent )
				{
					if ( _node.IsLeftChild )
					{
						_node.Parent.Balance += 1;
					}
					else
					{
						_node.Parent.Balance -= 1;
					}

					if ( _node.Parent.Balance != 0 )
					{
						_node = _node.Parent;
						continue;
					}
				}

				break;
			}
		}

		private void Rebalance( AVLNode<TKey, TValue> _node )
		{
			if ( _node.Balance < 0 )
			{
				if ( _node.Right.Balance > 0 )
				{
					RotateRight( _node.Right );
					RotateLeft( _node );
				}
				else
				{
					RotateLeft( _node );
				}
			}
			else if ( _node.Balance > 0 )
			{
				if ( _node.Left.Balance < 0 )
				{
					RotateLeft( _node.Left );
					RotateRight( _node );
				}
				else
				{
					RotateRight( _node );
				}
			}
		}

		private void RotateLeft( AVLNode<TKey, TValue> _node )
		{
			AVLNode<TKey, TValue> newRoot = _node.Right;
			_node.Right = newRoot.Left;
			if ( newRoot.HasLeftChild )
			{
				newRoot.Left.Parent = _node;
			}

			newRoot.Parent = _node.Parent;
			if ( _node.IsRoot )
			{
				m_Root = newRoot;
			}
			else
			{
				if ( _node.IsLeftChild )
				{
					_node.Parent.Left = newRoot;
				}
				else
				{
					_node.Parent.Right = newRoot;
				}
			}

			newRoot.Left = _node;
			_node.Parent = newRoot;
			_node.Balance += 1 - Math.Min( newRoot.Balance, 0 );
			newRoot.Balance += 1 + Math.Max( _node.Balance, 0 );
		}

		private void RotateRight( AVLNode<TKey, TValue> _node )
		{
			AVLNode<TKey, TValue> newRoot = _node.Left;
			_node.Left = newRoot.Right;
			if ( newRoot.HasRightChild )
			{
				newRoot.Right.Parent = _node;
			}

			newRoot.Parent = _node.Parent;
			if ( _node.IsRoot )
			{
				m_Root = newRoot;
			}
			else
			{
				if ( _node.IsLeftChild )
				{
					_node.Parent.Left = newRoot;
				}
				else
				{
					_node.Parent.Right = newRoot;
				}
			}

			newRoot.Right = _node;
			_node.Parent = newRoot;
			_node.Balance -= 1 - Math.Min( newRoot.Balance, 0 );
			newRoot.Balance -= 1 + Math.Max( _node.Balance, 0 );
		}
	}
}