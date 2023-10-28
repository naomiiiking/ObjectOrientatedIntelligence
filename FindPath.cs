using System.Xml.Linq;

namespace Assignment2
{
    /// <summary>
    /// Class used to create a safe path from the agent to the objective. This class uses a modfied
    /// version of the A* algoritm to find the most cost-effective (shortest) path from the agent to the objective
    /// </summary>
    public class FindPath
    {
        // Private property for the list of obstacles
        private List<Obstacle> obstacleList { get; set; }

        /// <summary>
        /// Public constructor for the find path class using the current obstacle list, objective location and agent
        /// location
        /// </summary>
        /// <param name="ObjectiveNode">The objective node</param>
        /// <param name="AgentNode">The orignal agent node</param>
        /// <param name="ObstacleManager">The current list of obstacles in the obstacle manager</param>
        public FindPath(Objective ObjectiveNode, Agent AgentNode, ObstacleManager ObstacleManager)
        {
            Node objectiveNode = new Node();
            objectiveNode.x = ObjectiveNode.ObjectiveLocation.x;
            objectiveNode.y = ObjectiveNode.ObjectiveLocation.y;

            Node agentNode = new Node();
            agentNode.x = AgentNode.agentLocationX;
            agentNode.y = AgentNode.agentLocationY;

            agentNode.setDistance(ObjectiveNode.ObjectiveLocation.x, AgentNode.agentLocationY);

            obstacleList = ObstacleManager.obstacleList;

            findpath(objectiveNode, agentNode, obstacleList);
        }

        /// <summary>
        /// Puiblic method to find the most effective path from the agent to the objective
        /// </summary>
        /// <param name="objectiveNode"></param>
        /// <param name="agentNode"></param>
        /// <param name="obstacleList"></param>
        private void findpath(Node objectiveNode, Node agentNode, List<Obstacle> obstacleList)
        {
            // Set the distance from the agent node to the objecttive node
            agentNode.setDistance(objectiveNode.x, objectiveNode.y);

            // Check that the objective is not blocked by an obstacle
            if (ObstacleManager.obstacleList.Any(o => o.obeserve(objectiveNode.x, objectiveNode.y)))
            {
                Console.WriteLine("The objective is blocked by an obstacle and cannot be reached.");
                return;
            }

            // Create the open and closed node lists, place the starting agent node in the open list
            List<Node> openNodes = new List<Node>();
            openNodes.Add(agentNode);
            
            var closedNodes = new List<Node>();
            int iterationSinceCostDecrease = 0;
            int prevCostDistance = 0;

            // Continue looping until either all nodes have been checked or goal is found
            // or 100 nodes have been checked since the cost has decreased (the chcked nodes are gettting
            // further away from the objective meaning there is no safe path)
            while (iterationSinceCostDecrease < 500)
            {
                // Find the node in the openNodes list with the lowest cost distance (current most
                // cost effective path to the objective)
                var checkNode = openNodes.OrderBy(x => x.costDistance).First();
                

                // If the current node being checked matches the location of the objective, print out safe path
                if (checkNode.x == objectiveNode.x && checkNode.y == objectiveNode.y)
                {
                    PrintSafePath(checkNode);
                }

                // Remove the node from the open list and add it too the closed list so it's not checked again
                closedNodes.Add(checkNode);
                openNodes.Remove(checkNode);

                // Get the nodes next to the current node and store as a list
                var safeNodes = GetSafeNodes(checkNode, objectiveNode, obstacleList);

                foreach (var safeNode in safeNodes)
                {
                    // Checks if node is already visited (in closedNodes list)
                    if (closedNodes.Any(X => X.x == safeNode.x && X.y == safeNode.y))
                    {
                        continue;
                    }

                    // Check that the node is in the open nodes list
                    if (openNodes.Any(X => X.x == safeNode.x && X.y == safeNode.y))
                    {
                        // Check if the current node is part of the most cost efficent path to the objective
                        var existingNode = openNodes.First(X => X.x == safeNode.x && X.y == safeNode.y);
                        if (existingNode.costDistance > checkNode.costDistance)
                        {
                            openNodes.Remove(existingNode);
                            openNodes.Add(safeNode);
                        }
                    }
                    else
                    {
                        // Add the node ot the open node list as it has not been checked before
                        openNodes.Add(safeNode);
                    }
                }
                if(checkNode.costDistance > prevCostDistance)
                {
                    
                    iterationSinceCostDecrease = 0;
                }
                else
                {
                    iterationSinceCostDecrease++;
                }
                prevCostDistance = checkNode.costDistance;
            }
            Console.WriteLine("There is no safe path to the objective.");
        }

        /// <summary>
        /// Private method to print out the safe path once found
        /// </summary>
        /// <param name="checkNode">The node which was last check, this being the objective location if a safe path is found</param>
        private void PrintSafePath(Node checkNode)
        {
            var node = checkNode;
            string safePath = "";

            while (true)
            {
                if (node.parent == null)
                {
                    Console.WriteLine("The following path will take you to the objective:");
                    Console.WriteLine(safePath);
                    return;
                }
                else
                {
                    int movementX = node.x - node.parent.x;
                    int movementY = node.y - node.parent.y;
                    if (movementX == 0 && movementY == -1)
                    {
                        safePath = "N" + safePath;
                    }
                    else if (movementX == 0 && movementY == 1)
                    {
                        safePath = "S" + safePath;
                    }
                    else if (movementX == -1 && movementY == 0)
                    {
                        safePath = "W" + safePath;
                    }
                    else if (movementX == 1 && movementY == 0)
                    {
                        safePath = "E" + safePath;
                    }
                    node = node.parent;
                }
            }
        }

        private List<Node> GetSafeNodes(Node currentNode, Node objectiveNode, List<Obstacle> obstacleList)
        {
            // Create a list of the 4 possible nodes that can be travelled to by the parent node
            var possibleNodes = new List<Node>()
         {
             new Node{x = currentNode.x, y = currentNode.y-1, parent = currentNode, cost = currentNode.cost + 1 },
             new Node{x = currentNode.x, y = currentNode.y+1, parent = currentNode, cost = currentNode.cost + 1 },
             new Node{x = currentNode.x+1, y = currentNode.y, parent = currentNode, cost = currentNode.cost + 1 },
             new Node{x = currentNode.x-1, y = currentNode.y, parent = currentNode, cost = currentNode.cost + 1 },
         };

            // For each node in the above list, set the distance to the objective
            possibleNodes.ForEach(node => node.setDistance(objectiveNode.x, objectiveNode.y));

            // Filter out the unsafe nodes (nodes that are in teh same location as an obstacle in the obstacle list
            var safeNodes = possibleNodes.Where(node =>
            {
                bool safeNode = !ObstacleManager.obstacleList.Any(o => o.obeserve(node.x, node.y));
                return safeNode;
            }).ToList();

            return safeNodes;
        }
    }
}