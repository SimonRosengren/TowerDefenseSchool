using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace TowerDefenseAlgorithm
{

    public static class PathFinder
    {
        public static PathNode[,] map = new PathNode[Globals.X_SIZE, Globals.Y_SIZE];
        //Holds the final path
        public static List<Vector2> path = new List<Vector2>();
        // Holds search nodes that are avaliable to search.
        public static List<PathNode> openList = new List<PathNode>();
        // Holds the nodes that have already been searched.
        public static Stack<PathNode> closedStack = new Stack<PathNode>();
        
        public static void CreateMap()
        {
            for (int i = 0; i < Globals.X_SIZE; i++)
            {
                for (int j = 0; j < Globals.Y_SIZE; j++)
                {
                    if (Board.board[i, j].isPassable())
                    {
                        map[i, j] = new PathNode(i, j, false);
                    }
                    else
                        map[i, j] = new PathNode(i, j, true);
                }
            }
            SetNeighbours();
        }

        public static void SetNeighbours()
        {
            for (int i = 1; i < Globals.X_SIZE - 1; i++) //Börjar på 1 för att skippa kanterna
            {
                for (int j = 1; j < Globals.Y_SIZE - 1; j++)
                {
                    if (i == 1 && j != 1 || i == 1 && j != Globals.Y_SIZE - 2)   //Sidor har inte 4 grannar
                    {
                        map[i, j].neighbours.Add(map[i, j - 1]);//Över
                        map[i, j].neighbours.Add(map[i + 1, j]); //en till höger
                        map[i, j].neighbours.Add(map[i, j + 1]); //under
                    }

                    if (j == 1 && i != Globals.X_SIZE - 2 || j == 1 && i != 1)   //Sidor har inte 4 grannar
                    {
                        map[i, j].neighbours.Add(map[i - 1, j]);//en till vänster
                        map[i, j].neighbours.Add(map[i + 1, j]); //en till höger
                        map[i, j].neighbours.Add(map[i, j + 1]); //under
                    }
                    else if (i == Globals.X_SIZE - 2 && j != Globals.Y_SIZE - 2 || i == Globals.X_SIZE - 2 && j != 1)
                    {
                        map[i, j].neighbours.Add(map[i - 1, j]); //en till vänster
                        map[i, j].neighbours.Add(map[i, j - 1]); //¨över
                        map[i, j].neighbours.Add(map[i, j + 1]); //under
                    }
                    else if (j == Globals.Y_SIZE - 2 && i != 1 || j == Globals.Y_SIZE - 2 && i != Globals.X_SIZE - 2)
                    {
                        map[i, j].neighbours.Add(map[i - 1, j]); //en till vänster
                        map[i, j].neighbours.Add(map[i + 1, j]); //en till höger
                        map[i, j].neighbours.Add(map[i, j - 1]); //över
                    }
                    if (i == 1 && j == 1)
                    {
                        map[i, j].neighbours.Add(map[i, j + 1]);
                        map[i, j].neighbours.Add(map[i + 1, j]);
                    }
                    if (i == 1 && j == Globals.Y_SIZE - 2)
                    {
                        map[i, j].neighbours.Add(map[i + 1, j]);
                        map[i, j].neighbours.Add(map[i, j + 1]);
                    }
                    if (i == Globals.X_SIZE - 2 && j == 1)
                    {
                        map[i, j].neighbours.Add(map[i - 1, j]); //en till vänster
                        map[i, j].neighbours.Add(map[i, j + 1]); //under
                    }
                    if (i == Globals.X_SIZE && j == Globals.Y_SIZE)
                    {
                        map[i, j].neighbours.Add(map[i, j - 1]); //över
                        map[i, j].neighbours.Add(map[i - 1, j]); //en till vänster
                    }
                    else
                    {
                        map[i, j].neighbours.Add(map[i + 1, j]); //en till höger
                        map[i, j].neighbours.Add(map[i, j + 1]); //under
                        map[i, j].neighbours.Add(map[i - 1, j]); //en till höger
                        map[i, j].neighbours.Add(map[i, j - 1]); //under
                    }
                }
            }
        }

        public static bool CalculateClosestPath()
        {
            if (FindPath(new Point(1, 2), new Point(12, 13)).Count != 0) //Finns det en path?
            {
                 path = FindPath(new Point(1, 2), new Point(12, 13));   
                 return true;
            }
            return false;   //Returnar false om ingen pathg går att hitta
        }

        public static float Heuristic(Point point1, Point point2)
        {
            return Math.Abs(point1.X - point2.X) +
                   Math.Abs(point1.Y - point2.Y);
        }

        public static void ResetSearchNodes()
        {
            openList.Clear();


            closedStack.Clear();

            for (int x = 0; x < Globals.X_SIZE; x++)
            {
                for (int y = 0; y < Globals.Y_SIZE; y++)
                {
                    PathNode node = map[x, y];

                    if (node == null)
                    {
                        continue;
                    }

                    node.InOpenList = false;
                    node.InClosedList = false;

                    node.DistanceTraveled = float.MaxValue;
                    node.DistanceToGoal = float.MaxValue;
                }
            }
        }

        private static PathNode FindBestNode()
        {
            PathNode currentTile = openList[0];

            float smallestDistanceToGoal = float.MaxValue;

            // Find the closest node to the goal.
            for (int i = 0; i < openList.Count; i++)
            {
                if (openList[i].DistanceToGoal < smallestDistanceToGoal)
                {
                    currentTile = openList[i];
                    smallestDistanceToGoal = currentTile.DistanceToGoal;
                }
            }
            return currentTile;
        }

        private static List<Vector2> FindFinalPath(PathNode startNode, PathNode endNode)
        {
            closedStack.Push(endNode);

            PathNode parentTile = endNode.Parent;

            while (parentTile != startNode)
            {
                closedStack.Push(parentTile);
                parentTile = parentTile.Parent;
            }

            List<Vector2> finalPath = new List<Vector2>();


            //Closed list som stack istället för lista, vilket gör attt vi inte behöver loopa genom den baklänges, och poppa sen bara ut dem            
            while (closedStack.Count ()!= 0)
            {
                finalPath.Add(new Vector2(closedStack.Peak().x, closedStack.Peak().y)); //Sparar ner plats till finalPath
                closedStack.Pop(); //Poppar ut noden
            }
            return finalPath;
        }

        public static List<Vector2> FindPath(Point startPoint, Point endPoint)
        {
            // Only try to find a path if the start and end points are different.
            if (startPoint == endPoint)
            {
                return new List<Vector2>();
            }
            ResetSearchNodes();

            PathNode startNode = map[startPoint.X, startPoint.Y];
            PathNode endNode = map[endPoint.X, endPoint.Y];

            startNode.InOpenList = true;

            startNode.DistanceToGoal = Heuristic(startPoint, endPoint);
            startNode.DistanceTraveled = 0;

            openList.Add(startNode);

            while (openList.Count > 0)
            {

                PathNode currentNode = FindBestNode();

                if (currentNode == null)
                {
                    break;
                }

                if (currentNode == endNode)
                {
                    // Trace our path back to the start.
                    return FindFinalPath(startNode, endNode);
                }

                for (int i = 0; i < currentNode.neighbours.Count; i++)
                {
                    PathNode neighbor = currentNode.neighbours[i];

                    if (neighbor == null || neighbor.isWall == true)
                    {
                        continue;
                    }

                    float distanceTraveled = currentNode.DistanceTraveled + 1;

                    // An estimate of the distance from this node to the end node.
                    float heuristic = Heuristic(new Point(neighbor.x, neighbor.y), endPoint);

                    if (neighbor.InOpenList == false && neighbor.InClosedList == false)
                    {

                        neighbor.DistanceTraveled = distanceTraveled;

                        neighbor.DistanceToGoal = distanceTraveled + heuristic;

                        neighbor.Parent = currentNode;

                        neighbor.InOpenList = true;
                        openList.Add(neighbor);
                    }

                    else if (neighbor.InOpenList || neighbor.InClosedList)
                    {

                        if (neighbor.DistanceTraveled > distanceTraveled)
                        {
                            neighbor.DistanceTraveled = distanceTraveled;
                            neighbor.DistanceToGoal = distanceTraveled + heuristic;

                            neighbor.Parent = currentNode;
                        }
                    }
                }

                openList.Remove(currentNode);
                currentNode.InClosedList = true;
            }

            // No path could be found.
            return new List<Vector2>();
        }
    }
}
