using NarrativeProject.Rooms;
using System;

namespace NarrativeProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var game = new Game();
            game.Add(new Bedroom());
            game.Add(new Bathroom());
            game.Add(new AtticRoom());
            game.Add(new LobbyRoom());
            game.Add(new KitchenRoom());
            game.Add(new CaveRoom());
            game.Add(new Library());
            game.Add(new LabRoom());
            game.Add(new GardenRoom());
            game.Add(new Door(escaperoom)());
            game.Add(new MysteryRoomOne());
            game.Add(new HallRoomOne());
            game.Add(new HallRoomTwo());
            game.Add(new MysteryRoomTwo());
            game.Add(new ManRoom());
            game.Add(new PotionRoomOne());
            game.Add(new PotionRoomTwo());



            while (!game.IsGameOver())
            {
                Console.WriteLine("--");

                Console.WriteLine(game.CurrentRoomDescription);
                string choice = Console.ReadLine().ToLower() ?? "";
                Console.Clear();
                game.ReceiveChoice(choice);
            }

            Console.WriteLine("END");
            Console.ReadLine();


        }


    }
}
