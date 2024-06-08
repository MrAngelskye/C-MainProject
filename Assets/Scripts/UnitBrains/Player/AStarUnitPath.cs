using System.Collections.Generic;
using Model;
using UnityEngine;

namespace UnitBrains.Pathfinding
{
    public class AStarUnitPath : BaseUnitPath
    {
        private class Node
        {
            public Vector2Int Position;
            public Node Parent;
            public float GCost;
            public float HCost;
            public float FCost => GCost + HCost;

            public Node(Vector2Int position, Node parent, float gCost, float hCost)
            {
                Position = position;
                Parent = parent;
                GCost = gCost;
                HCost = hCost;
            }
        }

        public AStarUnitPath(IReadOnlyRuntimeModel runtimeModel, Vector2Int startPoint, Vector2Int endPoint) : base(runtimeModel, startPoint, endPoint)
        {
        }

        protected override void Calculate()
        {
            var openSet = new List<Node>();
            var closedSet = new HashSet<Vector2Int>();
            var startNode = new Node(startPoint, null, 0, HeuristicCostEstimate(startPoint, endPoint));
            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                var currentNode = GetLowestFCostNode(openSet);
                openSet.Remove(currentNode);

                if (currentNode.Position == endPoint)
                {
                    ReconstructPath(currentNode);
                    return;
                }

                closedSet.Add(currentNode.Position);

                foreach (var neighbor in GetNeighbors(currentNode.Position))
                {
                    if (closedSet.Contains(neighbor))
                        continue;

                    if (IsObstacle(neighbor))
                        continue;

                    var gCost = currentNode.GCost + Vector2Int.Distance(currentNode.Position, neighbor);
                    var hCost = HeuristicCostEstimate(neighbor, endPoint);
                    var neighborNode = new Node(neighbor, currentNode, gCost, hCost);

                    if (openSet.Contains(neighborNode))
                    {
                        var existingNode = openSet.Find(n => n.Position == neighbor);
                        if (gCost < existingNode.GCost)
                        {
                            existingNode.GCost = gCost;
                            existingNode.Parent = currentNode;
                        }
                    }
                    else
                    {
                        openSet.Add(neighborNode);
                    }
                }
            }
            Debug.LogError("Path not found.");
        }

        private float HeuristicCostEstimate(Vector2Int from, Vector2Int to)
        {
            return Vector2Int.Distance(from, to);
        }

        private Node GetLowestFCostNode(List<Node> nodes)
        {
            var lowestFCost = float.MaxValue;
            Node lowestNode = null;

            foreach (var node in nodes)
            {
                if (node.FCost < lowestFCost)
                {
                    lowestFCost = node.FCost;
                    lowestNode = node;
                }
            }

            return lowestNode;
        }

        private void ReconstructPath(Node endNode)
        {
            var path = new List<Vector2Int>();
            var currentNode = endNode;

            while (currentNode != null)
            {
                path.Add(currentNode.Position);
                currentNode = currentNode.Parent;
            }

            path.Reverse(); 
            this.path = path.ToArray();
        }

        private bool[,] obstacleMap;
        private bool IsObstacle(Vector2Int position)
        {
            if (position.x < 0 || position.x >= obstacleMap.GetLength(0) ||
                position.y < 0 || position.y >= obstacleMap.GetLength(1))
            {
                return true;
            }

            return obstacleMap[position.x, position.y];
        }

        private List<Vector2Int> GetNeighbors(Vector2Int position)
        {
            var neighbors = new List<Vector2Int>();


            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0) continue;
                    var neighborPos = new Vector2Int(position.x + x, position.y + y);
                    if (!IsObstacle(neighborPos))
                    {
                        neighbors.Add(neighborPos);
                    }
                }
            }

            return neighbors;
        }

    }
}

