using System;
namespace Assignment2
{
    /// <summary>
    /// Public class for the point object. This is primarly used to assign locations of other objects
    /// </summary>
    public class Point
    {
        public int x = 0;
        public int y = 0;

        /// <summary>
        /// Public constructor to create a point
        /// </summary>
        /// <param name="x">X location of the point</param>
        /// <param name="y">Y location of the point</param>
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

    }
}

