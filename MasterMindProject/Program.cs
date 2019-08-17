//Simple mastermind game by -Josh Tempesta 2019

using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace MasterMind
{
    static class Program
    {
        const int difficulty = 4;
        const int maxattempts = 10;
        const bool allowDuplicates = true;
        //

        static int[] answer;
        static int attempt = 1;

        static void Main(string[] args)
        {
            Help();

            Answer();

            string input = null;
            do
            {
                Guess(input);

            } while (attempt < maxattempts);
        }

        private static void Answer()
        { //Generate the correct answer
            answer = new int[difficulty];
            for (var x = 0; x < difficulty; x++)
            {
                int r = 0;
                do
                {
                    r = new Random().Next(6);

                } while (r == 0 || !CheckForDuplicateAnswer(r));
                answer[x] = r;
            }
            Console.WriteLine("\n\nDEBUG: The secret answer is: " + string.Join(' ', answer) + "\n");
        }

        private static bool CheckForDuplicateAnswer(int r)
        { //returns true if duplicates are allowed or no duplicates detected, false if duplicates detected and not allowed
            if (!allowDuplicates)
            {
                foreach (int i in answer)
                {
                    if (r == i)
                        return false;
                }
            }

            return true;
        }

        private static void Help()
        {
            Console.WriteLine("MasterMind game by Josh Tempesta");
            Console.WriteLine("Each number in the answer is between 1 and 6");
            Console.WriteLine("You have 10 guesses to correctly guess the answer.");
            Console.WriteLine("Hints will be given in the form of a + for digits that are correct, a - for each digit that is correct but in the wrong position, and empty for not correct at all.");
        }

        private static bool Guess(string input)
        { //Process a user's recent guess
            do
            {
                Console.WriteLine("Attempt " + attempt + " of 10");
                Console.WriteLine("Enter your guess:");

                input = Console.ReadLine();

            } while (input == null || !ValidateInput(input));

            string hint = Hint(input);

            Console.WriteLine("Wrong. Your hint: "+hint);

            attempt++;

            return true;
        }

        private static string Hint(string input)
        { //Retrieves the hint given the validated user's input
            string joinedanswer = string.Join("", answer);           
            char[] distinctInput = input.Distinct().ToArray();

            int totallyCorrect = 0, positionCorrect = 0;
            for (int x = 0; x < distinctInput.Length; x++)
            {
                if (joinedanswer[x] == input[x])
                {
                    totallyCorrect++;
                }
                else if (joinedanswer.Contains(distinctInput[x]))
                {
                    positionCorrect++;
                }
            }

            string h = "";
            for (var x = 0; x < totallyCorrect; x++)
                h += "+";
            for (var x = 0; x < positionCorrect; x++)
                h += "-";

            return h;
        }

        private static bool ValidateInput(string input)
        { //Validates a user input to make sure they are playing by the rules
            if (input.Length < difficulty || input.Length > difficulty)
            {
                Console.WriteLine("The answer must be 4 digits long.");
                return false;
            }

            for (int x = 0; x < difficulty; x++)
            {
                int inputVal;
                if (!int.TryParse(input[x].ToString(), out inputVal))
                {
                    Console.WriteLine("The answer must be a number");
                    return false;
                }

                if (inputVal < 1 || inputVal > 6)
                {
                    Console.WriteLine("Each number in the answer must be between 1 and 6.");
                    return false;
                }
            }

            return true;
        }
    }
}
