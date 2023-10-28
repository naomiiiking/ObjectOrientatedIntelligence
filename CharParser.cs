using System;
namespace Assignment2
{
	public class CharParser : IParser<char>
	{
        /// <summary>
        /// Method which returns a char value from an inputted string. Repeats prompt if input is invalid
        /// </summary>
        /// <param name="input">String input</param>
        /// <param name="prompt">String prompt</param>
        /// <returns>A char value</returns>
        public char Parse(string? input, string prompt)
        {
            while (true)
            {
                try
                {
                    if (char.TryParse(input, out char direction))
                    {
                        return direction;
                    }
                    else
                    {
                        throw new FormatException();
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input.");
                    Console.WriteLine(prompt);
                    input = Console.ReadLine();
                    continue;
                }
            }
        }
    }
}

