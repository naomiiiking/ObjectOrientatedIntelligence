using System;

namespace Assignment2
{
    /// <summary>
    /// Public class for the guard obstacle
    /// </summary>
    public class Guard : Obstacle
    {
        // Private properties for the effected point
        private Point location;
        private List<Point> Effects = new List<Point>();

        // Return a list containing the effected point
        public override List<Point> effects
        {
            get { return Effects; }
            set { }
        }

        // Return the symbol of the guard obstacle for mapping purposes
        public override char symbol
        {
            get
            {
                return 'g';
            }
            set { }
        }

        // Public constructor for the guard
        public Guard(int x, int y)
		{
            location = new Point(x, y);
            Effects = new List<Point> { location };
        }

        /// <summary>
        /// Public method to check if a point is effected by the guard
        /// </summary>
        /// <param name="X">X location of the point</param>
        /// <param name="Y">Y location of the point</param>
        /// <returns></returns>
        public override bool obeserve(int X, int Y)
        {
            return Effects.Any(point => point.x == X && point.y == Y);
        }
    }
}

