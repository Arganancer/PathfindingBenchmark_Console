using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PathDataStructuresManagedEnvironment.Code.CsharpImplementation;
using PathDataStructuresManagedEnvironment.Code.Pathfinding;
using PathDataStructuresManagedEnvironment.Code.Types.Geometry;
using Pathfinding.GraphData;

namespace AvlCsTests
{
	[TestClass]
	public class NewAvlTreeTests
	{
		public int x = 0;
		public int y = 0;
		public int z = 0;

		public int xMin = 0;
		public int xMax = 0;
		public int yMin = 0;
		public int yMax = 0;
		public int zMin = 0;
		public int zMax = 0;

		public void SetPosMinMaxValues(int xMin, int xMax, int yMin, int yMax, int zMin, int zMax)
		{
			this.xMin = xMin;
			this.xMax = xMax;
			x = xMin;
			this.yMin = yMin;
			this.yMax = yMax;
			y = yMin;
			this.zMin = zMin;
			this.zMax = zMax;
			z = zMin;
		}

		public Vector3i GetNewPosition()
		{
			if (z++ > zMax)
			{
				z = zMin;
				if (y++ > yMax)
				{
					y = yMin;
					if (x++ > xMax)
					{
						x = xMin;
					}
				}
			}
			return new Vector3i(x, y, z);
		}

		public void ResetPosition()
		{
			x = xMin;
			y = yMin;
			z = zMin;
		}

		[TestMethod]
		public void TestAVLTreeAdd()
		{
			// Setup
			NewAvlTree<Vector3i, PathNode> avlTree = new NewAvlTree<Vector3i, PathNode>(new VectorComparer());

			List<Vector3i> positions = new List<Vector3i>();
			int count = 0;

			for (int x = 0; x < 10; x++)
			{
				for (int y = 0; y < 10; y++)
				{
					for (int z = 0; z < 10; z++)
					{
						Vector3i pos = new Vector3i(x, y, z);
						positions.Add(pos);
						avlTree.Insert(pos, new PathNode(pos, ++count, 0, 0));
					}
				}
			}

			// Tests
			count = 0;
			foreach (var pos in positions)
			{
				PathNode node = avlTree.Find(pos);
				Assert.IsNotNull(node, "Number of correct checks: " + count);
				if (node != null)
				{
					Assert.AreEqual(0, pos.CompareTo(node.position));
					Assert.AreEqual(++count, node.f);
				}
			}
		}

		[TestMethod]
		public void TestAVLTreeRemove()
		{
			// Setup
			NewAvlTree<Vector3i, PathNode> avlTree = new NewAvlTree<Vector3i, PathNode>(new VectorComparer());

			List<Vector3i> positions = new List<Vector3i>();
			int count = 0;

			for (int x = 0; x < 10; x++)
			{
				for (int y = 0; y < 10; y++)
				{
					for (int z = 0; z < 10; z++)
					{
						Vector3i pos = new Vector3i(x, y, z);
						positions.Add(pos);
						avlTree.Insert(pos, new PathNode(pos, ++count, 0, 0));
					}
				}
			}

			List<Vector3i> removedPositions = positions.GetRange(0, count / 2);
			positions.RemoveRange(0, removedPositions.Count);

			int passedChecks = 0;
			foreach (var position in removedPositions)
			{
				PathNode removedPathNode = avlTree.Remove(position);
				Assert.IsNotNull(removedPathNode, "Failed after " + passedChecks++ + " removals.");
				Assert.IsNotNull(removedPathNode.position);
				Assert.AreEqual(0, position.CompareTo(removedPathNode.position));
			}

			// Tests
			
			foreach (var pos in positions)
			{
				PathNode node = avlTree.Find(pos);
				Assert.IsNotNull(node);
				if (node != null)
				{
					Assert.AreEqual(0, pos.CompareTo(node.position));
				}
			}

			foreach (var pos in removedPositions)
			{
				PathNode node = avlTree.Find(pos);
				Assert.IsNull(node);
			}
		}

		[TestMethod]
		public void TestAVLTreePop()
		{
			// Setup
			NewAvlTree<float, PathNode> avlTree = new NewAvlTree<float, PathNode>(new FloatComparer());

			List<PathNode> pathNodes = new List<PathNode>();
			int count = 0;

			for (int x = 0; x < 10; x++)
			{
				for (int y = 0; y < 10; y++)
				{
					for (int z = 0; z < 10; z++)
					{
						Vector3i pos = new Vector3i(x, y, z);
						PathNode pathNode = new PathNode(pos, ++count, 0, 0);
						pathNodes.Add(pathNode);
						avlTree.Insert(pathNode.f, pathNode);
					}
				}
			}

			// Tests
			int passedChecks = 0;
			foreach (var pathNode in pathNodes)
			{
				PathNode removedPathNode = avlTree.Pop();
				Assert.IsNotNull(removedPathNode, "Failed after " + passedChecks++ + " removals.");
				Assert.IsNotNull(removedPathNode.position);
				Assert.AreEqual(0, pathNode.position.CompareTo(removedPathNode.position));
				Assert.AreEqual(pathNode.f, removedPathNode.f);
			}
		}

		[TestMethod]
		public void TestAvlTreeRandomRemoval()
		{
			// Setup
			NewAvlTree<float, PathNode> avlTree = new NewAvlTree<float, PathNode>(new FloatComparer());

			List<PathNode> pathNodes = new List<PathNode>();
			int fValue = 0;

			for (int x = 0; x < 30; x++)
			{
				for (int y = 0; y < 10; y++)
				{
					for (int z = 0; z < 20; z++)
					{
						Vector3i pos = new Vector3i(x, y, z);
						PathNode pathNode = new PathNode(pos, ++fValue, 0, 0);
						pathNodes.Add(pathNode);
						avlTree.Insert(pathNode.f, pathNode);
					}
				}
			}

			Random rand = new Random();

			List<PathNode> pathNodesRemoved = new List<PathNode>();
			int nbOfPathNodes = pathNodes.Count;

			ResetPosition();
			SetPosMinMaxValues(101, 200, 101, 200, 101, 200);

			// Tests
			for (int i = 0; i < nbOfPathNodes / 2; i++)
			{
				PathNode removedNode = pathNodes[rand.Next(0, pathNodes.Count)];
				pathNodesRemoved.Add(removedNode);
				pathNodes.Remove(removedNode);
				avlTree.Remove(removedNode.f);

				PathNode newNode = new PathNode(GetNewPosition(), ++fValue, 0, 0);
				avlTree.Insert(newNode.f, newNode);
				pathNodes.Add(newNode);
			}

			int passedChecks = 0;
			foreach (var pathNode in pathNodes)
			{
				PathNode removedPathNode = avlTree.Find(pathNode.f);
				Assert.IsNotNull(removedPathNode, "Failed after {0} checks. Pathnode was Null. Pos was: x:{1} y:{2} z:{3}, f value was: {4}", 
					passedChecks++, 
					pathNode.position.x, 
					pathNode.position.y, 
					pathNode.position.z,
					pathNode.f);
				Assert.IsNotNull(removedPathNode.position, "Position was null.");
				Assert.AreEqual(0, pathNode.position.CompareTo(removedPathNode.position));
			}

			passedChecks = 0;
			foreach (var pathNode in pathNodesRemoved)
			{
				PathNode removedPathNode = avlTree.Find(pathNode.f);
				Assert.IsNull(removedPathNode, "Failed after {0} checks. Pathnode was not Null. Pos was: x:{1} y:{2} z:{3}, f value was: {4}",
					passedChecks++,
					pathNode.position.x,
					pathNode.position.y,
					pathNode.position.z,
					pathNode.f);
			}
		}

		[TestMethod]
		public void TestAvlTreeRandomRemovalAndPopping()
		{
			// Setup
			NewAvlTree<float, PathNode> avlTree = new NewAvlTree<float, PathNode>(new FloatComparer());

			List<PathNode> pathNodes = new List<PathNode>();
			int fValue = 0;

			for (int x = 0; x < 10; x++)
			{
				for (int y = 0; y < 10; y++)
				{
					for (int z = 0; z < 10; z++)
					{
						Vector3i pos = new Vector3i(x, y, z);
						PathNode pathNode = new PathNode(pos, ++fValue, 0, 0);
						pathNodes.Add(pathNode);
						avlTree.Insert(pathNode.f, pathNode);
					}
				}
			}

			Random rand = new Random();

			List<PathNode> pathNodesRemoved = new List<PathNode>();
			int nbOfPathNodes = pathNodes.Count;

			ResetPosition();
			SetPosMinMaxValues(101, 200, 101, 200, 101, 200);

			// Tests
			for (int i = 0; i < nbOfPathNodes / 2; i++)
			{
				PathNode removedNode = pathNodes[rand.Next(0, pathNodes.Count)];
				pathNodesRemoved.Add(removedNode);
				pathNodes.Remove(removedNode);
				avlTree.Remove(removedNode.f);

				PathNode newNode = new PathNode(GetNewPosition(), ++fValue, 0, 0);
				avlTree.Insert(newNode.f, newNode);
				pathNodes.Add(newNode);

				PathNode poppedNode = avlTree.Pop();
				Assert.IsNotNull(poppedNode, "Popped Node was null");
				pathNodesRemoved.Add(poppedNode);
				pathNodes.Remove(poppedNode);
			}

			int passedChecks = 0;
			foreach (var pathNode in pathNodes)
			{
				PathNode removedPathNode = avlTree.Find(pathNode.f);
				Assert.IsNotNull(removedPathNode, "Failed after {0} checks. Pathnode was Null. Pos was: x:{1} y:{2} z:{3}, f value was: {4}",
					passedChecks++,
					pathNode.position.x,
					pathNode.position.y,
					pathNode.position.z,
					pathNode.f);
				Assert.IsNotNull(removedPathNode.position, "Position was null.");
				Assert.AreEqual(0, pathNode.position.CompareTo(removedPathNode.position));
			}

			passedChecks = 0;
			foreach (var pathNode in pathNodesRemoved)
			{
				PathNode removedPathNode = avlTree.Find(pathNode.f);
				Assert.IsNull(removedPathNode, "Failed after {0} checks. Pathnode was not Null. Pos was: x:{1} y:{2} z:{3}, f value was: {4}",
					passedChecks++,
					pathNode.position.x,
					pathNode.position.y,
					pathNode.position.z,
					pathNode.f);
			}
		}

		[TestMethod]
		public void TestAvlTreeRandomRemovalAndPopping2()
		{
			int removalMinOccurrence = 3;
			int removalMaxOccurence = 6;
			int popMinOccurence = 3;
			int popMaxOccurence = 5;
			int maxValue = 20;

			ResetPosition();
			SetPosMinMaxValues(0, maxValue + 1, 0, maxValue + 1, 0, maxValue + 1);

			Random rand = new Random();
			int fValue = 0;

			// Setup
			NewAvlTree<float, PathNode> avlTree = new NewAvlTree<float, PathNode>(new FloatComparer());

			List<PathNode> originalPathNodePool = new List<PathNode>();
			List<PathNode> insertedPathNodes = new List<PathNode>();
			List<PathNode> pathNodesRemoved = new List<PathNode>();

			// Fill Original Pool
			for (int i = 0; i < maxValue * maxValue * maxValue; i++)
			{
				PathNode node = new PathNode(GetNewPosition(), ++fValue, 0, 0);
				originalPathNodePool.Add(node);
			}

			// Operations
			int nbOfPathNodes = originalPathNodePool.Count;

			int nextRemoval = rand.Next(removalMinOccurrence, removalMaxOccurence);
			int nextPop = rand.Next(popMinOccurence, popMaxOccurence);

			for (int i = 0; i < nbOfPathNodes; i++)
			{
				// Add a new node.
				PathNode newNode = originalPathNodePool[rand.Next(0, originalPathNodePool.Count)];
				avlTree.Insert(newNode.f, newNode);
				insertedPathNodes.Add(newNode);
				originalPathNodePool.Remove(newNode);

				if (--nextRemoval <= 0)
				{
					nextRemoval = rand.Next(removalMinOccurrence, removalMaxOccurence);

					PathNode nodeToRemove = insertedPathNodes[rand.Next(0, insertedPathNodes.Count)];
					PathNode removedNode = avlTree.Remove(nodeToRemove.f);
					Assert.IsNotNull(removedNode, "Removed node was null");
					pathNodesRemoved.Add(nodeToRemove);
					insertedPathNodes.Remove(nodeToRemove);
					Assert.AreEqual(0, nodeToRemove.position.CompareTo(removedNode.position), "Removed node's position was not the same as the asked node.");
					Assert.AreEqual(nodeToRemove.f, removedNode.f, "Removed node's Fvalue was not the same as the asked node.");
				}

				if (--nextPop <= 0)
				{
					nextPop = rand.Next(popMinOccurence, popMaxOccurence);

					PathNode poppedNode = avlTree.Pop();
					Assert.IsNotNull(poppedNode, "Popped Node was null");
					pathNodesRemoved.Add(poppedNode);
					insertedPathNodes.Remove(poppedNode);
				}
			}

			// Tests
			int passedChecks = 0;
			foreach (var pathNode in insertedPathNodes)
			{
				PathNode removedPathNode = avlTree.Find(pathNode.f);
				Assert.IsNotNull(removedPathNode, "Failed after {0} checks. Pathnode was Null. Pos was: x:{1} y:{2} z:{3}, f value was: {4}",
					passedChecks++,
					pathNode.position.x,
					pathNode.position.y,
					pathNode.position.z,
					pathNode.f);
				Assert.IsNotNull(removedPathNode.position, "Position was null.");
				Assert.AreEqual(0, pathNode.position.CompareTo(removedPathNode.position));
			}

			passedChecks = 0;
			foreach (var pathNode in pathNodesRemoved)
			{
				PathNode removedPathNode = avlTree.Find(pathNode.f);
				Assert.IsNull(removedPathNode, "Failed after {0} checks. Pathnode was not Null. Pos was: x:{1} y:{2} z:{3}, f value was: {4}",
					passedChecks++,
					pathNode.position.x,
					pathNode.position.y,
					pathNode.position.z,
					pathNode.f);
			}
		}

		[TestMethod]
		public void TestOpenListAdd()
		{
			// Setup
			NewOpenList openList = new NewOpenList();

			List<PathNode> pathNodes = new List<PathNode>();
			int fValue = 0;

			for (int i = 0; i < 5; i++)
			{
				for (int j = 0; j < 5; j++)
				{
					PathNode newPathNode = new PathNode(new Vector3i(i, 1, j), fValue++);
					pathNodes.Add(newPathNode);
					openList.Add(newPathNode);
				}
			}

			// Asserts
			foreach (var pathNode in pathNodes)
			{
				Assert.AreEqual(true, openList.Contains(pathNode.position, out PathNode tmp));
			}
		}

		[TestMethod]
		public void TestOpenListPop()
		{
			// Setup
			NewOpenList openList = new NewOpenList();

			List<PathNode> pathNodes = new List<PathNode>();
			List<PathNode> poppedPathNodes = new List<PathNode>();
			List<PathNode> removedPathNodes = new List<PathNode>();

			int fValue = 0;

			for (int i = 0; i <30; i++)
			{
				for (int j = 0; j < 30; j++)
				{
					PathNode newPathNode = new PathNode(new Vector3i(i, 1, j), fValue++);
					pathNodes.Add(newPathNode);
					openList.Add(newPathNode);
				}
			}

			for (int i = 0; i < 5; i++)
			{
				removedPathNodes.Add(pathNodes.First());
				pathNodes.Remove(removedPathNodes[i]);
				poppedPathNodes.Add(openList.Pop());
			}

			// Asserts
			foreach (var pathNode in pathNodes)
			{
				Assert.AreEqual(true, openList.Contains(pathNode.position, out PathNode tmp));
			}

			for (int i = 0; i < removedPathNodes.Count; i++)
			{
				Assert.AreEqual(true, removedPathNodes[i].position == poppedPathNodes[i].position);
			}
		}

		[TestMethod]
		public void TestOpenListPop2()
		{
			// Setup
			NewOpenList openList = new NewOpenList();

			List<PathNode> pathNodes = new List<PathNode>();
			List<PathNode> poppedPathNodes = new List<PathNode>();
			List<PathNode> removedPathNodes = new List<PathNode>();

			int fValue = 0;

			for (int i = 0; i < 30; i++)
			{
				for (int j = 0; j < 30; j++)
				{
					PathNode newPathNode = new PathNode(new Vector3i(i, 1, j), fValue++);
					pathNodes.Add(newPathNode);
					openList.Add(newPathNode);
				}
			}

			for (int i = 0; i < 5; i++)
			{
				removedPathNodes.Add(pathNodes.First());
				pathNodes.Remove(removedPathNodes[i]);
				poppedPathNodes.Add(openList.Pop());
			}

			// Asserts
			foreach (var pathNode in pathNodes)
			{
				Assert.AreEqual(true, openList.Contains(pathNode.position, out PathNode tmp));
			}

			for (int i = 0; i < removedPathNodes.Count; i++)
			{
				Assert.AreEqual(true, removedPathNodes[i].position == poppedPathNodes[i].position);
			}
		}

		[TestMethod]
		public void TestOpenListRandomRemovalAndPopping()
		{
			int removalMinOccurrence = 3;
			int removalMaxOccurence = 6;
			int popMinOccurence = 3;
			int popMaxOccurence = 5;
			int maxValue = 20;

			ResetPosition();
			SetPosMinMaxValues(0, maxValue + 1, 0, maxValue + 1, 0, maxValue + 1);

			Random rand = new Random();
			int fValue = 0;

			// Setup
			NewOpenList openList = new NewOpenList();

			List<PathNode> originalPathNodePool = new List<PathNode>();
			List<PathNode> insertedPathNodes = new List<PathNode>();
			List<PathNode> pathNodesRemoved = new List<PathNode>();

			// Fill Original Pool
			for (int i = 0; i < maxValue * maxValue * maxValue; i++)
			{
				PathNode node = new PathNode(GetNewPosition(), ++fValue, 0, 0);
				originalPathNodePool.Add(node);
			}

			// Operations
			int nbOfPathNodes = originalPathNodePool.Count;

			int nextRemoval = rand.Next(removalMinOccurrence, removalMaxOccurence);
			int nextPop = rand.Next(popMinOccurence, popMaxOccurence);

			for (int i = 0; i < nbOfPathNodes; i++)
			{
				// Add a new node.
				PathNode newNode = originalPathNodePool[rand.Next(0, originalPathNodePool.Count)];
				openList.Add(newNode);
				insertedPathNodes.Add(newNode);
				originalPathNodePool.Remove(newNode);

				if (--nextRemoval <= 0)
				{
					nextRemoval = rand.Next(removalMinOccurrence, removalMaxOccurence);

					PathNode nodeToRemove = insertedPathNodes[rand.Next(0, insertedPathNodes.Count)];
					PathNode removedNode = openList.Remove(nodeToRemove);
					Assert.IsNotNull(removedNode, "Removed node was null");
					pathNodesRemoved.Add(nodeToRemove);
					insertedPathNodes.Remove(nodeToRemove);
					Assert.AreEqual(0, nodeToRemove.position.CompareTo(removedNode.position), "Removed node's position was not the same as the asked node.");
					Assert.AreEqual(nodeToRemove.f, removedNode.f, "Removed node's Fvalue was not the same as the asked node.");
				}

				if (--nextPop <= 0)
				{
					nextPop = rand.Next(popMinOccurence, popMaxOccurence);

					PathNode poppedNode = openList.Pop();
					Assert.IsNotNull(poppedNode, "Popped Node was null");
					pathNodesRemoved.Add(poppedNode);
					insertedPathNodes.Remove(poppedNode);
				}
			}

			// Tests
			int passedChecks = 0;
			foreach (var pathNode in insertedPathNodes)
			{
				openList.Contains(pathNode.position, out PathNode removedPathNode);
				Assert.IsNotNull(removedPathNode, "Failed after {0} checks. Pathnode was Null. Pos was: x:{1} y:{2} z:{3}, f value was: {4}",
					passedChecks++,
					pathNode.position.x,
					pathNode.position.y,
					pathNode.position.z,
					pathNode.f);
				Assert.IsNotNull(removedPathNode.position, "Position was null.");
				Assert.AreEqual(0, pathNode.position.CompareTo(removedPathNode.position));
			}

			passedChecks = 0;
			foreach (var pathNode in pathNodesRemoved)
			{
				openList.Contains(pathNode.position, out PathNode removedPathNode);
				Assert.IsNull(removedPathNode, "Failed after {0} checks. Pathnode was not Null. Pos was: x:{1} y:{2} z:{3}, f value was: {4}",
					passedChecks++,
					pathNode.position.x,
					pathNode.position.y,
					pathNode.position.z,
					pathNode.f);
			}

			Assert.AreEqual(openList.Count, insertedPathNodes.Count);
		}
	}
}
