using System.Data.Common;
using System.Collections.Generic;
using System.IO;

string defaultLocation = "Wizard Lane";
string currentLocation = "Wizard Lane";
string nextLocation = "Wizard Lane";
string gender = "undefined";
string user = "";
string path = System.IO.Directory.GetCurrentDirectory();
string intro = File.ReadAllText(path+"\\WizardLaneLogo.txt"); ;
bool introDone = false;
bool introInitialized = false;
bool choosing = false;
bool listedChoices = false;
List<string> currentChoices = new List<string>();
List<List<string>> events = new List<List<string>>();

List <string> choiceList1 = new List<string>();
choiceList1.Add("Wizard Lane");
choiceList1.Add("Head Wizard");
choiceList1.Add("The Guild of Witches");
List<string> choiceList2 = new List<string>();

Console.OutputEncoding = System.Text.Encoding.UTF8;

addEvent("Wizard Lane", "undefined", $"You arrive at Wizard Lane. In front of you stands a little wooden sign that says 'Welcome to Wizard Lane - Population: 87'. A man comes up to you and says 'welcome {user}!'", choiceList1, events);
addEvent("The Guild of Witches", "undefined", $"You arrive at the The Guild of Witches. 'welcome {user}!'", choiceList2, events);

//this bit of code is used to add new events to the game
static void addEvent(string location, string graphicPath, string displayText, List<string> choices, List<List<string>> events)
{
    List<string> newEvent = new List<string>();
    newEvent.Add(location);
    newEvent.Add(graphicPath);
    newEvent.Add(displayText);
    foreach (var i in choices)
    {
        newEvent.Add(i);
    }

    events.Add(newEvent);
}

void main()
{
    if (introDone)
    {
        if (!choosing)
        {
            for (var i = 0; i < events.Count; i++)
            {
                if (events[i][0] == nextLocation)
                {
                    //this section of code runs once the user has chosen an option

                    //this code shows text art if there is any to display
                    if (events[i][1] != "") Console.WriteLine(events[i][1]);
                    //prints the current event's text
                    Console.WriteLine(events[i][2]);
                    currentLocation = nextLocation;
                    listedChoices = false;
                    currentChoices.Clear();
                    //sets the code to go back to "choosing" mode so the user can move forward from the current event
                    choosing = true;
                }
            }
        }
        else
        {
            if (!listedChoices)
            {
                for (var i = 0; i < events.Count; i++)
                {
                    if (events[i][0] == currentLocation)
                    {
                        //if there's no options in an option the game will assume that you have reached an ending slide
                        //this if statement checks if there's options to run
                        if (events[i].Count > 3)
                        {
                            //this bit of code lists the user's options for continuing on from the current event
                            Console.WriteLine();
                            Console.WriteLine("What do you do next?");
                            int locationNum = 1;
                            for (var i2 = 3; i2 < events[i].Count; i2++)
                            {
                                Console.WriteLine(Convert.ToString(locationNum) + ". " + events[i][i2]);
                                locationNum++;
                                currentChoices.Add(events[i][i2]);
                                System.Threading.Thread.Sleep(120);
                                if (i2 == events[i].Count - 1)
                                {
                                    listedChoices = true;
                                    locationNum = 0;
                                }
                            }
                        }
                        else
                        {
                            //this code lets the user start over if there's no options to go forward at this event
                            listedChoices = true;
                            currentLocation = "";
                            Console.WriteLine("You have reached an ending slide! Press any key to start over!");
                            Console.ReadKey();
                            Console.Clear();
                            introInitialized = false;
                            introDone = false;
                            choosing = false;
                            listedChoices = false;
                            currentLocation = defaultLocation;
                            nextLocation = defaultLocation;
                        }
                    }
                }
            }
            else
            {
                //this section allows the user to pick which event they want to choose next
                if (currentChoices.Count() > 0)
                {
                    Console.Write("Type the number of the action you want to perform: ");
                    char answer = Console.ReadKey(false).KeyChar;
                    int chosenLocation = 0;
                    if (Int32.TryParse(Convert.ToString(answer), out int answerResult))
                    {
                        chosenLocation = answerResult - 1;
                    }
                    for (var i = 0; i < currentChoices.Count; i++)
                    {
                        if (chosenLocation == i)
                        {
                            Console.WriteLine();
                            Console.WriteLine($"You went to {currentChoices[i]}");
                            nextLocation = currentChoices[i];
                            choosing = false;
                        }
                    }
                }
            }
        }
        //Console.WriteLine("testing");
    }
    else
    {
        if (!introInitialized)
        {
            //this code shows the game's intro and ask the user's gender and name.
            Console.WriteLine(intro);
            Console.WriteLine();
            Console.WriteLine("Press any key to start the adeventure!");
            Console.ReadKey(true);
            Console.WriteLine("Journeying to the lands of the wizards will be exciting, but first what is your name adventurer?");
            Console.Write("My name is: ");
            user = Console.ReadLine();
            Console.WriteLine($"So {user} is it? Suits you! So what do you identify as {user}? Type the name of the option you want to choose");
            Console.WriteLine("1. Witch");
            Console.WriteLine("2. Wizard");
            string tempGender = Console.ReadLine().ToLower();
            if (tempGender == "wizard" || tempGender == "witch")
            {
                gender = tempGender;
                Console.WriteLine($"Ah! So you're a {gender}. We'll be arriving shortly {user}, why don't you take a nap during the last leg of our journey?");
                Console.WriteLine();
                introDone = true;
            }
            introInitialized = true;
        }
    }
}

//main loop
while (true)
{
    main();
}