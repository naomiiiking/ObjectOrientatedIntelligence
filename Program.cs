using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Xml.Linq;

namespace Assignment2;
class Program
{

    static void Main()
    {
        /// Initialise obstacle list, obstacle manager, prompt and call the read
        /// option function
        List <Obstacle> ObstacleList = new List<Obstacle>();
        ObstacleManager obstaclemanager = new ObstacleManager(ObstacleList);
        string prompt;
        ReadOptions();

        /// <summary>
        /// Method which display's the programs options and calls the appropriate function once
        /// a valid input is given.
        /// </summary>
        void ReadOptions()
        {
            string initInput = ("Select one of the following options\n" +
            "g) Add 'Guard' obstacle\n" +
            "f) Add 'Fence' obstacle\n" +
            "s) Add 'Sensor' obstacle\n" +
            "c) Add 'Camera' obstacle\n" +
            "e) Add 'Electric Fence' obstacle\n" +
            "d) Show safe directions\n" +
            "m) Display obstacle map\n" +
            "p) Find safe path\n" +
            "x) Exit\n" +
            "Enter code:\n");
            Console.Write(initInput);
            string? userInput = Console.ReadLine();
            while (true)
            {
                List<string> validInputs = new() { "g", "f", "s", "c", "e", "d", "m", "p", "x" };
                try
                {
                    if (!validInputs.Any(valid => valid == userInput))
                    {
                        throw new FormatException();
                    }
                    switch (userInput)
                    {
                        case "g":
                            ReadGuard();
                            break;
                        case "f":
                            ReadFence();
                            break;
                        case "s":
                            ReadSensor();
                            break;
                        case "c":
                            ReadCamera();
                            break;
                        case "e":
                            ReadElectricFence();
                            break;
                        case "d":
                            ReadDirections();
                            break;
                        case "m":
                            ReadMap();
                            break;
                        case "p":
                            ReadPath();
                            break;
                        case "x":
                            Exit();
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid option");
                    Console.WriteLine("Enter code:");
                    userInput = Console.ReadLine();
                    continue;
                }
            }
        }

        /// <summary>
        /// Method which creates a guard obstacle off the user's valid input
        /// </summary>
        void ReadGuard()
        {
            prompt = "Enter the guard's location (X,Y):";
            Console.WriteLine(prompt);
            IParser<int[]> intArrayParser = new IntArrayParser(',');
            int[] guardLocation = intArrayParser.Parse(Console.ReadLine(), prompt);
            Guard guard = new(guardLocation[0], guardLocation[1]);
            ReadOptions();
        }

        /// <summary>
        /// Method which creates a fence obstacle off the user's valid input
        /// </summary>
        void ReadFence()
        {
            try
            {
                prompt = "Enter the location where the fence starts (X,Y):";
                Console.WriteLine(prompt);
                IParser<int[]> intArrayParserStart = new IntArrayParser(',');
                int[] fenceStart = intArrayParserStart.Parse(Console.ReadLine(), prompt);

                prompt = "Enter the location where the fence ends (X,Y):";
                Console.WriteLine(prompt);
                IParser<int[]> intArrayParserEnd = new IntArrayParser(',');
                int[] fenceEnd = intArrayParserEnd.Parse(Console.ReadLine(), prompt);

                int dx = fenceStart[0] - fenceEnd[0];
                int dy = fenceStart[1] - fenceEnd[1];

                /// One of dx and dy must be 0 otherwise the fence is not vertical
                /// or horizontal -> format error
                if (dx != 0 && dy != 0)
                {
                    throw new FormatException();
                }
                /// The fence is one klick in length -> format error
                if (fenceStart[0] == fenceEnd[0] && fenceStart[1] == fenceEnd[1])
                {
                    throw new FormatException();
                }
                else{
                    Fence fence = new(fenceStart, dx, dy);
                    ReadOptions();
                }          
            }
            catch(FormatException)
            {
                Console.WriteLine("Fences must be horizontal or vertical.");
                ReadFence();
            }
        }

        /// <summary>
        /// Method which creates a sensor obstacle off the user's valid input
        /// </summary>
        void ReadSensor()
        {
            try {
                prompt = "Enter the sensor's location (X,Y):";
                Console.WriteLine(prompt);
                IParser<int[]> intArrayParser = new IntArrayParser(',');
                int[] sensorLocation = intArrayParser.Parse(Console.ReadLine(), prompt);

                prompt = "Enter the sensor's range (in klicks):";
                Console.WriteLine(prompt);
                IParser<float> floatParser = new FloatParser();
                float sensorRange = floatParser.Parse(Console.ReadLine(), prompt);

                /// The sensor range must be greater than 0, if not then
                /// -> format error
                if(sensorRange > 0)
                {
                    Sensor sensor = new(sensorLocation, sensorRange);
                    ReadOptions();
                }
                else
                {
                    throw new FormatException();
                }   
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Sensor range must be greater than 0.");
                ReadSensor();
            }
        }

        /// <summary>
        /// Method which creates a camera obstacle off the user's valid input
        /// </summary>
        void ReadCamera()
        {
            try
            {
                prompt = "Enter the camera's location (X,Y):";
                Console.WriteLine(prompt);
                IParser<int[]> intArrayParser = new IntArrayParser(',');
                int[] cameraLocation = intArrayParser.Parse(Console.ReadLine(), prompt);

                prompt = "Enter the direction the camera is facing (n, s, e or w):";
                Console.WriteLine(prompt);
                IParser<char> charParser = new CharParser();
                char cameraDirection = charParser.Parse(Console.ReadLine(), prompt);

                /// Camera direction must be a valid cardinal direction if not
                /// -> format error
                if (cameraDirection == 'n' || cameraDirection == 's' || cameraDirection == 'e' || cameraDirection == 'w')
                {
                    Camera camera = new Camera(cameraLocation, cameraDirection);
                    ReadOptions();
                }
                else
                {
                    throw new FormatException();
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid direction.");
                ReadSensor();
            }
        }

        /// <summary>
        /// This is my custom obstacle!
        /// Method which creates a Nicki Minaj (custom) obstacle off the user's valid input
        /// The Nicki Minaj obstacle is created as a cross in the desired position. It will ask the
        /// user to finish Nicki Minaj lyrics and grow each time they get it wrong.
        /// </summary>
        void ReadElectricFence()
        {
            prompt = "Enter horizontal or vertical ('h' or 'v'):";
            Console.WriteLine(prompt);
            IParser<char> charParser = new CharParser();
            char direction = charParser.Parse(Console.ReadLine(), prompt);

            prompt = "Enter axis for the electric fence:";
            Console.WriteLine(prompt);
            IParser<float> floatParser = new FloatParser();
            float axis = floatParser.Parse(Console.ReadLine(), prompt);

            ElectricFence electricFence = new(direction, axis);
            ReadOptions();

        }

        /// <summary>
        /// Method which reads the safe directions one klick away from the user in each
        /// cardinal direction
        /// </summary>
        void ReadDirections()
        {
            prompt = "Enter your current location (X,Y):";
            Console.WriteLine(prompt);
            IParser<int[]> intArrayParser = new IntArrayParser(',');
            int [] agentLocation = intArrayParser.Parse(Console.ReadLine(), prompt);

            /// Creates an agent object at the desired locations then calls the
            /// Show safe directios method in the agent class to return the safe
            /// directions in char form
            Agent agent = new(agentLocation[0], agentLocation[1]);
            Console.WriteLine("{0}", Agent.ShowSafeDirections(agent));
            ReadOptions();
        }

        /// <summary>
        /// Method which displays the map in the user's desired valid location
        /// </summary>
        void ReadMap()
        {
            while (true)
            {
                prompt = "Enter the location of the top-left cell of the map (X,Y):";
                Console.WriteLine(prompt);
                IParser<int[]> intArrayParser = new IntArrayParser(',');
                int[] topLeft = intArrayParser.Parse(Console.ReadLine(), prompt);

                prompt = "Enter the location of the bottom-right cell of the map (X,Y):";
                Console.WriteLine(prompt);
                IParser<int[]> intArrayParser2 = new IntArrayParser(',');
                int[] bottomRight = intArrayParser2.Parse(Console.ReadLine(), prompt);
                try
                {
                    /// If the top left corner is north or west of the bottom right corner
                    /// -> format exception
                    if (topLeft[0] >= bottomRight[0] || topLeft[1] >= bottomRight[1])
                    {
                        throw new FormatException();
                    }
                    ObstacleManager.DisplayMap(topLeft, bottomRight);
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid map specification.");
                }
            }
            ReadOptions();

        }

        /// <summary>
        /// Method which reads the safe path (or if there is no path) from the agent to the objective in a printed
        /// string of chars
        /// </summary>
        void ReadPath()
        {
            prompt = "Enter your current location (X,Y):";
            Console.WriteLine(prompt);
            IParser<int[]> intArrayParser = new IntArrayParser(',');
            int[] agentLocation = intArrayParser.Parse(Console.ReadLine(), prompt);
            Agent agent = new Agent(agentLocation[0], agentLocation[1]);

            prompt = "Enter the location of your objective (X,Y):";
            Console.WriteLine(prompt);
            IParser<int[]> intArrayParser2 = new IntArrayParser(',');
            int [] objectiveLocation = intArrayParser2.Parse(Console.ReadLine(), prompt);
            Objective objective = new Objective(objectiveLocation[0], objectiveLocation[1]);

            /// Call the find path method unless the agent is already at the objective
            if (agentLocation[0] == objectiveLocation[0] && agentLocation[1] == objectiveLocation[1])
            {
                Console.WriteLine("Agent, you are already at the objective.");
            }
            else
            {
                _ = new FindPath(objective, agent, obstaclemanager);
            }
            ReadOptions();
        }

        /// <summary>
        /// Method to exit the program
        /// </summary>
        void Exit()
        {
            Environment.Exit(0);
        }
    }
}