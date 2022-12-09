using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace Madder_Libs
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //could not find where c:\\templates was on my computer, so I found a different way to get the path, and the file will stay in the same position now too when I submit it
            StreamReader streamReader = new StreamReader(System.IO.Path.GetFullPath("MadLibsTemplate.txt"));
            //the number of lines in the text file
            int lines = 0;
            //the final result, which is set to empty at the start
            string story = null;

            //running through the file and getting all of the lines in it
            string line = null;
            while ((line = streamReader.ReadLine()) != null)
            {
                lines++;
            }

            streamReader.Close();
            //creating a new string array to read each word in the file
            string[] madLibs = new string[lines];
            //getting the file again
            streamReader = new StreamReader(System.IO.Path.GetFullPath("MadLibsTemplate.txt"));
            //removing the line breaks and causing them to create actual line breaks
            line = null;
            int counter = 0;
            while ((line = streamReader.ReadLine()) != null)
            {
                madLibs[counter] = line;
                madLibs[counter] = madLibs[counter].Replace("\\n", "\n");

                counter++;
            }
            streamReader.Close();

            //prompt the user which story they want to use by reading the digit they enter
            Console.WriteLine("Please enter which story you would wish to run: 0-5");
            int nChoice = Convert.ToInt32(Console.ReadLine());
            //creating the words array and splitting each word into a seperate thing based off of where the space is
            string[] words = madLibs[nChoice].Split(' ');
            //strings that will contain the same piece of information that will be repeated, without the user having to type it in again
            string food = null;
            string firstName = null;
            //runninf through the array of words and checking each one
            foreach(string word in words)
            {
                //looking for the user prompts by looking for {
                if (word[0] == '{')
                {
                    //drow is needed because c# would not let me simply say word = something else, because I would be changing the foreach variable, which cannot be done from anything but itself
                    //so drow lets me save what will be shown to the user
                    string drow;
                    drow = word.Replace("{", "").Replace("}", "").Replace("_", " ");

                    //Checks to see if drow contains the specific repeated prompts, and does things accordingly
                    //First checks if Food is there, and that the string is already populated, and skips prompting the user, does this first because it would still trigger the next statement if not
                    if (drow.Contains("Food") && food != null && drow.Contains("that") == false)
                    {
                        story += food + " ";
                    } 
                    //Checks if Food is there and then saves the user input to a string that will be used for the previous if statement, checks for that to avoid different prompts that have food in them
                    else if (drow.Contains("Food") && drow.Contains("that") == false)
                    {
                        Console.Write("Input a {0}: ", drow);
                        food = Console.ReadLine();
                        story +=  food + " ";
                    }
                    //same as above, but now checks for First, in reference to First Name
                    else if (drow.Contains("First") && firstName != null)
                    {
                        story += firstName + " ";
                    }
                    else if (drow.Contains("First"))
                    {
                        Console.Write("Input a {0}: ", drow);
                        firstName = Console.ReadLine();
                        story += firstName + " ";
                    }
                    //if there is no first or food, then normal proceedings occur with user input
                    else 
                    {
                        Console.Write("Input a {0}: ", drow);
                        story += Console.ReadLine() + " "; 
                    }
                }
                //if no user input is needed, fill up the story array proper
                else
                {
                    story += word + " ";
                }
            }
            //write the story once it is all done
            Console.WriteLine(story);
            //prompt user to want to replay
            string replay;
            Console.WriteLine("Do you want to play again?");
            replay = Console.ReadLine();
            //if user inputs yes, calls the Main method and triggers everything again, I placed words in there simply to give it something, since args is not used at all, no errors occur
            if (replay.Contains("yes"))
            {
                Main(words);
            }
            //if user inputs no, then end the program
            else if(replay.Contains("no"))
            {
                Environment.Exit(0);
            }
        }
    }
}
