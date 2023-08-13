//Challenge name: The Thing Namer 3000
// Goals:
//     •Rebuild the program above on your computer.
//     •Add comments near each of the four variables that describe what they store. You must use at least
//     one of each comment type (// and /* */).
//     •Find the bug in the text displayed and fix it.
//     •Answer this question: Aside from comments, what else could you do to make this code more understandable?
//            Answer: renaming variables to something more meaningful. 

namespace _04_comments.Challenges
{
    public class Challenge1
    {
        public static void Run()
        {
            // Find topic of conversation
            Console.WriteLine("What kind of thing are we talking about?");
            string a = Console.ReadLine();
            
            // Get description 
            Console.WriteLine("How would you describe it? Big? Azure? Tattered?");
            string b = Console.ReadLine();
            
            string c = "of Doom"; /* supplemental to thing */
            string d = "3000"; // number for theatrical purposes
            Console.WriteLine("The " + b + " " + a + " " + c + " " + d + "!");
        } 
    }
}


