using System;

namespace Assignment2
{
    /// <summary>
    /// Public class for the fence obstacle
    /// </summary>
    public class Fence : Obstacle
    {
        // Private properties for the fence start, length, effected points and distance in location
        private int[] fenceStart;
        private int length;
        private List<Point> Effects = new List<Point>();
        private int dx;
        private int dy;

        /// <summary>
        /// Returns a list of the points effected by the fence
        /// </summary>
        public override List<Point> effects
        {
            get { return Effects; }
            set {}
        }

        /// <summary>
        /// Returns the char value of the fence for mapping purposes
        /// </summary>
        public override char symbol {
            get
            {
                return 'f';
            }
            set { }
        }

        /// <summary>
        /// Public method to check if a point is observed by the fence
        /// </summary>
        /// <param name="X">X location of the point</param>
        /// <param name="Y">Y location of the point</param>
        /// <returns>Returns true if the point is observed by the fence</returns>
        public override bool obeserve(int X, int Y)
        {
            return Effects.Any(point => point.x == X && point.y == Y);
        }

        /// <summary>
        /// Public method to construct a fence
        /// </summary>
        /// <param name="fencestart">The location of the fence start</param>
        /// <param name="DX">Difference in x location from start to end</param>
        /// <param name="DY">Difference in y location from start to end</param>
        public Fence(int[] fencestart, int DX, int DY)
        {
            fenceStart = fencestart;
            dx = DX;
            dy = DY;
            length = Math.Abs(dx) + Math.Abs(dy);

            // Difference in x is not 0 therefore fence is horizontal
            if(dx != 0)
            {
                int[] currentPoint = fenceStart;
                // Difference in x is greater than 0 therefore fence moves from west to east -> increment X
                if (dx < 0)
                {
                    for(int i = 0; i <= length; i++)
                    {
                        Point addPoint = new Point(currentPoint[0], currentPoint[1]);
                        currentPoint[0]++;
                        Effects.Add(addPoint);
                    }
                }
                // Moving from east to west -> decrement X
                else
                {
                    for (int i = 0; i <= length; i++)
                    {
                        Point addPoint = new Point(currentPoint[0], currentPoint[1]);
                        currentPoint[0]--;
                        Effects.Add(addPoint);
                    }
                }
            }
            // Fence is vertical
            else
            {
                int[] currentPoint = fenceStart;
                // Difference in y is greater than 0 therefore fence moves from south to north -> increment Y
                if (dy < 0)
                {
                    for (int i = 0; i <= length; i++)
                    {
                        Point addPoint = new Point(currentPoint[0], currentPoint[1]);
                        currentPoint[1]++;
                        Effects.Add(addPoint);
                    }
                }
                // Moving from east to west -> decrement Y
                else
                {
                    for (int i = 0; i <= length; i++)
                    {
                        Point addPoint = new Point(currentPoint[0], currentPoint[1]);
                        currentPoint[1]--;
                        Effects.Add(addPoint);
                    }
                }
            }
        }
    }
}

