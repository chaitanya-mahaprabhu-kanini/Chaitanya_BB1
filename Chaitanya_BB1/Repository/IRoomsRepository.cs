using Chaitanya_BB1.Models;
using System.Collections.Generic;

//Interface contains the necessary functionalities that mandatorily have to be implemented.
//Repository acts like a middle-man to provide more security and modularity.
public interface IRoomsRepository
{
	IEnumerable<Room> GetRooms();
	Room GetRoomById(int id);
	void AddRoom(Room room);
	void UpdateRoom(Room room);
	void DeleteRoom(int id);
}
