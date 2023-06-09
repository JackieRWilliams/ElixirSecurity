using System.Text.Json;
internal class Program
{
    private static void Main(string[] args)
    {
            

        PromptForName();
    }



//Prompts user for name and redirects to store or answer questions as appropriate
        public static void PromptForName()
        {
            const string questionFile = "questions.json";

            Console.WriteLine("Hi, what is your name?");
            string userName = Console.ReadLine() ?? string.Empty;

            if (userName == string.Empty)
            {
                PromptForName();
            }
            else
            {
                //Create file name for file that will hold user answers
                string fileName;
                fileName = userName.ToLower() + "_answers.json";

                //Check if the user's file exists
                if (File.Exists(fileName))
                {
                    Console.WriteLine("Do you want to answer a security question?");
                    string response = Console.ReadLine() ?? string.Empty;

                    if (response.ToUpper() == "YES")
                        Answer(fileName, questionFile);
                    else
                        Store(fileName, questionFile);
                }
                else { Store(fileName, questionFile); }
            }
        }
//Asks user if they want to store security answer questions and persists them to a file as json
        public static void Store(string userFileName, string questionFilePath)
        {
            Console.WriteLine("Would you like to store answers to security questions?");
            string response = Console.ReadLine() ?? string.Empty;
            if (response.ToUpper() == "YES")
            {                
                string rawJson = File.ReadAllText(questionFilePath);
                List<Question> questions = JsonSerializer.Deserialize<List<Question>>(rawJson);
                List<Answer> answers = new List<Answer>();
                foreach (var q in questions)
                {
                    //Ask the user the question
                    Console.WriteLine(q.Text);
                    //Get the user response
                    string inputAnswer = Console.ReadLine() ?? string.Empty;
                    if (!String.IsNullOrEmpty(inputAnswer))
                    {
                        Answer a = new Answer();
                        a.QuestionId = q.Id;
                        a.Text = inputAnswer;
                        answers.Add(a);
                        //We can stop asking questions once we reach three answers from the user.
                        if (answers.Count == 3){
                            
                            // serialize answers as json
                            var json = JsonSerializer.Serialize(answers);
                            //write answers to file
                            File.WriteAllText(userFileName, json);

                            //return to original flow
                            PromptForName();
                        }
                    }
                }
                
            }
            else
                PromptForName();

        }
//Prompt the user to answer questions and determine if response is correct
        public static void Answer(string userFileName, string questionFilePath)
        { 
            string questionJson = File.ReadAllText(questionFilePath);
            string answerJson = File.ReadAllText(userFileName);
                List<Question> questions = JsonSerializer.Deserialize<List<Question>>(questionJson);
                List<Answer> answers = JsonSerializer.Deserialize<List<Answer>>(answerJson);
                foreach (var q in questions)
                {
                    Console.WriteLine(q.Text);
                    string inputAnswer = Console.ReadLine() ?? string.Empty;
                    if (!String.IsNullOrEmpty(inputAnswer))
                    {
                        string storedAnswer = answers.Find(x => x.QuestionId == q.Id).Text;
                        if (inputAnswer.ToUpper()  == storedAnswer.ToUpper())
                        {
                            Console.WriteLine("Congratulations. You answered the question correctly.");
                            PromptForName();
                        }                            
                    }
                }
                Console.WriteLine("You ran out of questions to answer.");
                PromptForName();
        }
    }
