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
        

        /// <summary>
        /// Creates the map with passable and not passable nodes
        /// </summary>
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
        /// <summary>
        /// Setting the passable neighbours for each node in the map
        /// </summary>
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
        /// <summary>
        /// Chescks if therw will be a possible path between two nodes, if there is one sets the path-list to this path.
        /// </summary>
        /// <returns></returns>
        public static bool CalculateClosestPath()
        {
            if (FindPath(new Point(1, 2), new Point(12, 13)).Count != 0) //Finns det en path?
            {
                 path = FindPath(new Point(1, 2), new Point(12, 13));   
                 return true;
            }
            return false;   //Returnar false om ingen pathg går att hitta
        }


        /// <summary>
        /// Calculates H, a estimated path between two points
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
            closedStack.Push(endNode);

            PathNode parentTile = endNode.Parent;

            // Trace back through the nodes using the parent fields
            // to find the best path.
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
