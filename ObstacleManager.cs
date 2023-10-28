using System;
namespace Assignment2
{
	public class ObstacleManager
	{
        // Public property for the universal list of obstacles
        public static List<Obstacle> obstacleList = new();

        /// <summary>
        /// Public method to add an obstacle to an obstacle list
        /// </summary>
        /// <param name="obstacle"></param>
        public static void addObstacle(Obstacle obstacle)
        {
            obstacleList.Add(obstacle);
        }

        // Public constructor for the obstacle manager
        public ObstacleManager(List <Obstacle> ObstacleList)
		{
            obstacleList = ObstacleList;
        }

        /// <summary>
        /// Public method to display the map of the specified size
        /// </summary>
        /// <param name="topLeft">Top left coordinate of the map</param>
        /// <param name="bottomRight">Bottom right coordinate of the map</param>
        public static void DisplayMap(int[] topLeft, int[] bottomRight)
        {
            List<int[]> written = new List<int[]>();
            for (int y = topLeft[1]; y <= bottomRight[1]; y++)
            {
                for (int x = topLeft[0]; x <= bottomRight[0]; x++)
                {
                    bool wrote = false;
                    int[] currentCoord = new int[] { x, y };
                    foreach (Obstacle obstacle in obstacleList)
                    {
                        if (obstacle.obeserve(x, y) && !written.Contains(currentCoord))
                        {
                            written.Add(currentCoord);
                            Console.Write(obstacle.symbol);
                            wrote = true;
                        }
                    }
                    if (!wrote && !written.Contains(currentCoord))
                    {
                        Console.Write('.');
                    }
                }
                Console.WriteLine();
            }
        }
    }
}