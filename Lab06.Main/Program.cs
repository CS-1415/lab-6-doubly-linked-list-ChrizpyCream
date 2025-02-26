using System; 
using Lab06; //  contains the implementation of the DoublyLinkedList class.

namespace Lab06.Main
{
    class Program
    {
        static void Main(string[] args)
        {
            // Creating an instance of DoublyLinkedList to store integers.
            DoublyLinkedList<int> list = new DoublyLinkedList<int>();

            // Prompting the user to enter numbers to add to the list.
            Console.WriteLine("Enter numbers to add to the list. Press Escape to end.");

            while (true) //loop to continuously take user input.
            {
                var key = Console.ReadKey(intercept: true); // reading a key press without displaying it in the console.

                if (key.Key == ConsoleKey.Escape) // exit
                {
                    break;
                }

                if (char.IsDigit(key.KeyChar)) // checking if the pressed key is a numeric digit.
                {
                    int value = int.Parse(key.KeyChar.ToString()); // Converting the character input into an integer.
                    list.AddLast(value); // adding the integer to the end of the doubly linked list.
                    Console.WriteLine($"\nAdded {value} to the list."); // Informing .
                }
                else
                {
                    Console.WriteLine("\nPlease enter a valid digit."); // Informing the user that only digits are allowed.
                }
            }

            // Displaying the final list of numbers added by the user.
            Console.WriteLine("\nFinal list:");
            foreach (int i in list) // iterating through the doubly linked list.
            {
                Console.WriteLine(i); // print
            }
        }
    }
}
