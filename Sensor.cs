using System;
using System.Drawing;

namespace Assignment2
{
	public class Sensor : Obstacle
	{
        // Private property for the sensor location
        private int[] sensorLocation;
        // Private property for the sensor range
        private float sensorRange;
        // Private property for the list of points the sensor effects within it's circular range
        private List<Point> Effects = new List<Point>();
        // Private property for the list of points the sensor effects within it's square range
        private List<Point> sensorSquareRange = new List<Point>();

        // Public property to return the list of points the sensor effects
        public override List<Point> effects
        {
            get { return Effects; }
            set { }
        }

        // Public property to return the symbol of the sensor for map display
        public override char symbol
        {
            get
            {
                return 's';
            }
            set { }
        }

        /// <summary>
        /// Public propertys
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <returns></returns>
        public override bool obeserve(int X, int Y)
        {
            return Effects.Any(point => point.x == X && point.y == Y); 
        }

        /// <summary>
        /// Sensor contstructor, creates the maximum "sqaure" range of the sensor and filters this list
        /// down to the actual effected squares using the Euclidean distance
        /// </summary>
        /// <param name="SensorLocation">Int array of sensors location</param>
        /// <param name="SensorRange">Floating point range of sensor</param>
        public Sensor(int[] SensorLocation, float SensorRange)
        {
            sensorLocation = SensorLocation;
            sensorRange = SensorRange;
            for (int x = Convert.ToInt32(sensorLocation[0] - Math.Floor(sensorRange)); x <= Convert.ToInt32(sensorLocation[0] + Math.Floor(sensorRange)); x++)
            {
                for(int y = Convert.ToInt32(sensorLocation[1] - Math.Floor(sensorRange)) ; y <= Convert.ToInt32(sensorLocation[1] + Math.Floor(sensorRange)); y++)
                {
                    Point currentPoint = new Point(x, y);
                    sensorSquareRange.Add(currentPoint);
                }
            }
            foreach(Point point in sensorSquareRange)
            {
                if (sensorRange >= Math.Sqrt(Math.Pow(point.x - sensorLocation[0], 2) + Math.Pow(point.y - sensorLocation[1], 2)))
                {
                    Effects.Add(point);
                }
            }
        }
    }
}