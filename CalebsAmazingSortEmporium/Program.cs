using System.Diagnostics;

namespace CalebsAmazingSortEmporium
{
    /// <summary>
    /// Simple program to demonstrate sorting a list of elements that are assigned a number and a color.
    /// <br/>
    /// They are sorted first by number, then by color where Red < Yellow < White.
    /// </summary>
    /// <remarks>
    /// I did put some thought into abstracting more of the logic into separate functions and
    /// classes, but I decided against it primarily because of the simplicity of the code when
    /// read sequentially and that the code is meant to only run once with the program. If the
    /// program were to be run multiple times, I would have abstracted more of the logic into
    /// separate functions and classes.
    /// </remarks>
    internal class Program
    {
        static void Main(string[] args)
        {
            //We'll start by creating a List of random integers
            var random = new Random();
            var List = new List<ColoredNumber>();
            for (int i = 0; i < 100; i++)
            {
                //We'll create a new ColoredNumber object and assign it a random number between 1 and 100
                ColoredNumber curr_coloredNumber = new ColoredNumber {
                    Number = random.Next(1, 100)
                    //Note that we are not yet assigning a color to the ColoredNumber object. This will be done later
                };
                //We'll now add the ColoredNumber object to the List
                List.Add(curr_coloredNumber);
            }
            //Let's now print out the List to Debug
            Debug.WriteLine("Unsorted List:");
            for (int i = 0; i < List.Count; i++)
            {
                ColoredNumber item = List[i];
                Debug.WriteLine($"Index: {i} - Number: {item.Number} - Color: {item.Color}");
                //Don't really need to show the color to the end-user since it should be null currently, so we'll just print out the number
                Console.WriteLine($"Index: {i} - Number: {item.Number}");

            }
            ConsolePrettificationFunctions.PrintBoxSeparator();

            //We now need to assign each item in the List a color being Red, Yellow, or White
            //Position 1 will be red, position 2 will be yellow, and position 3 will be white. This will repeat out through the List
            //We'll use a for loop to do this
            Console.WriteLine("Assigning colors to the List...");
            for (int i = 0; i < List.Count; i++)
            {
                //Let's use a switch statement to assign the color
                switch (i % 3)
                {
                    case 0:
                        List[i].Color = "Red";
                        break;
                    case 1:
                        List[i].Color = "Yellow";
                        break;
                    case 2:
                        List[i].Color = "White";
                        break;
                }
                // Display item with colored text
                ColoredNumber.SetConsoleColor(List[i].Color);
                Console.WriteLine($"Index: {i} - Number: {List[i].Number} - Color: {List[i].Color}");
            }
            // Reset the console color to default
            Console.ResetColor();
            ConsolePrettificationFunctions.PrintBoxSeparator();

            //The next step involves removing elements from the List based on certain conditions
            //Conditions:
            // 1. If the number is even and Red, remove it from the List
            // 2. If the number is odd and Yellow, remove it from the List
            // 3. If the number is divisible by 3 and is White, remove it from the List
            //We'll use another for loop to do this
            var newList = new List<ColoredNumber>();
            for (int i = 0; i < List.Count; i++)
            {
                ColoredNumber curr_coloredNumber = List[i];
                //I'm going to first write a switch statement that check the color of the
                //current ColoredNumber object, curr_coloredNumber. This is because I know
                //the possible colors are mutually exclusive
                switch (curr_coloredNumber.Color)
                {
                    case "Red":
                        //If the color is red, we need to check if the number is even
                        if (curr_coloredNumber.Number % 2 == 0)
                        {
                            //If the number is even, we need to remove it from the List
                            continue;
                            //List.Remove(curr_coloredNumber);
                        }
                        break;
                    case "Yellow":
                        //If the color is yellow, we need to check if the number is odd
                        if (curr_coloredNumber.Number % 2 == 1)
                        {
                            //If the number is odd, we need to remove it from the List
                            continue;
                            //List.Remove(curr_coloredNumber);
                        }
                        break;
                    case "White":
                        //If the color is white, we need to check if the number is divisible by 3
                        if (curr_coloredNumber.Number % 3 == 0)
                        {
                            //If the number is divisible by 3, we need to remove it from the List
                            continue;
                            //List.Remove(curr_coloredNumber);
                        }
                        break;
                }
                //If we've made it this far, we know that the current ColoredNumber object is not to be removed from the List. We can add it to the newList
                newList.Add(curr_coloredNumber);
            }

            //Lastly, we need to sort the newList. We first sort the newList by number, then by color where Red < Yellow < White
            //We'll use the OrderBy method to do this
            var SortedList = newList.OrderBy(x => x.Number).ThenBy(x => ColoredNumber.GetColorOrder(x.Color)).ToList();

            //Let's now print out the sorted newList to Debug
            Debug.WriteLine("Sorted List:");
            for (int i = 0; i < SortedList.Count; i++)
            {
                ColoredNumber item = SortedList[i];
                // Prepare the base message
                string message = $"Index: {i} - Number: {item.Number} - Color: {item.Color}";

                // Check if the current item and the previous or next item have the same number but different colors
                bool isPreviousItemMatching = (i > 0)
                                              && (SortedList[i].Number == SortedList[i - 1].Number)
                                              && (SortedList[i].Color != SortedList[i - 1].Color);
                bool isNextItemMatching = (i < SortedList.Count - 1)
                                          && (SortedList[i].Number == SortedList[i + 1].Number)
                                          && (SortedList[i].Color != SortedList[i + 1].Color);

                // Check if the current item and the previous item have the same number but different colors
                if (isPreviousItemMatching || isNextItemMatching)
                {
                    message += " <-- These were sorted by color!";
                }

                Debug.WriteLine(message);
                ColoredNumber.SetConsoleColor(item.Color);
                Console.WriteLine(message);
            }
            // Reset the console color to default
            Console.ResetColor();

        }
        /// <summary>
        /// Simple class to hold a number and a color. Makes it easier to bind the values
        /// together without having to create a dictionary.
        /// </summary>
        public class ColoredNumber
        {
            public int? Number { get; set; }
            public string? Color { get; set; }
            /// <summary>
            /// This method is used to sort the List by color. It returns an integer that is used to sort the List
            /// </summary>
            /// <param name="color"></param>
            /// <returns></returns>
            public static int GetColorOrder(string? color)
            {
                switch (color)
                {
                    case "Red": return 1;
                    case "Yellow": return 2;
                    case "White": return 3;
                    default: return 4; // Unknown or null colors come last
                };
            }
            /// <summary>
            /// This method is used to set the console color. It takes in a string and sets the console color based on the string
            /// </summary>
            /// <param name="color"></param>
            public static void SetConsoleColor(string? color)
            {
                
                switch (color)
                {
                    case "Red":
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    case "Yellow":
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                    case "White":
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    
                };
            }
        }
        /// <summary>
        /// Any functions that are used to prettify the console output are stored in this class
        /// </summary>
        public static class ConsolePrettificationFunctions
        {
            /// <summary>
            /// This method is used to print a box separator to the console. It's used to make the console
            /// output look nicer and to be able to more easily distinguish between different sections of the output
            /// </summary>
            public static void PrintBoxSeparator()
            {
                string horizontalLine = new string('═', 40); // Box drawing character for horizontal line
                Console.WriteLine($"\n╔{horizontalLine}╗"); // Top part of the box
                Console.WriteLine($"║{new string(' ', 40)}║"); // Empty space inside the box
                Console.WriteLine($"╚{horizontalLine}╝\n"); // Bottom part of the box
            }
        }
    }
}
