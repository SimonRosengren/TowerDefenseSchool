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
        public static List<Vector2> path = new List<Vector2>();

        // Holds search nodes that are avaliable to search.
        public static List<PathNode> openList = new List<PathNode>();
        // Holds the nodes that have already been searched.
        public static List<PathNode> closedList = new List<PathNode>();

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
                    if (i == 1 || j == 1)   //Sidor har inte 4 grannar
                    {
                        map[i, j].neighbours.Add(map[i + 1, j]); //en till höger
                        map[i, j].neighbours.Add(map[i, j + 1]); //under
                    }
                    else if (i == 13 || j == 13)
                    {
                        map[i, j].neighbours.Add(map[i - 1, j]); //en till höger
                        map[i, j].neighbours.Add(map[i, j - 1]); //under
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
        public static void CalculateClosestPath()
        {
            path = FindPath(new Point(1, 2), new Point(12, 13));
        }


        /// <summary>
        /// Returns an estimate of the distance between two points. (H)
        /// </summary>
        public static float Heuristic(Point point1, Point point2)
        {
            return Math.Abs(point1.X - point2.X) +
                   Math.Abs(point1.Y - point2.Y);
        }
        /// <summary>
        /// Resets the state of the search nodes.
        /// </summary>
        public static void ResetSearchNodes()
        {
            openList.Clear();
            closedList.Clear();

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
        /// <summary>
        /// Returns the node with the smallest distance to goal.
        /// </summary>
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
        /// <summary>
        /// Use the parent field of the search nodes to trace
        /// a path from the end node to the start node.
        /// </summary>
        private static List<Vector2> FindFinalPath(PathNode startNode, PathNode endNode)
        {
            closedList.Add(endNode);

            PathNode parentTile = endNode.Parent;

            // Trace back through the nodes using the parent fields
            // to find the best path.
            while (parentTile != startNode)
            {
                closedList.Add(parentTile);
                parentTile = parentTile.Parent;
            }

            List<Vector2> finalPath = new List<Vector2>();

            // Reverse the path and transform into world space.
            for (int i = closedList.Count - 1; i >= 0; i--)
            {
                finalPath.Add(new Vector2(closedList[i].x, //*32?
                                          closedList[i].y));
            }
            for (int i = 0; i < finalPath.Count; i++)
            {
            }
            return finalPath;
        }
        ///<summary>
        /// Finds the optimal path from one point to another.
        /// </summary>
        public static List<Vector2> FindPath(Point startPoint, Point endPoint)
        {
            // Only try to find a path if the start and end points are different.
            if (startPoint == endPoint)
            {
                return new List<Vector2>();
            }

            /////////////////////////////////////////////////////////////////////
            // Step 1 : Clear the Open and Closed Lists and reset each node’s F 
            //          and G values in case they are still set from the last 
            //          time we tried to find a path. 
            /////////////////////////////////////////////////////////////////////
            ResetSearchNodes();

            // Store references to the start and end nodes for convenience.
            PathNode startNode = map[startPoint.X, startPoint.Y];
            PathNode endNode = map[endPoint.X, endPoint.Y];

            /////////////////////////////////////////////////////////////////////
            // Step 2 : Set the start node’s G value to 0 and its F value to the 
            //          estimated distance between the start node and goal node 
            //          (this is where our H function comes in) and add it to the 
            //          Open List. 
            /////////////////////////////////////////////////////////////////////
            startNode.InOpenList = true;

            startNode.DistanceToGoal = Heuristic(startPoint, endPoint);
            startNode.DistanceTraveled = 0;

            openList.Add(startNode);

            /////////////////////////////////////////////////////////////////////
            // Setp 3 : While there are still nodes to look at in the Open list : 
            /////////////////////////////////////////////////////////////////////
            while (openList.Count > 0)
            {
                /////////////////////////////////////////////////////////////////
                // a) : Loop through the Open List and find the node that 
                //      has the smallest F value.
                /////////////////////////////////////////////////////////////////
                PathNode currentNode = FindBestNode();

                /////////////////////////////////////////////////////////////////
                // b) : If the Open List empty or no node can be found, 
                //      no path can be found so the algorithm terminates.
                /////////////////////////////////////////////////////////////////
                if (currentNode == null)
                {
                    break;
                }

                /////////////////////////////////////////////////////////////////
                // c) : If the Active Node is the goal node, we will 
                //      find and return the final path.
                /////////////////////////////////////////////////////////////////
                if (currentNode == endNode)
                {
                    // Trace our path back to the start.
                    return FindFinalPath(startNode, endNode);
                }

                /////////////////////////////////////////////////////////////////
                // d) : Else, for each of the Active Node’s neighbours :
                /////////////////////////////////////////////////////////////////
                for (int i = 0; i < currentNode.neighbours.Count; i++)
                {
                    PathNode neighbor = currentNode.neighbours[i];

                    //////////////////////////////////////////////////
                    // i) : Make sure that the neighbouring node can 
                    //      be walked across. 
                    //////////////////////////////////////////////////
                    if (neighbor == null || neighbor.isWall == true)
                    {
                        continue;
                    }

                    //////////////////////////////////////////////////
                    // ii) Calculate a new G value for the neighbouring node.
                    //////////////////////////////////////////////////
                    float distanceTraveled = currentNode.DistanceTraveled + 1;

                    // An estimate of the distance from this node to the end node.
                    float heuristic = Heuristic(new Point(neighbor.x, neighbor.y), endPoint);

                    //////////////////////////////////////////////////
                    // iii) If the neighbouring node is not in either the Open 
                    //      List or the Closed List : 
                    //////////////////////////////////////////////////
                    if (neighbor.InOpenList == false && neighbor.InClosedList == false)
                    {
                        // (1) Set the neighbouring node’s G value to the G value 
                        //     we just calculated.
                        neighbor.DistanceTraveled = distanceTraveled;
                        // (2) Set the neighbouring node’s F value to the new G value + 
                        //     the estimated distance between the neighbouring node and
                        //     goal node.
                        neighbor.DistanceToGoal = distanceTraveled + heuristic;
                        // (3) Set the neighbouring node’s Parent property to point at the Active 
                        //     Node.
                        neighbor.Parent = currentNode;
                        // (4) Add the neighbouring node to the Open List.
                        neighbor.InOpenList = true;
                        openList.Add(neighbor);
                    }
                    //////////////////////////////////////////////////
                    // iv) Else if the neighbouring node is in either the Open 
                    //     List or the Closed List :
                    //////////////////////////////////////////////////
                    else if (neighbor.InOpenList || neighbor.InClosedList)
                    {
                        // (1) If our new G value is less than the neighbouring 
                        //     node’s G value, we basically do exactly the same 
                        //     steps as if the nodes are not in the Open and 
                        //     Closed Lists except we do not need to add this node 
                        //     the Open List again.
                        if (neighbor.DistanceTraveled > distanceTraveled)
                        {
                            neighbor.DistanceTraveled = distanceTraveled;
                            neighbor.DistanceToGoal = distanceTraveled + heuristic;

                            neighbor.Parent = currentNode;
                        }
                    }
                }

                /////////////////////////////////////////////////////////////////
                // e) Remove the Active Node from the Open List and add it to the 
                //    Closed List
                /////////////////////////////////////////////////////////////////
                openList.Remove(currentNode);
                currentNode.InClosedList = true;
            }

            // No path could be found.
            return new List<Vector2>();
        }
    }
}
