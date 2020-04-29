using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pathfinding.Algorithm.Nodes;
using Pathfinding.DataStructures;
using Pathfinding.Sets.OpenSet;
using World.Geometry;

namespace PathfindingTests
{
	[TestClass]
	public class FinalAVLTreeTests
	{
		[TestMethod]
		public void TestAVLTreeAdd()
		{
			// Setup
			AvlTree<Vector3i, PathNode> avlTree = new AvlTree<Vector3i, PathNode>();

			List<Vector3i> positions = new List<Vector3i>();
			Int32 count = 0;

			for ( Int32 x = 0; x < 10; x++ )
			{
				for ( Int32 y = 0; y < 10; y++ )
				{
					for ( Int32 z = 0; z < 10; z++ )
					{
						Vector3i pos = new Vector3i( x, y, z );
						positions.Add( pos );
						avlTree.Insert( pos, new PathNode( pos, ++count ) );
					}
				}
			}

			count = 0;
			foreach ( Vector3i pos in positions )
			{
				PathNode node = avlTree.Search( pos );
				Assert.IsNotNull( node, "Number of correct checks: " + count );
				if ( node != null )
				{
					Assert.AreEqual( 0, pos.CompareTo( node.Position ) );
					Assert.AreEqual( ++count, node.F );
				}
			}
		}

		[TestMethod]
		public void TestAVLTreeRemove()
		{
			// Setup
			AvlTree<Vector3i, PathNode> avlTree = new AvlTree<Vector3i, PathNode>();

			List<Vector3i> positions = new List<Vector3i>();
			Int32 count = 0;

			for ( Int32 x = 0; x < 10; x++ )
			{
				for ( Int32 y = 0; y < 10; y++ )
				{
					for ( Int32 z = 0; z < 10; z++ )
					{
						Vector3i pos = new Vector3i( x, y, z );
						positions.Add( pos );
						avlTree.Search( pos, new PathNode( pos, ++count ) );
					}
				}
			}

			List<Vector3i> removedPositions = positions.GetRange( 0, count / 2 );
			positions.RemoveRange( 0, removedPositions.Count );

			Int32 passedChecks = 0;
			foreach ( Vector3i position in removedPositions )
			{
				PathNode removedPathNode = avlTree.Delete( position );
				Assert.IsNotNull( removedPathNode, "Failed after " + passedChecks++ + " removals." );
				Assert.IsNotNull( removedPathNode.Position );
				Assert.AreEqual( 0, position.CompareTo( removedPathNode.Position ) );
			}

			foreach ( Vector3i pos in positions )
			{
				PathNode node = avlTree.Search( pos );
				Assert.IsNotNull( node );
				if ( node != null )
				{
					Assert.AreEqual( 0, pos.CompareTo( node.Position ) );
				}
			}

			foreach ( Vector3i pos in removedPositions )
			{
				PathNode node = avlTree.Search( pos );
				Assert.IsNull( node );
			}
		}

		[TestMethod]
		public void TestOpenListAdd()
		{
			// Setup
			OpenList openList = new OpenList();

			List<PathNode> pathNodes = new List<PathNode>();
			Int32 fValue = 0;

			for ( Int32 i = 0; i < 5; i++ )
			{
				for ( Int32 j = 0; j < 5; j++ )
				{
					PathNode newPathNode = new PathNode( new Vector3i( i, 1, j ), fValue++ );
					pathNodes.Add( newPathNode );
					openList.Add( newPathNode );
				}
			}

			// Asserts
			foreach ( PathNode pathNode in pathNodes )
			{
				Assert.AreEqual( true, openList.Contains( pathNode.Position ) );
			}
		}

		[TestMethod]
		public void TestOpenListPop()
		{
			// Setup
			OpenList openList = new OpenList();

			List<PathNode> pathNodes = new List<PathNode>();
			List<PathNode> poppedPathNodes = new List<PathNode>();
			List<PathNode> removedPathNodes = new List<PathNode>();

			Int32 fValue = 0;

			for ( Int32 i = 0; i < 30; i++ )
			{
				for ( Int32 j = 0; j < 30; j++ )
				{
					PathNode newPathNode = new PathNode( new Vector3i( i, 1, j ), fValue++ );
					pathNodes.Add( newPathNode );
					openList.Add( newPathNode );
				}
			}

			for ( Int32 i = 0; i < 5; i++ )
			{
				removedPathNodes.Add( pathNodes.First() );
				pathNodes.Remove( removedPathNodes[i] );
				poppedPathNodes.Add( openList.Pop() );
			}

			// Asserts
			foreach ( PathNode pathNode in pathNodes )
			{
				Assert.AreEqual( true, openList.Contains( pathNode.Position ) );
			}

			for ( Int32 i = 0; i < removedPathNodes.Count; i++ )
			{
				Assert.AreEqual( true, removedPathNodes[i].Position == poppedPathNodes[i].Position );
			}
		}
	}
}