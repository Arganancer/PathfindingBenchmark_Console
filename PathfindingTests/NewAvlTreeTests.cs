using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pathfinding.Algorithm.Nodes;
using Pathfinding.DataStructures;
using World.Geometry;

namespace PathfindingTests
{
	[TestClass]
	public class AvlTreeTests
	{
		public Int32 X;
		public Int32 Y;
		public Int32 Z;

		public Int32 XMin;
		public Int32 XMax;
		public Int32 YMin;
		public Int32 YMax;
		public Int32 ZMin;
		public Int32 ZMax;

		public void SetPosMinMaxValues( Int32 _xMin, Int32 _xMax, Int32 _yMin, Int32 _yMax, Int32 _zMin, Int32 _zMax )
		{
			XMin = _xMin;
			XMax = _xMax;
			X = _xMin;
			YMin = _yMin;
			YMax = _yMax;
			Y = _yMin;
			ZMin = _zMin;
			ZMax = _zMax;
			Z = _zMin;
		}

		public Vector3i GetNewPosition()
		{
			if ( Z++ > ZMax )
			{
				Z = ZMin;
				if ( Y++ > YMax )
				{
					Y = YMin;
					if ( X++ > XMax )
					{
						X = XMin;
					}
				}
			}

			return new Vector3i( X, Y, Z );
		}

		public void ResetPosition()
		{
			X = XMin;
			Y = YMin;
			Z = ZMin;
		}

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
						avlTree.Insert( pos, new PathNode( pos, ++count, 0, 0 ) );
					}
				}
			}

			// Tests
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
						avlTree.Insert( pos, new PathNode( pos, ++count, 0, 0 ) );
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

			// Tests

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
		public void TestAVLTreePop()
		{
			// Setup
			AvlTree<Single, PathNode> avlTree = new AvlTree<Single, PathNode>();

			List<PathNode> pathNodes = new List<PathNode>();
			Int32 count = 0;

			for ( Int32 x = 0; x < 10; x++ )
			{
				for ( Int32 y = 0; y < 10; y++ )
				{
					for ( Int32 z = 0; z < 10; z++ )
					{
						Vector3i pos = new Vector3i( x, y, z );
						PathNode pathNode = new PathNode( pos, ++count, 0, 0 );
						pathNodes.Add( pathNode );
						avlTree.Insert( pathNode.F, pathNode );
					}
				}
			}

			// Tests
			Int32 passedChecks = 0;
			foreach ( PathNode pathNode in pathNodes )
			{
				PathNode removedPathNode = avlTree.Pop();
				Assert.IsNotNull( removedPathNode, "Failed after " + passedChecks++ + " removals." );
				Assert.IsNotNull( removedPathNode.Position );
				Assert.AreEqual( 0, pathNode.Position.CompareTo( removedPathNode.Position ) );
				Assert.AreEqual( pathNode.F, removedPathNode.F );
			}
		}

		[TestMethod]
		public void TestAvlTreeRandomRemoval()
		{
			// Setup
			AvlTree<Single, PathNode> avlTree = new AvlTree<Single, PathNode>();

			List<PathNode> pathNodes = new List<PathNode>();
			Int32 fValue = 0;

			for ( Int32 x = 0; x < 30; x++ )
			{
				for ( Int32 y = 0; y < 10; y++ )
				{
					for ( Int32 z = 0; z < 20; z++ )
					{
						Vector3i pos = new Vector3i( x, y, z );
						PathNode pathNode = new PathNode( pos, ++fValue, 0, 0 );
						pathNodes.Add( pathNode );
						avlTree.Insert( pathNode.F, pathNode );
					}
				}
			}

			Random rand = new Random();

			List<PathNode> pathNodesRemoved = new List<PathNode>();
			Int32 nbOfPathNodes = pathNodes.Count;

			ResetPosition();
			SetPosMinMaxValues( 101, 200, 101, 200, 101, 200 );

			// Tests
			for ( Int32 i = 0; i < nbOfPathNodes / 2; i++ )
			{
				PathNode removedNode = pathNodes[rand.Next( 0, pathNodes.Count )];
				pathNodesRemoved.Add( removedNode );
				pathNodes.Remove( removedNode );
				avlTree.Delete( removedNode.F );

				PathNode newNode = new PathNode( GetNewPosition(), ++fValue, 0, 0 );
				avlTree.Insert( newNode.F, newNode );
				pathNodes.Add( newNode );
			}

			Int32 passedChecks = 0;
			foreach ( PathNode pathNode in pathNodes )
			{
				PathNode removedPathNode = avlTree.Search( pathNode.F );
				Assert.IsNotNull( removedPathNode, "Failed after {0} checks. Pathnode was Null. Pos was: x:{1} y:{2} z:{3}, f value was: {4}",
					passedChecks++,
					pathNode.Position.X,
					pathNode.Position.Y,
					pathNode.Position.Z,
					pathNode.F );
				Assert.IsNotNull( removedPathNode.Position, "Position was null." );
				Assert.AreEqual( 0, pathNode.Position.CompareTo( removedPathNode.Position ) );
			}

			passedChecks = 0;
			foreach ( PathNode pathNode in pathNodesRemoved )
			{
				PathNode removedPathNode = avlTree.Search( pathNode.F );
				Assert.IsNull( removedPathNode, "Failed after {0} checks. Pathnode was not Null. Pos was: x:{1} y:{2} z:{3}, f value was: {4}",
					passedChecks++,
					pathNode.Position.X,
					pathNode.Position.Y,
					pathNode.Position.Z,
					pathNode.F );
			}
		}

		[TestMethod]
		public void TestAvlTreeRandomRemovalAndPopping()
		{
			// Setup
			AvlTree<Single, PathNode> avlTree = new AvlTree<Single, PathNode>();

			List<PathNode> pathNodes = new List<PathNode>();
			Int32 fValue = 0;

			for ( Int32 x = 0; x < 10; x++ )
			{
				for ( Int32 y = 0; y < 10; y++ )
				{
					for ( Int32 z = 0; z < 10; z++ )
					{
						Vector3i pos = new Vector3i( x, y, z );
						PathNode pathNode = new PathNode( pos, ++fValue, 0, 0 );
						pathNodes.Add( pathNode );
						avlTree.Insert( pathNode.F, pathNode );
					}
				}
			}

			Random rand = new Random();

			List<PathNode> pathNodesRemoved = new List<PathNode>();
			Int32 nbOfPathNodes = pathNodes.Count;

			ResetPosition();
			SetPosMinMaxValues( 101, 200, 101, 200, 101, 200 );

			// Tests
			for ( Int32 i = 0; i < nbOfPathNodes / 2; i++ )
			{
				PathNode removedNode = pathNodes[rand.Next( 0, pathNodes.Count )];
				pathNodesRemoved.Add( removedNode );
				pathNodes.Remove( removedNode );
				avlTree.Delete( removedNode.F );

				PathNode newNode = new PathNode( GetNewPosition(), ++fValue, 0, 0 );
				avlTree.Insert( newNode.F, newNode );
				pathNodes.Add( newNode );

				PathNode poppedNode = avlTree.Pop();
				Assert.IsNotNull( poppedNode, "Popped Node was null" );
				pathNodesRemoved.Add( poppedNode );
				pathNodes.Remove( poppedNode );
			}

			Int32 passedChecks = 0;
			foreach ( PathNode pathNode in pathNodes )
			{
				PathNode removedPathNode = avlTree.Search( pathNode.F );
				Assert.IsNotNull( removedPathNode, "Failed after {0} checks. Pathnode was Null. Pos was: x:{1} y:{2} z:{3}, f value was: {4}",
					passedChecks++,
					pathNode.Position.X,
					pathNode.Position.Y,
					pathNode.Position.Z,
					pathNode.F );
				Assert.IsNotNull( removedPathNode.Position, "Position was null." );
				Assert.AreEqual( 0, pathNode.Position.CompareTo( removedPathNode.Position ) );
			}

			passedChecks = 0;
			foreach ( PathNode pathNode in pathNodesRemoved )
			{
				PathNode removedPathNode = avlTree.Search( pathNode.F );
				Assert.IsNull( removedPathNode, "Failed after {0} checks. Pathnode was not Null. Pos was: x:{1} y:{2} z:{3}, f value was: {4}",
					passedChecks++,
					pathNode.Position.X,
					pathNode.Position.Y,
					pathNode.Position.Z,
					pathNode.F );
			}
		}

		[TestMethod]
		public void TestAvlTreeRandomRemovalAndPopping2()
		{
			Int32 removalMinOccurrence = 3;
			Int32 removalMaxOccurence = 6;
			Int32 popMinOccurence = 3;
			Int32 popMaxOccurence = 5;
			Int32 maxValue = 20;

			ResetPosition();
			SetPosMinMaxValues( 0, maxValue + 1, 0, maxValue + 1, 0, maxValue + 1 );

			Random rand = new Random();
			Int32 fValue = 0;

			// Setup
			AvlTree<Single, PathNode> avlTree = new AvlTree<Single, PathNode>();

			List<PathNode> originalPathNodePool = new List<PathNode>();
			List<PathNode> insertedPathNodes = new List<PathNode>();
			List<PathNode> pathNodesRemoved = new List<PathNode>();

			// Fill Original Pool
			for ( Int32 i = 0; i < maxValue * maxValue * maxValue; i++ )
			{
				PathNode node = new PathNode( GetNewPosition(), ++fValue, 0, 0 );
				originalPathNodePool.Add( node );
			}

			// Operations
			Int32 nbOfPathNodes = originalPathNodePool.Count;

			Int32 nextRemoval = rand.Next( removalMinOccurrence, removalMaxOccurence );
			Int32 nextPop = rand.Next( popMinOccurence, popMaxOccurence );

			for ( Int32 i = 0; i < nbOfPathNodes; i++ )
			{
				// Add a new node.
				PathNode newNode = originalPathNodePool[rand.Next( 0, originalPathNodePool.Count )];
				avlTree.Insert( newNode.F, newNode );
				insertedPathNodes.Add( newNode );
				originalPathNodePool.Remove( newNode );

				if ( --nextRemoval <= 0 )
				{
					nextRemoval = rand.Next( removalMinOccurrence, removalMaxOccurence );

					PathNode nodeToRemove = insertedPathNodes[rand.Next( 0, insertedPathNodes.Count )];
					PathNode removedNode = avlTree.Delete( nodeToRemove.F );
					Assert.IsNotNull( removedNode, "Removed node was null" );
					pathNodesRemoved.Add( nodeToRemove );
					insertedPathNodes.Remove( nodeToRemove );
					Assert.AreEqual( 0, nodeToRemove.Position.CompareTo( removedNode.Position ), "Removed node's position was not the same as the asked node." );
					Assert.AreEqual( nodeToRemove.F, removedNode.F, "Removed node's Fvalue was not the same as the asked node." );
				}

				if ( --nextPop <= 0 )
				{
					nextPop = rand.Next( popMinOccurence, popMaxOccurence );

					PathNode poppedNode = avlTree.Pop();
					Assert.IsNotNull( poppedNode, "Popped Node was null" );
					pathNodesRemoved.Add( poppedNode );
					insertedPathNodes.Remove( poppedNode );
				}
			}

			// Tests
			Int32 passedChecks = 0;
			foreach ( PathNode pathNode in insertedPathNodes )
			{
				PathNode removedPathNode = avlTree.Search( pathNode.F );
				Assert.IsNotNull( removedPathNode, "Failed after {0} checks. Pathnode was Null. Pos was: x:{1} y:{2} z:{3}, f value was: {4}",
					passedChecks++,
					pathNode.Position.X,
					pathNode.Position.Y,
					pathNode.Position.Z,
					pathNode.F );
				Assert.IsNotNull( removedPathNode.Position, "Position was null." );
				Assert.AreEqual( 0, pathNode.Position.CompareTo( removedPathNode.Position ) );
			}

			passedChecks = 0;
			foreach ( PathNode pathNode in pathNodesRemoved )
			{
				PathNode removedPathNode = avlTree.Search( pathNode.F );
				Assert.IsNull( removedPathNode, "Failed after {0} checks. Pathnode was not Null. Pos was: x:{1} y:{2} z:{3}, f value was: {4}",
					passedChecks++,
					pathNode.Position.X,
					pathNode.Position.Y,
					pathNode.Position.Z,
					pathNode.F );
			}
		}
	}
}