namespace Assignment2
{
    /// <summary>
    /// Abstract class to implement obstacles
    /// </summary>
    public abstract class Obstacle
    {
        // Public property of the list of points an obstacle effects
        public abstract List<Point> effects { get; set; }

        // Public property of the symbol of the point for displaying on the map
        public abstract char symbol { get; set; }

        // Public method which returns true if the point is observed by the obstacle
        public abstract bool obeserve(int x, int y);

        // Public constructor for the obstacle, adds the obstacle to the obstacle manager list
        public Obstacle()
        {
            this.effects = effects;
            this.symbol = symbol;
            ObstacleManager.addObstacle(this);
        }
    }
}