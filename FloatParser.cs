using System;
namespace Assignment2
{
    public class FloatParser : IParser<float>
    {
        /// <summary>
        /// Method which turns a string into a float. Repeats prompt if value is invalid
        /// </summary>
        /// <param name="input">The string input</param>
        /// <param name="prompt">The string prompt</param>
        /// <returns>A float value</returns>
        public float Parse(string? input, string prompt)
        {
            while (true)
            {
                try
                {
                    float number;
                    if (float.TryParse(input, out number))
                    {
                        return number;
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