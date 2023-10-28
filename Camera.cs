using System;
namespace Assignment2
{
    /// <summary>
    /// Class for the camera obstacle
    /// </summary>
	public class Camera : Obstacle
	{
        // Private properties for the effects list, camera location and direction
        private List<Point> Effects = new List<Point>();
        private int[] location;
        private char direction;

        // Public property to return the list of points effected by the camera
        public override List<Point> effects
        {
            get { return Effects; }
            set { }
        }

        // Public property to return the symbol for the camera on the map
        public override char symbol
        {
            get
            {
                return 'c';
            }
            set { }
        }

        /// <summary>
        /// Public method to return if a point is observed by a camera
        /// </summary>
        /// <param name="X">X location of the point</param>
        /// <param name="Y">Y location of the point</param>
        /// <returns>True if the point is observed by camera, false if not</returns>
        public override bool obeserve(int x, int y)
        {
            List<char> dir = calculateDirection(x, y);
            return dir.Contains(direction);
        }

        // Public constructor for the camera
        public Camera(int[] cameraLocation, char cameraDirection)
		{
            location = cameraLocation;
            direction = cameraDirection;
        }

        /// <summary>
        /// Private method to calculate the directions a point is observed by the camera
        /// </summary>
        /// <param name="x">X location of point</param>
        /// <param name="y">Y location of point</param>
        /// <returns>A list of the directions a point is observed by the camera</returns>
        private List<char> calculateDirection(int x, int y)
        {
            int dx = x - location[0];
            int dy = y - location[1];

            List<char> dirs = new List<char>();

            if (dy <= 0 && Math.Abs(dy) >= Math.Abs(dx)) dirs.Add('n');
            if (dy >= 0 && Math.Abs(dy) >= Math.Abs(dx)) dirs.Add('s');
            if (dx <= 0 && Math.Abs(dx) >= Math.Abs(dy)) dirs.Add('w');
            if (dx >= 0 && Math.Abs(dx) >= Math.Abs(dy)) dirs.Add('e');

            return dirs;
        }
    }
}