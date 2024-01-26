// author: Rod M
// topic: Capstone Project - Unit 2
// task: Pig Latin Translator

Console.WriteLine("--- Welcome to the Pig Latin Translator! ---");

string addAY = "ay";
bool playAgain = true;

while (playAgain)
{

    Console.WriteLine("What word(s) would you like to play with?");
    string incomingString = Console.ReadLine();


    /* Note-To-Self: LAB SUMMARY
     * 
     * Vowels: a e i o u
     * Consonants: B, C, D, F, G, H, J, K, L, M, N, P, Q, R, S, T, V, W, X, Y, Z 
     * - treat “y” as a consonant.
     * 
     * Task 1:
     * 
     * #1 if a word starts with a VOWEL, just add “way” onto the ending
     */

    // TASK 1 SUMMARY
    // 1a - check how many words were entered, check for white spaces after trimming
    // 1b - loop through string array
    // 1c - evaluate each word in string array
    //          if starts with VOWEL -> create flipVowel method
    //          else -> create flipConsonant method (Task 2)

    /*
     * Task 2
     * 
     * #2 if a word starts with a consonant, move all of the consonants 
     * that appear before the first vowel to the end of the word, 
     * then add “ay” to the end of the word
     * 
     * Example:
     * Input: {this sentence exists here}
     * Output: Isthay entencesay existsway erehay
     */

    List<string> list = new List<string>();
    List<string> pigLatin = new List<string>();

    // debug
    //Console.WriteLine($"You entered: {incomingString}");
    if (incomingString != null)
    {
        // prepare user input
        incomingString = incomingString.Trim(); // trim edges
        incomingString = incomingString.ToLower(); // simplify translation

        // 1a - check how many words were entered, check for white spaces after trimming
        incomingString = incomingString.Replace(' ', '+'); // use '+' to facilitate debugging
        list = incomingString.Split('+').ToList(); // split user input into list items
 
        if (list != null)
        {

            Console.WriteLine();
            Console.WriteLine("------ Pre-processing ------");

            foreach (string wordItem in list)
            {

                // trying to avoid redundant strings/chars
                if ( wordItem.Length == 0) // ignore empty characters
                {
                    //this will break the code during runtime
                    //int indexOfSpecialItem = list.IndexOf(stringItem);
                    //list.RemoveAt(indexOfSpecialItem);

                    Console.WriteLine("Contains EMPTY CHAR");
                } 
                else if (wordItem.Length > 0) 
                { 
                    // processing list
                    // Console.WriteLine($">>{stringItem}<<<"); // debugging

                    // 1c - evaluate each word in string array
                    //          if starts with VOWEL -> create flipVowel method
                    //          else -> create flipConsonant method (Task 2)
                    char[] englishChars = wordItem.ToCharArray();
                    char letterOne = englishChars[0];

                    // Vowels: a e i o u
                    switch (letterOne)
                    {
                        case 'a':
                        case 'e':
                        case 'i':
                        case 'o':
                        case 'u':
                            // process VOWEL
                            Console.WriteLine($">> PIG-LATIN Vowel: {flipVowel(wordItem, pigLatin)}");
                            break;
                        default:
                            // process CONSONANT
                            Console.WriteLine($">> PIG-LATIN Cons: {flipConsonant(englishChars, pigLatin)}");
                            break;


                    }

                    
                    //Console.WriteLine($">> ASCII for {letterOne} is {getASCII(letterOne)}");
                }
            }

            Console.WriteLine("\n-----------------");
            Console.WriteLine(">>> Results <<<");
            foreach (string latinItem in pigLatin) { Console.Write($"{latinItem} "); }
            Console.WriteLine();

        }

    }

    Console.WriteLine("\nWould you like to play again? yes/no");
    string replay = Console.ReadLine();
    if( !replay.ToLower().Trim().Contains("y"))
    {
        playAgain = false;
    }

}

static string flipVowel(string englishChars, List<string> pigLatin)
{
    //#1 if a word starts with a VOWEL, just add “way” onto the ending

    pigLatin.Add(englishChars + "ay");
    
    return englishChars + "ay";
}

static string flipConsonant(char[] englishChars, List<string> pigLatin)
{
    /*
    *Task 2
    *
    * #2 if a word starts with a consonant, move all of the consonants 
    *   that appear before the first vowel to the end of the word, 
    *   then add “ay” to the end of the word
    */

    // STEP ONE - how many letters in this word?
    int wordLength = englishChars.Length;

    // STEP TWO - we already know the first letter is a consonant based on previous switch logic
    List<char> consonantChars = new List<char>();
    consonantChars.Add(englishChars[0]);


    bool isConsonant = true;
    while (isConsonant) // check for more consecutive consonants
    {
        for (int i = 1; i < wordLength; i++) // start at second letter
        {
            if (checkConsonant(englishChars[i]))
            {
                consonantChars.Add(englishChars[i]);
            } 
            else
            {
                isConsonant = false; 
                break;
            }
        }


    }

    // debug
    foreach (char c in consonantChars) { Console.WriteLine($">> Consonant char {c}"); }

    char[] latinWord = new char[wordLength + 2];

    // EXAMPLE: time --> ime    t    ay
    // EXAMPLE: thrive --> ive thr ay
    // EXAMPLE: thin --> in th ay
    // EXAMPLE: yo --> oy ay

    // pseudo code
    // latin word = wordLength - consonantChar
    // string halfWord = englishChars( cccAAA )

    string englishWord = new string(englishChars);
    Console.WriteLine(">> English Word " + englishWord);

    int startIndex = consonantChars.Count();
    int subLength = englishChars.Count() - consonantChars.Count();
    string semiWordBeginWithVowel = englishWord.Substring(
                                startIndex,
                                subLength);
    Console.WriteLine(">> Partial word: " + semiWordBeginWithVowel);

    // combine semiWord with consonant list
    string consonantWord = new string(consonantChars.ToArray());
    string flippedWord = semiWordBeginWithVowel + consonantWord;

    // complete pig latin word
    string latinString = flippedWord + "ay";

    // Console.WriteLine($">> PIG-LATIN debug {latinString}"); // debug

    pigLatin.Add(latinString);

    return latinString;
}

static bool checkConsonant(char ch)
{
    switch (ch)
    {
        case 'a':
        case 'e':
        case 'i':
        case 'o':
        case 'u':
            return false;
        default:
            return true;
    }
}