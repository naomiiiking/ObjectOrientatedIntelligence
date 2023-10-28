using System;
namespace Assignment2
{
    /// <summary>
    /// Public interface to implement polymorphism to parse strings into either int arrays, floats or characters
    /// </summary>
    /// <typeparam name="T">The type of the desired output</typeparam>
    public interface IParser<T>
    {
        T Parse(string? input, string prompt);
    }
}