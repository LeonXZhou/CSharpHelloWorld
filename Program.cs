
const string CREATEACCOUNT = "create account";
const string LOGIN = "login";
const string USERNAME = "username";
const string PASSWORD = "password";
const string EMAIL = "email";
const string EXIT = "exit";

const string BASEPATH = "./accounts/";

bool keepPrompting = true;

if (!Directory.Exists(BASEPATH))
{
    DirectoryInfo di;
    try
    {
        // Try to create the directory.
        di = Directory.CreateDirectory(BASEPATH);
        Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(BASEPATH));
    }
    catch (UnauthorizedAccessException e)
    {
        Console.WriteLine("The caller does not have the required permission to create `{0}`", BASEPATH);
        return;
    }
}

while (keepPrompting)
{
    Console.WriteLine("Hello, World! what would you like to do? Options:");
    Console.WriteLine($"-{CREATEACCOUNT}");
    Console.WriteLine($"-{LOGIN}");
    Console.WriteLine($"-{EXIT}");

    var userActionChoice = Console.ReadLine();
    userActionChoice = userActionChoice?.Trim().ToLower();

    switch (userActionChoice)
    {
        case CREATEACCOUNT:
            Console.WriteLine($"You picked {CREATEACCOUNT}");

            Console.WriteLine($"What is your {USERNAME}");
            var username = Console.ReadLine();
            if (File.Exists(BASEPATH + username))
            {
                Console.WriteLine($"that username is taken");
                break;
            }

            Console.WriteLine($"What is your {EMAIL}");
            var email = Console.ReadLine();

            Console.WriteLine($"What is your {PASSWORD}");
            var passwordAttemptOne = Console.ReadLine();

            Console.WriteLine($"Confirm your password {PASSWORD}");
            var passwordAttemptTwo = Console.ReadLine();

            if (passwordAttemptOne == passwordAttemptTwo)
            {
                File.WriteAllText(BASEPATH + username, $"{username}\n");
                File.AppendAllText(BASEPATH + username, $"{email}\n");
                File.AppendAllText(BASEPATH + username, $"{passwordAttemptTwo}\n");
                Console.WriteLine("account created");
                break;
            }
            else
            {
                Console.WriteLine($"Passwords did not match");
                break;
            }
        case LOGIN:
            Console.WriteLine($"You picked {LOGIN}");
            Console.WriteLine($"What is your {USERNAME}");
            var loginUserName = Console.ReadLine();

            Console.WriteLine($"What is your {PASSWORD}");
            var loginPassword = Console.ReadLine();
            if (File.Exists(BASEPATH + loginUserName))
            {
                List<string> fileInfo = File.ReadAllLines(BASEPATH + loginUserName).ToList();
                bool match = fileInfo[0] == loginUserName && fileInfo[2] == loginPassword;
                if (match)
                {
                    Console.WriteLine("You are logged in");
                    keepPrompting = false;
                    break;
                }
                else
                {
                    Console.WriteLine("no match found for username or password");
                    break;
                }
            }
            else
            {
                Console.WriteLine("no match found for username or password");
                break;
            }
        case EXIT:
            keepPrompting = false;
            break;
        default:
            Console.WriteLine($"{userActionChoice} is not a valid action");
            break;
    }
}
