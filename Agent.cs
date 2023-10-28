using System;
using System.Linq;

namespace Assignment2
{
    /// <summary>
    /// Class for the agent object required for showing safe directions and path to objective
    /// </summary>
	public class Agent 
	{
        // Public property for agent's x location
        public int agentLocationX;

        // Public property for agent's y location
        public int agentLocationY;

        // Public constructor for agent
		public Agent(int x, int y)
		{
            agentLocationX = x;
            agentLocationY = y;
        }

        /// <summary>
        /// Public method which checks the safe directions for an agent to move in
        /// </summary>
        /// <param name="agent"> Current agent object</param>
        /// <returns>A string of the agent's safe directions to move in</returns>
        public static string ShowSafeDirections(Agent agent)
        {
            string directions = "";

            if (ObstacleManager.obstacleList.Any(o => o.obeserve(agent.agentLocationX, agent.agentLocationY)))
            {
                return "Agent, your location is compromised. Abort mission.";
            }
            else
            {
                if (!ObstacleManager.obstacleList.Any(o => o.obeserve(agent.agentLocationX + 0, agent.agentLocationY - 1)))
                {
                    directions += "N";
                }
                if (!ObstacleManager.obstacleList.Any(o => o.obeserve(agent.agentLocationX + 0, agent.agentLocationY + 1)))
                {
                    directions += "S";
                }
                if (!ObstacleManager.obstacleList.Any(o => o.obeserve(agent.agentLocationX + 1, agent.agentLocationY + 0)))
                {
                    directions += "E";
                }
                if (!ObstacleManager.obstacleList.Any(o => o.obeserve(agent.agentLocationX - 1, agent.agentLocationY + 0)))
                {
                    directions += "W";
                }
                if (directions.Length == 0)
                {
                    return "You cannot safely move in any direction. Abort mission.";
                }
                else
                {
                    return "You can safely take any of the following directions: " + directions;
                }
            }
        }
    }
}