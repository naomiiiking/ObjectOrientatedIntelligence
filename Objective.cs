using System;
namespace Assignment2
{
    /// <summary>
    /// Public class for the objetive object. This is usde for path finding
    /// </summary>
	public class Objective
	{
        // Public location of the objective as a point object
        public Point ObjectiveLocation { get; set; }

        // Public constructor for the objective
        public Objective(int x, int y)
        {
            ObjectiveLocation = new Point(x, y);
        }

    }
}

