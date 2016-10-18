using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TowerDefenseAlgorithm
{
    public class PathNode
    {
        /// <summary>
        /// A reference to the node that transfered this node to
        /// the open list. This will be used to trace our path back
        /// from the goal node to the start node.
        /// </summary>
        public PathNode Parent;

        /// <summary>
        /// Provides an easy way to check if this node
        /// is in the open list.
        /// </summary>
        public bool InOpenList;
        /// <summary>
        /// Provides an easy way to check if this node
        /// is in the closed list.
        /// </summary>
        public bool InClosedList;

        /// <summary>
        /// The approximate distance from the start node to the
        /// goal node if the path goes through this node. (F)
        /// </summary>
        public float DistanceToGoal;
        /// <summary>
        /// Distance traveled from the spawn point. (G)
        /// </summary>
        public float DistanceTraveled;


        public bool isWall;
        public List<PathNode> neighbours = new List<PathNode>();
        public int x;
        public int y;
        public PathNode(int x, int y, bool isWall)
        {
            this.isWall = isWall;
            this.x = x;
            this.y = y;
        }
    }
}
