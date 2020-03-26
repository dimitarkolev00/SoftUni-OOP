using System.Collections.Generic;

namespace P04_Hospital
{
    public class Department
    {
        private readonly List<Room> rooms;

        private Department()
        {
            this.rooms = new List<Room>();
            this.InitializeRooms();
        }
        public Department(string name)
            :this()
        {
            this.Name = name;
        }
        public string Name { get;  }

        public IReadOnlyCollection<Room> Rooms => this.rooms;
        private void InitializeRooms()
        {
            for (byte i = 1; i <=20; i++)
            {
                this.rooms.Add(new Room(i));
            }
        }
    }
}
