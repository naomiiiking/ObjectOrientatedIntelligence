using System;
namespace Assignment2
{
    public class IntArrayParser : IParser<int[]>
    {
        private char separator;

        public IntArrayParser(char separator)
        {
            this.separator = separator;
        }

        /// <summary>
        /// Method which turns a string into an int array. Repeats prompt if input is invalid.
        /// </summary>
        /// <param name="input">The input string</param>
        /// <param name="seperator">The charactor symbol which acts as a seperator</param>
        /// <returns> An int array with x,y </returns>
        public int[] Parse(string? input, string prompt)
        {
            int[] array;
            while (true)
            {
                try
                {
                    array = new int[0];
                    string[]? stringArray = input.Split(separator);
                    if (stringArray.Length != 2)
                    {
                        throw new FormatException();
                    }
                    else
                    {
                        array = new int[stringArray.Length];
                        for (int i = 0; i < array.Length; i++)
                        {
                            int n;
                            string s = stringArray[i];
                            if (int.TryParse(s, out n))
                            {
                                array[i] = n;
                            }
                        }
                        break;
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
            return array;
        }
    }
}