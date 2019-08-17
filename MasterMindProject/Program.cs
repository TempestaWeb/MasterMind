//Simple mastermind game by -Josh Tempesta 2019

using System;
using System.Linq;
using System.Collections.Generic;

namespace MasterMind
{
    static class Program
    {
        //Game controls for debugging and fun
        const int difficulty = 4; //input and answer length
        const int maxattempts = 10;
        const bool allowDuplicates = false;

        const bool debug = true;
        //

        static int[] answer;
        static int attempt = 1;

        static void Main(string[] args)
        {
            Help();

            Answer();

            string input = null;
            bool correct;
            do
            {
                correct = Guess(input);

            } while (attempt <= maxattempts && !correct);

            if(!correct)
            {
                Console.WriteLine("Sorry! You LOST. Run the program to try again.\n");
            }
            Console.WriteLine("Press any key to exit the program.");
            Console.ReadKey();
        }

        private static void Answer()
        { //Generates and stores a new answer 
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
            if (debug)
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
            Console.Write("MasterMind game by Josh Tempesta Each number in the answer is between 1 and 6.\nYou have 10 guesses to correctly guess the answer. \nHints will be given in the form of a + for digits that are correct.\nAnd a - for each digit that is correct but in the wrong position.\nAn empty hint means not correct.");
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

            if (hint == null)
            {
                Console.WriteLine("CORRECT! YOU WIN! Woot :)");
                return true;
            }
            else
            {
                Console.WriteLine("Wrong. Your hint: " + hint);
                attempt++;
                return false;
            }            
        }

        private static string Hint(string input)
        { //Retrieves the hint given the validated user's input
            string joinedanswer = string.Join("", answer); //TODO: might be a better way to do this
            char[] distinctInput = input.Distinct().ToArray();

            var totallyCorrect = new List<char>();
            var positionCorrect = new List<char>();

            for (int x = 0; x < difficulty; x++)
            {
                if (joinedanswer[x] == input[x])
                {
                    totallyCorrect.Add(input[x]);
                }
            }
            for (int x = 0; x < difficulty; x++)
            {
                if (joinedanswer.Contains(input[x]) && !totallyCorrect.Contains(input[x]))
                {
                    positionCorrect.Add(input[x]);
                }
            }

            string h = "";
            for (var x = 0; x < totallyCorrect.Count; x++)
                h += "+";
            for (var x = 0; x < positionCorrect.Count; x++)
                h += "-";

            if(totallyCorrect.Count == answer.Length)
                return null; //no hint because this one is correct
            else
                return h; //receive a hint
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
