using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Astar
{
    internal static class UserPrompts
    {
        public static int GetDimension()
        {
            int dimension = 0;
            bool dimensionRestart = false;
            do
            {
                dimensionRestart = false;
                Console.WriteLine("Please enter a height for your board: ");
                try
                {
                    dimension = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Please enter a number.");
                    dimensionRestart = true;
                }
            } while (dimensionRestart);
            return dimension;
        }

        public static int GetStartChoice(int cellCount, List<int> obstaclesList)
        {
            int userStart = 0;
            bool startChoiceRestart = false;
            do
            {
                startChoiceRestart = false;
                Console.WriteLine("Please choose your starting position by entering the cell number: ");
                try
                {
                    userStart = int.Parse(Console.ReadLine());

                    if (userStart > (cellCount - 1) || userStart < 0)
                    {
                        Console.WriteLine("Please enter a number 0 - " + (cellCount - 1) + ".");
                        startChoiceRestart = true;
                    }
                    else if (obstaclesList.Contains(userStart))
                    {
                        Console.WriteLine("That space is occupied, please choose another: ");
                        startChoiceRestart = true;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Please enter a number 0 - " + (cellCount - 1) + " that does not contain an x.");
                    startChoiceRestart = true;
                }
            } while (startChoiceRestart);
            return userStart;
        }

        public static int GetEndChoice(int cellCount, int userStart, List<int> obstaclesList)
        {
            int userEnd = 0;
            bool endChoiceRestart = false;
            do
            {
                endChoiceRestart = false;
                Console.WriteLine("Please choose your destination position by entering the cell number: ");
                try
                {
                    userEnd = int.Parse(Console.ReadLine());

                    if (userEnd > (cellCount - 1) || userEnd < 0)
                    {
                        Console.WriteLine("Please enter a number 0 - " + (cellCount - 1) + " that does not contain an x.");
                        endChoiceRestart = true;
                    }
                    else if (userStart == userEnd)
                    {
                        Console.WriteLine("Congrats! You're already to your destination.");
                        Console.WriteLine("You think you're clever don't you. Go again.");
                    }
                    else if (obstaclesList.Contains(userEnd))
                    {
                        Console.WriteLine("That space is occupied, please choose another: ");
                        endChoiceRestart = true;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Please enter a number 0 - " + (cellCount - 1) + ".");
                    endChoiceRestart = true;
                }

            } while (endChoiceRestart);
            return userEnd;
        }

        public static bool RepeatPrompt()
        {
            bool repeat = false;
            bool repeatPromptRestart = false;
            do
            {
                repeatPromptRestart = false;
                Console.WriteLine("Would you like to run it again? Y/N");
                string userInput = Console.ReadLine();
                if (userInput == "Y" || userInput == "y")
                {
                    repeat = true;
                }
                else if (userInput == "N" || userInput == "n")
                {
                    repeat = false;
                }
                else
                {
                    Console.WriteLine("Please enter Y or N");
                    repeatPromptRestart = true;
                }
            } while (repeatPromptRestart);
            return repeat;
        }
    }
}
