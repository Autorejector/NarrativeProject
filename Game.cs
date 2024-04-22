using System;
using System.Collections.Generic;

namespace NarrativeProject
{
    internal class Game
    {
        private int playerHealth = 100; // Player's health
        private List<Room> rooms = new List<Room>();
        private Room currentRoom;
        private bool isFinished;
        private string nextRoom = "";

        internal bool IsGameOver() => isFinished;

        internal void Add(Room room)
        {
            rooms.Add(room);
            if (currentRoom == null)
            {
                currentRoom = room;
            }
        }

        internal string CurrentRoomDescription => currentRoom.CreateDescription();

        internal void ReceiveChoice(string choice)
        {
            currentRoom.ReceiveChoice(choice);
            CheckTransition();
        }

        internal static void Transition<T>() where T : Room
        {
            nextRoom = typeof(T).Name;
        }

        internal static void Finish()
        {
            isFinished = true;
        }

        internal void CheckTransition()
        {
            foreach (var room in rooms)
            {
                if (room.GetType().Name == nextRoom)
                {
                    nextRoom = "";
                    currentRoom = room;
                    break;
                }
            }
        }

        
        internal void DecreasePlayerHealth(int amount)
        {
            playerHealth -= amount;
            Console.WriteLine($"Your health is now {playerHealth}.");
            if (playerHealth <= 0)
            {
                Console.WriteLine("You have run out of health. Game over.");
                Finish(); 
            }
        }
    }
}
