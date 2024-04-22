using System;

namespace NarrativeProject
{
    internal abstract class Room
    {
        internal abstract string CreateDescription();
        internal abstract void ReceiveChoice(string choice);
    }
}

namespace NarrativeProject.Rooms
{
    internal class LobbyRoom : Room
    {
        internal override string CreateDescription() =>
@"Congratulations! You are in the LobbyRoom.
Now it's either life or death! Collect all 5 Keys by solving riddles and make your way to the escape room to escape this nightmare!
In front of you is [Hall1].
On your left is the [GardenRoom].
On your right is the [KitchenRoom].";

        internal override void ReceiveChoice(string choice)
        {
            switch (choice)
            {
                case "Hall1":
                    Console.WriteLine("You enter the Hall.");
                    Game.Transition<HallRoomOne>();
                    break;
                case "GardenRoom":
                    Console.WriteLine("You enter the Garden.");
                    Game.Transition<GardenRoom>();
                    break;
                case "KitchenRoom":
                    Console.WriteLine("You go up and enter your Kitchen.");
                    Game.Transition<KitchenRoom>();
                    break;
                default:
                    Console.WriteLine("Invalid command.");
                    break;
            }
        }
    }
}//lobby



namespace NarrativeProject.Rooms
{
    internal class GardenRoom : Room
    {
        private bool riddleAnswered = false;
        private bool lifeKeyObtained = false;

        internal override string CreateDescription() =>
@"You are in the Garden. You see beautiful plants of every kind. 
You notice a man standing towards the far end of the Garden surrounded by plants, and you decide to approach him.
You have 2 chances to provide the right answer to his riddle:

I am a sentinel, standing tall yet unseen. My roots are my anchor, my leaves are my crown. 
I am a giver of life, yet I take without a sound. What am I?

Type your answer or [return] to go back to the lobby.";

        internal override void ReceiveChoice(string choice)
        {
            if (!riddleAnswered)
            {
                if (string.IsNullOrWhiteSpace(choice))
                {
                    Console.WriteLine("You choose not to answer the riddle and return to the lobby.");
                    Game.Transition<LobbyRoom>();
                    return;
                }

                if (choice.ToLower() == "tree")
                {
                    Console.WriteLine("You answered the riddle correctly. Your health increases by 15, and you obtained the Life key.");
                    Game.DecreasePlayerHealth(-15); // Increase player health by 15
                    lifeKeyObtained = true; // Obtain the Life key
                }
                else
                {
                    Console.WriteLine("You answered the riddle incorrectly. You lose 15 health and are sent back to the lobby.");
                    Game.DecreasePlayerHealth(15); // Decrease player health by 15
                }

                riddleAnswered = true; // Set riddle as answered
                Game.Transition<LobbyRoom>(); // Send player back to the lobby
            }
            else
            {
                Console.WriteLine("You've already answered the riddle. You decide to explore the garden further.");
            }
        }
    }
}//garden


namespace NarrativeProject.Rooms
{
    internal class KitchenRoom : Room
    {
        internal override string CreateDescription() =>
@"You have entered the kitchen. Glass is shattered, utensils thrown around everywhere. Quite odd.
In front of you is a dark alleyway that takes you to the [Cave]. Hmmm!?
On your left is [Hall2].
Or would you like to go back to the [LobbyRoom].";

        internal override void ReceiveChoice(string choice)
        {
            switch (choice.ToLower())
            {
                case "hall2":
                    Console.WriteLine("You decide to go to Hall 2.");
                    Game.Transition<HallRoomTwo>();
                    break;
                case "lobbyroom":
                    Console.WriteLine("You decide to return to the LobbyRoom.");
                    Game.Transition<LobbyRoom>();
                    break;
                case "cave":
                    Console.WriteLine("You decide to explore the dark alleyway and enter the cave.");
                    Game.Transition<Cave>();
                    break;
                default:
                    Console.WriteLine("Invalid command.");
                    break;
            }
        }
    }
}//kitchen

namespace NarrativeProject.Rooms
{
    internal class CaveRoom : Room
    {
        private bool doorLocked = true;
        private bool alleywayOpen = true;

        internal override string CreateDescription() =>
@"It's dark, and you can hear yourself breathe. Suddenly, you're locked in; the alleyway gets shut! A small candle is given to you for you to see.
To your left is a door, who knows what's on the other side [Door].
In front of you is another [Door2].
On your right is a man.";

        internal override void ReceiveChoice(string choice)
        {
            switch (choice.ToLower())
            {
                case "door":
                    if (doorLocked)
                    {
                        Console.WriteLine("Oh, the door is locked. There are 5 locks on it, all different.");
                        Console.WriteLine("You have two remaining options: [Door2] or [Man].");
                    }
                    break;
                case "door2":
                    Console.WriteLine("Oh, there is a sign: 'Go see the man or take this passage and lose -50% health and get sent to LobbyRoom.'");
                    break;
                case "man":
                    Console.WriteLine("You decide to approach the man.");
                    Game.Transition<ManRoom>();
                    break;
                default:
                    Console.WriteLine("Invalid command.");
                    break;
            }
        }
    }
}//Cave

namespace NarrativeProject.Rooms
{
    internal class ManRoom : Room
    {
        private bool riddleAnswered = false;

        internal override string CreateDescription() =>
@"You approach the man.
- What is my riddle, sir?
- 'I am heard but never spoken, In silence, I am awoken. I repeat what's said with a gentle flow, In valleys deep or mountains' snow. I travel to and from. What am I?'";

        internal override void ReceiveChoice(string choice)
        {
            if (!riddleAnswered)
            {
                if (choice.ToLower() == "echo")
                {
                    Console.WriteLine("You answered the riddle correctly. You obtain the Dark key.");
                    // Code to add Dark key to player's inventory can be added here if needed
                    Game.Transition<LobbyRoom>(); // Send player to the LobbyRoom
                }
                else
                {
                    Console.WriteLine("You answered the riddle incorrectly. You lose -15 health, and the man throws you out the door.");
                    Game.DecreasePlayerHealth(15); // Decrease player health by 15
                    Game.Transition<LobbyRoom>(); // Send player to the LobbyRoom
                }

                riddleAnswered = true; // Set riddle as answered
            }
            else
            {
                Console.WriteLine("You've already interacted with the man. You decide to explore further.");
            }
        }
    }
}//Man in the cave

namespace NarrativeProject.Rooms
{
    internal class HallRoomOne : Room
    {
        internal override string CreateDescription() =>
@"You entered a hall. This is the most important hall and can serve you the most benefits.
On your left, there is a door that leads you to a [Laboratory].
In front of you, there is a Mystery room [Mystery].
On your right, there is a small room [SmallRoom].
Finally, if you would like to go back [Return].
Where would you like to go?";

        internal override void ReceiveChoice(string choice)
        {
            switch (choice.ToLower())
            {
                case "laboratory":
                    Console.WriteLine("You decide to enter the Laboratory.");
                    Game.Transition<LabRoom>();
                    break;
                case "mystery":
                    Console.WriteLine("You decide to enter the Mystery room.");
                    Game.Transition<MysteryRoomOne>();
                    break;
                case "smallroom":
                    Console.WriteLine("You decide to enter the Small room.");
                    Game.Transition<PotionRoomOne>();
                    break;
                case "return":
                    Console.WriteLine("You decide to return.");
                    Game.Transition<LobbyRoom>();
                    break;
                default:
                    Console.WriteLine("Invalid command.");
                    break;
            }
        }
    }
}//Hall 1

namespace NarrativeProject.Rooms
{
    internal class LabRoom : Room
    {
        private bool riddleAnswered = false;

        internal override string CreateDescription() =>
@"You entered the Lab. It looks abandoned as if there was a failed experiment. The man is standing near a desk, you approach him to ask for your riddle.
- What is my riddle?
- 'I have billions of eyes, yet live in darkness. I have millions of ears, yet only four lobes. I have no muscles yet rule two hemispheres. What am I?'
If you don't want to answer, you can [Return].";

        internal override void ReceiveChoice(string choice)
        {
            if (!riddleAnswered)
            {
                switch (choice.ToLower())
                {
                    case "brain":
                        Console.WriteLine("Congratulations! You got the Science key. You gain +10 health points.");
                        Game.DecreasePlayerHealth(-10); // Increase player health by 10
                        Game.Transition<LobbyRoom>(); // Send player to the LobbyRoom
                        break;
                    case "return":
                        Console.WriteLine("You decide to return to the Hall.");
                        Game.Transition<HallRoomOne>(); // Send player back to HallRoomOne
                        break;
                    default:
                        Console.WriteLine("Sorry, that's wrong. You lose -15 health.");
                        Game.DecreasePlayerHealth(15); // Decrease player health by 15
                        Game.Transition<LobbyRoom>(); // Send player to the LobbyRoom
                        break;
                }

                riddleAnswered = true; // Set riddle as answered
            }
            else
            {
                Console.WriteLine("You've already interacted with the man. You decide to explore further.");
            }
        }
    }
}//Lab

namespace NarrativeProject.Rooms
{
    internal class MysteryRoomOne : Room
    {
        private bool gamePlayed = false;

        internal override string CreateDescription() =>
@"You reach the Mystery room. You look around and realize you've stepped into a tricky game. 
The original Glass Stepping Stone game. You see the Glass Key on the other side of the room. 
Try your luck and figure out the pattern or [Return] to the HallRoom. 
The pattern is a four-letter combination of L and R.";

        internal override void ReceiveChoice(string choice)
        {
            if (!gamePlayed)
            {
                switch (choice.ToLower())
                {
                    case "lrll":
                        Console.WriteLine("Congratulations! You successfully guessed the pattern.");
                        Console.WriteLine("You are awarded the Glass Key and gain +20 health points.");
                        Game.DecreasePlayerHealth(-20); // Increase player health by 20
                        Game.Transition<HallRoomOne>(); // Send player back to MainHallOne
                        break;
                    case "return":
                        Console.WriteLine("You decide to return to the HallRoom.");
                        Game.Transition<HallRoomOne>(); // Send player back to HallRoomOne
                        break;
                    default:
                        Console.WriteLine("Sorry, that's wrong. You lose -15 health.");
                        Game.DecreasePlayerHealth(15); // Decrease player health by 15
                        Game.Transition<HallRoomOne>(); // Send player back to HallRoomOne
                        break;
                }

                gamePlayed = true; // Set game as played
            }
            else
            {
                Console.WriteLine("You've already played the game.");
            }
        }
    }
}//MysteryRoom 1 ( glass steps) 

namespace NarrativeProject.Rooms
{
    internal class HallRoomTwo : Room
    {
        internal override string CreateDescription() =>
@"You are in the Hall next to the Kitchen. There are 3 doors.
The door on your right leads to the [Library].
The door in front of you is a [Mystery] room.
The door on your left is a [Small] room.
You can also return to the [Kitchen].";

        internal override void ReceiveChoice(string choice)
        {
            switch (choice.ToLower())
            {
                case "library":
                    Console.WriteLine("You decide to enter the Library.");
                    Game.Transition<Library>(); // Transition to the Library room
                    break;
                case "mystery":
                    Console.WriteLine("You decide to enter the Mystery room.");
                    Game.Transition<MysteryRoomTwo>(); // Transition to the MysteryRoomTwo
                    break;
                case "small":
                    Console.WriteLine("You decide to enter the Small room.");
                    Game.Transition<PotionRoomTwo>(); // Transition to the PotionRoomTwo
                    break;
                case "kitchen":
                    Console.WriteLine("You decide to return to the Kitchen.");
                    Game.Transition<Kitchen>(); // Transition to the Kitchen room
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please choose one of the available options.");
                    break;
            }
        }
    }
}//Hall 2

namespace NarrativeProject.Rooms
{
    internal class Library : Room
    {
        private bool riddleAsked = false;

        internal override string CreateDescription() =>
@"You are in the library now. You can choose to [Return]. 
Some books burnt around, scribbles on the walls and broken bookshelves. 
You see the man waiting for you sitting on a desk with a little lamp next to him reading a book.";

        internal override void ReceiveChoice(string choice)
        {
            if (!riddleAsked)
            {
                if (choice.ToLower() == "return")
                {
                    Console.WriteLine("You decide to return.");
                    Game.Transition<HallRoomTwo>(); // Transition to HallRoomTwo
                }
                else
                {
                    Console.WriteLine("You go up to the man and ask him for the riddle.");
                    Console.WriteLine("He replies: I have no voice, yet I speak to you: I tell of tales both new and old.");
                    Console.WriteLine("I paint with words, my canvas the mind; in pages of books, my essence you'll find. What am I?");
                    Console.Write("Enter your answer: ");
                    string answer = Console.ReadLine()?.Trim().ToLower();

                    if (answer == "poet")
                    {
                        Console.WriteLine("Congratulations, you got the answer!");
                        Console.WriteLine("You have been awarded the Book Key and +15 health points.");
                        Game.DecreasePlayerHealth(-15); // Increase player health by 15
                        Game.Transition<HallRoomTwo>(); // Transition to HallRoomTwo
                    }
                    else
                    {
                        Console.WriteLine("Sorry, that's incorrect. You lose -15 health points.");
                        Game.DecreasePlayerHealth(15); // Decrease player health by 15
                        Game.Transition<LobbyRoom>(); // Transition to LobbyRoom
                    }

                    riddleAsked = true; // Set riddle as asked
                }
            }
            else
            {
                Console.WriteLine("You've already asked the man for the riddle.");
            }
        }
    }
}//library