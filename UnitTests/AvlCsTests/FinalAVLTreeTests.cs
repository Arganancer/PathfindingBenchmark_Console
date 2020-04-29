namespace AvlCsTests
{
	[TestClass]
	public class FinalAVLTreeTests
	{
		[TestMethod]
		public void TestAVLTreeAdd()
		{
			// Setup
			AVLTree<Vector3i, PathNode> avlTree = new AVLTree<Vector3i, PathNode>();

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
						avlTree.Add(pos, new PathNode(pos, ++count, 0, 0));
					}
				}
			}

			// Tests
			Assert.AreEqual(count, avlTree.Count);

			count = 0;
			foreach (var pos in positions)
			{
				PathNode node = avlTree.Get(pos);
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
			AVLTree<Vector3i, PathNode> avlTree = new AVLTree<Vector3i, PathNode>();

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
						avlTree.Add(pos, new PathNode(pos, ++count, 0, 0));
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
			Assert.AreEqual(count / 2, avlTree.Count);
			
			foreach (var pos in positions)
			{
				PathNode node = avlTree.Get(pos);
				Assert.IsNotNull(node);
				if (node != null)
				{
					Assert.AreEqual(0, pos.CompareTo(node.position));
				}
			}

			foreach (var pos in removedPositions)
			{
				PathNode node = avlTree.Get(pos);
				Assert.IsNull(node);
			}
		}

		[TestMethod]
		public void TestOpenListAdd()
		{
			// Setup
			OpenList openList = new OpenList();

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
			OpenList openList = new OpenList();

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
	}
}
