using System;
using System.Numerics;

namespace Assignment2
{
    /// <summary>
    /// Class of a node object for pathfinding. Unlike a point object, a node has cost and distance properties to calculate the
    /// closest node to the objective. It also has public properties as these are accesed by the FindPath class
    /// </summary>
    public class Node
    {
        // Public properties for the x and y locations of the node on the grid
        public int x { get; set; }
        public int y { get; set; }

        // Public property for the previous nodes needed to cross to get here
        public int cost { get; set; }

        // Public property for the distance to objective -> calculated using setDistance method
        public int distance { get; set; }

        // Public property for the cost + distance. Ths is used to decide which nodes to work on by ordering by costDistance
        public int costDistance => cost + distance;

        // Public property for the previous node. This is primarly used for printing our the most effecient parth to the objective
        public Node parent { get; set;  }

        /// <summary>
        /// Public method to calculate the distance to the objective, ignores obstacles
        /// </summary>
        /// <param name="targetX">X location of the objective</param>
        /// <param name="targetY">Y location of the objective</param>
        public void setDistance(int targetX, int targetY)
        {
            distance = Math.Abs(targetX - x) + Math.Abs(targetY - y);
        }

        
    }
}

